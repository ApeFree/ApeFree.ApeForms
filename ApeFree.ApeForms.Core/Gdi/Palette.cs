using System.Collections.Generic;

namespace ApeFree.ApeForms.Core.Gdi
{
    /// <summary>
    /// 图形面板
    /// </summary>
    public class Palette
    {
        public List<IGraphic> Graphics { get; }

        protected Palette()
        {
            Graphics = new List<IGraphic>();
        }
    }

    /// <summary>
    /// Gdi图形画板
    /// </summary>
    public class GdiPalette : Palette
    {

    }
}
