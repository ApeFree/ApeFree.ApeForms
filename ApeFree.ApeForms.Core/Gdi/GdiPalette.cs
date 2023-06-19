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
            Image = new Bitmap(clientRectangle.Width, clientRectangle.Height);
            Canvas = Graphics.FromImage(Image);
        }

        ///<inheritdoc/>
        public override void UpdateCanvas()
        {
            Canvas.SmoothingMode = SmoothingMode.AntiAlias;
            Canvas.SmoothingMode = SmoothingMode.HighQuality;
            Canvas.CompositingQuality = CompositingQuality.HighQuality;
            Canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

            base.UpdateCanvas();
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
                Canvas.DrawPolygon(style.Pen, shape.Points.ToArray());
            }

            if (style.Brush != null)
            {
                Canvas.FillPolygon(style.Brush, shape.Points.ToArray());
            }
        }
    }
}
