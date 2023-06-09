using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    [ToolboxItem(false)]
    public class TabButton : SimpleButton
    {
        public Color? SidelineColor { get; set; }

        public int SidelineWidth { get; set; } = 5;

        public Postion SidelinePostion { get; set; } = Postion.Left;

        public Color? SelectedBackColor { get; set; }

        private bool IsDrawSideline { get; set; } = false;
        private Color BackupColor { get; set; }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            foreach (TabButton btn in Parent.Controls)
            {
                if (btn is TabButton && btn.IsDrawSideline)
                {
                    btn.IsDrawSideline = false;
                    btn.BackColor = btn.BackupColor;
                }
            }

            IsDrawSideline = true;
            BackupColor = BackColor;
            BackColor = SelectedBackColor ?? BackColor.Luminance(1.1f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!IsDrawSideline)
            {
                return;
            }

            Point p1, p2;

            switch (SidelinePostion)
            {
                case Postion.Left:
                    p1 = new Point(0, 0);
                    p2 = new Point(0, Height);
                    break;
                case Postion.Top:
                    p1 = new Point(0, 0);
                    p2 = new Point(Width, 0);
                    break;
                case Postion.Right:
                    p1 = new Point(Width, 0);
                    p2 = new Point(Width, Height);
                    break;
                case Postion.Bottom:
                    p1 = new Point(0, Height);
                    p2 = new Point(Width, Height);
                    break;
                default:
                    p1 = new Point(0, 0);
                    p2 = new Point(0, 0);
                    break;
            }
            using (var pen = new Pen(SidelineColor ?? ForeColor, SidelineWidth))
            {
                e.Graphics.DrawLine(pen, p1, p2);
            }
        }
        public enum Postion
        {
            Top,
            Left,
            Right,
            Bottom,
        }

    }
}
