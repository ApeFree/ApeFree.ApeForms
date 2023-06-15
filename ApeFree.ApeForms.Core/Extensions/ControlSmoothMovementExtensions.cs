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

        /// <summary>
        /// 尺寸渐进改变
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="targetSize"></param>
        /// <param name="rate"></param>
        /// <param name="finishCallback"></param>
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

        /// <summary>
        /// 位置渐进改变
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="targetPoint"></param>
        /// <param name="rate"></param>
        /// <param name="finishCallback"></param>
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

        /// <summary>
        /// 垂直滚动条渐进滑动
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="childControl"></param>
        /// <param name="offset"></param>
        /// <param name="rate"></param>
        /// <param name="finishCallback"></param>
        public static void VerticalScrollGradualChange<T>(this T control, Control childControl,int offset = 0, byte rate = 5, Action<T> finishCallback = null) where T : ScrollableControl
        {
            lock (control)
            {
                
                SharedTimedTaskManager.Manager.AddTask(new TimedTaskItem(control, TimedTaskTag.VerticalScrollGradualChange,
                    () =>
                    {
                        var targetY = childControl.Top - control.AutoScrollPosition.Y;
                        control.ModifyInUI(() =>
                        {
                            control.VerticalScroll.Value = Gradual(control.VerticalScroll.Value, targetY, rate);
                            control.Invalidate();
                        });
                    },
                    () =>
                    {
                        var targetY = childControl.Top - control.AutoScrollPosition.Y;
                        return control.VerticalScroll.Value == targetY;
                    }, finishCallback == null ? null : (sender => finishCallback.Invoke((T)sender))));
            }
        }

        /// <summary>
        /// 透明度渐进改变
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <param name="targetOpacity"></param>
        /// <param name="rate"></param>
        /// <param name="finishCallback"></param>
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
