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
            if (view != null)
            {
                view.Dock = DockStyle.Fill;
                panelView.Controls.Clear();
                panelView.Controls.Add(view);
            }
        }

        public void AddButton(Control button)
        {
            flpOptions.Controls.Add(button);
        }

        public Control FindButtonByText(string text)
        {
            foreach(Control c in flpOptions.Controls)
            {
                if(c.Text == text)
                {
                    return c;
                }
            }
            return null;
        }

        public void ClearButtons()
        {
            flpOptions.Controls.Clear();
        }
    }
}
