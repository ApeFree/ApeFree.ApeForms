using ApeFree.Cake2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Gdi
{
    public partial class CanvasForm : Form
    {
        /// <summary>
        /// GDI+绘图画板
        /// </summary>
        public GdiPalette Palette { get; } = new GdiPalette();

        public CanvasForm()
        {
            InitializeComponent();
        }

        protected Layer<GdiStyle> HoverLayer { get; set; }
        protected Layer<GdiStyle> FocusLayer { get; set; }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var layer = Palette.SelectTopLayerByCastingPoint(e.Location);

            if (layer == null)
            {
                return;
            }

            if (HoverLayer != layer)
            {
                HoverLayer?.RaiseMouseLeave(HoverLayer, EventArgs.Empty);

                HoverLayer = layer;
                HoverLayer.RaiseMouseEnter(HoverLayer, EventArgs.Empty);
            }

            HoverLayer.RaiseMouseMove(HoverLayer, e.Convert());
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HoverLayer?.RaiseMouseLeave(HoverLayer, EventArgs.Empty);
            HoverLayer = null;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            HoverLayer?.RaiseClick(HoverLayer, e);
            FocusLayer = HoverLayer;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            HoverLayer?.RaiseDoubleClick(HoverLayer, e);
            FocusLayer = HoverLayer;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            HoverLayer?.RaiseMouseDown(HoverLayer, e.Convert());
            FocusLayer = HoverLayer;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            HoverLayer?.RaiseMouseUp(HoverLayer, e.Convert());
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            HoverLayer?.RaiseMouseWheel(HoverLayer, e.Convert());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RefreshPalette();
        }

        protected void RefreshPalette()
        {
            try
            {
                // 需要绘制的矩形区域
                Rectangle rect = ClientRectangle;
                Palette.OutputSize = rect.Size;

                Palette.UpdateCanvas();

                // 将位图缓存直接绘制到 Panel 控件上
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.DrawImageUnscaled(Palette.Canvas, rect);
                }
            }
            catch (Exception) { }
        }
    }

    internal static class EventArgsExtensions
    {
        public static Cake2D.Events.MouseEventArgs Convert(this MouseEventArgs e)
        {
            return new Cake2D.Events.MouseEventArgs((Cake2D.Events.MouseButtons)(int)e.Button, e.Clicks, e.X, e.Y, e.Delta);
        }
    }
}
