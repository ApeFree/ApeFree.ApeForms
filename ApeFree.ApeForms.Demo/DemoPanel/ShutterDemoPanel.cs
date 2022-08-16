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
    public partial class ShutterDemoPanel : UserControl
    {
        public ShutterDemoPanel()
        {
            InitializeComponent();

            simpleButtonShutter1.AddChildButton("Child1");
            simpleButtonShutter1.AddChildButton("Child2");
            simpleButtonShutter1.AddChildButton("Child3");
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    shutter1.OpenState = !shutter1.OpenState;
        //}
    }
}
