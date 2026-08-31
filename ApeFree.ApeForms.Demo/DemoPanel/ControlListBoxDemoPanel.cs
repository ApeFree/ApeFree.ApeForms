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
    public partial class ControlListBoxDemoPanel : UserControl
    {
        public ControlListBoxDemoPanel()
        {
            InitializeComponent();

            RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3, radioButton4 };
            foreach (RadioButton radioButton in radioButtons)
            {
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
            }

            // 默认选择
            radioButton1.Checked = true;
            checkBox1.Checked = true;
        }



        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // 以下代码只为展示控件属性的切换效果，
            // 实际使用中只需要在设计器中设置即可
            RadioButton radioButton = sender as RadioButton;    
            if (radioButton.Checked)
            {
                var direction = (FlowDirection)Enum.Parse(typeof(FlowDirection), radioButton.Text);
                controlListBox1.Direction = direction;

                if(direction == FlowDirection.LeftToRight|| direction == FlowDirection.RightToLeft)
                {
                    controlListBox1.Height = 50;
                    controlListBox1.Dock = DockStyle.Top;
                }
                else
                {
                    controlListBox1.Height = 80;
                    controlListBox1.Dock = DockStyle.Left;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            controlListBox1.AutoScroll = checkBox1.Checked;
        }

        private void btnResetSize_Click(object sender, EventArgs e)
        {
            Size size = new Size(80, 50);
            controlListBox1.TraverseChildControls(ctrl => ctrl.Size = size);
        }
    }
}
