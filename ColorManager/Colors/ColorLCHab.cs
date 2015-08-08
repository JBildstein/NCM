
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCHab color model
    /// </summary>
    public unsafe sealed class ColorLCHab : ColorLCHBase
    {
        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "LCHuv"; }
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
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        public ColorLCHab()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCHab(double L, double C, double H)
            : base(L, C, H)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorLCHab(Whitepoint wp)
            : base(wp)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorLCHab(double L, double C, double H, Whitepoint wp)
            : base(L, C, H, wp)
        { }
    }
}
