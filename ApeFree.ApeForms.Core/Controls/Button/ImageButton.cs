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
    [DefaultEvent("Click")]
    public partial class ImageButton : UserControl
    {
        [DefaultValue(null)]
        [Localizable(true)]
        public override Image BackgroundImage { get => base.BackgroundImage; set { base.BackgroundImage = value; RefreshImage(); } }
        public override Color BackColor { get => base.BackColor; set { base.BackColor = value; RefreshImage(); } }
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; RefreshImage(); } }

        [Browsable(true)]
        [Description("计算按钮图标时保留Alpha通道")]
        public bool UseAlphaChannel
        {
            get => useAlphaChannel; set
            {
                if (useAlphaChannel != value)
                {
                    useAlphaChannel = value;
                    RefreshImage();
                }
            }
        }
        private bool useAlphaChannel = true;

        [Browsable(true)]
        [Description("启用图片纯色计算")]
        public bool UsePureColor
        {
            get => usePureColor; set
            {
                if (usePureColor != value)
                {
                    usePureColor = value;
                    RefreshImage();
                }
            }
        }
        private bool usePureColor = true;

        /// <summary>
        //按钮状态图集
        /// </summary>
        private ButtonStyles styles;

        /// <summary>
        /// 鼠标处于控件内部
        /// </summary>
        private bool isInSide = false;
        private bool isLoaded = false;

        public ImageButton()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Zoom;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            isLoaded = true;
            RefreshImage();
        }

        /// <summary>
        /// 触发Click事件
        /// </summary>
        public void PerformClick() => OnClick(new EventArgs());

        /// <summary>
        /// 生成按钮所需的所有图像
        /// </summary>
        /// 
        private ButtonStyles GenerateStatusImages(Bitmap bitmap, bool usePureColor, bool useAlphaChannel)
        {
            var styles = new ButtonStyles(bitmap, BackColor);

            Bitmap[] bmps;
            if (usePureColor)
            {
                bmps = bitmap.ToPureColor(new Color[] {
                            ControlPaint.Light(ForeColor) ,
                            ControlPaint.Dark(ForeColor, 0.02f),
                            Color.DarkGray,
                            ForeColor,
                        }, useAlphaChannel);
            }
            else
            {
                bmps = new Bitmap[] { bitmap, bitmap, bitmap, bitmap };
            }

            styles.TouchImage = bmps[0];
            styles.PressImage = bmps[1];
            styles.DisableImage = bmps[2];
            styles.NormalImage = bmps[3];

            return styles;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            RefreshImage();
        }

        private void RefreshImage()
        {
            lock (this)
            {
                if (!isLoaded)
                {
                    return;
                }

                try
                {
                    Bitmap bitmap = (Bitmap)base.BackgroundImage;
                    if (bitmap == null)
                    {
                        return;
                    }

                    // styles?.Dispose();
                    styles = GenerateStatusImages(bitmap, UsePureColor, UseAlphaChannel);

                    base.BackgroundImage = styles.NormalImage;
                    base.BackColor = styles.NormalBackColor;
                }
                catch (Exception)
                {
                    // TODO: 需要查明原因
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.BackgroundImage = styles.PressImage;
            base.BackColor = styles.NormalBackColor;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            base.BackgroundImage = isInSide ? styles.TouchImage : styles.NormalImage;
            base.BackColor = isInSide ? styles.TouchBackColor : styles.NormalBackColor;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!isInSide)
            {
                base.BackColor = styles.TouchBackColor;
                base.BackgroundImage = styles.TouchImage;
            }
            isInSide = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isInSide = false;
            base.BackgroundImage = styles.NormalImage;
            base.BackColor = styles.NormalBackColor;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            base.BackgroundImage = Enabled ? styles.NormalImage : styles.DisableImage;
            base.BackColor = Enabled ? styles.NormalBackColor : styles.DisableBackColor;
        }
    }

    /// <summary>
    /// 按钮状态图片集
    /// </summary>
    class ButtonStyles : IDisposable
    {
        public ButtonStyles() { }

        public ButtonStyles(Image normalImage, Color normalBackColor)
        {
            NormalImage = TouchImage = PressImage = DisableImage = normalImage;
            NormalBackColor = TouchBackColor = PressBackColor = DisableBackColor = normalBackColor;

            TouchBackColor = ControlPaint.Light(normalBackColor);
            PressBackColor = ControlPaint.Dark(normalBackColor, 0.02f);
            DisableBackColor = Color.DarkGray;
        }

        public Image NormalImage { get; set; }
        public Image TouchImage { get; set; }
        public Image PressImage { get; set; }
        public Image DisableImage { get; set; }
        public Color NormalBackColor { get; set; }
        public Color TouchBackColor { get; set; }
        public Color PressBackColor { get; set; }
        public Color DisableBackColor { get; set; }

        public void Dispose()
        {
            NormalImage.Dispose();
            TouchImage.Dispose();
            PressImage.Dispose();
            DisableImage.Dispose();
        }
    }
}
