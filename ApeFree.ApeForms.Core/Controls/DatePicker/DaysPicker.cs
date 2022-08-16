using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class DaysPicker : UserControl
    {
        public delegate void DayPickedEventHandler(DaysPicker picker, DayPickedEventArgs e);
        /// <summary>
        /// 日期选择事件
        /// </summary>
        public DayPickedEventHandler DayPicked;


        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;
        public int Year
        {
            get { return year; }
            set
            {
                if (value > 0 && value < 9999)
                {
                    year = value;
                    RefreshDaysTable(value, month);
                }
            }
        }
        public int Month
        {
            get { return month; }
            set
            {
                if (value > 0 && value <= 12)
                {
                    month = value;
                    RefreshDaysTable(year, value);
                }
            }
        }

        public int SelectedNumber { get; private set; }
        public DateTime SelectedDate { get; private set; }

        private SimpleButton[] btns = new SimpleButton[42];
        public DaysPicker()
        {
            InitializeComponent();

            for (int i = 0; i < btns.Length; i++)
            {
                SimpleButton btn = new SimpleButton()
                {
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    BackColor = Color.White,
                    ForeColor = ((i + 1) % 7 > 1) ? Color.FromArgb(0, 122, 204) : Color.FromArgb(255, 122, 0),
                    Text = string.Empty,
                };
                btn.Click += (s, e) =>
                {
                    // 选项如果无文字则无效
                    if (string.IsNullOrWhiteSpace(btn.Text)) return;

                    int day = int.Parse(btn.Text);
                    SelectedNumber = day;
                    DayPicked?.Invoke(this, new DayPickedEventArgs() { Date = new DateTime(year, month, day) });
                };
                panel.Controls.Add(btn);
                btn.Margin = new Padding(0);
                btns[i] = btn;
            }

            // 设置按钮事件
            TitleBar.LeftButtonClick += (s, e) =>
            {
                if (Month > 1)
                {
                    Month--;
                }
                else
                {
                    year--;
                    Month = 12;
                }
            };
            TitleBar.RightButtonClick += (s, e) =>
            {
                if (Month < 12)
                {
                    Month++;
                }
                else
                {
                    year++;
                    Month = 1;
                }
            };

            DateTime now = DateTime.Now;
            year = now.Year;
            month = now.Month;
            RefreshDaysTable(year, month);
        }

        private void RefreshDaysTable(int year, int month)
        {
            panel.SuspendLayout();

            DateTime dateTime = new DateTime(year, month, 1);
            int fristDayOfWeek = (int)dateTime.DayOfWeek;

            for (int i = 0; i < btns.Length; i++)
            {
                // btns[i].Enabled = false;
                btns[i].Text = string.Empty;
            }

            for (int i = (int)dateTime.DayOfWeek; i < btns.Length; i++)
            {
                if (i >= fristDayOfWeek)
                {
                    int dayOfMonth = i - fristDayOfWeek + 1;
                    btns[i].Text = dayOfMonth.ToString();
                    btns[i].Enabled = true; ;
                    dateTime = dateTime.AddDays(1);
                    if (dateTime.Month != month)
                        break;
                }
            }
            TitleBar.Text = $"{year}-{month}";
            panel.ResumeLayout(false);
        }
    }

    /// <summary>
    /// 日期选择事件参数
    /// </summary>
    public class DayPickedEventArgs : EventArgs
    {
        public DateTime Date { get; set; }
    }
}
