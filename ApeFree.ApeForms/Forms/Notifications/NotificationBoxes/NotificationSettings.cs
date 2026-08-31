using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    /// <summary>
    /// 通知设置
    /// </summary>
    public class NotificationSettings
    {
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 提示色
        /// </summary>
        public Color ReminderColor { get; set; } = SystemColors.Highlight;

        /// <summary>
        /// 主视图
        /// </summary>
        internal Control MainView { get; set; }

        /// <summary>
        /// 副视图
        /// </summary>
        internal Control SpareView { get; set; }

        /// <summary>
        /// 通知栏显示驻留的时长，单位毫秒
        /// </summary>
        public uint RetentionTime { get; set; } = 10000;

        /// <summary>
        /// 选项列表
        /// </summary>
        public List<NotificationOption> Options { get; set; }

        /// <summary>
        /// 构造通知设置对象
        /// </summary>
        /// <param name="mainView"></param>
        /// <param name="spareView"></param>
        protected NotificationSettings(Control mainView, Control spareView = null)
        {
            Options = new List<NotificationOption>();

            MainView = mainView;
            SpareView = spareView;
        }
    }

    /// <summary>
    /// 文本通知设置
    /// </summary>
    public class TextNotificationSettings : NotificationSettings
    {
        private Label Label => MainView as Label;
        public string Message { get => Label.Text; set => Label.Text = value; }


        public TextNotificationSettings() : base(new Label(), null)
        {

        }

        public TextNotificationSettings(string title, string text) : this()
        {
            Title = title;
            Message = text;
        }
    }

    /// <summary>
    /// 图片文本通知设置
    /// </summary>
    public class ImageTextNotificationSettings : NotificationSettings
    {
        private Label Label => MainView as Label;
        private PictureBox PictureBox => SpareView as PictureBox;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get => Label.Text; set => Label.Text = value; }

        /// <summary>
        /// 图片
        /// </summary>
        public Image Image { get => PictureBox.Image; set => PictureBox.Image = value; }

        /// <summary>
        /// 图像定位方式
        /// </summary>
        public PictureBoxSizeMode ImageSizeMode { get => PictureBox.SizeMode; set => PictureBox.SizeMode = value; }

        public ImageTextNotificationSettings() : base(new Label(), new PictureBox())
        {
            ImageSizeMode = PictureBoxSizeMode.Zoom;
        }

        public ImageTextNotificationSettings(string title, string text, Image image) : this()
        {
            Title = title;
            Message = text;
            Image = image;
        }
    }
}
