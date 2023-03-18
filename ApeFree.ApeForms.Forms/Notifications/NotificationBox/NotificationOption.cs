using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    /// <summary>
    /// Notification选项信息
    /// </summary>
    public class NotificationOption
    {
        /// <summary>
        /// 选项文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 选项背景色
        /// </summary>
        public Color BackColor { get; set; } = Color.LightGray;

        /// <summary>
        /// 选项前景色
        /// </summary>
        public Color ForeColor { get; set; } = Color.Black;

        /// <summary>
        /// 选项被选中时的回调过程 
        /// </summary>
        public EventHandler<OptionClickEventArgs> OptionClickHandler { get; set; }

        public NotificationOption(string text, EventHandler<OptionClickEventArgs> optionClickHandler = null)
        {
            Text = text;
            OptionClickHandler = optionClickHandler;
        }
    }

    /// <summary>
    /// 选项单击事件
    /// </summary>
    public class OptionClickEventArgs : EventArgs
    {
        public OptionClickEventArgs(NotificationBox notificationBox, Control optionControl)
        {
            NotificationBox = notificationBox;
            OptionControl = optionControl;
            IsDisappear = true;
        }

        public NotificationBox NotificationBox { get; }
        public Control OptionControl { get; }
        public bool IsDisappear { get; set; }
    }
}
