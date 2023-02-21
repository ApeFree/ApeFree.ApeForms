using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{

    public class SimpleButton : Button
    {
        private Color normalBackColor;
        [Obsolete]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle { get; set; }

        [Obsolete("请使用Text属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title { get => Text; set => Text = value; }

        public int BorderSize { get => FlatAppearance.BorderSize; set => FlatAppearance.BorderSize = value; }
        public Color BorderColor { get => FlatAppearance.BorderColor; set => FlatAppearance.BorderColor = value; }

        public new Color BackColor
        {
            get => normalBackColor;
            set
            {
                normalBackColor = value;

                if (Enabled)
                {
                    base.BackColor = value;

                    FlatAppearance.MouseDownBackColor = value.Luminance(0.8f);
                    FlatAppearance.MouseOverBackColor = value.Luminance(1.2f);
                    FlatAppearance.CheckedBackColor = value.Luminance(1.1f);
                }
            }
        }

        public SimpleButton()
        {
            Size = new Size(50, 35);

            FlatStyle = FlatStyle.Flat;
            BorderSize = 0;

            base.BackColor = BackColor = Color.FromArgb(0, 122, 204);
            base.ForeColor = ForeColor = Color.WhiteSmoke;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (Enabled)
            {
                BackColor = normalBackColor;
            }
            else
            {
                base.BackColor = Color.Gray;
            }
        }
    }
}
