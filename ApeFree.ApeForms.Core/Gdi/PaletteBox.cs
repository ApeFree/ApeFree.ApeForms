using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Gdi
{
    [Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
    public class PaletteBox : PictureBox
    {
        private readonly GdiPalette _palette;
        private Point _mouseDownLocation;
        private bool isDragged;

        public GdiPalette Palette => _palette;

        public Color CanvasBackColor
        {
            get => _palette.BackColor;
            set
            {
                if (_palette.BackColor != value)
                {
                    _palette.BackColor = value;
                    RefreshImage();
                }
            }
        }

        public bool EnableMouseWheelScale { get; set; }
        public bool AllowDragCanvas { get; private set; }

        public PaletteBox()
        {
            _palette = new GdiPalette();
        }

        public void RefreshImage()
        {
            _palette.UpdateCanvas();
            Image = _palette.Canvas;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _mouseDownLocation = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);


            if (AllowDragCanvas)
            {
                if (e.Button != MouseButtons.Left || _mouseDownLocation == new Point(-1, -1) || e.Location == _mouseDownLocation)
                {
                    return;
                }

                isDragged = true;

                var offset = new Point(e.Location.X - _mouseDownLocation.X, e.Location.Y - _mouseDownLocation.Y);
                _palette.ZoomCenter = new PointF(
                    _palette.ZoomCenter.X - offset.X * _palette.Scale,
                    _palette.ZoomCenter.Y - offset.Y * _palette.Scale);

                _mouseDownLocation = e.Location;

                RefreshImage();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // 如果鼠标位置没有变化，则不进行处理
            if (isDragged)
            {
                _mouseDownLocation = new Point(-1, -1);
                isDragged = false;
                return;
            }

            // 获取鼠标在图像上位置
            var point = this.GetImageCoordinate(e.Location);

            // 获取鼠标在画板上位置
            var point2 = _palette.TransformRelCoordsToAbsCoords(point);

            // 获取鼠标在画板上的位置对应的图层
            var layer = _palette.SelectTopLayerByCastingPoint(point2);
            if (layer == null)
            {
                return;
            }

            layer.Style.Brush = Brushes.Blue;
            RefreshImage();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (EnableMouseWheelScale)
            {
                _palette.Scale += ((e.Delta > 0 ? 1 : -1) * (_palette.Scale * 0.1f));
                RefreshImage();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _palette.Dispose();
        }
    }
}
