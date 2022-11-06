using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Core.Gdi.Graphics
{
    /// <summary>
    /// 复合图形
    /// </summary>
    public class ComplexGraphic : IGraphic
    {
        public ComplexGraphic()
        {
            Graphics = new LinkedList<IGraphic>();
        }

        public LinkedList<IGraphic> Graphics { get; }

        public IEnumerable<PointF> Points => Graphics.SelectMany(g => g.Points);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="centralPoint"></param>
        /// <param name="angle"></param>
        public void Rotate(PointF centralPoint, float angle)
        {
            foreach (IGraphic g in Graphics)
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
            foreach (IGraphic g in Graphics)
            {
                g.Scale(scaling);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="distanceX"></param>
        /// <param name="distanceY"></param>
        public void Translation(float distanceX, float distanceY)
        {
            foreach (IGraphic g in Graphics)
            {
                g.Translation(distanceX, distanceY);
            }
        }
    }
}
