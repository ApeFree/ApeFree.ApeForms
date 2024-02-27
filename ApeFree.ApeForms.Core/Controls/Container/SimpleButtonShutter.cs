using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            MainControl.Paint += MainControl_Paint;
        }

        protected override void OnOpenStateSwitchComplete(EventArgs e)
        {
            base.OnOpenStateSwitchComplete(e);
            MainControl.Refresh();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            MainControl.Text = Text;
        }

        private void MainControl_Paint(object sender, PaintEventArgs e)
        {
            // 创建Graphics对象
            Graphics g = e.Graphics;

            // 创建Brush对象
            Brush brush = new SolidBrush(MainControl.ForeColor);

            var text = OpenState ? "▼" : "▶";

            // 获取按钮控件的尺寸
            SizeF textSize = g.MeasureString(text, MainControl.Font);
            float x = (MainControl.Width - textSize.Width) * 0.95f;
            float y = (MainControl.Height - textSize.Height) / 2;

            // 使用StringFormat对象设置对齐方式为居中
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            // 绘制文本
            g.DrawString(text, MainControl.Font, brush, new RectangleF(x, y, textSize.Width, textSize.Height), sf);

            brush.Dispose();
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
