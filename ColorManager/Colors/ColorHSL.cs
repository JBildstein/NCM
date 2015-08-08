using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the HSL color model
    /// </summary>
    public sealed class ColorHSL : ColorHSx
    {
        /// <summary>
        /// L-Channel
        /// </summary>
        public double L
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "HSL"; }
        }
        /// <summary>
        /// Short name for the X-Channel
        /// </summary>
        protected override string ChannelXNameShort
        {
            get { return "L"; }
        }
        /// <summary>
        /// Full name for the X-Channel
        /// </summary>
        protected override string ChannelXNameFull
        {
            get { return "Lightness"; }
        }

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        public ColorHSL()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="L">Value for the Lightness channel</param>
        public ColorHSL(double H, double S, double L)
            : base(H, S, L)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorHSL(ColorspaceRGB colorspace)
            : base(colorspace)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorHSL(double H, double S, double L, ColorspaceRGB colorspace)
            : base(H, S, L, colorspace)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorHSL(ICCProfile profile)
            : base(profile)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorHSL(double H, double S, double L, ICCProfile profile)
            : base(H, S, L, profile)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorHSL(ColorspaceICC space)
            : base(space)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSL"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorHSL(double H, double S, double L, ColorspaceICC space)
            : base(H, S, L, space)
        { }

        #endregion
    }
}
