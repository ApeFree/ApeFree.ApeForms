namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// 图层
    /// </summary>
    public class Layer<TStyle>
    {
        /// <summary>
        /// 风格样式
        /// </summary>
        public TStyle Style { get; set; }

        /// <summary>
        /// 图形
        /// </summary>
        public IShape Shape { get; set; }

        /// <summary>
        /// 可见性
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 画板
        /// </summary>
        public Palette<TStyle> Parent { get; }

        /// <summary>
        /// 可聚焦
        /// </summary>
        public bool Focusable { get; set; }

        /// <summary>
        /// 构造图层
        /// </summary>
        /// <param name="parent">所属画板</param>
        internal Layer(Palette<TStyle> parent)
        {
            Visible = true;
            Parent = parent;
        }

        /// <summary>
        /// 构造图层
        /// </summary>
        /// <param name="parent">所属画板</param>
        /// <param name="style">样式风格</param>
        /// <param name="shape">图形</param>
        internal Layer(Palette<TStyle> parent, TStyle style, IShape shape) : this(parent)
        {
            Style = style;
            Shape = shape;
        }
    }

    public class Layer<TStyle, TShape> : Layer<TStyle> where TShape : IShape
    {
        /// <summary>
        /// 图形
        /// </summary>
        public new TShape Shape { get => (TShape)base.Shape; set => base.Shape = value; }

        internal Layer(Palette<TStyle> parent) : base(parent)
        {
        }

        internal Layer(Palette<TStyle> parent, TStyle style, IShape shape) : base(parent, style, shape)
        {
        }
    }
}
