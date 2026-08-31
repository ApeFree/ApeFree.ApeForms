using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ApeFree.Cake2D.Elements.Billboards;
using ApeFree.Cake2D.Elements.Containers;
using ApeFree.Cake2D.Elements.Primitives;
using ApeFree.Cake2D.Events;
using ApeFree.Cake2D.Gdi.Controls;
using ApeFree.Cake2D.Math;
using ApeFree.Cake2D.Rendering;
using ApeFree.Cake2D.Scene;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class GdiPaletteRadarChartDemoPanel : UserControl
    {
        public GdiPaletteRadarChartDemoPanel()
        {
            InitializeComponent();

            var chart = new RadarChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            groupBox1.Controls.Add(chart);
            chart.BringToFront();

            chart.ChartAreas.Add(new RadarChartAreas { Name = "输出 (ATK)", MaxValue = 100, MinValue = 0 });
            chart.ChartAreas.Add(new RadarChartAreas { Name = "防御 (DEF)", MaxValue = 50, MinValue = 0 });
            chart.ChartAreas.Add(new RadarChartAreas { Name = "血量 (HP)", MaxValue = 10000, MinValue = 0 });
            chart.ChartAreas.Add(new RadarChartAreas { Name = "攻速 (SPD)", MaxValue = 100, MinValue = 0 });
            chart.ChartAreas.Add(new RadarChartAreas { Name = "暴击 (CRT)", MaxValue = 100, MinValue = 0 });
            chart.ChartAreas.Add(new RadarChartAreas { Name = "辅助 (SUP)", MaxValue = 100, MinValue = 0 });

            chart.Series.Add(new RadarChartSeries { Name = "狂战士 (小李)", Color = Color.FromArgb(239, 68, 68), Values = new decimal[] { 100, 15, 3000, 20, 80, 45 } });
            chart.Series.Add(new RadarChartSeries { Name = "圣骑士 (小王)", Color = Color.FromArgb(59, 130, 246), Values = new decimal[] { 60, 45, 9000, 40, 30, 65 } });
            chart.Series.Add(new RadarChartSeries { Name = "暗影刺客 (老张)", Color = Color.FromArgb(16, 185, 129), Values = new decimal[] { 85, 20, 4500, 95, 90, 25 } });

            chart.RefreshChart();

            propertyGrid.SelectedObject = chart;
        }
    }

    /// <summary>
    /// 基于 Cake2D 的现代化可交互雷达图控件
    /// </summary>
    public partial class RadarChart : Cake2DControl
    {
        private const int MaxAxis = 12;
        private const int MaxRings = 10;
        private const float MaxRadius = 150f;

        private int ringCount = 5;
        private byte areaOpacity = 110;
        private Color ringColor = Color.FromArgb(203, 213, 225);
        private Color axisColor = Color.FromArgb(148, 163, 184);

        private List<RadarChartSeries> series = new List<RadarChartSeries>();
        private List<RadarChartAreas> chartAreas = new List<RadarChartAreas>();

        private readonly GroupElement _ringsGroup = new GroupElement { ZIndex = 1, IsInteractive = false };
        private readonly GroupElement _axesGroup = new GroupElement { ZIndex = 2, IsInteractive = false };
        private readonly GroupElement _labelsGroup = new GroupElement { ZIndex = 3, IsInteractive = false };
        private readonly GroupElement _seriesGroup = new GroupElement { ZIndex = 4 };
        private readonly TextElement _tooltipText;

        [Category("RadarChart")]
        [Description("同心圈数")]
        public int RingCount
        {
            get => ringCount;
            set
            {
                int val = System.Math.Max(1, System.Math.Min(value, MaxRings));
                if (ringCount != val)
                {
                    ringCount = val;
                    RefreshChart();
                }
            }
        }

        [Category("RadarChart")]
        [Description("填充区域不透明度 (0~255)")]
        public byte AreaOpacity
        {
            get => areaOpacity;
            set
            {
                areaOpacity = value;
                RefreshChart();
            }
        }

        [Category("RadarChart")]
        [Description("同心圈线颜色")]
        public Color RingColor
        {
            get => ringColor;
            set
            {
                ringColor = value;
                RefreshChart();
            }
        }

        [Category("RadarChart")]
        [Description("轴线颜色")]
        public Color AxisColor
        {
            get => axisColor;
            set
            {
                axisColor = value;
                RefreshChart();
            }
        }

        [Category("RadarChart")]
        [Description("数据系列列表")]
        public List<RadarChartSeries> Series
        {
            get => series;
            set
            {
                series = value ?? new List<RadarChartSeries>();
                RefreshChart();
            }
        }

        [Category("RadarChart")]
        [Description("维度/轴定义")]
        public List<RadarChartAreas> ChartAreas
        {
            get => chartAreas;
            set
            {
                chartAreas = value ?? new List<RadarChartAreas>();
                RefreshChart();
            }
        }

        public RadarChart()
        {
            Scene.BackgroundColor = Color.White;
            Scene.Viewport.SetView(PointF.Empty, 420f);

            Scene.Add(_ringsGroup);
            Scene.Add(_axesGroup);
            Scene.Add(_labelsGroup);
            Scene.Add(_seriesGroup);

            // 悬停提示文字
            _tooltipText = new TextElement(string.Empty, 0, -MaxRadius - 30f)
            {
                FontSize = 11f,
                IsBold = true,
                StrokeColor = Color.FromArgb(15, 23, 42),
                Alignment = TextAlignment.MiddleCenter,
                IsInteractive = false,
                ZIndex = 100
            };
            Scene.Add(_tooltipText);
        }

        public void RefreshChart()
        {
            RedrawRings();
            RedrawAxesAndLabels();
            RedrawSeries();
            Invalidate();
        }

        private void RedrawRings()
        {
            _ringsGroup.Children.Clear();
            int axisNum = ChartAreas.Count >= 3 ? ChartAreas.Count : 6;
            float interval = MaxRadius / RingCount;

            for (int i = 1; i <= RingCount; i++)
            {
                float radius = interval * i;
                PointF[] ringPts = new PointF[axisNum];
                for (int a = 0; a < axisNum; a++)
                {
                    float angle = (360f / axisNum) * a - 90f;
                    ringPts[a] = Math2D.CalculatePointOnCircle(PointF.Empty, radius, angle);
                }

                var ringPoly = new PolygonElement(ringPts)
                {
                    StrokeColor = RingColor,
                    FillColor = null,
                    LineWidth = i == RingCount ? 1.5f : 1f,
                    IsInteractive = false
                };
                _ringsGroup.AddChild(ringPoly);
            }
        }

        private void RedrawAxesAndLabels()
        {
            _axesGroup.Children.Clear();
            _labelsGroup.Children.Clear();

            int axisNum = ChartAreas.Count >= 3 ? ChartAreas.Count : 6;

            for (int a = 0; a < axisNum; a++)
            {
                float angle = (360f / axisNum) * a - 90f;
                PointF axisEnd = Math2D.CalculatePointOnCircle(PointF.Empty, MaxRadius, angle);

                var axisLine = new LineElement(Vector2D.Zero, new Vector2D(axisEnd))
                {
                    StrokeColor = AxisColor,
                    LineWidth = 1.2f,
                    IsInteractive = false
                };
                _axesGroup.AddChild(axisLine);

                string labelText = a < ChartAreas.Count ? ChartAreas[a].Name : $"轴 {a + 1}";
                PointF labelPos = Math2D.CalculatePointOnCircle(PointF.Empty, MaxRadius + 22f, angle);

                var label = new TextElement(labelText, labelPos.X, labelPos.Y)
                {
                    FontSize = 9.5f,
                    IsBold = true,
                    StrokeColor = Color.FromArgb(71, 85, 105),
                    Alignment = TextAlignment.MiddleCenter,
                    IsInteractive = false
                };
                _labelsGroup.AddChild(label);
            }
        }

        private void RedrawSeries()
        {
            _seriesGroup.Children.Clear();
            int axisNum = ChartAreas.Count >= 3 ? ChartAreas.Count : 6;

            foreach (var s in Series)
            {
                if (s.Values == null || s.Values.Length == 0) continue;

                PointF[] pts = new PointF[axisNum];
                for (int a = 0; a < axisNum; a++)
                {
                    decimal val = a < s.Values.Length ? s.Values[a] : 0;
                    decimal min = a < ChartAreas.Count ? ChartAreas[a].MinValue : 0;
                    decimal max = a < ChartAreas.Count ? ChartAreas[a].MaxValue : 100;
                    decimal span = max > min ? max - min : 1;

                    float rate = (float)((val - min) / span);
                    rate = System.Math.Max(0f, System.Math.Min(1f, rate));

                    float angle = (360f / axisNum) * a - 90f;
                    pts[a] = Math2D.CalculatePointOnCircle(PointF.Empty, MaxRadius * rate, angle);
                }

                Color baseColor = s.Color;
                var poly = new PolygonElement(pts)
                {
                    Name = s.Name,
                    StrokeColor = baseColor,
                    FillColor = Color.FromArgb(AreaOpacity, baseColor),
                    LineWidth = 2f,
                    ZIndex = 5
                };

                // 交互高亮与 Tooltip
                var capturedSeries = s;
                poly.MouseEnter += (sender, e) =>
                {
                    poly.FillColor = Color.FromArgb(System.Math.Min(255, AreaOpacity + 70), baseColor);
                    poly.LineWidth = 3.5f;
                    poly.ZIndex = 20;

                    string metrics = string.Join(" | ", capturedSeries.Values.Select((v, idx) =>
                        $"{(idx < ChartAreas.Count ? ChartAreas[idx].Name : $"D{idx + 1}")}: {v}"));
                    _tooltipText.Text = $"【{capturedSeries.Name}】 {metrics}";
                    _tooltipText.StrokeColor = baseColor;
                    Cursor = Cursors.Hand;
                    Invalidate();
                };

                poly.MouseLeave += (sender, e) =>
                {
                    poly.FillColor = Color.FromArgb(AreaOpacity, baseColor);
                    poly.LineWidth = 2f;
                    poly.ZIndex = 5;
                    _tooltipText.Text = string.Empty;
                    Cursor = Cursors.Default;
                    Invalidate();
                };

                _seriesGroup.AddChild(poly);
            }
        }
    }

    [Serializable]
    public struct RadarChartAreas
    {
        public string Name { get; set; }
        public decimal MaxValue { get; set; }
        public decimal MinValue { get; set; }
    }

    [Serializable]
    public struct RadarChartSeries
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public decimal[] Values { get; set; }
    }
}