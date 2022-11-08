using System;
using System.Collections.Generic;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi.Shapes
{
    public class EllipseGraphic : IGraphic
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

        public void Translation(float distanceX, float distanceY)
        {
            throw new NotImplementedException();
        }
    }
}
