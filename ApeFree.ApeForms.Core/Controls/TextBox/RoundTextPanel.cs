using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    /// <summary>
    /// 圆角输入框面板
    /// </summary>
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    public partial class RoundTextPanel : ColorlessPanel
    {
        private System.Windows.Forms.TextBox textBox;
        private int cornerRadius = 20;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler TextChanged;

        /// <summary>
        /// 圆角半径
        /// </summary>
        [Description("圆角半径")]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                if (value > 0)
                {
                    cornerRadius = value;
                }
            }
        }

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
        /// 提示文本
        /// </summary>
        [Description("提示文本")]
        public string Hint { get; set; }

        /// <summary>
        /// 提示文本的颜色
        /// </summary>
        [Description("提示文本的颜色")]
        public Color HintColor { get; set; } = Color.Gray;

        /// <inheritdoc/>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                IsHintMode = string.IsNullOrEmpty(value) && !textBox.Focused;
            }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色")]
        public new Color BackColor { get => textBox.BackColor; set => textBox.BackColor = value; }

        /// <summary>
        /// 只读性
        /// </summary>
        [Description("获取或设置一个值，该值指示文本框中的文本是否为只读。")]
        public bool ReadOnly { get => textBox.ReadOnly; set => textBox.ReadOnly = value; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        [Description("获取或设置 TextBox 控件中文本的对齐方式。")]
        public HorizontalAlignment TextAlign { get => textBox.TextAlign; set => textBox.TextAlign = value; }


        private bool IsHintMode
        {
            get => isHintMode;
            set
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
            textBox = new System.Windows.Forms.TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Parent = this;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.SizeChanged += TextBox_SizeChanged;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;

            TextBoxCentering();

            Load += RoundTextPanel_Load;

            (this as Control).TextChanged += RoundTextPanel_TextChanged;
        }

        private void RoundTextPanel_TextChanged(object sender, EventArgs e)
        {
            this.TextChanged?.Invoke(sender, e);
        }

        private void RoundTextPanel_Load(object sender, EventArgs e)
        {
            TextBox_LostFocus(sender, e);
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            IsHintMode = string.IsNullOrEmpty(Text);
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            IsHintMode = false;
            textBox.Text = Text;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isHintMode && Text != textBox.Text)
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
            if (!textBox.Focused && IsHintMode)
            {
                IsHintMode = false;
            }
            else if (textBox.Focused)
            {
                IsHintMode = false;
            }
            base.OnTextChanged(e);
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
            // TextBoxCentering();
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            TextBoxCentering();
            Invalidate();
        }

        private void TextBoxCentering()
        {
            var minSide = Math.Min(Width, Height);
            var maxCornerRadius = minSide / 2;
            var cornerRadius = CornerRadius > maxCornerRadius ? maxCornerRadius : CornerRadius;


            var height = Height - cornerRadius - BorderWidth * 2 - Padding.Top - Padding.Bottom - 2;
            var width = Width - cornerRadius - BorderWidth * 2 - Padding.Left - Padding.Right - 2;
            textBox.Size = new Size(width, height);
            textBox.PerformLayout();
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
