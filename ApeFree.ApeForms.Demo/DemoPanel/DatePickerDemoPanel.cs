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
    public partial class DatePickerDemoPanel : UserControl
    {
        public DatePickerDemoPanel()
        {
            InitializeComponent();

            var now = DateTime.Now;
            datePicker1.Year = now.Year; 
            datePicker1.Month = now.Month;
            datePicker1.DatePicked += DatePicker1_DatePicked;
        }

        private void DatePicker1_DatePicked(Core.Controls.DatePicker picker, Core.Controls.DatePickedEventArgs e)
        {
            labDate.Text = e.Date.ToShortDateString();
        }
    }
}
