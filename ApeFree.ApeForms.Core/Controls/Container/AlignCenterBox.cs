using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    [ToolboxItem(true)]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class AlignCenterBox : UserControl
    {
        public Control MainControl
        {
            get
            {
                if (Controls.Count == 0) return null;
                return Controls[0];
            }
            set
            {
                if (value == null) return;
                if (Controls.Count != 0) Controls.Clear();
                Controls.Add(value);
            }
        }

        public AlignCenterBox()
        {
            InitializeComponent();
        }

        public AlignCenterBox(Control control)
        {
            InitializeComponent();
            MainControl = control;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Reposition();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(0);
            }
            Reposition();
        }

        private void Reposition()
        {
            var ctrl = MainControl;
            if (ctrl == null) return;

            SuspendLayout();
            ctrl.SuspendLayout();
            ctrl.Top = (Height - ctrl.Height) / 2;
            ctrl.Left = (Width - ctrl.Width) / 2;
            ctrl.ResumeLayout();
            ResumeLayout();
        }
    }
}
