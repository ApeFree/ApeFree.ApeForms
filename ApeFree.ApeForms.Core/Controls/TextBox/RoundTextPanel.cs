using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApeFree.ApeForms.Core.Controls
{
    /// <summary>
    /// 圆角输入框面板
    /// </summary>
    public partial class RoundTextPanel : ColorlessPanel
    {
        private int cornerRadius = 20;

        /// <summary>
        /// 圆角半径
        /// </summary>
        [Description("圆角半径")]
        public int CornerRadius { get => cornerRadius; set => cornerRadius = value > 0 ? value : cornerRadius; }

        /// <summary>
        /// 边框宽度
        /// </summary>
        [Description("边框宽度")]
        public ushort BorderWidth { get; set; } = 2;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色")]
        public Color BorderColor { get; set; } = SystemColors.Highlight;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色")]
        public string Hint { get; set; }

        public Color HintColor { get; set; } = Color.Gray;

        /// <inheritdoc/>
        [Browsable(true)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色")]
        public new Color BackColor { get => textBox.BackColor; set => textBox.BackColor = value; }
        public bool IsHintMode
        {
            get => isHintMode; set
            {
                if (isHintMode = value)
                {
                    textBox.Text = Hint;
                    textBox.ForeColor = HintColor;
                }
                else
                {
                    textBox.Text = Text;
                    textBox.ForeColor = ForeColor;
                }
            }
        }

        private bool isHintMode = false;

        public RoundTextPanel()
        {
            InitializeComponent();
            textBox.TextChanged += TextBox_TextChanged;
            textBox.SizeChanged += TextBox_SizeChanged;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;

            Load += RoundTextPanel_Load;
        }

        private void RoundTextPanel_Load(object sender, EventArgs e)
        {
            TextBox_LostFocus(sender, e);
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                IsHintMode = true;
                textBox.Text = Hint;
                textBox.ForeColor = HintColor;
            }
            else
            {
                IsHintMode = false;
            }
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            IsHintMode = false;
            textBox.Text = Text;
            textBox.ForeColor = ForeColor;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isHintMode)
            {
                Text = textBox.Text;
            }
        }

        private void TextBox_SizeChanged(object sender, EventArgs e)
        {
            MinimumSize = new Size((int)(CornerRadius * 1.5), textBox.Height + 6);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (!textBox.Focused && IsHintMode)
            {
                IsHintMode = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 创建一个 Graphics 对象
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 创建一个 Pen 对象
            using (Pen pen = new Pen(BorderColor, BorderWidth))
            {
                // 创建一个 Brush 对象
                using (Brush brush = new SolidBrush(textBox.BackColor))
                {
                    // 创建一个圆角矩形
                    Rectangle rect = new Rectangle(2, 2, Width - 4, Height - 4);
                    var maxCornerRadius = rect.Height / 2;
                    if (maxCornerRadius <= 0)
                    {
                        maxCornerRadius = 1;
                    }
                    GraphicsPath path = CreateRoundedRectangle(rect, CornerRadius > maxCornerRadius ? maxCornerRadius : CornerRadius);

                    // 绘制圆角矩形的边缘
                    g.DrawPath(pen, path);

                    // 填充圆角矩形的内部
                    g.FillPath(brush, path);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            TextBoxCentering();
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            TextBoxCentering();
            Invalidate();
        }

        public void TextBoxCentering()
        {
            textBox.Width = Width - CornerRadius - Padding.Left - Padding.Right;
            textBox.Height = Height - CornerRadius - Padding.Top - Padding.Bottom;
            textBox.Location = new Point((Width - textBox.Width) / 2, (Height - textBox.Height) / 2);
        }

        // 创建一个圆角矩形的 GraphicsPath 对象
        private GraphicsPath CreateRoundedRectangle(Rectangle rect, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);

            // 右上角
            path.AddArc(rect.Right - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);

            // 右下角
            path.AddArc(rect.Right - cornerRadius * 2, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);

            // 左下角
            path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);

            // 闭合路径
            path.CloseFigure();

            return path;
        }
    }
}
