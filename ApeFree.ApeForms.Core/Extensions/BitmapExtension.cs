using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Drawing
{
    public static class BitmapExtension
    {
        private static Color ColorPurificationWithoutAlphaHandle(Color color, Color pureColor)
        {
            return pureColor;
        }

        private static Color ColorPurificationReserveAlphaHandle(Color color, Color pureColor)
        {
            return Color.FromArgb(color.A, pureColor);
        }

        /// <summary>
        /// 转换为纯色图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="color"></param>
        /// <param name="reserveAlpha"></param>
        /// <returns></returns>
        public static Bitmap ToPureColor(this Bitmap bitmap, Color color, bool reserveAlpha)
        {
            return bitmap.ToPureColor(new Color[] { color }, reserveAlpha)[0];
        }

        /// <summary>
        /// 转换为多张纯色图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="colors"></param>
        /// <param name="reserveAlpha"></param>
        /// <returns></returns>
        public static Bitmap[] ToPureColor(this Bitmap bitmap, Color[] colors, bool reserveAlpha)
        {
            try
            {
                Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                bitmap = (Bitmap)bitmap.Clone(rectangle,Imaging.PixelFormat.Format32bppArgb);

                // 获取长宽，创建一个新的Bitmap副本
                int w = bitmap.Width;
                int h = bitmap.Height;

                // 创建Bitmap数组
                var bmps = colors.Select(c => new Bitmap(w, h)).ToArray();

                // 根据是否保留Alpha通道选择不同的处理过程
                Func<Color, Color, Color> colorPurificationHandle;
                if (reserveAlpha)
                {
                    colorPurificationHandle = ColorPurificationReserveAlphaHandle;
                }
                else
                {
                    colorPurificationHandle = ColorPurificationWithoutAlphaHandle;
                }


                // 根据传入Color[]的个数选择是否使用循环
                Action<Color, int, int> pixelHandle;
                if (colors.Length <= 1)
                {
                    pixelHandle = (originalColor, vx, vy) =>
                    {
                        bmps[0].SetPixel(vx, vy, colorPurificationHandle(originalColor, colors[0]));
                    };
                }
                else
                {
                    pixelHandle = (originalColor, vx, vy) =>
                    {
                        for (int i = 0; i < colors.Length; ++i)
                        {
                            bmps[i].SetPixel(vx, vy, colorPurificationHandle(originalColor, colors[i]));
                        }
                    };
                }

                // 遍历像素点
                int x, y;
                for (x = 0; x < w; ++x)
                {
                    for (y = 0; y < h; ++y)
                    {
                        var originalColor = bitmap.GetPixel(x, y);
                        if (originalColor.A == 0) continue;

                        pixelHandle.Invoke(originalColor, x, y);
                    }
                }

                return bmps;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
