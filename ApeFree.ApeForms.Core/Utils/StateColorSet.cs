using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Core.Utils
{
    public class StateColorSet
    {
        public Color MouseMoveForegroundColor { get; set; } = Color.White;
        public Color MouseMoveBackgroundColor { get; set; } = Color.FromArgb(82, 176, 239);
        public Color MouseLeaveForegroundColor { get; set; } = Color.White;
        public Color MouseLeaveBackgroundColor { get; set; } = Color.FromArgb(0, 122, 204);
        public Color MouseDownForegroundColor { get; set; } = Color.White;
        public Color MouseDownBackgroundColor { get; set; } = Color.FromArgb(14, 97, 152);
        public Color LostFocusForegroundColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Color LostFocusBackgroundColor { get; set; } = Color.FromArgb(251, 251, 251);
        public Color GotFocusForegroundColor { get; set; } = Color.White;
        public Color GotFocusBackgroundColor { get; set; } = Color.FromArgb(0, 122, 204);
    }
}
