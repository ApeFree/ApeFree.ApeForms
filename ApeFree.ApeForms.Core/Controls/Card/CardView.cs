using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class CardView : UserControl
    {

        [Browsable(true)]
        public string Title { get => labTitle.Text; set => labTitle.Text = value; }

        [Browsable(true)]
        public Font TitleFont { get => labTitle.Font; set => labTitle.Font = value; }

        [Browsable(true)]
        public string Content { get => labContent.Text; set => labContent.Text = value; }

        [Browsable(true)]
        public Font ContentFont { get => labContent.Font; set => labContent.Font = value; }

        [Browsable(true)]
        public Image ContentImage { get => panelContent.BackgroundImage; set => panelContent.BackgroundImage = value; }

        private Control contentView;
        public Control ContentView
        {
            get => contentView;
            set
            {
                if (contentView != null)
                {
                    panelContent.Controls.Remove(contentView);
                }
                contentView = value;
                if (value != null)
                {
                    panelContent.Controls.Add(contentView);
                    contentView.Dock = DockStyle.Fill;
                    contentView.BringToFront();
                }
            }
        }

        public CardView()
        {
            InitializeComponent();

            BackColorChanged += CardView_BackColorChanged;
            ForeColorChanged += CardView_ForeColorChanged;

            panelButtons.Visible = false;
        }

        private void CardView_ForeColorChanged(object sender, EventArgs e)
        {
            labContent.ForeColor = labTitle.ForeColor = ForeColor;
            foreach (Control c in panelButtons.Controls)
                c.ForeColor = ForeColor;
        }

        private void CardView_BackColorChanged(object sender, EventArgs e)
        {
            labContent.BackColor = labTitle.BackColor = BackColor;
            var btnBackColor = ControlPaint.Dark(BackColor, 0.02f);
            foreach (Control c in panelButtons.Controls)
                c.BackColor = btnBackColor;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            panelButtons.Height = Height / 6;

            if (panelButtons.Height > 50) panelButtons.Height = 50;
            else if (panelButtons.Height < 25) panelButtons.Height = 25;
        }

        public SimpleButton AddButton(string buttonName, Action<SimpleButton, CardView> action = null)
        {
            SimpleButton btn = new SimpleButton();
            btn.Text = buttonName;
            btn.Width = 90;
            btn.Height = panelButtons.Height-2;
            btn.ForeColor = Color.White;// ForeColor;
            btn.BackColor = Color.FromArgb(0, 122, 204);// ControlPaint.Dark(BackColor, 0.02f);

            if (action != null)
            {
                btn.Click += (s, ea) =>
                {
                    action.Invoke(btn, this);
                };
            }
            panelButtons.Controls.Add(btn);
            panelButtons.Visible = true;
            return btn;
        }
    }
}
