using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace ApeFree.ApeForms.Core.Controls
{
    [ToolboxItem(true)]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Shutter : UserControl
    {

        [Browsable(true)]
        [Description("主要控件")]
        public virtual Control MainControl { get; set; }

        [Browsable(true)]
        [Description("隐藏控件")]
        public virtual Control HiddenControl { get; set; }

        [Browsable(true)]
        [Description("展开状态")]
        public bool OpenState
        {
            get => openState;
            set
            {
                if (openState != value)
                {
                    OnOpenStateChanged(EventArgs.Empty);
                }
                openState = value;
                AdjustSize();
            }
        }

        /// <summary>
        /// 当展开状态发生变化时
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOpenStateChanged(EventArgs e)
        {

        }

        /// <summary>
        /// 当展开状态切换完成
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOpenStateSwitchComplete(EventArgs e)
        {
            if (openState)
            {
                if (Parent == null)
                {
                    return;
                }

                Parent.GetChildControls().ForEach(x =>
                {
                    if(x is Shutter s && x != this)
                    {
                        s.OpenState = false;
                    }
                });
            }
        }

        private bool openState;


        public Shutter()
        {
            InitializeComponent();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            SuspendLayout();

            base.OnControlAdded(e);

            if (MainControl == null)
            {
                MainControl = e.Control;
                Height = MainControl.Height;
                MainControl.SizeChanged += InnerControl_SizeChanged;
            }
            else if (HiddenControl == null)
            {
                HiddenControl = e.Control;
                HiddenControl.SizeChanged += InnerControl_SizeChanged;
            }
            else
            {
                throw new InvalidOperationException("The space available for the control is full.");
            }

            ResumeLayout();
            AdjustSize();
        }

        private void InnerControl_SizeChanged(object sender, EventArgs e)
        {
            // 当内部控件的尺寸发生改变时，Shutter的尺寸也要跟着改变才能完整显示所有内容
            AdjustSize();
        }

        private void AdjustSize()
        {
            if (MainControl != null)
            {
                MainControl.Dock = DockStyle.Top;
                MainControl?.SendToBack();
            }

            if (HiddenControl != null)
            {
                HiddenControl.Dock = DockStyle.Top;
            }

            if (MainControl == null || HiddenControl == null)
            {
                return;
            }

            if (MainControl == HiddenControl)
            {
                return;
            }

            if (OpenState)
            {
                this.SizeGradualChange(new Size(Width, MainControl.Height + HiddenControl.Height), 5, x => OnOpenStateSwitchComplete(EventArgs.Empty));
            }
            else
            {
                this.SizeGradualChange(new Size(Width, MainControl.Height), 5, x => OnOpenStateSwitchComplete(EventArgs.Empty));
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (MainControl == null && HiddenControl == null)
            {
                return;
            }
            else if (MainControl != null && HiddenControl == null)
            {
                MainControl.Size = Size;
                return;
            }
            else if (MainControl == null && HiddenControl != null)
            {
                HiddenControl.Size = Size;
                return;
            }
            else
            {
            }

            AdjustSize();
        }
    }
}
