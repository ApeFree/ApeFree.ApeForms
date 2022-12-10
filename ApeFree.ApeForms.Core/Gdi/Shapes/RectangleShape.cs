using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace ApeFree.ApeForms.Core.Gdi.Shapes
{
    /// <summary>
    /// 矩形形状
    /// </summary>
    public class RectangleShape : IShape
    {
        public IEnumerable<PointF> Points => new PointF[] { Location };

        public void Rotate(PointF centralPoint, float angle)
        {
            throw new NotImplementedException();
        }

        public void Scale(float scaling)
        {
            throw new NotImplementedException();
        }

        public static readonly RectangleShape Empty;

        private float x;

        private float y;

        private float width;

        private float height;

        [Browsable(false)]
        public PointF Location
        {
            get
            {
                return new PointF(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        [Browsable(false)]
        public SizeF Size
        {
            get
            {
                return new SizeF(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        [Browsable(false)]
        public float Left => X;

        [Browsable(false)]
        public float Top => Y;

        [Browsable(false)]
        public float Right => X + Width;

        [Browsable(false)]
        public float Bottom => Y + Height;

        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (!(Width <= 0f))
                {
                    return Height <= 0f;
                }

                return true;
            }
        }

        public RectangleShape(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public RectangleShape(PointF location, SizeF size)
        {
            x = location.X;
            y = location.Y;
            width = size.Width;
            height = size.Height;
        }

        public static RectangleShape FromLTRB(float left, float top, float right, float bottom)
        {
            return new RectangleShape(left, top, right - left, bottom - top);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RectangleShape))
            {
                return false;
            }

            RectangleShape rectangleF = (RectangleShape)obj;
            if (rectangleF.X == X && rectangleF.Y == Y && rectangleF.Width == Width)
            {
                return rectangleF.Height == Height;
            }

            return false;
        }

        public static bool operator ==(RectangleShape left, RectangleShape right)
        {
            if (left.X == right.X && left.Y == right.Y && left.Width == right.Width)
            {
                return left.Height == right.Height;
            }

            return false;
        }

        public static bool operator !=(RectangleShape left, RectangleShape right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public bool Contains(float x, float y)
        {
            if (X <= x && x < X + Width && Y <= y)
            {
                return y < Y + Height;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Contains(PointF pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(RectangleShape rect)
        {
            if (X <= rect.X && rect.X + rect.Width <= X + Width && Y <= rect.Y)
            {
                return rect.Y + rect.Height <= Y + Height;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (int)((uint)X ^ (((uint)Y << 13) | ((uint)Y >> 19)) ^ (((uint)Width << 26) | ((uint)Width >> 6)) ^ (((uint)Height << 7) | ((uint)Height >> 25)));
        }

        public void Inflate(float x, float y)
        {
            X -= x;
            Y -= y;
            Width += 2f * x;
            Height += 2f * y;
        }

        public void Inflate(SizeF size)
        {
            Inflate(size.Width, size.Height);
        }

        public static RectangleShape Inflate(RectangleShape rect, float x, float y)
        {
            RectangleShape result = rect;
            result.Inflate(x, y);
            return result;
        }

        public void Intersect(RectangleShape rect)
        {
            RectangleShape rectangleF = Intersect(rect, this);
            X = rectangleF.X;
            Y = rectangleF.Y;
            Width = rectangleF.Width;
            Height = rectangleF.Height;
        }

        public static RectangleShape Intersect(RectangleShape a, RectangleShape b)
        {
            float num = Math.Max(a.X, b.X);
            float num2 = Math.Min(a.X + a.Width, b.X + b.Width);
            float num3 = Math.Max(a.Y, b.Y);
            float num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if (num2 >= num && num4 >= num3)
            {
                return new RectangleShape(num, num3, num2 - num, num4 - num3);
            }

            return Empty;
        }

        public bool IntersectsWith(RectangleShape rect)
        {
            if (rect.X < X + Width && X < rect.X + rect.Width && rect.Y < Y + Height)
            {
                return Y < rect.Y + rect.Height;
            }

            return false;
        }

        public static RectangleShape Union(RectangleShape a, RectangleShape b)
        {
            float num = Math.Min(a.X, b.X);
            float num2 = Math.Max(a.X + a.Width, b.X + b.Width);
            float num3 = Math.Min(a.Y, b.Y);
            float num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new RectangleShape(num, num3, num2 - num, num4 - num3);
        }

        public void Offset(PointF pos)
        {
            Offset(pos.X, pos.Y);
        }

        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        public static implicit operator RectangleShape(Rectangle r)
        {
            return new RectangleShape(r.X, r.Y, r.Width, r.Height);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) + ",Width=" + Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + Height.ToString(CultureInfo.CurrentCulture) + "}";
        }
    }
}
