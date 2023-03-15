using ApeFree.ApeForms.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public partial class ToastForm : Form
    {
        [Browsable(false)]
        public int Delay { get => timerWait.Interval; set { timerHide.Enabled = false; Opacity = 1; timerWait.Enabled = false; timerWait.Interval = value; timerWait.Enabled = true; } }

        internal ToastForm(string content, int delay = 2000)
        {
            InitializeComponent();

            Text = content;

            var g = this.CreateGraphics();
            var strSize = g.MeasureString(content, Font);
            Size = new Size((int)(strSize.Width + 20), (int)(strSize.Height + 20));
            g.Dispose();
            Reposition();

            timerWait.Interval = delay;
            timerWait.Enabled = true;
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void Reposition()
        {
            // 将当前进程中打开的窗体依次压栈
            Stack<Form> stack = new Stack<Form>();
            foreach (Form f in Application.OpenForms)
            {
                stack.Push(f);
            }

            int left, top;

            while (stack.Any())
            {
                // 判断是否有活动窗体
                var form = stack.Pop();

                // 如果是Dialog窗体则不会被当做是Toast的背景窗体
                if (form is DialogForm)
                {
                    continue;
                }

                // 如果是Toast窗体则不会被当做是Toast的背景窗体
                if (form is ToastForm)
                {
                    continue;
                }

                // 如果是Notificatio窗体则不会被当做是Toast的背景窗体
                if (form is NotificationBox)
                {
                    continue;
                }

                // Toast定位到父窗体的相对位置
                left = (form.Width - Width) / 2 + form.Left;
                top = form.Height / 4 * 3 + form.Top;
                Location = new Point(left, top);
                return;
            }

            // Toast定位到工作区的相对位置
            var rect = Screen.GetWorkingArea(this);
            left = (rect.Width - Width) / 2;
            top = rect.Height / 4 * 3;
            Location = new Point(left, top);
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
            this.Size = new Size((int)(strSize.Width + 20), (int)(strSize.Height + 20));
            var strLocation = new Point(10, 10);
            e.Graphics.DrawString(Text, Font, Brushes.White, strLocation);
            Reposition();
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
                timerHide.Stop();
                timerHide.Dispose();
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
