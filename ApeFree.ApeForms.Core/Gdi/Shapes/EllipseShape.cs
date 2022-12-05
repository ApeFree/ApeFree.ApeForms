using System;
using System.Collections.Generic;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi.Shapes
{
    public class EllipseShape : RectangleShape
    {
        public IEnumerable<PointF> Points => new PointF[] { Location };


        public EllipseShape(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public EllipseShape(PointF location, SizeF size) : base(location, size)
        {
        }

        /// <summary>
        /// 圆心点
        /// </summary>
        public PointF CentrePoint => new PointF(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);

        public void Rotate(PointF centralPoint, float angle)
        {
            return;
        }

        public void Scale(float scaling)
        {
            throw new NotImplementedException();
        }

        public void Offset(float distanceX, float distanceY)
        {
            throw new NotImplementedException();
        }

        public bool Contains(PointF point)
        {
            throw new NotImplementedException();
        }
    }
}
