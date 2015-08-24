using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the HSL color model
    /// </summary>
    public sealed class ColorHSV : ColorHSx
    {
        #region Variables

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
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min_H, Min_S, Min_V }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_H, Max_S, Max_V }; }
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


        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_H, Min_S, Min_V }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_H, Max_S, Max_V }; }
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
        /// Minimum value for the <see cref="V"/> channel
        /// </summary>
        public static readonly double Min_V = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="ColorHSx.H"/> channel
        /// </summary>
        public static readonly double Max_H = 360.0;
        /// <summary>
        /// Maximum value for the <see cref="ColorHSx.S"/> channel
        /// </summary>
        public static readonly double Max_S = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="V"/> channel
        /// </summary>
        public static readonly double Max_V = 1.0;

        #endregion

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
