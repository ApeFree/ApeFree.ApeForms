namespace System.Drawing
{
    public static class ColorExtensions
    {
        /// <summary>
        /// 按比例调节亮度
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="ratio">比例</param>
        /// <returns></returns>
        public static Color Luminance(this Color color, float ratio)
        {
            if (ratio < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ratio));
            }

            byte[] values = new byte[] { color.R, color.G, color.B };
            for (int i = 0; i < values.Length; i++)
            {
                var cv = (int)Math.Round(values[i] * ratio) + 1;
                values[i] = (byte)Math.Min(255, cv);
            }
            return Color.FromArgb(color.A, values[0], values[1], values[2]);
        }
    }
}
