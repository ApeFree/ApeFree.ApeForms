using ApeFree.CodePlus.Algorithm.DataStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public static class ControlSmoothMovementExtensions
    {
        private const int MINIMUM_SPEED = 5;

        public static void SizeGradualChange(this Control control, Size targetSize, byte rate = 5)
        {
            SmoothMovementTaskManager.Manager.AddTask(new SmoothMovementTaskManager.TimerTask(control, () =>
            {
                control.ModifyInUI(() =>
                {
                    control.Width = Gradual(control.Width, targetSize.Width, rate);
                    control.Height = Gradual(control.Height, targetSize.Height, rate);
                });
            }, () =>
            {
                return (control.Width == targetSize.Width) && (control.Height == targetSize.Height);
            }));
        }

        public static void LocationGradualChange(this Control control, Point targetPoint, byte rate = 5)
        {
            lock (control)
            {
                SmoothMovementTaskManager.Manager.AddTask(new SmoothMovementTaskManager.TimerTask(control,
                    () =>
                    {
                        control.ModifyInUI(() =>
                        {
                            var left = Gradual(control.Left, targetPoint.X, rate);
                            var top = Gradual(control.Top, targetPoint.Y, rate);
                            control.Location = new Point(left, top);
                        });
                    },
                    () =>
                    {
                        return (control.Left == targetPoint.X) && (control.Top == targetPoint.Y);
                    }));
            }
        }

        private static int Gradual(int currentValue, int targetValue, byte rate)
        {
            if (Math.Abs(targetValue - currentValue) <= MINIMUM_SPEED)
                return targetValue;
            var speed = (currentValue - targetValue) / rate;
            if (Math.Abs(speed) <= MINIMUM_SPEED)
            {
                speed = currentValue < targetValue ? -MINIMUM_SPEED : MINIMUM_SPEED;
            }
            return currentValue - speed;
        }
    }

    public class SmoothMovementTaskManager
    {
        private static readonly Lazy<SmoothMovementTaskManager> manager = new Lazy<SmoothMovementTaskManager>(() => new SmoothMovementTaskManager());
        public static SmoothMovementTaskManager Manager { get { return manager.Value; } }

        private readonly Timer timer;

        private readonly object _lockerTaskListItemsChange = new object();

        private readonly EventableList<TimerTask> tasks;

        private SmoothMovementTaskManager()
        {
            timer = new Timer() { Interval = 10 };
            timer.Tick += Timer_Tick;

            tasks = new EventableList<TimerTask>();
            tasks.ItemAdded += Tasks_ItemAdded;
            tasks.ItemRemoved += Tasks_ItemRemoved;
        }

        private void Tasks_ItemRemoved(object sender, ListItemsChangedEventArgs<TimerTask> e) => timer.Enabled = tasks.Any();
        private void Tasks_ItemAdded(object sender, ListItemsChangedEventArgs<TimerTask> e) => timer.Enabled = true;

        public void AddTask(TimerTask task)
        {
            lock (_lockerTaskListItemsChange)
            {
                var t = tasks.FirstOrDefault(i => i.Id == task.Id);
                if (t == null)
                {

                    tasks.Add(task);
                }
                else
                {
                    t.Run = task.Run;
                    t.IsFinish = task.IsFinish;
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (_lockerTaskListItemsChange)
            {
                List<TimerTask> deleteTasks = new List<TimerTask>();
                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks[i];
                    if (task.IsFinish())
                    {
                        deleteTasks.Add(task);
                    }
                    task.Run();
                }
                deleteTasks.ForEach(t => tasks.Remove(t));
            }
        }

        public class TimerTask
        {
            public object Id { get; }
            public Action Run { get; set; }
            public Func<bool> IsFinish { get; set; }
            public TimerTask(object id, Action run, Func<bool> isFinish)
            {
                Id = id;
                Run = run;
                IsFinish = isFinish;
            }
        }
    }
}
