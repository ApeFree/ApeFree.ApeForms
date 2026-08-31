using ApeFree.ApeForms.Forms.Notifications;

namespace System.Windows.Forms
{
    public static class ToastExtensions
    {
        /// <summary>
        /// 弹出显示Toast消息框
        /// </summary>
        /// <param name="control"></param>
        /// <param name="content">内容</param>
        /// <param name="mode">显示模式</param>
        /// <param name="delay">延时(毫秒)</param>
        public static void ShowToast(this Control control, string content, ToastMode mode, int delay = 2000)
        {
            control.ModifyInUI(() =>
            {
                Toast.Show(content, delay, control, mode);
            });
        }
    }
}
