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
    public partial class StageLayoutDemoPanel : UserControl
    {
        private int count = 0;
        private Color[] colors = new Color[] { Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen,
        Color.DarkOrange,Color.DarkOrchid,Color.DarkRed,Color.DarkSalmon,Color.DarkSeaGreen,Color.DarkSlateBlue,Color.DarkSlateGray,};
        public StageLayoutDemoPanel()
        {
            InitializeComponent();
            movableStageLayoutPanel.MovingRateLevel = (byte)nudRate.Value;
        }

        private void btnAddTestControl_Click(object sender, EventArgs e)
        {
            count++;

            var btn1 = new Button()
            {
                Text = $"View {count}",
                ForeColor = Color.White,
                BackColor = colors[count % colors.Length],
            };
            btn1.Click += (s, ea) => stageLayoutPanel.SelectMainControl(btn1);
            stageLayoutPanel.AddControl(btn1);

            var btn2 = new Button()
            {
                Text = $"View {count}",
                ForeColor = Color.White,
                BackColor = colors[count % colors.Length],
            };
            btn2.Click += (s, ea) => movableStageLayoutPanel.SelectMainControl(btn2);
            movableStageLayoutPanel.AddControl(btn2);

        }

        private void cbSort_CheckedChanged(object sender, EventArgs e)
        {
            stageLayoutPanel.GridItemSortingHandler = cbSort.Checked ? GridItemSortingHandler : (Func<Control, object>)null;
            movableStageLayoutPanel.GridItemSortingHandler = cbSort.Checked ? GridItemSortingHandler : (Func<Control, object>)null;
        }

        private object GridItemSortingHandler(Control control)
        {
            // 取按钮上的序号作为排序依据
            return int.Parse(control.Text.Split(' ').Last());
        }

        private void nudRate_ValueChanged(object sender, EventArgs e)
        {
            movableStageLayoutPanel.MovingRateLevel = (byte)nudRate.Value;
        }
    }
}
