using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the Luv color model
    /// </summary>
    public sealed class ColorLuv : Color
    {
        #region Variables

        /// <summary>
        /// L-Channel
        /// </summary>
        public double L
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// u-Channel
        /// </summary>
        public double u
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// v-Channel
        /// </summary>
        public double v
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "CIE Luv"; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get { return 3; }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min_L, Min_u, Min_v }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_L, Max_u, Max_v }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "L", "u", "v" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Lightness", "u", "v" }; }
        }

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_L, Min_u, Min_v }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_L, Max_u, Max_v }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="L"/> channel
        /// </summary>
        public static readonly double Min_L = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="u"/> channel
        /// </summary>
        public static readonly double Min_u = -128.0;
        /// <summary>
        /// Minimum value for the <see cref="v"/> channel
        /// </summary>
        public static readonly double Min_v = -128.0;

        /// <summary>
        /// Maximum value for the <see cref="L"/> channel
        /// </summary>
        public static readonly double Max_L = 100.0;
        /// <summary>
        /// Maximum value for the <see cref="u"/> channel
        /// </summary>
        public static readonly double Max_u = 127.0;
        /// <summary>
        /// Maximum value for the <see cref="v"/> channel
        /// </summary>
        public static readonly double Max_v = 127.0;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        public ColorLuv()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="u">Value for the u channel</param>
        /// <param name="v">Value for the v channel</param>
        public ColorLuv(double L, double u, double v)
            : base(Colorspace.Default, L, u, v)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorLuv(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="u">Value for the u channel</param>
        /// <param name="v">Value for the v channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorLuv(double L, double u, double v, Whitepoint wp)
            : base(new Colorspace(wp), L, u, v)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        public ColorLuv(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="u">Value for the u channel</param>
        /// <param name="v">Value for the v channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorLuv(double L, double u, double v, ICCProfile profile)
            : base(new ColorspaceICC(profile), L, u, v)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorLuv(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLuv"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="u">Value for the u channel</param>
        /// <param name="v">Value for the v channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorLuv(double L, double u, double v, ColorspaceICC space)
            : base(space, L, u, v)
        { }

        #endregion
    }
}
