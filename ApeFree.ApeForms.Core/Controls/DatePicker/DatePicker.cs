using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public delegate void DatePickedEventHandler(DatePicker picker, DatePickedEventArgs e);
    public partial class DatePicker : UserControl
    {
        /// <summary>
        /// 日期选择事件
        /// </summary>
        [Browsable(true)]
        public event DatePickedEventHandler DatePicked;

        private int year;
        private int month = 1;
        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get => selectedDate; set
            {
                selectedDate = value;
                Year = value.Year;
                Month = value.Month;
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                yearPicker.MiddleYear = year;
                monthPicker.Year = year;
                daysPicker.Year = year;
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                month = value;
                daysPicker.Month = month;
            }
        }

        public DatePicker()
        {
            InitializeComponent();

            slide.AddPage(yearPicker);
            slide.AddPage(monthPicker);
            slide.AddPage(daysPicker);

            monthPicker.TitleBar.TitleBarClick += (s, e) =>
            {
                slide.Jump(0);
            };
            daysPicker.TitleBar.TitleBarClick += (s, e) =>
            {
                slide.Jump(1);
            };

            yearPicker.YearPicked += (s, e) =>
            {
                monthPicker.Year = ((YearPickedEventArgs)e).Year;
                slide.Jump(1);
            };
            monthPicker.MonthPicked += (s, e) =>
            {
                daysPicker.Year = e.Year;
                daysPicker.Month = e.Month;
                slide.Jump(2);
            };
            daysPicker.DayPicked += (s, e) =>
            {
                SelectedDate = e.Date;
                DatePicked?.Invoke(this, new DatePickedEventArgs() { Date = e.Date });
            };

        }
    }

    /// <summary>
    /// 日期选择事件参数
    /// </summary>
    public class DatePickedEventArgs : EventArgs
    {
        public DateTime Date { get; set; }
    }
}
