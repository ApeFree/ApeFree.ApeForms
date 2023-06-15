using System;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Utils
{
    // 共享定时任务管理器
    internal class SharedTimedTaskManager
    {
        private static readonly Lazy<SharedTimedTaskManager> manager = new Lazy<SharedTimedTaskManager>(() => new SharedTimedTaskManager());
        public static SharedTimedTaskManager Manager { get { return manager.Value; } }

        private readonly Timer timer;

        private readonly object _lockerTaskListItemsChange = new object();

        private readonly EventableList<TimedTaskItem> TaskItems;

        private SharedTimedTaskManager()
        {
            timer = new Timer() { Interval = 10 };
            timer.Tick += Timer_Tick;

            TaskItems = new EventableList<TimedTaskItem>();
            TaskItems.ItemAdded += (s, e) =>
            {
                timer.Enabled = TaskItems.Any();
            };
            TaskItems.ItemRemoved += (s, e) =>
            {
                timer.Enabled = true;
            };
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

        /// <summary>
        /// 定时任务标签
        /// </summary>
        internal enum TimedTaskTag
        {
            SizeGradualChange,
            LocationGradualChange,
            OpacityGradualChange,
            VerticalScrollGradualChange,
        }
    }


}
