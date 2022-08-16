using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class YearPicker : UserControl
    {
        public delegate void YearPickedEventHandler(YearPicker picker, YearPickedEventArgs e);
        /// <summary>
        /// 年份选择事件
        /// </summary>
        public YearPickedEventHandler YearPicked;

        [Browsable(true)]
        [Description("起始年")]
        public int StartYear { get { return startYear; } set { ChangeStartYear(value); startYear = value; } }
        private int startYear;

        [Browsable(true)]
        [Description("显示在表格中心的年份")]
        public int MiddleYear { get { return startYear+4; } set { StartYear = value-4; } }

        public int SelectedNumber { get; private set; }

        private SimpleButton[] btns = new SimpleButton[9];


        public YearPicker()
        {
            InitializeComponent();

            for (int i=0;i<btns.Length;i++)
            {
                SimpleButton btn = new SimpleButton() {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(0, 122, 204)
                };
                btn.Click += (s,e) => {
                    int year = int.Parse(btn.Text);
                    SelectedNumber = year;
                    YearPicked?.Invoke(this, new YearPickedEventArgs() { Year = year }) ;
                };
                panel.Controls.Add(btn);
                btn.Margin = new Padding(0);
                btns[i] = btn;
            }

            // 设置初始年(默认年)
            SelectedNumber = DateTime.Now.Year;
            MiddleYear = DateTime.Now.Year;
            
            // 设置按钮事件
            TitleBar.LeftButtonClick += (s, e) => StartYear -= 9;
            TitleBar.RightButtonClick += (s, e) => StartYear += 9;
        }

        private void ChangeStartYear(int year)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].Text = year + i + "";
            }
            TitleBar.Text = $"{year} - {year+8}";
        }
    }

    /// <summary>
    /// 年份选择事件参数
    /// </summary>
    public class YearPickedEventArgs : EventArgs
    {
        public int Year { get; set; }
    }
}
