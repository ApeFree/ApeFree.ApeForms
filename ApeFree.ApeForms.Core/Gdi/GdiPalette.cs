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
    public partial class GdiPalette : Palette<Bitmap, GdiStyle>
    {
        private Graphics Graphic;

        protected override void OnOutputSizeChanged()
        {
            base.OnOutputSizeChanged();

            Canvas?.Dispose();
            Graphic?.Dispose();
            if (OutputSize.Width > 0 && OutputSize.Height > 0)
            {
                Canvas = new Bitmap((int)OutputSize.Width, (int)OutputSize.Height);
                Graphic = Graphics.FromImage(Canvas);
                Graphic.Clear(Color.Transparent);
            }
        }

        ///<inheritdoc/>
        public override void UpdateCanvas()
        {
            try
            {
                Graphic.Clear(Color.Transparent);

                Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                Graphic.SmoothingMode = SmoothingMode.HighQuality;
                Graphic.CompositingQuality = CompositingQuality.HighQuality;
                Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                base.UpdateCanvas();
            }
            catch (System.Exception) { }
        }

        public override void Dispose()
        {
            base.Dispose();

            if (Graphic != null)
            {
                Graphic.Dispose();
                Graphic = null;
            }
        }
    }

    public partial class GdiPalette
    {
        ///<inheritdoc/>
        protected override void DrawEllipseHandler(GdiStyle style, EllipseShape graphic)
        {
            var p = graphic.Points.First();
            p = TransformAbsCoordsToRelCoords(p);
            var size = new SizeF(graphic.Width * Scale, graphic.Height * Scale);

            if (style.Pen != null)
            {
                Graphic.DrawEllipse(style.Pen, p.X, p.Y, size.Width, size.Height);
            }

            if (style.Brush != null)
            {
                Graphic.FillEllipse(style.Brush, p.X, p.Y, size.Width, size.Height);
            }
        }

        ///<inheritdoc/>
        protected override void DrawRectangleHandler(GdiStyle style, RectangleShape graphic)
        {
            var p = graphic.Points.First();
            p = TransformAbsCoordsToRelCoords(p);
            if (style.Pen != null)
            {
                Graphic.DrawRectangle(style.Pen, p.X, p.Y, graphic.Width, graphic.Height);
            }

            if (style.Brush != null)
            {
                Graphic.FillRectangle(style.Brush, p.X, p.Y, graphic.Width, graphic.Height);
            }
        }

        ///<inheritdoc/>
        protected override void DrawCircleHandler(GdiStyle style, CircleShape graphic)
        {
            var p = graphic.Location;
            p = TransformAbsCoordsToRelCoords(p);

            var r = graphic.Radius * Scale;
            var d = r * 2;
            if (style.Pen != null)
            {
                Graphic.DrawEllipse(style.Pen, p.X - r, p.Y - r, d, d);
            }

            if (style.Brush != null)
            {
                Graphic.FillEllipse(style.Brush, p.X - r, p.Y - r, d, d);
            }
        }

        ///<inheritdoc/>
        protected override void DrawVectorHandler(GdiStyle style, VectorSahpe graphic)
        {
            var p1 = TransformAbsCoordsToRelCoords(graphic.StartPoint);
            var p2 = TransformAbsCoordsToRelCoords(graphic.EndPoint);

            Graphic.DrawLine(style.Pen, p1, p2);
        }

        ///<inheritdoc/>
        protected override void DrawLineHandler(GdiStyle style, LineShape graphic)
        {
            var p1 = TransformAbsCoordsToRelCoords(graphic.StartPoint);
            var p2 = TransformAbsCoordsToRelCoords(graphic.EndPoint);

            Graphic.DrawLine(style.Pen, p1, p2);
        }

        ///<inheritdoc/>
        protected override void DrawPolygonHandler(GdiStyle style, PolygonShape shape)
        {
            var ps = shape.DisplayPoints.Select(TransformAbsCoordsToRelCoords).ToArray();

            if (style.Pen != null)
            {
                Graphic.DrawPolygon(style.Pen, ps);
            }

            if (style.Brush != null)
            {
                Graphic.FillPolygon(style.Brush, ps);
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
                var size = Graphic.MeasureString(shape.Text, font);
                width = size.Width;
                height = size.Height;
            }

            // 绘制文字
            Graphic.DrawString(shape.Text, font, brush, new RectangleF(shape.Left, shape.Top, width, height), format);
        }

        ///<inheritdoc/>
        protected override void DrawComplexShapeHandler(GdiStyle style, ComplexShape shape)
        {

        }

        ///<inheritdoc/>
        protected override void DrawImageHandler(GdiStyle style, ImageShape shape)
        {
            if (shape.Image as Bitmap == null)
            {
                return;
            }

            // 绘制文字
            Graphic.DrawImage(shape.Image as Bitmap, shape.Location.X, shape.Location.Y, shape.Width, shape.Height);
        }
    }
}
