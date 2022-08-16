using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public class TallerLabel : Label
    {
        private bool autoHeight = true;

        [DefaultValue(true)]
        public bool AutoHeight
        {
            get { return this.autoHeight; }
            set
            {
                this.autoHeight = value;
                RecalcHeight();
            }
        }

        protected override void OnAutoSizeChanged(EventArgs e)
        {
            base.OnAutoSizeChanged(e);
            RecalcHeight();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RecalcHeight();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RecalcHeight();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            RecalcHeight();
        }

        private void RecalcHeight()
        {
            if (this.AutoSize || !this.autoHeight)
                return;

            var size = TextRenderer.MeasureText(this.Text, this.Font);
            int lc = (int)Math.Ceiling(size.Width * 1.0 / this.ClientSize.Width);
            this.Height = lc * size.Height;
        }
    }
}
