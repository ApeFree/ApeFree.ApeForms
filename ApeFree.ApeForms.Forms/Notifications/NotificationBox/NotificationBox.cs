using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApeFree.ApeForms.Core.Controls;
using ApeFree.ApeForms.Core.Utils;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public partial class NotificationBox : Form, INotification
    {
        /// <summary>
        /// 选项单击事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public delegate void OptionClickEventHandler(object sender, OptionClickEventArgs e);

        /// <summary>
        /// 自动消失时间间隔
        /// </summary>
        public int DisappearInterval { get; set; } = 5000;

        internal bool IsDisappear { get; set; } = false;

        /// <summary>
        /// 内容控件
        /// </summary>
        public Control ContentView { get; }

        /// <summary>
        /// 备用控件
        /// </summary>
        public Control SpareView { get; }

        /// <summary>
        /// 提醒色
        /// </summary>
        public Color ReminderColor
        {
            get => reminderColor; set
            {
                reminderColor = value;
                // 关闭按钮前景色
                btnClose.ForeColor = reminderColor;
                Refresh();
            }
        }

        private NotificationBox()
        {
            InitializeComponent();
            Size = DefaultFormsSize;
        }

        internal NotificationBox(Control contentView, Control spareView = null)
        {
            InitializeComponent();
            Size = DefaultFormsSize;

            ContentView = contentView;
            SpareView = spareView;

            if (ContentView != null)
            {
                ContentView.Dock = DockStyle.Fill;
                panelMain.Controls.Add(ContentView);
                ContentView.BringToFront();
            }

            if (SpareView != null)
            {
                SpareView.Dock = DockStyle.Fill;
                panelSpareControl.Controls.Add(SpareView);
            }

            panelSpareControl.Visible = SpareView != null;
        }

        /// <summary>
        /// 让窗体不显示在alt+Tab视图窗体中
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            labTitle.Text = Text;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = IsDisappear;

            if (!IsDisappear)
            {
                Disappear();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (var ctrl in this.GetChildControls(true))
            {
                ctrl.MouseLeave += NotificationBox_MouseLeave;
                ctrl.MouseEnter += NotificationBox_MouseEnter;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Pen pen = new Pen(ReminderColor, 5);
            e.Graphics.DrawLine(pen, 0, 0, 0, Height);

            Pen penBorder = new Pen(BackColor.Luminance(0.5f), 1);
            e.Graphics.DrawRectangle(penBorder, new Rectangle(0, 0, Width - 1, Height - 1));

            pen.Dispose();
            penBorder.Dispose();
            e.Dispose();
        }

        private void PanelSpareControl_SizeChanged(object sender, EventArgs e)
        {
            panelSpareControl.Width = panelSpareControl.Height;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        public new void Show()
        {
            try
            {
                this.ShowWindow(ShowWindowMode.ShowNoactivate);
                // this.DropShadow();
                timerDisappear.Interval = DisappearInterval;
                timerDisappear.Enabled = true;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// 激活窗体，重置消失倒计时
        /// </summary>
        public void Active()
        {
            timerDisappear.Enabled = false;
            timerDisappear.Enabled = true;
        }

        /// <summary>
        /// 使窗体消失
        /// </summary>
        public void Disappear()
        {
            IsDisappear = true;
            timerDisappear.Enabled = false;
            timerDisappear.Dispose();

            this.GraduallyClose(0.05, box =>
            {
                NotifyForms.Remove(box);
                box.Dispose();
            });
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="text"></param>
        /// <param name="onClick"></param>
        /// <returns></returns>
        public Control AddOption(string text, OptionClickEventHandler onClick)
        {
            var btn = new SimpleButton();
            btn.Text = text;
            btn.Size = new Size(65, 25);
            btn.BackColor = Color.LightGray;
            btn.ForeColor = Color.Black;
            btn.AutoSize = true;
            btn.TabStop = false;
            btn.Click += (s, e) =>
            {
                var args = new OptionClickEventArgs(this, btn);
                onClick?.Invoke(btn, args);

                if (args.IsDisappear)
                {
                    Disappear();
                }
            };

            flpOptions.Controls.Add(btn);

            return btn;
        }

        private void timerDisappear_Tick(object sender, EventArgs e)
        {
            Disappear();
        }

        // ==============================================================================================================================

        internal static readonly EventableList<NotificationBox> NotifyForms;
        private Color reminderColor = SystemColors.Highlight;

        /// <summary>
        /// 通知栏之间的间隔距离
        /// </summary>
        public static int SpacingDistance { get; set; } = 10;

        /// <summary>
        /// 通知栏默认大小
        /// </summary>
        public static Size DefaultFormsSize { get; set; } = new Size(350, 150);

        /// <summary>
        /// 通知排列方向
        /// </summary>
        public static NotifyOrientation Orientation { get; set; } = NotifyOrientation.Stack;

        /// <summary>
        /// 通知起始方向
        /// </summary>
        public static NotifyPrimeDirection PrimeDirection { get; set; } = NotifyPrimeDirection.Bottom;

        static NotificationBox()
        {
            NotifyForms = new EventableList<NotificationBox>();

            NotifyForms.ItemAdded += NotifyForms_ItemChanged;
            NotifyForms.ItemInserted += NotifyForms_ItemChanged;
            NotifyForms.ItemRemoved += NotifyForms_ItemChanged;
        }

        public static INotification Publish(string title, Control contentView, Control spareView = null, uint delay = 10000)
        {
            var form = new NotificationBox(contentView, spareView);
            form.Text = title;
            form.DisappearInterval = (int)delay;

            NotifyForms.Add(form);
            return form;
        }

        public static INotification Publish(string title, string content, uint delay = 10000)
        {
            var contentView = new Label { Text = content, AutoSize = false };
            return Publish(title, contentView, null, delay);
        }

        public static INotification Publish(string title, string content, Image image, uint delay = 10000)
        {
            var contentView = new Label { Text = content, AutoSize = false };
            var spareView = new PictureBox() { Image = image, SizeMode = PictureBoxSizeMode.Zoom };
            return Publish(title, contentView, spareView, delay);
        }


        private static void NotifyForms_ItemChanged(object sender, ListItemsChangedEventArgs<NotificationBox> e)
        {
            // 获取屏幕
            var screen = Screen.AllScreens.FirstOrDefault();
            if (screen == null)
            {
                throw new Exception("No screens are currently available");
            }

            for (int i = 0; i < NotifyForms.Count; ++i)
            {
                NotificationBox form = NotifyForms[i];
                var point = CalculateLastPosition(screen, i);
                if (form.Visible)
                {
                    form.LocationGradualChange(point);
                    // form.Location = point;
                }
                else
                {
                    form.GraduallyShow(0.03f, 0.9);
                    form.Location = point;
                }
            }
        }

        private static Point CalculateLastPosition(Screen screen, int index)
        {
            // 获取工作区宽高
            var h = screen.WorkingArea.Height;
            var w = screen.WorkingArea.Width;

            // 排列方向
            if (Orientation == NotifyOrientation.Stack)
            {
                index = NotifyForms.Count - index - 1;
            }

            var top = 0;
            if (PrimeDirection == NotifyPrimeDirection.Top)
            {
                top = (DefaultFormsSize.Height + SpacingDistance) * index + SpacingDistance;
            }
            else if (PrimeDirection == NotifyPrimeDirection.Bottom)
            {
                top = h - (DefaultFormsSize.Height + SpacingDistance) * index - DefaultFormsSize.Height;
            }

            var left = w - 5 - DefaultFormsSize.Width;

            return new Point(left, top);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Disappear();
        }

        private void NotificationBox_MouseEnter(object sender, EventArgs e)
        {
            MousePositionChanged();
        }

        private void NotificationBox_MouseLeave(object sender, EventArgs e)
        {
            MousePositionChanged();
    }

        private void MousePositionChanged()
        {
            var rect = new Rectangle(Location, Size);
            timerDisappear.Enabled = !rect.Contains(MousePosition);
        }
    }

    /// <summary>
    /// 通知排列方向
    /// </summary>
    public enum NotifyOrientation
    {
        Queue,
        Stack,
    }

    /// <summary>
    /// 通知起始方向
    /// </summary>
    public enum NotifyPrimeDirection
    {
        Top,
        Bottom,
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
