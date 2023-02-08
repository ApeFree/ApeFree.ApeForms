using ApeFree.ApeForms.Demo.Properties;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class NotificationBoxDemoPanel : UserControl
    {
        public NotificationBoxDemoPanel()
        {
            InitializeComponent();

            if (NotificationBox.Orientation == NotifyOrientation.Queue)
            {
                rbtnOrientationQueue.Checked = true;
            }

            if (NotificationBox.PrimeDirection == NotifyPrimeDirection.Top)
            {
                rbtnPrimeDirectionTop.Checked = true;
            }

            nudDefaultWidth.Value = NotificationBox.DefaultFormsSize.Width;
            nudDefaultHeight.Value = NotificationBox.DefaultFormsSize.Height;
            nudSpacingDistance.Value = NotificationBox.SpacingDistance;

        }



        private void rbtnOrientation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOrientationQueue.Checked)
            {
                NotificationBox.Orientation = NotifyOrientation.Queue;

            }
            else if (rbtnOrientationStack.Checked)
            {
                NotificationBox.Orientation = NotifyOrientation.Stack;
            }
        }

        private void rbtnPrimeDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPrimeDirectionTop.Checked)
            {
                NotificationBox.PrimeDirection = NotifyPrimeDirection.Top;

            }
            else if (rbtnPrimeDirectionBottom.Checked)
            {
                NotificationBox.PrimeDirection = NotifyPrimeDirection.Bottom;
            }
        }

        private void btnChangeDefaultSzie_Click(object sender, EventArgs e)
        {
            NotificationBox.DefaultFormsSize = new Size((int)nudDefaultWidth.Value, (int)nudDefaultHeight.Value);
            NotificationBox.SpacingDistance = (int)nudSpacingDistance.Value;
        }

        private void btnPublishText_Click(object sender, EventArgs e)
        {
            NotificationBox.Publish(tbNotificationTitle.Text, "Notification(Text)", (uint)nudDisappearDelay.Value);
        }

        private void btnPublishImageText_Click(object sender, EventArgs e)
        {
            var notify = NotificationBox.Publish(tbNotificationTitle.Text, "Notification(Image + Text)", Resources.Magnet_12, (uint)nudDisappearDelay.Value);
            notify.AddOption("关闭通知栏", (s, args) =>
            {
                Toast.Show("此按钮会关闭通知栏");
            });

            notify.AddOption("不关闭通知栏", (s, args) =>
            {
                args.IsDisappear = false;
                Toast.Show("修改事件参数的IsDisappear属性，通知栏将不会被关闭");
            });
        }

        private void btnPublishOptionDemo_Click(object sender, EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Minimized;

            var notify = NotificationBox.Publish("ApeForms", "Demo窗体已被最小化到开始栏，可通过下方按键还原窗体。", Resources.ImageButton_1, 10000);
            notify.AddOption("窗口最大化", (s, args) =>
            {
                this.FindForm().WindowState = FormWindowState.Normal;
            });
        }
    }
}
