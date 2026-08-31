using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace System.Windows.Forms
{
    public static class FormExtension
    {
        /// <summary>
        /// 窗体透明度渐进显示
        /// </summary>
        /// <param name="form"></param>
        /// <param name="stepSize">步长</param>
        /// <param name="targetOpacityValue">目标值</param>
        /// <param name="finishCallback">完成回调</param>
        /// <param name="useReflectionShowMethod">是否使用反射来调用Show方法</param>
        /// <returns></returns>
        public static void GraduallyShow<T>(this T form, double stepSize = 0.1, double targetOpacityValue = 1, Action<T> finishCallback = null, bool useReflectionShowMethod = true) where T : Form
        {
            form.Opacity = 0;

            // Show方法可能存在重写，使用反射来调用Show方法可以调用到真实类型的Show方法
            var mi = form.GetType().GetMethods().FirstOrDefault(m => m.Name == "Show" && !m.GetParameters().Any());

            if (useReflectionShowMethod && mi != null)
            {
                mi.Invoke(form, null);
            }
            else
            {
                form.Show();
            }

            form.OpacityGradualChange(targetOpacityValue, (byte)(1f / stepSize), finishCallback);
        }

        /// <summary>
        /// 窗体透明度渐进关闭
        /// </summary>
        /// <param name="form"></param>
        /// <param name="stepSize">步长</param>
        /// <param name="finishCallback"></param>
        /// <returns></returns>
        public static void GraduallyClose<T>(this T form, double stepSize = 0.1, Action<T> finishCallback = null) where T : Form
        {
            form.OpacityGradualChange(0, (byte)(255 * stepSize), f =>
            {

                // Close方法可能存在重写
                var mi = form.GetType().GetMethods().FirstOrDefault(m => m.Name == "Close" && !m.GetParameters().Any());
                if (mi != null)
                {
                    mi.Invoke(form, null);
                }
                else
                {
                    form.Close();
                }

                finishCallback?.Invoke(f);

            });
        }

        /// <summary>
        /// 窗口震动
        /// </summary>
        /// <param name="form"></param>
        /// <param name="shakeTimes">震动次数</param>
        /// <param name="amplitude">震动幅度(像素)</param>
        public static void Shake(this Form form, int shakeTimes = 8, int amplitude = 20)
        {
            Task.Run(() =>
            {
                // 备份原位置
                var originalSite = form.Left;

                // 振幅减半（以原点为中心左右摇摆）
                amplitude /= 2;

                while (shakeTimes-- > 0)
                {
                    form.ModifyInUI(() =>
                    {
                        form.Left = originalSite + (shakeTimes % 2 == 0 ? amplitude : -amplitude);
                    });
                    Thread.Sleep(10);
                }
                form.ModifyInUI(() =>
                {
                    form.Left = originalSite;
                });
            });
        }
    }
}
