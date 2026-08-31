using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApeFree.ApeForms.Core.Extensions;
using ApeFree.Cake2D.Elements.Billboards;
using ApeFree.Cake2D.Elements.Primitives;
using ApeFree.Cake2D.Events;
using ApeFree.Cake2D.Gdi;
using ApeFree.Cake2D.Math;
using ApeFree.Cake2D.Rendering;
using ApeFree.Cake2D.Scene;

namespace ApeFree.ApeForms.Core.Forms
{
    public partial class FramelessForm : Form
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

        private Point? _MouseMoveOffsetPoint;
        private Color _reminderColor = ButtonBackColor;

        private readonly Scene2D _scene = new Scene2D();
        private readonly Engine2D _engine = new Engine2D();

        private readonly RectangleElement _borderElement;
        private readonly TextElement _textElement;
        private readonly LineElement _lineElement;
        private readonly RectangleElement _closeButtonElement;
        private readonly LineElement _closeCrossLine1;
        private readonly LineElement _closeCrossLine2;
        private readonly RectangleElement _maximizeButtonElement;
        private readonly RectangleElement _minimizeButtonElement;

        private RectangleElement[] Buttons => new[] { _closeButtonElement, _maximizeButtonElement, _minimizeButtonElement };

        public Font TitleFont
        {
            get => new Font(_textElement.FontName, _textElement.FontSize, _textElement.IsBold ? FontStyle.Bold : FontStyle.Regular);
            set
            {
                if (value != null)
                {
                    _textElement.FontName = value.Name;
                    _textElement.FontSize = value.SizeInPoints;
                    _textElement.IsBold = value.Bold;
                    Invalidate();
                }
            }
        }

        public Color TitleColor
        {
            get => _textElement.StrokeColor ?? Color.Black;
            set
            {
                _textElement.StrokeColor = value;
                Invalidate();
            }
        }

        public Color ReminderColor
        {
            get => _reminderColor;
            set
            {
                _reminderColor = value;
                _lineElement.StrokeColor = value;
                _closeButtonElement.FillColor = value;
                Invalidate();
            }
        }

        public float ReminderLineWidth
        {
            get => _lineElement.LineWidth;
            set
            {
                _lineElement.LineWidth = value;
                Invalidate();
            }
        }

        public FramelessForm()
        {
            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            InitializeComponent();

            _borderElement = new RectangleElement(0, 0, Width - 1, Height - 1)
            {
                StrokeColor = Color.DarkGray,
                FillColor = null,
                LineWidth = 1f,
                IsInteractive = false,
                ZIndex = 1
            };

            _textElement = new TextElement(Text, 10, 8)
            {
                StrokeColor = Color.Black,
                FontName = Font.Name,
                FontSize = 12f,
                IsBold = true,
                Alignment = TextAlignment.TopLeft,
                IsInteractive = false,
                ZIndex = 2
            };

            _lineElement = new LineElement(0, 0, 9999, 0)
            {
                StrokeColor = ButtonBackColor,
                LineWidth = 5f,
                IsInteractive = false,
                ZIndex = 2
            };

            _closeButtonElement = new RectangleElement(0, 0, ButtonSize, ButtonSize)
            {
                FillColor = ButtonBackColor,
                StrokeColor = null,
                ZIndex = 3
            };

            _maximizeButtonElement = new RectangleElement(0, 0, ButtonSize, ButtonSize)
            {
                FillColor = ButtonBackColor,
                StrokeColor = null,
                IsVisible = false,
                ZIndex = 3
            };

            _minimizeButtonElement = new RectangleElement(0, 0, ButtonSize, ButtonSize)
            {
                FillColor = ButtonBackColor,
                StrokeColor = null,
                IsVisible = false,
                ZIndex = 3
            };

            _closeCrossLine1 = new LineElement(0, 0, 0, 0)
            {
                StrokeColor = Color.White,
                LineWidth = 1.5f,
                IsInteractive = false,
                ZIndex = 4
            };

            _closeCrossLine2 = new LineElement(0, 0, 0, 0)
            {
                StrokeColor = Color.White,
                LineWidth = 1.5f,
                IsInteractive = false,
                ZIndex = 4
            };

            _scene.Add(_borderElement);
            _scene.Add(_textElement);
            _scene.Add(_lineElement);
            _scene.Add(_closeButtonElement);
            _scene.Add(_maximizeButtonElement);
            _scene.Add(_minimizeButtonElement);
            _scene.Add(_closeCrossLine1);
            _scene.Add(_closeCrossLine2);

            foreach (var btn in Buttons)
            {
                btn.MouseEnter += ButtonElement_MouseEnter;
                btn.MouseLeave += ButtonElement_MouseLeave;
            }

            _closeButtonElement.Click += (s, e) => Close();
            _scene.InvalidateRequested += (s, e) => Invalidate();
        }

