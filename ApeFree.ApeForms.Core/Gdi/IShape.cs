using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>图形接口</summary>
    public interface IShape
    {
        /// <summary>缩放</summary>
        /// <param name="scaling">缩放比例</param>
        void Scale(float scaling);

        /// <summary>平移</summary>
        /// <param name="distanceX">X轴平移距离</param>
        /// <param name="distanceY">Y轴平移距离</param>
        void Offset(float distanceX, float distanceY);

        /// <summary>旋转</summary>
        /// <param name="centralPoint">中心点</param>
        /// <param name="angle">旋转角度</param>
        void Rotate(PointF centralPoint, float angle);

        /// <summary>指定点是否在图形内部</summary>
        /// <param name="point"></param>
        bool Contains(PointF point);

        /// <summary>图形上所有的点</summary>
        IEnumerable<PointF> Points { get; }

        /// <summary>获取外接矩形</summary>
        RectangleShape GetBounds();
    }

    /// <summary>平面图形接口</summary>
    public interface IPlaneShape : IShape
    {
        /// <summary>计算周长</summary>
        double CalculatePerimeter();

        /// <summary>计算面积</summary>
        double CalculateArea();
    }

    /// <summary>多边形接口</summary>
    public interface IPolygon : IPlaneShape
    {
        /// <summary>多边形的重心</summary>
        PointF Centroid { get; }
    }

    /// <summary>多边形</summary>
    public class PolygonShape : IPolygon
    {
        protected List<PointF> _points = new List<PointF>(); // 存储多边形的各个顶点坐标

        public PolygonShape(IEnumerable<PointF> points)
        {
            _points.AddRange(points);
        }

        public virtual void Scale(float scaling)
        {
            // 缩放多边形的各个顶点坐标
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new Point((int)(_points[i].X * scaling), (int)(_points[i].Y * scaling));
            }
        }

        public virtual void Offset(float distanceX, float distanceY)
        {
            // 平移多边形的各个顶点坐标
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new PointF(_points[i].X + distanceX, _points[i].Y + distanceY);
            }
        }

        public virtual void Rotate(PointF centralPoint, float angle)
        {
            // 将多边形的各个顶点坐标绕中心点旋转指定角度
            for (int i = 0; i < _points.Count; i++)
            {
                float x = ((float)((_points[i].X - centralPoint.X) * Math.Cos(angle) - (_points[i].Y - centralPoint.Y) * Math.Sin(angle) + centralPoint.X));
                float y = ((float)((_points[i].X - centralPoint.X) * Math.Sin(angle) + (_points[i].Y - centralPoint.Y) * Math.Cos(angle) + centralPoint.Y));
                _points[i] = new PointF(x, y);
            }
        }

        public virtual bool Contains(PointF point)
        {
            // 判断指定点是否在多边形内部
            bool result = false;
            int j = _points.Count - 1;
            for (int i = 0; i < _points.Count; i++)
            {
                if ((_points[i].Y < point.Y && _points[j].Y >= point.Y || _points[j].Y < point.Y && _points[i].Y >= point.Y)
                    && (_points[i].X + (point.Y - _points[i].Y) / (_points[j].Y - _points[i].Y) * (_points[j].X - _points[i].X) < point.X))
                {
                    result = !result;
                }
                j = i;
            }
            return result;
        }

        public IEnumerable<PointF> Points
        {
            get { return _points; }
        }

        public virtual RectangleShape GetBounds()
        {
            // 获取多边形的外接矩形
            float left = _points.Min(p => p.X);
            float top = _points.Min(p => p.Y);
            float right = _points.Max(p => p.X);
            float bottom = _points.Max(p => p.Y);
            return new RectangleShape(left, top, right - left, bottom - top);
        }

        public virtual double CalculatePerimeter()
        {
            // 计算多边形的周长
            double perimeter = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                int j = (i + 1) % _points.Count;
                perimeter += Math.Sqrt(Math.Pow(_points[j].X - _points[i].X, 2) + Math.Pow(_points[j].Y - _points[i].Y, 2));
            }
            return perimeter;
        }

        public virtual double CalculateArea()
        {
            // 计算多边形的面积
            double area = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                int j = (i + 1) % _points.Count;
                area += _points[i].X * _points[j].Y - _points[j].X * _points[i].Y;
            }
            return Math.Abs(area / 2);
        }

        public virtual PointF Centroid
        {
            get
            {
                // 计算多边形的重心
                double cx = 0;
                double cy = 0;
                double area = 0;
                for (int i = 0; i < _points.Count; i++)
                {
                    int j = (i + 1) % _points.Count;
                    double temp = _points[i].X * _points[j].Y - _points[j].X * _points[i].Y;
                    area += temp;
                    cx += (double)(_points[i].X + _points[j].X) * temp;
                    cy += (double)(_points[i].Y + _points[j].Y) * temp;
                }
                area /= 2;
                cx /= 6 * area;
                cy /= 6 * area;
                return new Point((int)cx, (int)cy);
            }
        }
    }

    /// <summary>矩形接口</summary>
    public interface IRectangle
    {
        /// <summary>矩形宽度</summary>
        float Width { get; set; }
        /// <summary>矩形高度</summary>
        float Height { get; set; }
        /// <summary>旋转角度</summary>
        float Angle { get; set; }
    }

    public class RectangleShape : PolygonShape, IRectangle
    {
        private float _width;
        private float _height;
        private float _angle;

        public RectangleShape(PointF location, float width, float height) : base(new[] { location, new PointF(location.X + width, location.Y), new PointF(location.X + width, location.Y + height), new PointF(location.X, location.Y + height) })
        {
            _width = width;
            _height = height;
        }

        public RectangleShape(float left, float top, float width, float height) : this(new PointF(left, top), width, height) { }

        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                UpdatePoints();
            }
        }

        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                UpdatePoints();
            }
        }

        public float Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                UpdatePoints();
            }
        }

        private void UpdatePoints()
        {
            // 更新矩形的各个顶点坐标
            PointF center = new PointF(Left + Width / 2, Top + Height / 2);
            double sin = Math.Sin(Angle);
            double cos = Math.Cos(Angle);
            PointF[] points = new PointF[4];
            points[0] = new PointF(center.X - (int)(0.5 * Width * cos + 0.5 * Height * sin), center.Y - (int)(0.5 * Height * cos - 0.5 * Width * sin));
            points[1] = new PointF(center.X + (int)(0.5 * Width * cos - 0.5 * Height * sin), center.Y - (int)(0.5 * Height * cos + 0.5 * Width * sin));
            points[2] = new PointF(center.X + (int)(0.5 * Width * cos + 0.5 * Height * sin), center.Y + (int)(0.5 * Height * cos - 0.5 * Width * sin));
            points[3] = new PointF(center.X - (int)(0.5 * Width * cos - 0.5 * Height * sin), center.Y + (int)(0.5 * Height * cos + 0.5 * Width * sin));
            base._points.Clear();
            base._points.AddRange(points);
        }

        public float Left
        {
            get { return base.GetBounds().Left; }
            set
            {
                float delta = value - Left;
                Offset(delta, 0);
            }
        }

        public float Top
        {
            get { return base.GetBounds().Top; }
            set
            {
                float delta = value - Top;
                Offset(0, delta);
            }
        }
    }

    /// <summary>椭圆形接口</summary>
    public interface IEllipse : IRectangle
    {
        /// <summary>圆心</summary>
        PointF CenterPoint { get; }
    }

    public class EllipseShape : RectangleShape, IEllipse
    {
        public EllipseShape(PointF centerPoint, float width, float height) : base(centerPoint.X - width / 2, centerPoint.Y - height / 2, width, height) { }

        public EllipseShape(float left, float top, float width, float height) : base(left, top, width, height) { }

        public PointF CenterPoint => Centroid;
    }

    /// <summary>正圆形接口</summary>
    public interface ICircle
    {
        /// <summary>圆心</summary>
        PointF CenterPoint { get; }

        /// <summary>半径</summary>
        float Radius { get; set; }
    }

    public class CircleShape : ICircle, IPlaneShape
    {
        private PointF centerPoint;
        private float radius;

        public CircleShape(PointF centerPoint, float radius)
        {
            this.centerPoint = centerPoint;
            this.radius = radius;
        }

        public PointF CenterPoint => centerPoint;

        public float Radius
        {
            get => radius;
            set => radius = value;
        }

        public double CalculateArea()
        {
            return Math.PI * radius * radius;
        }

        public double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }

        public bool Contains(PointF point)
        {
            return (point.X - centerPoint.X) * (point.X - centerPoint.X) +
                   (point.Y - centerPoint.Y) * (point.Y - centerPoint.Y) <= radius * radius;
        }

        public RectangleShape GetBounds()
        {
            return new RectangleShape((int)(centerPoint.X - radius), (int)(centerPoint.Y - radius),
                                 (int)(2 * radius), (int)(2 * radius));
        }

        public void Offset(float distanceX, float distanceY)
        {
            centerPoint = new PointF(centerPoint.X + distanceX, centerPoint.Y + distanceY);
        }

        public PointF Centroid => centerPoint;

        public IEnumerable<PointF> Points
        {
            get
            {
                yield return centerPoint;
            }
        }

        public void Rotate(PointF centralPoint, float angle)
        {
            centerPoint = GdiMath.PointAround(centralPoint, centerPoint, angle);
        }

        public void Scale(float scaling)
        {
            radius *= scaling;
        }
    }

    public class VectorSahpe : IShape
    {
        public PointF StartPoint { get; set; }

        public float Length { get; set; }

        public float Angle { get; set; }

        public PointF EndPoint { get; }

        public IEnumerable<PointF> Points => new PointF[] { StartPoint, EndPoint };

        public bool Contains(PointF point)
        {
            throw new NotImplementedException();
        }

        public RectangleShape GetBounds()
        {
            throw new NotImplementedException();
        }

        public void Offset(float distanceX, float distanceY)
        {
            throw new NotImplementedException();
        }

        public void Rotate(PointF centralPoint, float angle)
        {
            throw new NotImplementedException();
        }

        public void Scale(float scaling)
        {
            throw new NotImplementedException();
        }
    }

    public class LineShape : IShape
    {
        public PointF StartPoint { get; set; }

        public PointF EndPoint { get; set; }

        private PointF CentrePoint => new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);

        public IEnumerable<PointF> Points => new PointF[] { StartPoint, EndPoint };


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
            // 计算中心点
            PointF center = CentrePoint;

            // 计算起点和中心点的距离
            float startDistance = (float)GdiMath.CalculateLengthFromTwoPoints(StartPoint, center);

            // 根据缩放比例计算新的距离
            float newStartDistance = startDistance * scaling;

            // 计算新的起点和终点
            float angle = (float)GdiMath.CalculateAngleFromTwoPoints(center, StartPoint);

            StartPoint = GdiMath.CalculatePointOnCircle(new PointF(CentrePoint.X, CentrePoint.Y), newStartDistance, angle);
            EndPoint = GdiMath.CalculatePointOnCircle(new PointF(CentrePoint.X, CentrePoint.Y), newStartDistance, 360 - angle);
        }

        public void Offset(float distanceX, float distanceY)
        {
            StartPoint = new PointF(StartPoint.X + distanceX, StartPoint.Y + distanceY);
            EndPoint = new PointF(EndPoint.X + distanceX, EndPoint.Y + distanceY);
        }

        public bool Contains(PointF p)
        {
            if (StartPoint == p || EndPoint == p)
            {
                return true;
            }

            // 判断点是否在直线上
            float a = EndPoint.Y - StartPoint.Y;
            float b = StartPoint.X - EndPoint.X;
            float c = EndPoint.X * StartPoint.Y - StartPoint.X * EndPoint.Y;
            return a * p.X + b * p.Y + c == 0;
        }

        public RectangleShape GetBounds()
        {
            return new RectangleShape(StartPoint, (int)(EndPoint.X - StartPoint.X), (int)(EndPoint.Y - StartPoint.Y));
        }

        /// <summary>
        /// 构造线图形
        /// </summary>
        /// <param name="x1">起始点X坐标</param>
        /// <param name="y1">起始点Y坐标</param>
        /// <param name="x2">结束点X坐标</param>
        /// <param name="y2">结束点Y坐标</param>
        public LineShape(int x1, int y1, int x2, int y2) : this(new Point(x1, y1), new Point(x2, y2)) { }

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
        public LineShape(Point startPoint, double length, float angle)
        {
            StartPoint = startPoint;
            EndPoint = GdiMath.CalculatePointOnCircle(startPoint, (float)length, angle);
        }
    }
}
