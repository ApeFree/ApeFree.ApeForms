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
    public partial class SimpleCard : UserControl
    {
        [Browsable(true)]
        public Font TextFont { get => labText.Font; set => labText.Font = value; }

        [Browsable(true)]
        public Image Image { get => picImage.Image; set => picImage.Image = value; }

        [Browsable(true)]
        public new string Text { get => labText.Text; set => labText.Text = value; }

        [Browsable(true)]
        [Description("图文内容的边距")]
        public int ImageMargin { get => Padding.All; set => Padding = new Padding(value); }

        public SimpleCard()
        {
            InitializeComponent();
            labText.Click += SimpleCardClick;
            picImage.Click += SimpleCardClick;

            labText.MouseMove += SimpleCardMouseMove;
            picImage.MouseMove += SimpleCardMouseMove;
        }

        private void SimpleCardMouseMove(object sender, MouseEventArgs e) => OnMouseMove(e);
        private void SimpleCardClick(object sender, EventArgs e) => OnClick(e);
        protected virtual void LabText_FontChanged(object sender, EventArgs e)
        {
            // 获取字体的文字实例的高度
            Graphics g = this.CreateGraphics();
            SizeF sizeF = g.MeasureString("X", labText.Font);
            labText.Height = (int)sizeF.Height;
        }
    }
}
