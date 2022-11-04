using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// GDI+绘图数学库
    /// </summary>
    public static class GdiMath
    {
        /// <summary>
        /// 计算圆上点的坐标
        /// </summary>
        /// <param name="centrePoint">圆心</param>
        /// <param name="radius">半径</param>
        /// <param name="angle">角度</param>
        /// <returns></returns>
        public static PointF CalculatePointOnCircle(PointF centrePoint, float radius, float angle)
        {
            var x = centrePoint.X + radius * Math.Cos(angle * Math.PI / 180);
            var y = centrePoint.Y + radius * Math.Sin(angle * Math.PI / 180);
            return new PointF((float)x, (float)y);
        }

        /// <summary>
        /// 通过两点坐标计算距离
        /// </summary>
        /// <param name="p1">坐标1</param>
        /// <param name="p2">坐标2</param>
        /// <returns></returns>
        public static double CalculateLengthFromTwoPoints(PointF p1, PointF p2)
        {
            double length = Math.Sqrt(Math.Abs(p1.X - p2.X) * Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) * Math.Abs(p1.Y - p2.Y));
            return length;
        }

        /// <summary>
        /// 计算两点之间的角度
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double CalculateAngleFromTwoPoints(PointF p1, PointF p2)
        {
            double angle = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;
            return angle > 0 ? angle : 360 + angle;
        }

        /// <summary>
        /// 点位环绕
        /// </summary>
        /// <param name="centrePoint">中心点</param>
        /// <param name="satellitePoint">卫星点</param>
        /// <param name="rotationAngle">旋转半径</param>
        /// <returns></returns>
        public static PointF PointAround(PointF centrePoint, PointF satellitePoint, float rotationAngle)
        {
            var radius = CalculateLengthFromTwoPoints(centrePoint, satellitePoint);
            var angle = CalculateAngleFromTwoPoints(centrePoint, satellitePoint);
            var newPoint = CalculatePointOnCircle(centrePoint, (float)radius, (float)(angle + rotationAngle));

            return newPoint;
        }
    }
}
