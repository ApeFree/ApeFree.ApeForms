using ApeFree.ApeForms.Core.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class ControlSmoothMovementExtensions
    {
        private const int MINIMUM_SPEED = 5;

        public static void SizeGradualChange<T>(this T control, Size targetSize, byte rate = 5, Action<T> finishCallback = null) where T : Control
        {
            SmoothMovementTaskManager.Manager.AddTask(new TimedTaskItem(control, TimedTaskTag.SizeGradualChange, () =>
            {
                control.ModifyInUI(() =>
                {
                    control.Width = Gradual(control.Width, targetSize.Width, rate);
                    control.Height = Gradual(control.Height, targetSize.Height, rate);
                });
            }, () =>
            {
                return (control.Width == targetSize.Width) && (control.Height == targetSize.Height);
            }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
        }

        public static void LocationGradualChange<T>(this T control, Point targetPoint, byte rate = 5, Action<T> finishCallback = null) where T : Control
        {
            lock (control)
            {
                SmoothMovementTaskManager.Manager.AddTask(new TimedTaskItem(control, TimedTaskTag.LocationGradualChange,
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
                    }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
            }
        }

        public static void OpacityGradualChange<T>(this T form, double targetOpacity, byte rate = 50, Action<T> finishCallback = null) where T : Form
        {
            lock (form)
            {
                SmoothMovementTaskManager.Manager.AddTask(new TimedTaskItem(form, TimedTaskTag.OpacityGradualChange,
                    () =>
                    {
                        form.ModifyInUI(() =>
                        {
                            var no = Gradual((int)(form.Opacity * 100), (int)(targetOpacity * 100), rate);
                            form.Opacity = no / 100.0;
                        });
                    },
                    () =>
                    {
                        return form.Opacity == targetOpacity;
                    }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
            }
        }

        private static int Gradual(int currentValue, int targetValue, byte rate)
        {
            if (Math.Abs(targetValue - currentValue) <= MINIMUM_SPEED)
            {
                return targetValue;
            }

            var speed = (currentValue - targetValue) / rate;

            if (Math.Abs(speed) <= MINIMUM_SPEED)
            {
                speed = currentValue < targetValue ? -MINIMUM_SPEED : MINIMUM_SPEED;
            }

            return currentValue - speed;
        }
    }

    internal class SmoothMovementTaskManager
    {
        private static readonly Lazy<SmoothMovementTaskManager> manager = new Lazy<SmoothMovementTaskManager>(() => new SmoothMovementTaskManager());
        public static SmoothMovementTaskManager Manager { get { return manager.Value; } }

        private readonly Timer timer;

        private readonly object _lockerTaskListItemsChange = new object();

        private readonly EventableList<TimedTaskItem> TaskItems;

        private SmoothMovementTaskManager()
        {
            timer = new Timer() { Interval = 10 };
            timer.Tick += Timer_Tick;

            TaskItems = new EventableList<TimedTaskItem>();
            TaskItems.ItemAdded += (s, e) =>
            {
                timer.Enabled = TaskItems.Any();
                LOG();
            };
            TaskItems.ItemRemoved += (s, e) =>
            {
                timer.Enabled = true;
                LOG();
            };
        }

        private void LOG()
        {
            Debug.WriteLine(TaskItems.Select(i=>((Control)i.Context).Text).Join(", "));
        }

        public void AddTask(TimedTaskItem item)
        {
            lock (_lockerTaskListItemsChange)
            {
                var t = TaskItems.FirstOrDefault(i => i.Context == item.Context && i.Tag == item.Tag);
                if (t == null)
                {
                    TaskItems.Add(item);
                }
                else
                {
                    t.StepAction = item.StepAction;
                    t.CheckFinishCallback = item.CheckFinishCallback;
                    t.FinishCallback = item.FinishCallback;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (_lockerTaskListItemsChange)
            {
                for (int i = 0; i < TaskItems.Count; i++)
                {
                    var item = TaskItems[i];
                    lock (item)
                    {
                        if (item.CheckFinishCallback())
                        {
                            TaskItems.RemoveAt(i--);
                            if (item.FinishCallback != null)
                            {
                                item.InvokeFinishCallback();
                                // Task.Run(item.InvokeFinishCallback);
                            }
                        }
                        else
                        {
                            item.StepAction();
                            // Task.Run(item.StepAction);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 定时任务项
    /// </summary>
    internal class TimedTaskItem
    {
        public object Context { get; }
        public TimedTaskTag Tag { get; }
        public Action StepAction { get; set; }
        public Func<bool> CheckFinishCallback { get; set; }
        public Action<object> FinishCallback { get; set; }

        public TimedTaskItem(object context, TimedTaskTag tag, Action stepAction, Func<bool> isFinish, Action<object> finishCallback = null)
        {
            Context = context;
            Tag = tag;
            StepAction = stepAction;
            CheckFinishCallback = isFinish;
            FinishCallback = finishCallback;
        }

        public void InvokeFinishCallback() => FinishCallback?.Invoke(Context);
    }

    internal enum TimedTaskTag
    {
        SizeGradualChange,
        LocationGradualChange,
        OpacityGradualChange,
    }
}
