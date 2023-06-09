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
        public Color MouseMoveForeColor { get; set; } = Color.White;
        public Color MouseMoveBackColor { get; set; } = Color.FromArgb(82, 176, 239);
        public Color MouseLeaveForeColor { get; set; } = Color.White;
        public Color MouseLeaveBackColor { get; set; } = Color.FromArgb(0, 122, 204);
        public Color MouseDownForeColor { get; set; } = Color.White;
        public Color MouseDownBackColor { get; set; } = Color.FromArgb(14, 97, 152);
        public Color LostFocusForeColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Color LostFocusBackColor { get; set; } = Color.FromArgb(251, 251, 251);
        public Color GotFocusForeColor { get; set; } = Color.White;
        public Color GotFocusBackColor { get; set; } = Color.FromArgb(0, 122, 204);
    }
}
