using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// 图形接口
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="scaling">缩放比例</param>
        void Scale(float scaling);

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="distanceX">X轴平移距离</param>
        /// <param name="distanceY">Y轴平移距离</param>
        void Offset(float distanceX, float distanceY);

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="centralPoint">中心点</param>
        /// <param name="angle">旋转角度</param>
        void Rotate(PointF centralPoint, float angle);

        /// <summary>
        /// 指定点是否在图形内部
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool Contains(PointF point);

        /// <summary>
        /// 当前图形上的点
        /// </summary>
        IEnumerable<PointF> Points { get; }
    }
}
