using ApeFree.ApeDialogs.Theme;
using System;
using System.Drawing;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    /// <summary>
    /// ApeDialogs 跨平台类型与 System.Drawing 类型之间的转换扩展
    /// </summary>
    public static class ApeDialogConversions
    {
        /// <summary>
        /// 将 ApeColor 转换为 System.Drawing.Color
        /// </summary>
        public static Color ToColor(this ApeColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// 将 System.Drawing.Color 转换为 ApeColor
        /// </summary>
        public static ApeColor ToApeColor(this Color color)
        {
            return new ApeColor(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// 将 ApeSize 转换为 System.Drawing.Size
        /// </summary>
        public static Size ToSize(this ApeSize size)
        {
            return new Size(size.Width, size.Height);
        }

        /// <summary>
        /// 将 DialogFont 转换为 System.Drawing.Font
        /// </summary>
        public static Font ToFont(this DialogFont font)
        {
            var style = FontStyle.Regular;
            if (font.Weight == FontWeight.Bold)
            {
                style |= FontStyle.Bold;
            }
            if (font.Italic)
            {
                style |= FontStyle.Italic;
            }

            var family = !string.IsNullOrEmpty(font.Family) ? font.Family : SystemFonts.DefaultFont.FontFamily.Name;
            var size = font.Size > 0 ? font.Size : SystemFonts.DefaultFont.SizeInPoints;
            return new Font(family, size, style);
        }

        /// <summary>
        /// 同步显示 Dialog 并返回结果。
        /// </summary>
        public static ApeFree.ApeDialogs.Core.DialogResult<TResult> Show<TResult>(this ApeFree.ApeDialogs.Core.IDialog<TResult> dialog)
        {
            return dialog.ShowAsync().GetAwaiter().GetResult();
        }
    }
}