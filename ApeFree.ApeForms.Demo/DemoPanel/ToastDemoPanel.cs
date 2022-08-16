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
    public partial class ToastDemoPanel : UserControl
    {
        private int toastCount = 0;
        public ToastDemoPanel()
        {
            InitializeComponent();
        }

        private void btnShowToast_Click(object sender, EventArgs e)
        {
            var rbtn = new RadioButton[] { tbtnToastMode0, tbtnToastMode1, tbtnToastMode2 }.FirstOrDefault(r => r.Checked);
            ToastMode mode = (ToastMode)Enum.Parse(typeof(ToastMode), rbtn.Text.Split(' ')[0]);

            Toast.Show($"{tbToastContent.Text} - {++toastCount}", trackBarToastDelay.Value * 500, tbToastContent, mode);
            labToastCount.Text = toastCount + "";
        }
    }
}
