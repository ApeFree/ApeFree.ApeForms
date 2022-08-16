using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    public static class ControlUIExtensions
    {
        /// <summary>
        /// 将控件四角设置为圆角
        /// 若应用在尺寸会变化的控件上(如窗体)，应重写OnResize方法或添加Resize事件，使其每次尺寸变化都能重新绘制界面
        /// </summary>
        /// <param name="control"></param>
        /// <param name="diameter">圆角直径</param>
        public static Drawing.Rectangle Fillet(this Control control, int diameter)
        {
            Drawing.Rectangle rect = new Drawing.Rectangle(0, 0, control.Width, control.Height);
            control.Region = new Drawing.Region(GetFilletGraphics(rect, diameter));

            return rect;
        }

        /// <summary>
        /// 将控件四角设置为圆角
        /// 若应用在尺寸会变化的控件上(如窗体)，应重写OnResize方法或添加Resize事件，使其每次尺寸变化都能重新绘制界面
        /// 圆角的默认直径为:长宽中较小值的1/4
        /// </summary>
        /// <param name="control"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static Drawing.Rectangle Fillet(this Control control, double rate = 0.25)
        {
            if (rate > 1 || rate < 0)
            {
                throw new ArgumentException("参数[rate]取值范围：0 <= rate <= 1");
            }

            int r = (int)(Math.Min(control.Width, control.Height) * rate);
            return Fillet(control, r);
        }

        public static void Ellipse(this Control control)
        {
            if (control == null || control.IsDisposed) return;

            GraphicsPath path = new GraphicsPath();//创建一条线
            path.AddEllipse(0, 0, control.Width, control.Height);//画一个椭圆 （x,y,宽,高）
            Graphics g = control.CreateGraphics();//为窗体创建画布

            // 抗锯齿
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.SmoothingMode = SmoothingMode.HighQuality;
            //g.CompositingQuality = CompositingQuality.HighQuality;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawEllipse(new Pen(Color.Black, 2), 1, 1, control.Width - 2, control.Height - 2);//为画布画一个椭圆(笔,x,y，宽,高)
            control.Region = new Region(path);//设置控件的窗口区域
        }

        private static GraphicsPath GetFilletGraphics(Rectangle rect, int diameter)
        {
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath gp = new GraphicsPath();
            try
            {
                gp.AddArc(arcRect, 180, 90);
                arcRect.X = rect.Right - diameter;
                gp.AddArc(arcRect, 270, 90);
                arcRect.Y = rect.Bottom - diameter;
                gp.AddArc(arcRect, 0, 90);
                arcRect.X = rect.Left;
                gp.AddArc(arcRect, 90, 90);
                gp.CloseFigure();
            }
            catch (Exception) { }

            return gp;
        }


        public static void DrawBorder(this Control control, Color color, int size)
        {
            if (control == null || control.IsDisposed) return;

            ControlPaint.DrawBorder(control.CreateGraphics(),
                            new Rectangle(0, 0, control.Width, control.Height),
                            color,
                            size,
                            ButtonBorderStyle.Solid,
                            color,
                            size,
                            ButtonBorderStyle.Solid,
                            color,
                            size,
                            ButtonBorderStyle.Solid,
                            color,
                            size,
                            ButtonBorderStyle.Solid);
        }
    }
}
