using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public class Toast
    {
        private static ConcurrentQueue<ToastMsg> QueueMsg { get; } = new ConcurrentQueue<ToastMsg>();
        private static bool IsBusy { get; set; } = false;
        private static Control ContextControl { get; set; }
        private static ToastForm CurrentToastForm { get; set; }
        public static void Clear()
        {
            try
            {
                while (QueueMsg.Count > 0)
                    QueueMsg.TryDequeue(out ToastMsg msg);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 显示消息提示
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="delay">消息提示时长</param>
        /// <param name="context">当前处于焦点的控件</param>
        public static void Show(string content, int delay = 2000, Control context = null, ToastMode mode = ToastMode.Queue)
        {
            ContextControl = context = context ?? Form.ActiveForm;

            // 抢占模式或复用模式时，会清空队列中的消息
            if (mode == ToastMode.Preemption || mode == ToastMode.Reuse)
            {
                Clear();
            }

            // 如果是复用模式
            if (mode == ToastMode.Reuse && CurrentToastForm != null)
            {
                try
                {
                    // 重置延时和文本信息
                    CurrentToastForm.Delay = delay;
                    CurrentToastForm.Text = content;

                    // 复用模式直接退出
                    return;
                }
                catch (Exception) { }
            }
            else
            {
                // 将新消息加入队列
                QueueMsg.Enqueue(new ToastMsg(content, delay));
            }

            if (!IsBusy)
            {
                IsBusy = true;
                if (QueueMsg.Count > 0)
                {
                    if (context != null)
                    {
                        context.FindForm().Invoke(new EventHandler(delegate { ShowNextMsg(); }));
                    }
                    else
                    {
                        ShowNextMsg();
                    }
                }
                else
                {
                    IsBusy = false;
                }
            }
        }

        private static void ShowNextMsg()
        {
            if (QueueMsg.TryDequeue(out ToastMsg tm))
            {
                ToastForm tf = new ToastForm(tm.Content, tm.Delay);
                tf.TopMost = true;
                CurrentToastForm = tf;
                tf.Show();
                tf.FormClosed += (s, e) =>
                {
                    CurrentToastForm.Dispose();
                    CurrentToastForm = null;
                    ShowNextMsg();
                };
            }
            else
            {
                // 解除忙碌状态
                IsBusy = false;
            }

            // Toast窗体会获得焦点，所以要弹出后瞬间交还焦点
            try
            {
                ContextControl?.Focus();
            }
            catch (Exception) { }
        }

        class ToastMsg
        {
            public string Content { get; set; }
            public int Delay { get; set; }
            public ToastMsg(string content, int delay)
            {
                Content = content;
                Delay = delay;
            }
        }
    }
}

namespace System.Windows.Forms
{
    /// <summary>
    /// Toast显示模式
    /// </summary>
    public enum ToastMode
    {
        /// <summary>
        /// 队列模式：消息加入队列，顺序显示
        /// </summary>
        Queue,
        /// <summary>
        /// 抢占模式：清除队列，下一次弹出时显示
        /// </summary>
        Preemption,
        /// <summary>
        /// 复用模式：清除队列，使用当前正在显示的Toast
        /// </summary>
        Reuse,

        // 优先模式
    }
}
