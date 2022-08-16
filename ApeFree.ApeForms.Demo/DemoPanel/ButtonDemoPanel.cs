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
    public partial class ButtonDemoPanel : UserControl
    {
        public ButtonDemoPanel()
        {
            InitializeComponent();

            // 按比例切圆角
            imageButton1.Fillet(0.5);

            // 切至圆形
            imageButton2.Ellipse();
        }

        private void btnReplaceImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PNG透明图片 (*.png)|*.png";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var path = openFileDialog.FileName;
                    Image image = Image.FromFile(path);

                    groupBox3.BackgroundImage = image;
                    imageButton1.BackgroundImage = image;
                    imageButton2.BackgroundImage = image;
                    imageButton3.BackgroundImage = image;
                }
            }
        }
    }
}
