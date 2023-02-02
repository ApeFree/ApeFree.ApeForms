using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    /// <summary>
    /// 图形按钮
    /// </summary>
    [ToolboxItem(true)]
    public class ShapeButton : Button
    {
        private float graphicScale = 0.5f;
        private SimpleShape shape;

        [Browsable(false)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// 图形比例
        /// </summary>
        public float GraphicScale { get => graphicScale; set { graphicScale = value; Refresh(); } }

        /// <summary>
        /// 图形样式
        /// </summary>
        public SimpleShape Shape { get => shape; set { shape = value; Refresh(); } }

        public ShapeButton()
        {
            Text = string.Empty;

            BackColor = SystemColors.Highlight;
            ForeColor = Color.White;

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            FlatAppearance.CheckedBackColor = BackColor.Luminance(1.2f);
            FlatAppearance.MouseDownBackColor = BackColor.Luminance(0.8f);
            FlatAppearance.MouseOverBackColor = BackColor.Luminance(1.2f);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
        }


        protected override void OnTextChanged(EventArgs e)
        {
            Text = string.Empty;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            DrawShape(g);
        }

        protected virtual void DrawShape(Graphics g)
        {
            // 计算图形的颜色
            var shapeColor = ForeColor;

            // 计算图形的尺寸和位置
            var size = (int)(Math.Min(Size.Width, Size.Height) * GraphicScale);
            var location = new Point((Width - size) / 2, (Height - size) / 2);

            switch (Shape)
            {
                case SimpleShape.Close:
                    {
                        var penWidth = size / 20;
                        Pen pen = new Pen(shapeColor, penWidth);
                        g.DrawLine(pen, location, new Point(location.X + size, location.Y + size));
                        g.DrawLine(pen, new Point(location.X, location.Y + size), new Point(location.X + size, location.Y));
                    }
                    break;
                case SimpleShape.Maximize:
                    {
                        var penWidth = size / 20;
                        Pen pen = new Pen(shapeColor, penWidth);
                        g.DrawRectangle(pen, location.X, location.Y, size, size);
                    }
                    break;
                case SimpleShape.Minimize:
                    {
                        location = new Point((Width - size) / 2, Height / 2);
                        var penWidth = size / 20;
                        Pen pen = new Pen(shapeColor, penWidth);
                        g.DrawLine(pen, location, new Point(location.X + size, location.Y));
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SimpleShape
        {
            Close,
            Maximize,
            Minimize,
        }
    }
}
