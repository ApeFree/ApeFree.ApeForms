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
    public partial class NotificationBox : Form, INotificationBox
    {
        /// <summary>
        /// 自动消失时间间隔
        /// </summary>
        public int DisappearInterval { get; set; } = 5000;

        internal bool IsDisappear { get; set; } = false;

        /// <summary>
        /// 内容控件
        /// </summary>
        public Control MainView { get; }

        /// <summary>
        /// 备用控件
        /// </summary>
        public Control SpareView { get; }

        /// <summary>
        /// 标题控件
        /// </summary>
        public Control TitleView { get; }

        /// <summary>
        /// 提醒色
        /// </summary>
        public Color ReminderColor
        {
            get => reminderColor;
            set
            {
                reminderColor = value;
                // 关闭按钮前景色
                btnClose.ForeColor = reminderColor;
                Refresh();
            }
        }
        private Color reminderColor;

        private NotificationBox()
        {
            InitializeComponent();
            Size = Notification.DefaultFormsSize;
        }

        internal NotificationBox(Control contentView, Control spareView = null) : this()
        {
            TitleView = labTitle;
            MainView = contentView;
            SpareView = spareView;

            if (MainView != null)
            {
                MainView.Dock = DockStyle.Fill;
                panelMain.Controls.Add(MainView);
                MainView.BringToFront();
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

            this.SetWindowToTopWithoutFocus();

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

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            btnClose.BackColor = this.BackColor;
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

                var args = new EventArgs();
                OnLoad(args);
                OnShown(args);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// 使窗体消失
        /// </summary>
        public void Disappear()
        {
            IsDisappear = true;
            timerDisappear.Enabled = false;
            timerDisappear.Dispose();

            // this.LocationGradualChange(new Point(Location.X + Width * 2, Location.Y), 5);
            this.GraduallyClose(0.05, box =>
            {
                NotifyForms.Remove(box);

                MainView?.Dispose();
                SpareView?.Dispose();
                foreach (Control control in flpOptions.Controls)
                {
                    control.Dispose();
                }

                box.Dispose();
            });
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="option">选项设置</param>
        /// <returns></returns>
        public Control AddOption(NotificationOption option)
        {
            var btn = new SimpleButton();
            btn.Text = option.Text;
            btn.Size = new Size(50, 25);
            btn.BackColor = option.BackColor;
            btn.ForeColor = option.ForeColor;
            btn.AutoSize = true;
            btn.TabStop = false;
            btn.Click += (s, e) =>
            {
                var args = new OptionClickEventArgs(this, btn);
                option.OptionClickHandler?.Invoke(btn, args);

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

        static NotificationBox()
        {
            NotifyForms = new EventableList<NotificationBox>();

            NotifyForms.ItemAdded += NotifyForms_ItemChanged;
            NotifyForms.ItemInserted += NotifyForms_ItemChanged;
            NotifyForms.ItemRemoved += NotifyForms_ItemChanged;
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
                    form.GraduallyShow(0.03f, Notification.UnhoveringOpacity, box =>
                    {
                        box.timerDisappear.Interval = box.DisappearInterval;
                        box.timerDisappear.Enabled = true;
                    });
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
            if (Notification.Orientation == NotifyOrientation.Stack)
            {
                index = NotifyForms.Count - index - 1;
            }

            var top = 0;
            if (Notification.PrimeDirection == NotifyPrimeDirection.Top)
            {
                top = (Notification.DefaultFormsSize.Height + Notification.SpacingDistance) * index + Notification.SpacingDistance;
            }
            else if (Notification.PrimeDirection == NotifyPrimeDirection.Bottom)
            {
                top = h - (Notification.DefaultFormsSize.Height + Notification.SpacingDistance) * index - Notification.DefaultFormsSize.Height;
            }

            var left = w - 5 - Notification.DefaultFormsSize.Width;

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
            var isInside = rect.Contains(MousePosition);

            timerDisappear.Enabled = !isInside;

            if (!IsDisappear)
            {
                // 使用透明度渐变效果
                // this.OpacityGradualChange(isInside ? 1 : Notification.UnhoveringOpacity);

                // 直接调节透明度
                this.Opacity = isInside ? 1 : Notification.UnhoveringOpacity;
            }
        }
    }

    /// <summary>
    /// 通知排列方向
    /// </summary>
    public enum NotifyOrientation
    {
        /// <summary>
        /// 队列模式
        /// </summary>
        Queue,

        /// <summary>
        /// 栈模式
        /// </summary>
        Stack,
    }

    /// <summary>
    /// 通知起始方向
    /// </summary>
    public enum NotifyPrimeDirection
    {
        /// <summary>
        /// 自顶部开始
        /// </summary>
        Top,

        /// <summary>
        /// 自底部开始
        /// </summary>
        Bottom,
    }
}
