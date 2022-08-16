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
    public partial class SlideBoxDemoPanel : UserControl
    {
        private readonly Color[] colors = new Color[] { Color.IndianRed, Color.DarkGreen, Color.DarkBlue, Color.DarkOrange, Color.MediumPurple, Color.DarkGray };

        public SlideBoxDemoPanel()
        {
            InitializeComponent();

            AddTestPage(5);

            trackBar.Value = slideBox.Rate;
        }

        private void AddTestPage(int pageCount)
        {
            int i = 0;
            while (i++ < pageCount)
            {
                var index = slideBox.PageCount;

                Label label = new Label()
                {
                    Text = $"Page {index + 1}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(Font.Name, 12, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = colors[(index % colors.Length)],
                };
                slideBox.AddPage(label);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            slideBox.PreviousPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            slideBox.NextPage();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            slideBox.Jump(0);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            slideBox.Jump(slideBox.PageCount - 1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTestPage(1);
            slideBox.Jump(slideBox.PageCount - 1);
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            slideBox.Rate = trackBar.Value;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            slideBox.Clear();
        }
    }
}
