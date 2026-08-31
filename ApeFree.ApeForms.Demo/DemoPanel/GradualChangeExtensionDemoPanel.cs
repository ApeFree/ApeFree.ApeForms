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

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class GradualChangeExtensionDemoPanel : UserControl
    {
        // 运动速率
        private const int rate = 10;

        public GradualChangeExtensionDemoPanel()
        {
            InitializeComponent();
        }

        private void btnTopLeft_Click(object sender, EventArgs e)
        {
            pic.LocationGradualChange(new Point(0, 0), rate, _ => Toast.Show($"已到达位置[{(sender as Control).Text}]", mode: ToastMode.Reuse));
        }

        private void btnTopRight_Click(object sender, EventArgs e)
        {
            var containerSize = panelTest.Size;
            var posX = containerSize.Width - pic.Width;
            pic.LocationGradualChange(new Point(posX, 0), rate, _ => Toast.Show($"已到达位置[{(sender as Control).Text}]", mode: ToastMode.Reuse));
        }

        private void btnBottomLeft_Click(object sender, EventArgs e)
        {
            var containerSize = panelTest.Size;
            var posY = containerSize.Height - pic.Height;
            pic.LocationGradualChange(new Point(0, posY), rate, _ => Toast.Show($"已到达位置[{(sender as Control).Text}]", mode: ToastMode.Reuse));
        }

        private void btnBottomRight_Click(object sender, EventArgs e)
        {
            var containerSize = panelTest.Size;
            var posX = containerSize.Width - pic.Width;
            var posY = containerSize.Height - pic.Height;
            pic.LocationGradualChange(new Point(posX, posY), rate, _ => Toast.Show($"已到达位置[{(sender as Control).Text}]", mode: ToastMode.Reuse));
        }

        private void btnSize50_Click(object sender, EventArgs e)
        {
            pic.SizeGradualChange(new Size(50, 50), rate);
        }

        private void btnSize100_Click(object sender, EventArgs e)
        {
            pic.SizeGradualChange(new Size(100, 100), rate);
        }

        private void btnSize150_Click(object sender, EventArgs e)
        {
            pic.SizeGradualChange(new Size(150, 150), rate);

        }

        private void btnOpacity100_Click(object sender, EventArgs e)
        {
            FindForm().OpacityGradualChange(1.0, rate);
        }

        private void btnOpacity50_Click(object sender, EventArgs e)
        {
            FindForm().OpacityGradualChange(0.5, rate);
        }

        private void btnOpacity10_Click(object sender, EventArgs e)
        {
            FindForm().OpacityGradualChange(0.1, rate);
        }
    }
}
