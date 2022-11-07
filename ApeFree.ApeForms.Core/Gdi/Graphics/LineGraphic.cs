using System;
using System.Collections.Generic;
using System.Drawing;

namespace ApeFree.ApeForms.Core.Gdi.Graphics
{
    public class LineGraphic : IGraphic
    {
        public PointF StartPoint { get; set; }

        public PointF EndPoint { get; set; }

        public IEnumerable<PointF> Points => new PointF[] { StartPoint, EndPoint };

        private PointF CentrePoint => new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);

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

        public void Translation(float distanceX, float distanceY)
        {
            StartPoint = StartPoint.Add(distanceX, distanceY);
            EndPoint = EndPoint.Add(distanceX, distanceY);
        }
    }
}
