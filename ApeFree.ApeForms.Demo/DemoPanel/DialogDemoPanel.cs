using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notification;
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
    public partial class DialogDemoPanel : UserControl
    {
        ApeFormsDialogProvider provider = new ApeFormsDialogProvider();

        public DialogDemoPanel()
        {
            InitializeComponent();
        }

        private void btnMessageDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateMessageDialog(new MessageDialogSettings() { Title = tbTitle.Text,Content = tbContent.Text}, null);
            dialog.Show();
        }

        private void btnInputDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateInputDialog(new InputDialogSettings() { Title = tbTitle.Text, Content = tbContent.Text }, null);
            dialog.Show();
            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入内容为：{dialog.Result.Data}");
            }
        }

        private void btnInputMultiLineDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateInputDialog(new InputDialogSettings() { Title = tbTitle.Text, Content = tbContent.Text,IsMultiline = true }, null);
            dialog.Show();
            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入内容为：{dialog.Result.Data}");
            }
        }
    }
}
