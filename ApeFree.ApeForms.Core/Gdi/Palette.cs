using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// 图形面板基类
    /// </summary>
    /// <typeparam name="TStyle">绘制风格类型</typeparam>
    public abstract class Palette<TStyle> : IDisposable
    {
        /// <summary>图层</summary>
        public IList<Layer<TStyle>> Layers { get; private set; }

        /// <summary>构造画板</summary>
        protected internal Palette()
        {
            Layers = new List<Layer<TStyle>>();
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Layers.Clear();
            Layers = null;
        }

        /// <summary>更新画布</summary>
        public virtual void UpdateCanvas()
        {
            foreach (var layer in Layers)
            {
                // 跳过绘制不可见的图层
                if (!layer.Visible)
                {
                    continue;
                }

                // 根据图形类型调用对应的绘制实现
                switch (layer.Shape)
                {
                    case LineShape shape:
                        DrawLineHandler(layer.Style, shape);
                        break;
                    case EllipseShape shape:
                        DrawEllipseHandler(layer.Style, shape);
                        break;
                    case RectangleShape shape:
                        DrawRectangleHandler(layer.Style, shape);
                        break;
                    case CircleShape shape:
                        DrawCircleHandler(layer.Style, shape);
                        break;
                }
            }
        }

        /// <summary>绘制线</summary>
        public Layer<TStyle, LineShape> DrawLine(TStyle style, LineShape graphic) => Draw(style, graphic);

        /// <summary>绘制椭圆</summary>
        public Layer<TStyle, EllipseShape> DrawEllipse(TStyle style, EllipseShape graphic) => Draw(style, graphic);

        /// <summary>绘制矩形</summary>
        public Layer<TStyle, RectangleShape> DrawRectangle(TStyle style, RectangleShape graphic) => Draw(style, graphic);

        /// <summary>绘制圆形</summary>
        public Layer<TStyle, CircleShape> DrawCircle(TStyle style, CircleShape graphic) => Draw(style, graphic);

        public Layer<TStyle, TShape> Draw<TShape>(TStyle style, TShape graphic) where TShape : IShape
        {
            var layer = new Layer<TStyle, TShape>(this, style, graphic);
            Layers.Add(layer);
            return layer;
        }

        /// <summary>绘制线的实现过程</summary>
        protected abstract void DrawEllipseHandler(TStyle style, EllipseShape graphic);
        /// <summary>绘制椭圆的实现过程</summary>
        protected abstract void DrawLineHandler(TStyle style, LineShape graphic);
        /// <summary>绘制矩形的实现过程</summary> 
        protected abstract void DrawRectangleHandler(TStyle style, RectangleShape graphic);
        /// <summary>绘制圆形的实现过程</summary> 
        protected abstract void DrawCircleHandler(TStyle style, CircleShape graphic);
    }

    /// <summary>
    /// 图形面板基类
    /// </summary>
    /// <typeparam name="TCanvas">画布类型</typeparam>
    /// <typeparam name="TStyle">绘制风格类型</typeparam>
    public abstract class Palette<TCanvas, TStyle> : Palette<TStyle>
    {
        /// <summary>
        /// 画布
        /// </summary>
        public TCanvas Canvas { get; set; }

        /// <summary>
        /// 构造画板
        /// </summary>
        protected Palette() { }

        /// <summary>
        /// 构造画板
        /// </summary>
        /// <param name="canvas">画布对象</param>
        protected Palette(TCanvas canvas) : this()
        {
            Canvas = canvas;
        }
    }

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
        protected override void DrawLineHandler(GdiStyle style, LineShape graphic)
        {
            Canvas.DrawLine(style.Pen, graphic.StartPoint, graphic.EndPoint);
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
            if (style.Pen != null)
            {
                Canvas.DrawEllipse(style.Pen, p.X - r, p.Y - r, r, r);
            }

            if (style.Brush != null)
            {
                Canvas.FillEllipse(style.Brush, p.X - r, p.Y - r, r, r);
            }
        }
    }

    /// <summary>
    /// GDI+图形样式
    /// </summary>
    public class GdiStyle
    {
        /// <summary>
        /// 画笔
        /// </summary>
        public Pen Pen { get; set; }

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Brush { get; set; }
    }
}
