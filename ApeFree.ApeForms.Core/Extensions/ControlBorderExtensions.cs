using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public static class ControlBorderExtensions
    {
        private static Control _LastChangedControl;
        private static Color moveColor = Color.FromArgb(0, 122, 204);
        private static Color leaveColor = SystemColors.Control;
        private static int borderSize = 1;

        public static void BorderChangeWhenMouseMove(this Control control)
        {
            RevocationEventListen(control);
            control.MouseMove += Control_MouseMove;
            control.Resize += Control_Resize;
            control.DrawBorder(leaveColor, borderSize);
            control.ShareEventDelegate("MouseMove");
        }

        /// <summary>
        /// 撤销事件监听
        /// </summary>
        /// <param name="control"></param>
        private static void RevocationEventListen(Control control)
        {
            control.MouseMove -= Control_MouseMove;
            control.Resize -= Control_Resize;
        }

        private static void Control_Resize(object sender, EventArgs e)
        {
            Control currentControl = (Control)sender;

            // 如果控件已被销毁，撤销事件监听
            if (currentControl.IsDisposed)
            {
                RevocationEventListen(currentControl);
                return;
            }

            if (currentControl == _LastChangedControl)
            {
                currentControl.DrawBorder(moveColor, borderSize);
            }
            else
            {
                currentControl.DrawBorder(leaveColor, borderSize);
            }
        }

        private static void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control currentControl = (Control)sender;

            // 如果控件已被销毁，撤销事件监听
            if (currentControl.IsDisposed)
            {
                RevocationEventListen(currentControl);
                return;
            }

            if (currentControl == _LastChangedControl) return;
            _LastChangedControl?.DrawBorder(leaveColor, borderSize);
            currentControl.DrawBorder(moveColor, borderSize);
            _LastChangedControl = currentControl;
        }
    }
}
