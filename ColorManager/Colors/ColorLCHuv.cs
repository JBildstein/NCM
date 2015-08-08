
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCHuv color model
    /// </summary>
    public unsafe sealed class ColorLCHuv : ColorLCHBase
    {
        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "LCHab"; }
        }
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
            get { return new double[] { 100.0, 255.0, 360.0 }; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHuv"/> class
        /// </summary>
        public ColorLCHuv()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHuv"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCHuv(double L, double C, double H)
            : base(L, C, H)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHuv"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorLCHuv(Whitepoint wp)
            : base(wp)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHuv"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorLCHuv(double L, double C, double H, Whitepoint wp)
            : base(L, C, H, wp)
        { }
    }
}
