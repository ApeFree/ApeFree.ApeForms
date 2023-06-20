using System;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// GDI+图形样式
    /// </summary>
    public class GdiStyle : IDisposable
    {
        public GdiStyle(Pen pen = null, Brush brush = null)
        {
            Pen = pen;
            Brush = brush;
        }

        public GdiStyle(Brush brush)
        {
            Brush = brush;
        }

        /// <summary>
        /// 画笔
        /// </summary>
        public Pen Pen { get; set; }

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Brush { get; set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            Pen?.Dispose();
            Brush?.Dispose();

            Pen = null;
            Brush = null;
        }
    }
}
