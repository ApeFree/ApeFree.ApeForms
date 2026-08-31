using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    /// <summary>
    /// PictureBox控件的扩展方法
    /// </summary>
    public static class PictureBoxExtensions
    {
        /// <summary>
        /// 获取PictureBox中鼠标位置对应的图像像素坐标（支持所有SizeMode）
        /// </summary>
        /// <param name="pictureBox">目标PictureBox控件</param>
        /// <param name="mousePosition">鼠标在PictureBox中的位置</param>
        /// <returns>如果鼠标在图像显示区域内，返回对应的图像坐标；否则返回(-1, -1)</returns>
        public static Point GetImageCoordinate(this PictureBox pictureBox, Point mousePosition)
        {
            // 检查是否有图像加载
            if (pictureBox.Image == null || pictureBox.SizeMode == PictureBoxSizeMode.AutoSize)
                return new Point(-1, -1);

            // 获取图像和控件的尺寸
            Size imageSize = pictureBox.Image.Size;
            Size controlSize = pictureBox.ClientSize;

            // 根据不同的SizeMode采取不同的计算方式
            switch (pictureBox.SizeMode)
            {
                case PictureBoxSizeMode.Normal:
                    return GetNormalModeCoordinate(mousePosition, imageSize, controlSize, pictureBox.AutoScrollOffset);

                case PictureBoxSizeMode.StretchImage:
                    return GetStretchModeCoordinate(mousePosition, imageSize, controlSize);

                case PictureBoxSizeMode.Zoom:
                    return GetZoomModeCoordinate(mousePosition, imageSize, controlSize);

                case PictureBoxSizeMode.CenterImage:
                    return GetCenterModeCoordinate(mousePosition, imageSize, controlSize);

                default:
                    return new Point(-1, -1);
            }

            /// <summary>
            /// 处理Normal模式的坐标转换
            /// </summary>
            Point GetNormalModeCoordinate(Point mousePos, Size imageSize, Size controlSize, Point autoScrollPos)
            {
                // 在Normal模式下，图像从(0,0)开始显示，可能被裁剪或带有滚动条
                int x = mousePos.X + autoScrollPos.X;
                int y = mousePos.Y + autoScrollPos.Y;

                // 检查是否在图像范围内
                if (x >= 0 && x < imageSize.Width && y >= 0 && y < imageSize.Height)
                    return new Point(x, y);

                return new Point(-1, -1);
            }

            /// <summary>
            /// 处理StretchImage模式的坐标转换
            /// </summary>
            Point GetStretchModeCoordinate(Point mousePos, Size imageSize, Size controlSize)
            {
                // 在Stretch模式下，图像被拉伸填充整个控件
                float scaleX = (float)imageSize.Width / controlSize.Width;
                float scaleY = (float)imageSize.Height / controlSize.Height;

                int x = (int)(mousePos.X * scaleX);
                int y = (int)(mousePos.Y * scaleY);

                // 由于图像被拉伸填充，理论上鼠标始终在图像范围内
                x = Math.Max(0, Math.Min(x, imageSize.Width - 1));
                y = Math.Max(0, Math.Min(y, imageSize.Height - 1));

                return new Point(x, y);
            }

            /// <summary>
            /// 处理Zoom模式的坐标转换
            /// </summary>
            Point GetZoomModeCoordinate(Point mousePos, Size imageSize, Size controlSize)
            {
                // 计算保持宽高比的缩放比例
                float zoomRatio = Math.Min(
                    (float)controlSize.Width / imageSize.Width,
                    (float)controlSize.Height / imageSize.Height);

                // 计算图像在控件中的实际显示尺寸
                Size displaySize = new Size(
                    (int)(imageSize.Width * zoomRatio),
                    (int)(imageSize.Height * zoomRatio));

                // 计算图像在控件中的起始位置（居中显示）
                int startX = (controlSize.Width - displaySize.Width) / 2;
                int startY = (controlSize.Height - displaySize.Height) / 2;

                // 检查鼠标是否在图像显示区域内
                if (mousePos.X < startX || mousePos.X >= startX + displaySize.Width ||
                    mousePos.Y < startY || mousePos.Y >= startY + displaySize.Height)
                {
                    return new Point(-1, -1);
                }

                // 计算相对于图像显示区域的坐标并转换为图像坐标
                int imageX = (int)((mousePos.X - startX) / zoomRatio);
                int imageY = (int)((mousePos.Y - startY) / zoomRatio);

                return new Point(imageX, imageY);
            }

            /// <summary>
            /// 处理CenterImage模式的坐标转换
            /// </summary>
            Point GetCenterModeCoordinate(Point mousePos, Size imageSize, Size controlSize)
            {
                // 在Center模式下，图像居中显示，但不缩放
                int startX = (controlSize.Width - imageSize.Width) / 2;
                int startY = (controlSize.Height - imageSize.Height) / 2;

                // 检查鼠标是否在图像显示区域内
                if (mousePos.X < startX || mousePos.X >= startX + imageSize.Width ||
                    mousePos.Y < startY || mousePos.Y >= startY + imageSize.Height)
                {
                    return new Point(-1, -1);
                }

                // 直接计算相对于图像原点的偏移
                return new Point(mousePos.X - startX, mousePos.Y - startY);
            }
        }
    }
}
