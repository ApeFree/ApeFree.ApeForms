using ApeFree.ApeForms.Core.Gdi;
using ApeFree.ApeForms.Core.Gdi.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class GdiPaletteDemoPanel : UserControl
    {
        // 表盘中心点
        private PointF centralPoint = new PointF(200, 200);
        // 表盘半径
        private int clockRadius = 150;

        // 画板
        private readonly GdiPalette palette;
        // 时分秒针图层
        private readonly Layer<GdiPalette.ShapeStyle> layerHour;
        private readonly Layer<GdiPalette.ShapeStyle> layerMinute;
        private readonly Layer<GdiPalette.ShapeStyle> layerSecond;

        /// <summary>
        /// 画布控件
        /// </summary>
        private Control Canvas => gbCanvas;

        public GdiPaletteDemoPanel()
        {
            InitializeComponent();

            // 为当做画布的控件设置重绘事件
            Canvas.Paint += GbCanvas_Paint;

            // 创建GDI+画板
            palette = new GdiPalette();

            // 绘制圆形表盘和刻度
            palette.Draw(new GdiPalette.ShapeStyle() { Pen = new Pen(Color.Black, 3) }, new EllipseShape(centralPoint.X - clockRadius, centralPoint.Y - clockRadius, clockRadius * 2, clockRadius * 2));
            for (int i = 0; i < 60; i++)
            {
                palette.Draw(new GdiPalette.ShapeStyle() { Pen = new Pen(Color.DarkGray, 2) }, new LineShape(GdiMath.CalculatePointOnCircle(centralPoint, clockRadius - 10, i * 6), GdiMath.CalculatePointOnCircle(centralPoint, clockRadius - (i % 5 == 0 ? 25 : 15), i * 6)));
            }

            // 绘制时针分针秒针，并将图层存到全局
            layerSecond = palette.Draw(new GdiPalette.ShapeStyle() { Pen = new Pen(Color.DarkRed, 1) }, new LineShape(centralPoint, clockRadius - 10, -90));
            layerMinute = palette.Draw(new GdiPalette.ShapeStyle() { Pen = new Pen(Color.DarkGreen, 3) }, new LineShape(centralPoint, clockRadius - 30, -90));
            layerHour = palette.Draw(new GdiPalette.ShapeStyle() { Pen = new Pen(Color.DarkBlue, 5) }, new LineShape(centralPoint, clockRadius - 50, -90));
        }

        private void GbCanvas_Paint(object sender, PaintEventArgs e)
        {
            // 创建空白图
            Bitmap bmp = new Bitmap(Canvas.Width, Canvas.Height);
            Graphics graph = Graphics.FromImage(bmp);
            graph.Clear(Canvas.BackColor);

            // 将画板内容绘制到空白图
            palette.Canvas = graph;
            palette.Refresh();

            // 将图绘制到控件
            e.Graphics.DrawImage(bmp, Canvas.ClientRectangle);
            e.Graphics.Dispose();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            // 获取当前时间，并计算当前是今日的第几秒
            var now = DateTime.Now;
            var totalSeconds = now.Hour * 3600 + now.Minute * 60 + now.Second;

            // 根据当前时间计算时针分针秒针的指向
            (layerSecond.Shape as LineShape).With(shape =>
            {
                shape.EndPoint = GdiMath.CalculatePointOnCircle(shape.StartPoint, (float)shape.Length, totalSeconds % 60 / 60f * 360 - 90);
            });
            (layerMinute.Shape as LineShape).With(shape =>
            {
                shape.EndPoint = GdiMath.CalculatePointOnCircle(shape.StartPoint, (float)shape.Length, totalSeconds % 3600 / 3600f * 360 - 90);
            });
            (layerHour.Shape as LineShape).With(shape =>
            {
                shape.EndPoint = GdiMath.CalculatePointOnCircle(shape.StartPoint, (float)shape.Length, totalSeconds / 86400f * 360 - 90);
            });

            // 刷新画布控件
            Canvas.Refresh();
        }
    }
}
