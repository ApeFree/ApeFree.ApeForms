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
        private byte buttonGroupId;

        [Browsable(false)]
        public new SimpleButton MainControl { get => (SimpleButton)base.MainControl; set => base.MainControl = value; }

        [Browsable(false)]
        public override Control HiddenControl { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// 按钮分组编号
        /// </summary>
        public byte ButtonGroupId
        {
            get => buttonGroupId;
            set
            {
                buttonGroupId = value;
                var btns = HiddenControl.Controls.Cast<Control>().Where(c => c is TabButton).Select(b => (TabButton)b);
                foreach (var btn in btns)
                {
                    btn.GroupId = buttonGroupId;
                }
            }
        }

        public SimpleButtonShutter()
        {
            Controls.Add(new SimpleButton());
            Controls.Add(new Panel() { AutoSize = true, Height = 0 });

            MainControl.Click += MainControl_Click;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            MainControl.Text = Text;
        }

        private void MainControl_Click(object sender, EventArgs e)
        {
            OpenState = !OpenState;
        }

        public TabButton AddChildButton(string text, EventHandler clickHandler = null)
        {
            var btn = new TabButton();
            btn.Text = text;
            btn.GroupId = ButtonGroupId;
            if (clickHandler != null)
            {
                btn.Click += clickHandler;
            }
            btn.Dock = DockStyle.Top;
            HiddenControl.Controls.Add(btn);
            return btn;
        }

        public SimpleButtonShutter AddChildShutter(string text)
        {
            var shutter = new SimpleButtonShutter();
            shutter.Text = text;
            AddChildShutter(shutter);
            return shutter;
        }

        public void AddChildShutter(SimpleButtonShutter shutter)
        {
            shutter.ButtonGroupId = ButtonGroupId;
            shutter.Dock = DockStyle.Top;
            HiddenControl.Controls.Add(shutter);
        }
    }
}
