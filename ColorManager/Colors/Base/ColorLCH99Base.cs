
namespace ColorManager
{
    /// <summary>
    /// Base class for LCH99 colors (abstract class)
    /// </summary>
    public abstract class ColorLCH99Base : ColorLCHBase
    {
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { 100.0, 70.0, 360.0 }; }
        }

        private static readonly Whitepoint wp = new WhitepointD65();

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99Base"/> class
        /// </summary>
        public ColorLCH99Base()
            : base(wp)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99Base"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCH99Base(double L, double C, double H)
            : base(L, C, H, wp)
        { }
    }
}
