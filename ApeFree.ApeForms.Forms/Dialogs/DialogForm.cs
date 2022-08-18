using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public partial class DialogForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

        public DialogForm(string content, int delay = 2000)
        {
            InitializeComponent();
        }
    }
}
