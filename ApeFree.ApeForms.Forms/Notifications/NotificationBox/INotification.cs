using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Notifications
{
    public interface INotification
    {
        Control ContentView { get; }
        Control SpareView { get; }

        void Active();
        Control AddOption(string text, NotificationBox.OptionClickEventHandler onClick);
        void Disappear();
    }
}
