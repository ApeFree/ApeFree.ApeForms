using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{

    public class SimpleButton : Button
    {
        private Color normalBackColor;
        private Color normalForeColor;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PressedBackColor { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PressedForeColor { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TouchedBackColor { get; private set; }

        public Color TouchedForeColor { get; private set; }

        [Obsolete]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoScroll { get; set; }

        [Obsolete]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle { get; set; }

        public int BorderSize { get => FlatAppearance.BorderSize; set => FlatAppearance.BorderSize = value; }
        public Color BorderColor { get => FlatAppearance.BorderColor; set => FlatAppearance.BorderColor = value; }

        public new Color BackColor
        {
            get => normalBackColor;
            set
            {
                base.BackColor = value;
                normalBackColor = value;
                PressedBackColor = value.Luminance(0.9f);
                TouchedBackColor = value.Luminance(1.1f);
            }
        }
        public new Color ForeColor
        {
            get => normalForeColor;
            set
            {
                base.ForeColor = value;
                normalForeColor = value;
                PressedForeColor = value.Luminance(0.9f);
                TouchedForeColor = value.Luminance(1.1f);
            }
        }

        public string Title { get => Text; set => Text = value; }


        public SimpleButton()
        {
            Size = new Size(50, 35);

            FlatStyle = FlatStyle.Flat;
            BorderSize = 0;

            base.BackColor = BackColor = Color.FromArgb(0, 122, 204);
            base.ForeColor = ForeColor = Color.WhiteSmoke;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.BackColor = PressedBackColor;
            base.ForeColor = PressedForeColor;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.BackColor = TouchedBackColor;
            base.ForeColor = TouchedForeColor;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.BackColor = BackColor;
            base.ForeColor = ForeColor;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.BackColor = TouchedBackColor;
            base.ForeColor = TouchedForeColor;
            base.OnMouseUp(mevent);
        }
    }
}
