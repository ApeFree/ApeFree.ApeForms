using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
        /// <param name="bitmap">位图</param>
        /// <param name="color">目标颜色</param>
        /// <param name="reserveAlpha">是否保留透明通道</param>
        /// <returns></returns>
        public static Bitmap ToPureColor(this Bitmap bitmap, Color color, bool reserveAlpha = false)
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
                bitmap = bitmap.Clone(rectangle, PixelFormat.Format32bppArgb);

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


        public static Bitmap CopyRegion(this Bitmap src, Rectangle rect)
        {
            // 计算每行像素的字节数
            int bytesPerPixel = Image.GetPixelFormatSize(src.PixelFormat) / 8;

            //var bytesPerPixel = src.PixelFormat switch
            //{
            //    Imaging.PixelFormat.Format16bppArgb1555 => 2,
            //    Imaging.PixelFormat.Format16bppGrayScale => 2,
            //    Imaging.PixelFormat.Format16bppRgb565 => 2,
            //    Imaging.PixelFormat.Format16bppRgb555 => 2,

            //    Imaging.PixelFormat.Format24bppRgb => 3,

            //    Imaging.PixelFormat.Format32bppRgb => 4,
            //    Imaging.PixelFormat.Format32bppArgb => 4,
            //    Imaging.PixelFormat.Format32bppPArgb => 4,

            //    Imaging.PixelFormat.Format48bppRgb => 6,

            //    Imaging.PixelFormat.Format64bppArgb => 8,

            //    _ => throw new NotSupportedException("不受支持的像素格式。"),
            //};

            var rowBytesCount = src.Width * bytesPerPixel;
            rowBytesCount += (4 - rowBytesCount % 4) % 4;

            var startRowIndex = rect.Left * bytesPerPixel;
            var endRowIndex = (rect.Left + rect.Width) * bytesPerPixel;

            if (startRowIndex < 0 || startRowIndex > rowBytesCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rect), "坐标在图像之外。");
            }

            if (endRowIndex < 0 || endRowIndex > rowBytesCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rect), "坐标在图像之外。");
            }

            // TODO: 还有高度

            // 创建新图片
            var dest = new Bitmap(rect.Width, rect.Height, src.PixelFormat);

            BitmapData srcBitmapData = src.LockBits(rect, ImageLockMode.ReadOnly, src.PixelFormat);
            BitmapData destBitmapData = dest.LockBits(new Rectangle(new Point(0), dest.Size), ImageLockMode.WriteOnly, src.PixelFormat);

            // 计算拷贝数据的长度
            int copyLength = rect.Width * rect.Height * bytesPerPixel;

            // 使用Marshal.Copy方法进行数据拷贝
            var arr = new byte[copyLength];
            Marshal.Copy(srcBitmapData.Scan0, arr, 0, copyLength);
            Marshal.Copy(arr, 0, destBitmapData.Scan0, copyLength);


            // 释放锁定的区域
            src.UnlockBits(srcBitmapData);
            dest.UnlockBits(destBitmapData);

            return dest;
        }


        public static Bitmap CopyRegion2(this Bitmap src, Rectangle rect, Bitmap dest = null)
        {
            // 计算每行像素的字节数
            int bytesPerPixel = Image.GetPixelFormatSize(src.PixelFormat) / 8;

            // 计算实际拷贝区域
            rect = Rectangle.Intersect(new Rectangle(0, 0, src.Width, src.Height), rect);


            // 判断
            var rowBytesCount = src.Width * bytesPerPixel;
            rowBytesCount += (4 - rowBytesCount % 4) % 4;

            var startRowIndex = rect.Left * bytesPerPixel;
            var endRowIndex = (rect.Left + rect.Width) * bytesPerPixel;

            if (startRowIndex < 0 || startRowIndex > rowBytesCount || rect.Top < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rect), "坐标在图像之外。");
            }

            if (endRowIndex < 0 || endRowIndex > rowBytesCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rect), "坐标在图像之外。");
            }

            if (dest != null)
            {
                if (dest.Size != rect.Size || src.PixelFormat != dest.PixelFormat)
                {
                    // 销毁原图片
                    dest.Dispose();
                    // 创建新图片
                    dest = new Bitmap(rect.Width, rect.Height, src.PixelFormat);
                }
            }
            else
            {
                // 创建新图片
                dest = new Bitmap(rect.Width, rect.Height, src.PixelFormat);
            }

            // 获取两张图片选中区域的原始数据
            BitmapData srcBitmapData = src.LockBits(rect, ImageLockMode.ReadOnly, src.PixelFormat);
            BitmapData destBitmapData = dest.LockBits(new Rectangle(new Point(0), dest.Size), ImageLockMode.WriteOnly, src.PixelFormat);

            // 计算每行像素数据的字节数，包含字节对齐
            int srcStride = srcBitmapData.Stride;
            int destStride = destBitmapData.Stride;

            // 循环复制数据
            for (int y = 0; y < rect.Height; y++)
            {
                CopyData(
                    IntPtr.Add(srcBitmapData.Scan0, y * srcStride),
                    destBitmapData.Scan0 + y * destStride,
                    rect.Width * bytesPerPixel
                );
            }

            // 释放锁定的区域
            src.UnlockBits(srcBitmapData);
            dest.UnlockBits(destBitmapData);

            return dest;
        }

        /// <summary>
        /// 灰度拉伸
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static void GrayScale(this Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            int width = bitmap.Width;
            int height = bitmap.Height;

            // 锁定Bitmap的数据
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int stride = data.Stride;

            unsafe
            {
                byte* bytes = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* row = bytes + (y * stride); // 获取当前行的首地址

                    for (int x = 0; x < width; x++)
                    {
                        byte* pixel = row + x * bytesPerPixel; // 获取当前像素的首地址

                        // 将彩色转为灰度
                        byte gray = (byte)((0.299 * pixel[2]) + (0.587 * pixel[1]) + (0.114 * pixel[0]));

                        // 将灰度值写入RGB通道
                        pixel[0] = gray; // Blue
                        pixel[1] = gray; // Green
                        pixel[2] = gray; // Red
                    }
                }
            }

            // 解锁Bitmap的数据
            bitmap.UnlockBits(data);
        }


        /// <summary>
        /// 根据指定的对比度因子在原图像上调节亮度。
        /// </summary>
        public static void AdjustBrightness(this Bitmap src, short value)
        {
            if (value == 0)
            {
                return;
            }

            // 锁定图像的像素数据
            BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(src.PixelFormat) / 8;
            int stride = data.Stride;

            var width = src.Width;
            var height = src.Height;

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* currentLine = ptr + (y * stride);
                    for (int x = 0; x < width; x++)
                    {
                        for (int i = 0; i < bytesPerPixel; i++)
                        {
                            var val = (currentLine[i] + value);
                            currentLine[i] = (byte)Math.Max(byte.MinValue, Math.Min(byte.MaxValue, val));
                        }
                        currentLine += bytesPerPixel;
                    }
                }
            }

            // 解锁像素数据
            src.UnlockBits(data);
        }




        /// <summary>
        /// 调整图像的对比度，直接在当前的Bitmap上进行操作。
        /// 对比度参数取值范围为[-100, 100]，其中取值0表示不进行对比度调整，负值减小对比度，正值增加对比度。
        /// </summary>
        /// <param name="src">要调整对比度的源图像</param>
        /// <param name="contrast">对比度调整参数，取值范围为[-100, 100]</param>
        public static void AdjustContrast(this Bitmap src, float contrast)
        {

            if (contrast < -100 || contrast > 100)
            {
                throw new ArgumentException("Contrast parameter is out of range [-100, 100]");
            }

            float factor = (259 * (contrast + 255)) / (255 * (259 - contrast));

            BitmapData bmpData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);

            unsafe
            {
                int bytesPerPixel = Bitmap.GetPixelFormatSize(src.PixelFormat) / 8;
                int heightInPixels = bmpData.Height;
                int widthInBytes = bmpData.Width * bytesPerPixel;

                byte* ptrFirstPixel = (byte*)bmpData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bmpData.Stride);

                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldValue = currentLine[x];
                        int newValue = (int)Math.Max(0, Math.Min(255, factor * (oldValue - 128) + 128));
                        currentLine[x] = (byte)newValue;

                        if (bytesPerPixel > 1)
                        {
                            for (int i = 1; i < bytesPerPixel; i++)
                            {
                                currentLine[x + i] = (byte)Math.Max(0, Math.Min(255, factor * (currentLine[x + i] - 128) + 128));
                            }
                        }
                    }
                }
            }

            src.UnlockBits(bmpData);
        }

        private static void CopyData(IntPtr src, IntPtr dest, int length)
        {
            unsafe
            {
                byte* srcPtr = (byte*)src;
                byte* destPtr = (byte*)dest;

                for (int i = 0; i < length; i++)
                {
                    destPtr[i] = srcPtr[i];
                }
            }
        }
    }
}
