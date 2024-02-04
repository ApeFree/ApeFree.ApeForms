using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{

    public class SimpleButton : Button
    {
        private Color normalBackColor;
        private Image icon;
        private float iconScaling = 0.6f;
        private bool usePureColorIcon = true;

        [Obsolete]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle { get; set; }

        [Obsolete("请使用Text属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title { get => Text; set => Text = value; }

        public int BorderSize { get => FlatAppearance.BorderSize; set => FlatAppearance.BorderSize = value; }
        public Color BorderColor { get => FlatAppearance.BorderColor; set => FlatAppearance.BorderColor = value; }

        /// <summary>
        /// 图标
        /// </summary>
        [Browsable(true)]
        [Description("图标")]
        public Image Icon
        {
            get => icon;
            set
            {
                icon = value;
                DisplayIcon?.Dispose();
                DisplayIcon = value;

                if (icon == null)
                {
                    Refresh();
                    return;
                }

                if (UsePureColorIcon)
                {
                    DisplayIcon = ((Bitmap)DisplayIcon).ToPureColor(ForeColor, true);
                    Refresh();
                    return;
                }
            }
        }

        /// <summary>
        /// 纯色图标
        /// </summary>
        private Image DisplayIcon { get; set; }

        /// <summary>
        /// 图标缩放比例
        /// </summary>
        [Browsable(true)]
        [Description("图标缩放比例，取值范围[0,1]")]
        public float IconScaling
        {
            get => iconScaling;
            set
            {
                if (value > 1 || value < 0)
                {
                    return;
                }
                if (iconScaling != value)
                {
                    iconScaling = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 图标颜色
        /// </summary>
        [Browsable(true)]
        [Description("是否启用纯色图标")]
        public bool UsePureColorIcon
        {
            get => usePureColorIcon;
            set
            {
                usePureColorIcon = value;
                Icon = Icon;    // 刷新图标
            }
        }

        public override Color BackColor
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

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            Icon = icon;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            var icon = DisplayIcon;

            if (Icon != null)
            {
                var side = (int)(Math.Min(Width, Height) * iconScaling);
                var size = new Size(side, side);
                var xy = (Math.Min(Width, Height) - side) / 2;
                var location = new Point(xy, xy);

                pevent.Graphics.DrawImage(icon, new Rectangle(location, size));
            }
        }
    }
}
