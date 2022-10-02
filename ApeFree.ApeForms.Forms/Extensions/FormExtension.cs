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
        /// <param name="stepValue"></param>
        /// <returns></returns>
        public static Task GraduallyShow(this Form form, double stepValue = 0.1)
        {
            form.Opacity = 0;
            form.Show();

            return Task.Run(() =>
            {
                AutoResetEvent evt = new AutoResetEvent(false);

                double targetOpacityValue = 1;

                Timers.Timer timer = new Timers.Timer(10);
                timer.Elapsed += (s, e) =>
                {
                    double value = form.Opacity + stepValue;
                    form.ModifyInUI(() =>
                    {
                        form.Opacity = value > targetOpacityValue ? targetOpacityValue : value;
                    });
                    if (form.Opacity == targetOpacityValue)
                    {
                        timer.Stop();
                        timer.Dispose();
                        evt.Set();
                    }
                };
                timer.Start();

                evt.WaitOne();
            });
        }
    }
}
