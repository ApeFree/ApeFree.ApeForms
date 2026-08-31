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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class ToastDemoPanel : UserControl
    {
        private int toastCount = 0;
        public ToastDemoPanel()
        {
            InitializeComponent();

            comboBoxLocation.Items.AddRange(Enum.GetNames(typeof(ToastLocation)));
            comboBoxLocation.SelectedIndex = 0;
        }

        private void btnShowToast_Click(object sender, EventArgs e)
        {
            var rbtn = new RadioButton[] { tbtnToastMode0, tbtnToastMode1, tbtnToastMode2 }.FirstOrDefault(r => r.Checked);
            ToastMode mode = (ToastMode)Enum.Parse(typeof(ToastMode), rbtn.Text.Split(' ')[0]);

            Toast.Show($"{tbToastContent.Text} - {++toastCount}", trackBarToastDelay.Value * 500, tbToastContent, mode);
            labToastCount.Text = toastCount + "";
        }

        private void btnToastBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AllowFullOpen = false;
            dialog.ShowHelp = true;
            dialog.Color = Toast.BackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnToastBackColor.BackColor = Toast.BackColor = dialog.Color;
            }
        }

        private void btnToastForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AllowFullOpen = false;
            dialog.ShowHelp = true;
            dialog.Color = Toast.BackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnToastForeColor.ForeColor = Toast.ForeColor = dialog.Color;
            }
        }

        private void btnToastFont_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.ShowColor = true;
            dialog.Font = Toast.Font;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnToastFont.Font = Toast.Font = dialog.Font;
            }
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Toast.Location = (ToastLocation)comboBoxLocation.SelectedIndex;
        }
    }
}
