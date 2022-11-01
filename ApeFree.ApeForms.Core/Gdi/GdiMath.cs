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
    }
}
