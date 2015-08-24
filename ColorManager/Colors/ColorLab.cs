using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the Lab color model
    /// </summary>
    public sealed class ColorLab : Color
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
        /// a-Channel
        /// </summary>
        public double a
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// b-Channel
        /// </summary>
        public double b
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "CIE Lab"; }
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
            get { return new double[] { Min_L, Min_a, Min_b }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_L, Max_a, Max_b }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "L", "a", "b" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Lightness", "a", "b" }; }
        }
        
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_L, Min_a, Min_b }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_L, Max_a, Max_b }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="L"/> channel
        /// </summary>
        public static readonly double Min_L = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="a"/> channel
        /// </summary>
        public static readonly double Min_a = -255.0;
        /// <summary>
        /// Minimum value for the <see cref="b"/> channel
        /// </summary>
        public static readonly double Min_b = -255.0;

        /// <summary>
        /// Maximum value for the <see cref="L"/> channel
        /// </summary>
        public static readonly double Max_L = 100.0;
        /// <summary>
        /// Maximum value for the <see cref="a"/> channel
        /// </summary>
        public static readonly double Max_a = 255.0;
        /// <summary>
        /// Maximum value for the <see cref="b"/> channel
        /// </summary>
        public static readonly double Max_b = 255.0;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        public ColorLab()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="a">Value for the a channel</param>
        /// <param name="b">Value for the b channel</param>
        public ColorLab(double L, double a, double b)
            : base(Colorspace.Default, L, a, b)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorLab(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="a">Value for the a channel</param>
        /// <param name="b">Value for the b channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorLab(double L, double a, double b, Whitepoint wp)
            : base(new Colorspace(wp), L, a, b)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorLab(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="a">Value for the a channel</param>
        /// <param name="b">Value for the b channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorLab(double L, double a, double b, ICCProfile profile)
            : base(new ColorspaceICC(profile), L, a, b)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorLab(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="a">Value for the a channel</param>
        /// <param name="b">Value for the b channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorLab(double L, double a, double b, ColorspaceICC space)
            : base(space, L, a, b)
        { }

        #endregion
    }
}
