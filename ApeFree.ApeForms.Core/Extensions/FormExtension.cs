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
        /// <param name="gradientValue"></param>
        /// <returns></returns>
        public static Task GraduallyShow(this Form form, double gradientValue = 0.1)
        {
            form.Opacity = 0;

            // Show方法可能存在重写
            var mi = form.GetType().GetMethods().FirstOrDefault(m => m.Name == "Show" && !m.GetParameters().Any());
            if (mi != null)
            {
                mi.Invoke(form, null);
            }
            else
            {
            form.Show();

            return Task.Run(() =>
            {
                AutoResetEvent evt = new AutoResetEvent(false);

                double targetOpacityValue = 1;
                bool hasError = false;

                Timers.Timer timer = new Timers.Timer(10);
                timer.Elapsed += (s, e) =>
                {
                    timer.Stop();

                    double value = form.Opacity + gradientValue;

                    try
                    {
                        form.Invoke(() =>
                        {
                            form.Opacity = value > targetOpacityValue ? targetOpacityValue : value;
                        });
                    }
                    catch (Exception ex)
                    {
                        hasError = true;
                    }

                    if (form.Opacity == targetOpacityValue || hasError)
                    {
                        timer.Dispose();
                        evt.Set();
                    }
                    else
                    {
                        timer.Start();
                    }
                };
                timer.Start();

                evt.WaitOne();
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
