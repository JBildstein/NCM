using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the HSL color model
    /// </summary>
    public sealed class ColorHSV : ColorHSx
    {
        /// <summary>
        /// V-Channel
        /// </summary>
        public double V
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "HSV"; }
        }
        /// <summary>
        /// Short name for the X-Channel
        /// </summary>
        protected override string ChannelXNameShort
        {
            get { return "V"; }
        }
        /// <summary>
        /// Full name for the X-Channel
        /// </summary>
        protected override string ChannelXNameFull
        {
            get { return "Value"; }
        }

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        public ColorHSV()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="V">Value for the Value channel</param>
        public ColorHSV(double H, double S, double V)
            : base(H, S, V)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorHSV(ColorspaceRGB colorspace)
            : base(colorspace)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="V">Value for the Value channel</param>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorHSV(double H, double S, double V, ColorspaceRGB colorspace)
            : base(H, S, V, colorspace)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorHSV(ICCProfile profile)
            : base(profile)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="V">Value for the Value channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorHSV(double H, double S, double V, ICCProfile profile)
            : base(H, S, V, profile)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorHSV(ColorspaceICC space)
            : base(space)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSV"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="V">Value for the Value channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorHSV(double H, double S, double V, ColorspaceICC space)
            : base(H, S, V, space)
        { }

        #endregion
    }
}
