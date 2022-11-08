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
    }
}
