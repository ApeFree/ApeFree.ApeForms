using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class MonthPicker : UserControl
    {
        public delegate void MonthPickedEventHandler(MonthPicker picker, MonthPickedEventArgs e);
        /// <summary>
        /// 月份选择事件
        /// </summary>
        public MonthPickedEventHandler MonthPicked;

        public int SelectedNumber { get; private set; }

        private SimpleButton[] btns = new SimpleButton[12];

        public override string Text { get { return TitleBar.Text; } set { TitleBar.Text = value; } }


        private int year = DateTime.Now.Year;
        public int Year
        {
            get { return year; }
            set
            {
                if (value > 0 && value < 9999)
                {
                    year = value; Text = year.ToString();
                }
            }
        }

        public MonthPicker()
        {

            InitializeComponent();
            Text = "";

            for (int i = 0; i < btns.Length; i++)
            {
                SimpleButton btn = new SimpleButton()
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(0, 122, 204),
                    Text = (i + 1).ToString()
                };
                btn.Click += (s, e) =>
                {
                    int month = int.Parse(btn.Text);
                    SelectedNumber = month;
                    MonthPicked?.Invoke(this, new MonthPickedEventArgs() { Year = year, Month = month });
                };
                panel.Controls.Add(btn);
                btn.Margin = new Padding(0);
                btns[i] = btn;
            }

            // 设置按钮事件
            TitleBar.LeftButtonClick += (s, e) =>
            {
                Year = year - 1;
            };
            TitleBar.RightButtonClick += (s, e) =>
            {
                Year = year + 1;
            };
        }
    }

    /// <summary>
    /// 月份选择事件参数
    /// </summary>
    public class MonthPickedEventArgs : EventArgs
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
