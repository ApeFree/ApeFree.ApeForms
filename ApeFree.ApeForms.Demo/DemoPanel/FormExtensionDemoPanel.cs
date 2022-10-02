using ApeFree.ApeForms.Demo.Properties;
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
    public partial class FormExtensionDemoPanel : UserControl
    {
        public FormExtensionDemoPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成临时窗体
        /// </summary>
        public Func<Form> generateTempFormHandler = () => new Form()
        {
            Text = "Test Form",
            BackgroundImage = Resources.Magnet_12,
            BackgroundImageLayout = ImageLayout.Stretch,
            StartPosition = FormStartPosition.CenterScreen,
            TopMost = true,
        };

        private void btnGraduallyShowDefault_Click(object sender, EventArgs e)
        {
            Form form = generateTempFormHandler.Invoke();
            form.GraduallyShow();
        }
        private void btnGraduallyShow1_Click(object sender, EventArgs e)
        {
            Form form = generateTempFormHandler.Invoke();
            form.GraduallyShow(0.02);
        }

        private void btnGraduallyShow2_Click(object sender, EventArgs e)
        {
            Form form = generateTempFormHandler.Invoke();
            form.GraduallyShow(0.1);
        }

        private void btnShakeFormDefault_Click(object sender, EventArgs e)
        {
            FindForm().Shake();
        }

        private void btnShakeForm1_Click(object sender, EventArgs e)
        {
            FindForm().Shake(shakeTimes: 16);
        }

        private void btnShakeForm2_Click(object sender, EventArgs e)
        {
            FindForm().Shake(amplitude: 40);
        }
    }
}
