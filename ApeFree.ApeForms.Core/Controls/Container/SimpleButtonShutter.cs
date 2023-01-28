using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    [ToolboxItem(true)]
    public class SimpleButtonShutter : Shutter
    {
        [Browsable(false)]
        public new SimpleButton MainControl { get => (SimpleButton)base.MainControl; set => base.MainControl = value; }

        [Browsable(false)]
        public override Control HiddenControl { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get => base.Text; set => base.Text = value; }

        public SimpleButtonShutter()
        {
            Controls.Add(new SimpleButton());
            Controls.Add(new Panel() { AutoSize = true,Height = 0 });

            MainControl.Click += MainControl_Click;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            MainControl.Text= Text;
        }

        private void MainControl_Click(object sender, EventArgs e)
        {
            OpenState = !OpenState;
        }

        public SimpleButton AddChildButton(string text,EventHandler clickHandler = null)
        {
            var btn = new SimpleButton();
            btn.Text = text;
            if(clickHandler!= null)
            {
                btn.Click += clickHandler;
            }
            btn.Dock = DockStyle.Top;
            HiddenControl.Controls.Add(btn);
            return btn;
        }
    }
}
