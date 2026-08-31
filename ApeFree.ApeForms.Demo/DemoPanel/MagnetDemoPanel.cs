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
    public partial class MagnetDemoPanel : UserControl
    {
        public MagnetDemoPanel()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            groupBox2.Height = table1.Width / table1.ColumnCount * table1.RowCount + groupBox2.Height - table1.Height;
            groupBox3.Height = table2.Width / table2.ColumnCount * table2.RowCount + groupBox3.Height - table2.Height;
        }
    }

}