        private void ButtonElement_MouseEnter(object sender, ElementMouseEventArgs e)
        {
            if (sender is RectangleElement btn)
            {
                var color = ReminderColor.Luminance(1.2f);
                btn.FillColor = color;
                _lineElement.StrokeColor = color;
                Cursor = Cursors.Hand;
                Invalidate();
            }
        }

        private void ButtonElement_MouseLeave(object sender, ElementMouseEventArgs e)
        {
            if (sender is RectangleElement btn)
            {
                btn.FillColor = ReminderColor;
                _lineElement.StrokeColor = ReminderColor;
                Cursor = Cursors.Default;
                Invalidate();
            }
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
            _closeButtonElement.IsVisible = ControlBox;
            base.OnPaint(e);

            _scene.Viewport.Resize(Width, Height);
            _scene.Viewport.SetView(new PointF(Width / 2f, Height / 2f), Width);

            GdiRenderCanvas canvas = new GdiRenderCanvas(e.Graphics);
            _engine.Render(_scene, canvas);
        }

        private void RepositionButtons()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Position = new Vector2D(Width - (i + 1) * ButtonSize, 0);
            }

            _borderElement.Width = Width - 1;
            _borderElement.Height = Height - 1;

            float padding = 10f;
            float btnLeft = _closeButtonElement.Position.X;
            float btnTop = _closeButtonElement.Position.Y;

            _closeCrossLine1.Start = new Vector2D(btnLeft + padding, btnTop + padding);
            _closeCrossLine1.End = new Vector2D(btnLeft + ButtonSize - padding, btnTop + ButtonSize - padding);

            _closeCrossLine2.Start = new Vector2D(btnLeft + ButtonSize - padding, btnTop + padding);
            _closeCrossLine2.End = new Vector2D(btnLeft + padding, btnTop + ButtonSize - padding);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            PointF screenLoc = e.Location;
            PointF worldLoc = _scene.Viewport.ScreenToWorld(screenLoc);
            CakeMouseButton btn = CakeMouseButton.None;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) btn |= CakeMouseButton.Left;
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right) btn |= CakeMouseButton.Right;
            if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle) btn |= CakeMouseButton.Middle;

            ElementMouseEventArgs args = new ElementMouseEventArgs(screenLoc, worldLoc, btn, e.Clicks, e.Delta);
            bool handled = _scene.InteractionManager.OnMouseDown(args);

            if (!handled && e.Button == MouseButtons.Left && e.Y <= 32)
            {
                _MouseMoveOffsetPoint = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            PointF screenLoc = e.Location;
            PointF worldLoc = _scene.Viewport.ScreenToWorld(screenLoc);
            CakeMouseButton btn = CakeMouseButton.None;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) btn |= CakeMouseButton.Left;
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right) btn |= CakeMouseButton.Right;
            if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle) btn |= CakeMouseButton.Middle;

            ElementMouseEventArgs args = new ElementMouseEventArgs(screenLoc, worldLoc, btn, e.Clicks, e.Delta);
            bool handled = _scene.InteractionManager.OnMouseMove(args);

            if (!handled && _MouseMoveOffsetPoint != null)
            {
                var point = MousePosition;
                point.Offset(-_MouseMoveOffsetPoint.Value.X, -_MouseMoveOffsetPoint.Value.Y);
                Location = point;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            PointF screenLoc = e.Location;
            PointF worldLoc = _scene.Viewport.ScreenToWorld(screenLoc);
            CakeMouseButton btn = CakeMouseButton.None;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) btn |= CakeMouseButton.Left;
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right) btn |= CakeMouseButton.Right;
            if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle) btn |= CakeMouseButton.Middle;

            ElementMouseEventArgs args = new ElementMouseEventArgs(screenLoc, worldLoc, btn, e.Clicks, e.Delta);
            _scene.InteractionManager.OnMouseUp(args);

            _MouseMoveOffsetPoint = null;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (_textElement != null)
            {
                _textElement.Text = Text;
                Invalidate();
            }
        }
    }
}