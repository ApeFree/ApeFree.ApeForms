using System;
using System.Drawing;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public partial class Notification
    {
        /// <summary>
        /// 通知栏之间的间隔距离
        /// </summary>
        public static int SpacingDistance { get; set; } = 10;

        /// <summary>
        /// 通知栏默认大小
        /// </summary>
        public static Size DefaultFormsSize { get; set; } = new Size(350, 150);

        /// <summary>
        /// 通知排列方向
        /// </summary>
        public static NotifyOrientation Orientation { get; set; } = NotifyOrientation.Stack;

        /// <summary>
        /// 通知起始方向
        /// </summary>
        public static NotifyPrimeDirection PrimeDirection { get; set; } = NotifyPrimeDirection.Bottom;

        /// <summary>
        /// 通知构造器
        /// </summary>
        public static NotificationBuilder Builder { get; } = new NotificationBuilder();

        /// <summary>
        /// 发布通知
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static INotificationBox Notify(NotificationSettings settings)
        {
            var form = new NotificationBox(settings.MainView, settings.SpareView);

            form.Text = settings.Title;
            form.DisappearInterval = (int)settings.RetentionTime;
            form.ReminderColor = settings.ReminderColor;

            foreach (NotificationOption o in settings.Options)
            {
                form.AddOption(o);
            }

            NotificationBox.NotifyForms.Add(form);
            return form;
        }
    }

    public class NotificationBuilder
    {
        internal NotificationBuilder() { }

        public INotificationBox ShowNotification<T>(Action<T> settingsHandler, Action<INotificationBox> notifyHandler = null) where T : NotificationSettings
        {
            var settings = Activator.CreateInstance<T>();
            settingsHandler.Invoke(settings);

            var box = Notification.Notify(settings);
            notifyHandler?.Invoke(box);

            return box;
        }

        public INotificationBox ShowTextNotification(Action<TextNotificationSettings> settingsHandler, Action<INotificationBox> notifyHandler = null)
            => ShowNotification(settingsHandler, notifyHandler);

        public INotificationBox ShowImageTextNotification(Action<ImageTextNotificationSettings> settingsHandler, Action<INotificationBox> notifyHandler = null)
            => ShowNotification(settingsHandler, notifyHandler);
    }
}
