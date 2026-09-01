using ApeFree.ApeForms.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public partial class DialogForm : FramelessForm
    {
        public string Title { get => Text; set => Text = value; }
        public string Content { get => labContent.Text; set => labContent.Text = value; }
        public Font ContentFont
        {
            get => labContent.Font;
            set
            {
                if (value != null)
                {
                    labContent.Font = value;
                }
            }
        }

        public DialogForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var maxContentHeight = flpOptions.Top - panelContent.Top - panelView.Height;
            if (panelView.Controls.Count > 0 && maxContentHeight > Height / 3)
            {
                maxContentHeight = Height / 3;
            }
            var maxSize = new Size(panelContent.Width, Math.Max(20, maxContentHeight));
            panelContent.MaximumSize = maxSize;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.SetWindowToTopWithoutFocus();
        }

        public void SetContentView(Control view)
        {
            panelView.Controls.Clear();
            if (view != null)
            {
                view.Dock = DockStyle.Fill;
                panelView.Controls.Add(view);
                panelView.Visible = true;
            }
            else
            {
                panelView.Visible = false;
            }
        }

        public void AddButton(Control button)
        {
            flpOptions.Controls.Add(button);
        }

        public Control FindButtonByText(string text)
        {
            foreach (Control c in flpOptions.Controls)
            {
                if (c.Text == text)
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