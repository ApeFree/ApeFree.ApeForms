using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ApeFree.Cake2D.Elements.Billboards;
using ApeFree.Cake2D.Elements.Primitives;
using ApeFree.Cake2D.Events;
using ApeFree.Cake2D.Math;
using ApeFree.Cake2D.Rendering;
using ApeFree.Cake2D.Scene;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class GdiPaletteClockDemoPanel : UserControl
    {
        // 表盘中心点（世界坐标）
        private readonly PointF centralPoint = PointF.Empty;

        // 表盘半径
        private readonly float clockRadius = 140f;

        // 时分秒针图元
        private readonly LineElement _lineHour;
        private readonly LineElement _lineMinute;
        private readonly LineElement _lineSecond;
        private readonly PolygonElement _polygonTips;
        private readonly TextElement _textTime;
        private readonly TextElement _textHover;

        public Scene2D Scene => box.Scene;

        public GdiPaletteClockDemoPanel()
        {
            InitializeComponent();

            Scene.BackgroundColor = Color.FromArgb(245, 247, 250);
            Scene.Viewport.SetView(centralPoint, 360f);

            // 1. 表盘外圈与填充
            var dial = new CircleElement(centralPoint.X, centralPoint.Y, clockRadius)
            {
                Name = "表盘",
                StrokeColor = Color.FromArgb(30, 41, 59),
                LineWidth = 3f,
                FillColor = Color.White,
                ZIndex = 1
            };
            Scene.Add(dial);

            // 2. 绘制 60 个刻度线
            for (int i = 0; i < 60; i++)
            {
                float angle = i * 6f - 90f;
                bool isHourTick = i % 5 == 0;
                float rOuter = clockRadius - 4f;
                float rInner = isHourTick ? clockRadius - 16f : clockRadius - 10f;

                PointF pOuter = Math2D.CalculatePointOnCircle(centralPoint, rOuter, angle);
                PointF pInner = Math2D.CalculatePointOnCircle(centralPoint, rInner, angle);

                var tickLine = new LineElement(new Vector2D(pInner), new Vector2D(pOuter))
                {
                    StrokeColor = isHourTick ? Color.FromArgb(30, 41, 59) : Color.FromArgb(148, 163, 184),
                    LineWidth = isHourTick ? 2.5f : 1f,
                    IsInteractive = false,
                    ZIndex = 2
                };
                Scene.Add(tickLine);
            }

            // 3. 绘制 12 个小时数字
            for (int h = 1; h <= 12; h++)
            {
                float angle = h * 30f - 90f;
                PointF numPos = Math2D.CalculatePointOnCircle(centralPoint, clockRadius - 28f, angle);
                var numText = new TextElement(h.ToString(), numPos.X, numPos.Y)
                {
                    FontSize = 10f,
                    IsBold = true,
                    StrokeColor = Color.FromArgb(51, 65, 85),
                    Alignment = TextAlignment.MiddleCenter,
                    IsInteractive = false,
                    ZIndex = 3
                };
                Scene.Add(numText);
            }

            // 4. 指针端点连接的多边形
            _polygonTips = new PolygonElement(new PointF[3])
            {
                Name = "指针关联区域",
                FillColor = Color.FromArgb(45, 0, 122, 204),
                StrokeColor = Color.FromArgb(160, 0, 122, 204),
                LineWidth = 1.2f,
                ZIndex = 4
            };
            Scene.Add(_polygonTips);

            // 5. 时分秒针图元
            _lineHour = new LineElement(new Vector2D(centralPoint), new Vector2D(0, -60))
            {
                Name = "时针 (Hour Hand)",
                StrokeColor = Color.FromArgb(29, 78, 216),
                LineWidth = 4.5f,
                ZIndex = 6
            };

            _lineMinute = new LineElement(new Vector2D(centralPoint), new Vector2D(0, -90))
            {
                Name = "分针 (Minute Hand)",
                StrokeColor = Color.FromArgb(21, 128, 61),
                LineWidth = 3f,
                ZIndex = 7
            };

            _lineSecond = new LineElement(new Vector2D(centralPoint), new Vector2D(0, -115))
            {
                Name = "秒针 (Second Hand)",
                StrokeColor = Color.FromArgb(220, 38, 38),
                LineWidth = 1.5f,
                ZIndex = 8
            };

            // 中心固定圆销
            var centerPin = new CircleElement(centralPoint.X, centralPoint.Y, 5f)
            {
                FillColor = Color.FromArgb(220, 38, 38),
                StrokeColor = Color.FromArgb(30, 41, 59),
                LineWidth = 1f,
                IsInteractive = false,
                ZIndex = 9
            };

            // 数字时间与提示文字
            _textTime = new TextElement(DateTime.Now.ToString("HH:mm:ss"), centralPoint.X, centralPoint.Y + 45f)
            {
                FontSize = 10f,
                StrokeColor = Color.FromArgb(100, 116, 139),
                Alignment = TextAlignment.MiddleCenter,
                IsInteractive = false,
                ZIndex = 3
            };

            _textHover = new TextElement(string.Empty, centralPoint.X, -clockRadius - 15f)
            {
                FontSize = 11f,
                IsBold = true,
                StrokeColor = Color.FromArgb(37, 99, 235),
                Alignment = TextAlignment.MiddleCenter,
                IsInteractive = false,
                ZIndex = 10
            };

            Scene.Add(_lineHour);
            Scene.Add(_lineMinute);
            Scene.Add(_lineSecond);
            Scene.Add(centerPin);
            Scene.Add(_textTime);
            Scene.Add(_textHover);

            // 6. 绑定图元交互高亮与事件
            BindHandHoverEvents(_lineHour, 4.5f);
            BindHandHoverEvents(_lineMinute, 3f);
            BindHandHoverEvents(_lineSecond, 1.5f);

            _polygonTips.MouseEnter += (s, e) =>
            {
                _polygonTips.FillColor = Color.FromArgb(90, 0, 122, 204);
                _textHover.Text = "悬停于指针三角形区域";
                box.Invalidate();
            };
            _polygonTips.MouseLeave += (s, e) =>
            {
                _polygonTips.FillColor = Color.FromArgb(45, 0, 122, 204);
                _textHover.Text = string.Empty;
                box.Invalidate();
            };

            UpdateHands();
        }

        private void BindHandHoverEvents(LineElement hand, float normalWidth)
        {
            Color originalColor = hand.StrokeColor ?? Color.Black;

            hand.MouseEnter += (s, e) =>
            {
                hand.LineWidth = normalWidth + 2.5f;
                hand.StrokeColor = Color.FromArgb(234, 88, 12);
                _textHover.Text = $"当前悬停: {hand.Name}";
                Cursor = Cursors.Hand;
                box.Invalidate();
            };

            hand.MouseLeave += (s, e) =>
            {
                hand.LineWidth = normalWidth;
                hand.StrokeColor = originalColor;
                _textHover.Text = string.Empty;
                Cursor = Cursors.Default;
                box.Invalidate();
            };
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateHands();
        }

        private void UpdateHands()
        {
            var now = DateTime.Now;
            var totalSeconds = now.Hour * 3600 + now.Minute * 60 + now.Second + now.Millisecond / 1000f;

            float secondAngle = (totalSeconds % 60f) / 60f * 360f - 90f;
            float minuteAngle = (totalSeconds % 3600f) / 3600f * 360f - 90f;
            float hourAngle = (totalSeconds % 43200f) / 43200f * 360f - 90f;

            PointF pSec = Math2D.CalculatePointOnCircle(centralPoint, 115f, secondAngle);
            PointF pMin = Math2D.CalculatePointOnCircle(centralPoint, 90f, minuteAngle);
            PointF pHour = Math2D.CalculatePointOnCircle(centralPoint, 60f, hourAngle);

            _lineSecond.End = new Vector2D(pSec);
            _lineMinute.End = new Vector2D(pMin);
            _lineHour.End = new Vector2D(pHour);

            _polygonTips.Points = new List<PointF> { pSec, pMin, pHour };
            _textTime.Text = now.ToString("HH:mm:ss");

            box.Invalidate();
        }
    }
}