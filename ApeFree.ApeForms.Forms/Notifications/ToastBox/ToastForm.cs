using ApeFree.ApeForms.Forms.Dialogs;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public partial class ToastForm : Form
    {
        [Browsable(false)]
        public int Delay { get => timerWait.Interval; set { timerHide.Enabled = false; Opacity = 1; timerWait.Enabled = false; timerWait.Interval = value; timerWait.Enabled = true; } }

        public ToastForm(string content, int delay = 2000)
        {
            InitializeComponent();

            Text = content;

            // TODO: 可以通过自定义的attribute、全局属性、最小限制来确定Toast的显示位置（相对屏幕/相对窗体）

            // 判断是否有活动窗体
            var parent = ActiveForm;
            // 如果活动窗体是DialogForm则不会被当做是Toast的背景窗体
            if (parent is DialogForm)
            {
                parent = null;
            }
            if (parent != null)
            {
                Left = (parent.Width - Width) / 2 + parent.Left;
                Top = parent.Height / 4 * 3 + parent.Top;
            }
            else
            {
                var rect = Screen.GetWorkingArea(this);
                Left = (rect.Width - Width) / 2;
                Top = rect.Height / 4 * 3;
            }

            timerWait.Interval = delay;
            timerWait.Enabled = true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);
            var strSize = e.Graphics.MeasureString(Text, Font);
            this.Width = (int)(strSize.Width + 20);
            this.Height = (int)(strSize.Height + 20);
            var strLocation = new Point(10, 10);
            e.Graphics.DrawString(Text, Font, Brushes.White, strLocation);
        }

        public new void Show()
        {
            this.ShowWindow(ShowWindowMode.ShowNoactivate);
        }

        private void TimerHide_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0.1)
            {
                Opacity -= 0.1;
            }
            else
            {
                Close();
            }
        }

        private void TimerWait_Tick(object sender, EventArgs e)
        {
            timerWait.Enabled = false;
            timerHide.Enabled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            timerWait.Enabled = false;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            timerWait.Enabled = true;
        }
    }
}
