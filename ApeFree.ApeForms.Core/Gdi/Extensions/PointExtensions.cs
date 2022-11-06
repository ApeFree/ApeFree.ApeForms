namespace System.Drawing
{
    public static class PointExtensions
    {
        public static PointF Add(this PointF point, float x, float y)
        {
            return new PointF(point.X + x, point.Y + y);
        }

        public static PointF Subtract(this PointF point, float x, float y)
        {
            return new PointF(point.X - x, point.Y - y);
        }

        public static PointF Multiply(this PointF point, float multiplier)
        {
            return new PointF(point.X * multiplier, point.Y * multiplier);
        }

        public static PointF Divide(this PointF point, float dividend)
        {
            return new PointF(point.X / dividend, point.Y / dividend);
        }


    }
}
