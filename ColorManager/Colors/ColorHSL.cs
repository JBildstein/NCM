using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the HSL color model
    /// </summary>
    public sealed class ColorHSL : ColorHSx
    {
        #region Variables

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
            get { return ModelName; }
        }
        /// <summary>
        /// The name of this model
        /// </summary>
        public static string ModelName
        {
            get { return "HSL"; }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min_H, Min_S, Min_L }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_H, Max_S, Max_L }; }
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

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_H, Min_S, Min_L }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_H, Max_S, Max_L }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="ColorHSx.H"/> channel
        /// </summary>
        public static readonly double Min_H = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="ColorHSx.S"/> channel
        /// </summary>
        public static readonly double Min_S = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="L"/> channel
        /// </summary>
        public static readonly double Min_L = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="ColorHSx.H"/> channel
        /// </summary>
        public static readonly double Max_H = 360.0;
        /// <summary>
        /// Maximum value for the <see cref="ColorHSx.S"/> channel
        /// </summary>
        public static readonly double Max_S = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="L"/> channel
        /// </summary>
        public static readonly double Max_L = 1.0;

        #endregion

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
