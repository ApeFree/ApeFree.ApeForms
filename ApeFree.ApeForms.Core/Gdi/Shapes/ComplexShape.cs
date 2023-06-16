using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Core.Gdi.Shapes
{
    /// <summary>
    /// 复合图形
    /// </summary>
    public class ComplexShape : IShape
    {
        public ComplexShape()
        {
            Graphics = new LinkedList<IShape>();
        }

        public LinkedList<IShape> Graphics { get; }

        public IEnumerable<Point> Points => Graphics.SelectMany(g => g.Points);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="centralPoint"></param>
        /// <param name="angle"></param>
        public void Rotate(Point centralPoint, float angle)
        {
            foreach (IShape g in Graphics)
            {
                g.Rotate(centralPoint, angle);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="scaling"></param>
        public void Scale(float scaling)
        {
            foreach (IShape g in Graphics)
            {
                g.Scale(scaling);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="distanceX"></param>
        /// <param name="distanceY"></param>
        public void Offset(int distanceX, int distanceY)
        {
            foreach (IShape g in Graphics)
            {
                g.Offset(distanceX, distanceY);
            }
        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
