using ApeFree.ApeForms.Core.Gdi;
using ApeFree.Cake2D;
using ApeFree.Cake2D.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Forms
{
    public partial class FramelessForm : CanvasForm
    {
        public const int ButtonSize = 32;
        public static Color ButtonBackColor = SystemColors.Highlight;
        public static Color ButtonForeColor = Color.White;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// GDI+绘图画板
        /// </summary>
        private Point? _MouseMoveOffsetPoint;
        private Color reminderColor = ButtonBackColor;
        private readonly Layer<GdiStyle, RectangleShape> borderLayer;
        private readonly Layer<GdiStyle, TextShape> textLayer;
        private readonly Layer<GdiStyle, LineShape> lineLayer;
        private readonly Layer<GdiStyle, RectangleShape> closeButtonLayer;
        private readonly Layer<GdiStyle, LineShape> closeCrossLine1;
        private readonly Layer<GdiStyle, LineShape> closeCrossLine2;
        private readonly Layer<GdiStyle, RectangleShape> maximizeButtonLayer;
        private readonly Layer<GdiStyle, RectangleShape> minimizeButtonLayer;

        private readonly GdiStyle closeButtonLineStyle = new GdiStyle() { Pen = new Pen(Color.White) };

        private Layer<GdiStyle, RectangleShape>[] Buttons => new Layer<GdiStyle, RectangleShape>[] { closeButtonLayer, maximizeButtonLayer, minimizeButtonLayer };
        private RectangleShape[] ButtonShapes => Buttons.Select(b => b.Shape).ToArray();


        public Font TitleFont { get => textLayer.Style.Font; set => textLayer.Style.Font = value; }
        public Color TitleColor { get => textLayer.Style.Pen.Color; set => textLayer.Style.Pen.Color = value; }

        public Color ReminderColor
        {
            get => reminderColor;
            set
            {
                reminderColor = value;

                lineLayer.Style.Pen.Color = value;
                closeButtonLayer.Style.Brush.Dispose();
                closeButtonLayer.Style.Brush = new SolidBrush(value);
            }
        }


        public float ReminderLineWidth { get => lineLayer.Style.Pen.Width; set => lineLayer.Style.Pen.Width = value; }

        public FramelessForm()
        {
            InitializeComponent();

            borderLayer = Palette.DrawRectangle(new GdiStyle() { Pen = new Pen(Color.DarkGray) }, new RectangleShape(new PointF(), Width - 1, Height - 1));
            textLayer = Palette.DrawText(new GdiStyle() { Font = new Font(Font.Name,12f,FontStyle.Bold) }, new TextShape(new PointF(10, 10), 9999, 9999, Text));
            lineLayer = Palette.DrawLine(new GdiStyle() { Pen = new Pen(ButtonBackColor, 5) }, new LineShape(new Point(), 9999, 0));
            closeButtonLayer = Palette.DrawRectangle(new GdiStyle() { Brush = new SolidBrush(ButtonBackColor) }, new RectangleShape(0, 0, ButtonSize, ButtonSize));
            maximizeButtonLayer = Palette.DrawRectangle(new GdiStyle() { Brush = new SolidBrush(ButtonBackColor) }, new RectangleShape(0, 0, ButtonSize, ButtonSize));
            minimizeButtonLayer = Palette.DrawRectangle(new GdiStyle() { Brush = new SolidBrush(ButtonBackColor) }, new RectangleShape(0, 0, ButtonSize, ButtonSize));
            maximizeButtonLayer.Visible = false;
            minimizeButtonLayer.Visible = false;
            closeCrossLine1 = Palette.DrawLine(closeButtonLineStyle, new LineShape(0, 0, 0, 0));
            closeCrossLine2 = Palette.DrawLine(closeButtonLineStyle, new LineShape(0, 0, 0, 0));
            closeCrossLine1.Selectable = false;
            closeCrossLine2.Selectable = false;

            foreach (var btn in Buttons)
            {
                btn.MouseEnter += ButtonLayer_MouseEnter;
                btn.MouseLeave += ButtonLayer_MouseLeave; ;
            }

            closeButtonLayer.Click += CloseButtonLayer_Click;

            //this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            //this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            //this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void ButtonLayer_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as Layer<GdiStyle, RectangleShape>;
            btn.Style.Brush.Dispose();
            btn.Style.Brush = new SolidBrush(ReminderColor);
            lineLayer.Style.Pen.Color = ReminderColor;
            Cursor = Cursors.Default;
            RefreshPalette();
        }

        private void ButtonLayer_MouseEnter(object sender, EventArgs e)
        {
            var color = ReminderColor.Luminance(1.2f);

            var btn = sender as Layer<GdiStyle, RectangleShape>;
            btn.Style.Brush.Dispose();
            btn.Style.Brush = new SolidBrush(color);
            lineLayer.Style.Pen.Color = color;
            Cursor = Cursors.Hand;
            RefreshPalette();
        }

        private void CloseButtonLayer_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RepositionButtons();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Visible)
            {
                RepositionButtons();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            closeButtonLayer.Visible = ControlBox;
            base.OnPaint(e);
        }

        private void RepositionButtons()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                ButtonShapes[i].Left = Width - (i + 1) * ButtonSize;
            }

            borderLayer.Shape.Width = Width - 1;
            borderLayer.Shape.Height = Height - 1;

            var padding = 8;
            closeCrossLine1.Shape.StartPoint = closeButtonLayer.Shape.Points[0].Add(padding, padding);
            closeCrossLine1.Shape.EndPoint = closeButtonLayer.Shape.Points[3].Add(-padding, -padding);
            closeCrossLine2.Shape.StartPoint = closeButtonLayer.Shape.Points[1].Add(-padding, padding);
            closeCrossLine2.Shape.EndPoint = closeButtonLayer.Shape.Points[2].Add(padding, -padding);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _MouseMoveOffsetPoint = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _MouseMoveOffsetPoint = null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_MouseMoveOffsetPoint != null)
            {
                var point = MousePosition;
                point.Offset(-_MouseMoveOffsetPoint.Value.X, -_MouseMoveOffsetPoint.Value.Y);
                Location = point;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (textLayer != null)
                textLayer.Shape.Text = Text;
        }
    }
}
