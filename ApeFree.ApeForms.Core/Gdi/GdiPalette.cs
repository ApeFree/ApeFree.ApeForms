using ApeFree.Cake2D;
using ApeFree.Cake2D.Shapes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// WinForm Gdi+图形画板
    /// </summary>
    public class GdiPalette : Palette<Graphics, GdiStyle>
    {
        private Rectangle clientRectangle;

        public Bitmap Image { get; private set; }

        public Rectangle ClientRectangle
        {
            get => clientRectangle;
            set
            {
                if (clientRectangle != value)
                {
                    // 这里还有优化的空间
                    clientRectangle = value;
                    UpdateClientRectangle();
                }
            }
        }

        private void UpdateClientRectangle()
        {
            Image?.Dispose();
            Canvas?.Dispose();
            if (clientRectangle.Width > 0 && clientRectangle.Height > 0)
            {
                Image = new Bitmap(clientRectangle.Width, clientRectangle.Height);
                Canvas = Graphics.FromImage(Image);
            }
        }

        ///<inheritdoc/>
        public override void UpdateCanvas()
        {
            try
            {
                Canvas.SmoothingMode = SmoothingMode.AntiAlias;
                Canvas.SmoothingMode = SmoothingMode.HighQuality;
                Canvas.CompositingQuality = CompositingQuality.HighQuality;
                Canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

                base.UpdateCanvas();
            }
            catch (System.Exception) { }
        }

        ///<inheritdoc/>
        protected override void DrawEllipseHandler(GdiStyle style, EllipseShape graphic)
        {
            var p = graphic.Points.First();
            if (style.Pen != null)
            {
                Canvas.DrawEllipse(style.Pen, p.X, p.Y, graphic.Width, graphic.Height);
            }

            if (style.Brush != null)
            {
                Canvas.FillEllipse(style.Brush, p.X, p.Y, graphic.Width, graphic.Height);
            }
        }

        ///<inheritdoc/>
        protected override void DrawRectangleHandler(GdiStyle style, RectangleShape graphic)
        {
            var p = graphic.Points.First();
            if (style.Pen != null)
            {
                Canvas.DrawRectangle(style.Pen, p.X, p.Y, graphic.Width, graphic.Height);
            }

            if (style.Brush != null)
            {
                Canvas.FillRectangle(style.Brush, p.X, p.Y, graphic.Width, graphic.Height);
            }
        }

        ///<inheritdoc/>
        protected override void DrawCircleHandler(GdiStyle style, CircleShape graphic)
        {
            var p = graphic.CenterPoint;
            var r = graphic.Radius;
            var d = r * 2;
            if (style.Pen != null)
            {
                Canvas.DrawEllipse(style.Pen, p.X - r, p.Y - r, d, d);
            }

            if (style.Brush != null)
            {
                Canvas.FillEllipse(style.Brush, p.X - r, p.Y - r, d, d);
            }
        }

        ///<inheritdoc/>
        protected override void DrawVectorHandler(GdiStyle style, VectorSahpe graphic)
        {
            Canvas.DrawLine(style.Pen, graphic.StartPoint, graphic.EndPoint);
        }

        ///<inheritdoc/>
        protected override void DrawLineHandler(GdiStyle style, LineShape graphic)
        {
            Canvas.DrawLine(style.Pen, graphic.StartPoint, graphic.EndPoint);
        }

        ///<inheritdoc/>
        protected override void DrawPolygonHandler(GdiStyle style, PolygonShape shape)
        {
            if (style.Pen != null)
            {
                Canvas.DrawPolygon(style.Pen, shape.Points);
            }

            if (style.Brush != null)
            {
                Canvas.FillPolygon(style.Brush, shape.Points);
            }
        }

        ///<inheritdoc/>
        protected override void DrawTextHandler(GdiStyle style, TextShape shape)
        {
            if (string.IsNullOrEmpty(shape.Text))
            {
                return;
            }

            var brush = style.Brush ?? Brushes.Black;
            var format = style.StringFormat ?? new StringFormat();
            var font = style.Font ?? SystemFonts.DefaultFont;

            var width = shape.Width;
            var height = shape.Height;

            if (shape.Width <= 0 || shape.Height <= 0)
            {
                var size = Canvas.MeasureString(shape.Text, font);
                width = size.Width;
                height = size.Height;
            }

            // 绘制文字
            Canvas.DrawString(shape.Text, font, brush, new RectangleF(shape.Left, shape.Top, width, height), format);
        }
    }
}
