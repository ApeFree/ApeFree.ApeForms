using ApeFree.ApeForms.Core.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class NavigationTitleBar : UserControl
    {
        [Browsable(true)]
        public event EventHandler LeftButtonClick;
        [Browsable(true)]
        public event EventHandler RightButtonClick;
        [Browsable(true)]
        public event EventHandler TitleBarClick;

        public NavigationTitleBar()
        {
            InitializeComponent();

            LeftButton.Click += LeftButton_Click;
            RightButton.Click += RightButton_Click;
            TitleLabel.Click += TitleLabel_Click;

            LeftButton.Size = new System.Drawing.Size(Height, Height);
            RightButton.Size = new System.Drawing.Size(Height, Height);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            TitleLabel.Text = Text;
        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {
            TitleBarClick?.Invoke(sender, e);
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            RightButtonClick?.Invoke(sender, e);
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            LeftButtonClick?.Invoke(sender, e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (LeftButton != null && RightButton != null)
            {
                LeftButton.Width = Height;
                RightButton.Width = Height;
            }
        }
    }
}
