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
        /// <returns></returns>
        public static Task GraduallyShow(this Form form, double stepSize = 0.1, double targetOpacityValue = 1)
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
            }

            return Task.Run(() =>
            {
                AutoResetEvent evt = new AutoResetEvent(false);

                bool hasError = false;

                Timers.Timer timer = new Timers.Timer(10);
                timer.Elapsed += (s, e) =>
                {
                    timer.Stop();

                    double value = form.Opacity + stepSize;

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
        /// 窗体透明度渐进关闭
        /// </summary>
        /// <param name="form"></param>
        /// <param name="stepSize">步长</param>
        /// <returns></returns>
        public static Task GraduallyClose(this Form form, double stepSize = 0.1)
        {
            var task = Task.Run(() =>
            {
                AutoResetEvent evt = new AutoResetEvent(false);

                bool hasError = false;

                Timers.Timer timer = new Timers.Timer(10);
                timer.Elapsed += (s, e) =>
                {
                    timer.Stop();

                    double value = form.Opacity - stepSize;

                    try
                    {
                        form.Invoke(() =>
                        {
                            form.Opacity = value < 0 ? 0 : value;
                        });
                    }
                    catch (Exception ex)
                    {
                        hasError = true;
                    }

                    if (form.Opacity == 0 || hasError)
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

                // Close方法可能存在重写
                form.Invoke(() =>
                {
                    var mi = form.GetType().GetMethods().FirstOrDefault(m => m.Name == "Close" && !m.GetParameters().Any());
                    if (mi != null)
                    {
                        mi.Invoke(form, null);
                    }
                    else
                    {
                        form.Close();
                    }
                });
            });
            return task;
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
