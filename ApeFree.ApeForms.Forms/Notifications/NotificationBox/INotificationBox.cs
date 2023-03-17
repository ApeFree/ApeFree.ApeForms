using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public interface INotificationBox
    {
        Control MainView { get; }
        Control SpareView { get; }
        Color ReminderColor { get; set; }

        Control AddOption(NotificationOption option);

        void Disappear();
    }
}
