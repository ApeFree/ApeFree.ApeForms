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
    [DefaultEvent("Click")]
    public partial class Magnet : CardView
    {
        private static Magnet lastShownMagnet;

        public Image Image { get => pictureBox.Image; set => pictureBox.Image = value; }

        public PictureBoxSizeMode ImageSizeMode { get => pictureBox.SizeMode; set => pictureBox.SizeMode = value; }

        public override Color BackColor
        {
            get => base.BackColor; set
            {
                base.BackColor = value;

                if (pictureBox != null)
                {
                    pictureBox.BackColor = value;
                }
            }
        }

        public Magnet()
        {
            InitializeComponent();

            var ctrls = this.GetChildControls(true);
            foreach (var ctrl in ctrls)
            {
                ctrl.MouseMove += ChildCobtrol_MouseMove;
                ctrl.Click += ChildCobtrol_Click;
                ctrl.MouseLeave += ChildCobtrol_MouseLeave;
                ctrl.Cursor = Cursors.Hand;
            }

            pictureBox.Size = Size;
        }

        private void ChildCobtrol_MouseLeave(object sender, EventArgs e) => OnMouseLeave(e);

        private void ChildCobtrol_Click(object sender, EventArgs e) => OnClick(e);

        private void ChildCobtrol_MouseMove(object sender, MouseEventArgs e) => OnMouseMove(e);

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (pictureBox != null)
                pictureBox.Size = Size;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            // 定时判断lastShownMagnet
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (lastShownMagnet != this)
            {
                lastShownMagnet?.HidePicture(false);
                lastShownMagnet = this;
                HidePicture(true);
            }
        }

        public void HidePicture(bool hide)
        {
            pictureBox.LocationGradualChange(hide ? new Point(0, -Height) : new Point(0, 0), 3);
        }


    }
}
