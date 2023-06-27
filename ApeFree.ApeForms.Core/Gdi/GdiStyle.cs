using System;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// GDI+图形样式
    /// </summary>
    public class GdiStyle
    {
        public GdiStyle(Pen pen = null, Brush brush = null, Font font = null, StringFormat stringFormat = null)
        {
            Pen = pen;
            Brush = brush;
            Font = font;
            StringFormat = stringFormat;
        }

        public GdiStyle(Font font, StringFormat stringFormat)
        {
            Font = font;
            StringFormat = stringFormat;
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

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 文本格式
        /// </summary>
        public StringFormat StringFormat { get; set; }

        /// <inheritdoc/>
        public void Clear()
        {
            Pen?.Dispose();
            Brush?.Dispose();
            Font?.Dispose();
            StringFormat?.Dispose();

            Pen = null;
            Brush = null;
            Font = null;
            StringFormat = null;
        }
    }
}
