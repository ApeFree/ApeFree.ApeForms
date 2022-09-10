using ApeFree.ApeForms.Forms.Dialogs;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notification
{
    public partial class ToastForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

        [Browsable(true)]
        public new string Text { get => labContent.Text; set { labContent.Text = value; Delay = Delay; } }

        [Browsable(false)]
        public int Delay { get => timerWait.Interval; set { timerHide.Enabled = false; Opacity = 1; timerWait.Enabled = false; timerWait.Interval = value; timerWait.Enabled = true; } }

        public ToastForm(string content, int delay = 2000)
        {
            InitializeComponent();

            Text = content;
            var area = Screen.PrimaryScreen.WorkingArea;
            var graphics = Graphics.FromImage(new Bitmap(area.Width, area.Height));
            var size = graphics.MeasureString(content, Font);

            Width = (int)size.Width + (Padding.Left + Padding.Right) + 30;
            Height = (int)size.Height;

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

        public new void Show()
        {
            ShowWindow(new HandleRef(this, this.Handle), 4);
        }

        /*
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }*/

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

        private void ToastForm_Load(object sender, EventArgs e)
        {
        }

        private void TimerWait_Tick(object sender, EventArgs e)
        {
            timerWait.Enabled = false;
            timerHide.Enabled = true;
        }

        private void LabContent_MouseMove(object sender, MouseEventArgs e)
        {
            timerWait.Enabled = false;
        }

        private void LabContent_MouseLeave(object sender, EventArgs e)
        {
            timerWait.Enabled = true;
        }
    }
}
