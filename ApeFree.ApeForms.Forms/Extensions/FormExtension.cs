using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public static Task GraduallyShow(this Form form, double stepValue = 0.02)
        {
            return Task.Run(() =>
            {
                Form.ActiveForm.ModifyInUI(() =>
                {
                    form.Opacity = 0;
                    form.Enabled = false;
                    form.Show();
                    while (!GradualTransparencyOnce(form, stepValue, 1))
                    {
                        // Task.Delay(100);
                        Thread.Sleep(10);
                    }
                    form.Enabled = true;
                });
            });
        }

        private static bool GradualTransparencyOnce(this Form form, double stepValue, double target = 1)
        {
            double value = form.Opacity + stepValue;
            form.Opacity = value > target ? target : value;
            return form.Opacity == target;
        }
    }
}
