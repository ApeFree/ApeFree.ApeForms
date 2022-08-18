using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public partial class DialogForm : Form
    {
        public string Title { get => Text; set => Text = value; }
        public string Content { get => labContent.Text; set => labContent.Text = value; }

        public DialogForm()
        {
            InitializeComponent();
        }

        public void SetContentView(Control view)
        {
            view.Dock = DockStyle.Fill;
            panelView.Controls.Clear();
            panelView.Controls.Add(view);
        }

        public void SetOptions(IEnumerable<Control> buttons)
        {
            flpOptions.Controls.Clear();
            flpOptions.Controls.AddRange(buttons.ToArray());
        }
    }
}
