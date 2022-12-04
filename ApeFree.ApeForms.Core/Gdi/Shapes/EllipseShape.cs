using System;
using System.Collections.Generic;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi.Shapes
{
    public class EllipseShape : IShape
    {
        public IEnumerable<PointF> Points => throw new NotImplementedException();

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
