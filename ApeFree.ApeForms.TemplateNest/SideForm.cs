using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.TemplateNest
{
    public partial class SideForm : Form
    {
        public Control MainControl => Controls.Cast<Control>().FirstOrDefault();
        public SideProperties Properties { get; protected set; } = new SideProperties();

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

        private SideForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        public SideForm(Control mainControl = null, SideProperties properties = null)
        {
            CheckForIllegalCrossThreadCalls = false;

            Properties = properties ?? Properties;

            InitializeComponent();

            if (mainControl != null)
            {
                mainControl.Parent = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var area = Screen.GetWorkingArea(this);
            Size = new Size(0, area.Height);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (Controls.Count > 1)
            {
                Controls.Remove(e.Control);

                if (DesignMode)
                {
                    Toast.Show("SideForm中只能存在一个主控件");
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            var area = Screen.GetWorkingArea(this);

            if (MainControl == null)
            {
                return;
            }

            if (Properties.Orientation == SideOrientation.Left)
            {
                Location = new Point(area.Location.X, area.Location.Y);

                MainControl.Dock = DockStyle.Right;
            }
            else
            {
                Location = new Point(area.Size.Width - Width + area.Location.X, area.Location.Y);
                MainControl.Dock = DockStyle.Left;
                MainControl.Refresh();
            }
        }

        public void Show()
        {
            if (!Visible)
            {
                if (Properties.UseFadeOut)
                {
                    this.GraduallyShow(0.01, Properties.Opacity, null, false);
                }
                else
                {
                    Opacity = Properties.Opacity;
                    base.Show();
                }
                this.SetWindowToTopWithoutFocus();
                this.SizeGradualChange(new Size(MainControl.Width, Height), Properties.MovingRate);
            }
        }

        public void Hide()
        {
            if (Visible)
            {
                this.SizeGradualChange(new Size(2, Height), Properties.MovingRate, f => base.Hide());
            }
        }

        public class SideProperties
        {
            /// <summary>
            /// 移动速率<br/>
            /// 取值范围[1,10]
            /// </summary>
            public byte MovingRate { get; set; } = 5;

            /// <summary>
            /// 侧边栏宽度
            /// </summary>
            // public int SideWidth { get; set; } = 300;

            /// <summary>
            /// 侧边栏所处的方向
            /// </summary>
            public SideOrientation Orientation { get; set; } = SideOrientation.Left;

            /// <summary>
            /// 侧边栏显示的最大不透明度
            /// </summary>
            public float Opacity { get; set; } = 1;

            /// <summary>
            /// 是否启用平滑淡出
            /// </summary>
            public bool UseFadeOut { get; set; } = true;
        }

        public enum SideOrientation
        {
            Left,
            Right,
        }
    }
}
