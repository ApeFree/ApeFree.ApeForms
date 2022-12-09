using System;
using System.Collections.Generic;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi.Shapes
{
    public class LineShape : IShape
    {
        public PointF StartPoint { get; set; }

        public PointF EndPoint { get; set; }

        public IEnumerable<PointF> Points => new PointF[] { StartPoint, EndPoint };

        private PointF CentrePoint => new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);

        /// <summary>
        /// 线长
        /// </summary>
        public double Length => GdiMath.CalculateLengthFromTwoPoints(StartPoint, EndPoint);

        public void Rotate(PointF centralPoint, float angle)
        {
            var cp = CentrePoint;
            StartPoint = GdiMath.PointAround(cp, StartPoint, angle);
            EndPoint = GdiMath.PointAround(cp, EndPoint, angle);
        }

        public void Scale(float scaling)
        {
            throw new NotImplementedException();
        }

        public void Offset(float distanceX, float distanceY)
        {
            StartPoint = StartPoint.Add(distanceX, distanceY);
            EndPoint = EndPoint.Add(distanceX, distanceY);
        }

        public bool Contains(PointF point)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 构造线图形
        /// </summary>
        /// <param name="x1">起始点X坐标</param>
        /// <param name="y1">起始点Y坐标</param>
        /// <param name="x2">结束点X坐标</param>
        /// <param name="y2">结束点Y坐标</param>
        public LineShape(float x1, float y1, float x2, float y2) : this(new PointF(x1, y1), new PointF(x2, y2)) { }

        /// <summary>
        /// 构造线图形
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="endPoint">结束点</param>
        public LineShape(PointF startPoint, PointF endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        /// 构造线图形
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="length">长度</param>
        /// <param name="angle">角度</param>
        public LineShape(PointF startPoint, double length, float angle)
        {
            StartPoint = startPoint;
            EndPoint = GdiMath.CalculatePointOnCircle(startPoint, (float)length, angle);
        }
    }
}
