using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    [DefaultEvent("Click")]
    // [DefaultProperty("Text")]
    public partial class SimpleButton : UserControl
    {
        [Browsable(false)]
        // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get => Title; set => Title = value; }

        [Browsable(true)]
        public string Title
        {
            get
            {
                return labButtonText == null ? base.Text : labButtonText.Text;
            }
            set
            {
                base.Text = value;
                if (labButtonText != null)
                {
                    labButtonText.Text = value;
                }
            }
        }

        /// <summary>
        /// 按钮的尺寸跟随文本的尺寸自适应
        /// </summary>
        [Browsable(true)]
        [Description("按钮的尺寸跟随文本的尺寸自适应")]
        public override bool AutoSize { get; set; }

        public SimpleButton()
        {
            InitializeComponent();
            Text = Name;
            AutoSize = true;
            labButtonText.TextChanged += LabButtonText_TextChanged;
        }

        private void LabButtonText_TextChanged(object sender, EventArgs e)
        {
            if (AutoSize)
            {
                SizeF sizeF = labButtonText.GetTextSize();
                labButtonText.Height = (int)sizeF.Height + 6;
                labButtonText.Width = (int)sizeF.Width + 6;
            }
        }

        public void PerformClick()
        {
            OnClick(new EventArgs());
        }

        private void labButtonText_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (Enabled)
            {
                labButtonText.ForeColor = ForeColor;
                labButtonText.BackColor = BackColor;
            }
            else
            {
                labButtonText.ForeColor = Color.White;
                labButtonText.BackColor = Color.DarkGray;
            }
        }

        private void labButtonText_MouseDown(object sender, MouseEventArgs e)
        {
            labButtonText.ForeColor = ControlPaint.Dark(ForeColor, 0.02f);
            labButtonText.BackColor = ControlPaint.Dark(BackColor, 0.02f);
        }

        private void labButtonText_MouseUp(object sender, MouseEventArgs e)
        {
            labButtonText.ForeColor = ControlPaint.Light(ForeColor);
            labButtonText.BackColor = ControlPaint.Light(BackColor);
        }

        private void labButtonText_MouseMove(object sender, MouseEventArgs e)
        {
            labButtonText.ForeColor = ControlPaint.Light(ForeColor);
            labButtonText.BackColor = ControlPaint.Light(BackColor);
        }

        private void labButtonText_MouseLeave(object sender, EventArgs e)
        {
            labButtonText.ForeColor = ForeColor;
            labButtonText.BackColor = BackColor;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            labButtonText.ForeColor = ForeColor;
            labButtonText.BackColor = BackColor;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            labButtonText.Font = Font;
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            labButtonText.BackColor = BackColor;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            labButtonText.ForeColor = ForeColor;
        }

        /// <summary>
        /// 创建简单的按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="action">单击动作</param>
        /// <returns></returns>
        public static SimpleButton CreateSimpleButton(string text, Action action)
        {
            SimpleButton btn = new SimpleButton();
            btn.Text = text;
            btn.Click += (s, e) => action.Invoke();
            return btn;
        }
    }
}
