using ApeFree.ApeForms.Core.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static ApeFree.ApeForms.Core.Utils.SharedTimedTaskManager;

namespace System.Windows.Forms
{
    public static class ControlSmoothMovementExtensions
    {
        private const int MINIMUM_SPEED = 5;

        public static void SizeGradualChange<T>(this T control, Size targetSize, byte rate = 5, Action<T> finishCallback = null) where T : Control
        {
            SharedTimedTaskManager.Manager.AddTask(new TimedTaskItem(control, TimedTaskTag.SizeGradualChange, () =>
            {
                control.Width = Gradual(control.Width, targetSize.Width, rate);
                control.Height = Gradual(control.Height, targetSize.Height, rate);
            }, () =>
            {
                return (control.Width == targetSize.Width) && (control.Height == targetSize.Height);
            }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
        }

        public static void LocationGradualChange<T>(this T control, Point targetPoint, byte rate = 5, Action<T> finishCallback = null) where T : Control
        {
            lock (control)
            {
                SharedTimedTaskManager.Manager.AddTask(new TimedTaskItem(control, TimedTaskTag.LocationGradualChange,
                    () =>
                    {
                        var left = Gradual(control.Left, targetPoint.X, rate);
                        var top = Gradual(control.Top, targetPoint.Y, rate);
                        control.Location = new Point(left, top);
                    },
                    () =>
                    {
                        return (control.Left == targetPoint.X) && (control.Top == targetPoint.Y);
                    }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
            }
        }

        public static void OpacityGradualChange<T>(this T form, double targetOpacity, byte rate = 50, Action<T> finishCallback = null) where T : Form
        {
            lock (form)
            {
                SharedTimedTaskManager.Manager.AddTask(new TimedTaskItem(form, TimedTaskTag.OpacityGradualChange,
                    () =>
                    {
                        var no = Gradual((int)(form.Opacity * 100), (int)(targetOpacity * 100), rate);
                        form.Opacity = no / 100.0;
                    },
                    () =>
                    {
                        return form.Opacity == targetOpacity;
                    }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
            }
        }

        private static int Gradual(int currentValue, int targetValue, byte rate)
        {
            if (Math.Abs(targetValue - currentValue) <= MINIMUM_SPEED)
            {
                return targetValue;
            }

            var speed = (currentValue - targetValue) / rate;

            if (Math.Abs(speed) <= MINIMUM_SPEED)
            {
                speed = currentValue < targetValue ? -MINIMUM_SPEED : MINIMUM_SPEED;
            }

            return currentValue - speed;
        }
    }


}
