using System.Threading;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class ShakeFormExtension
    {
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
