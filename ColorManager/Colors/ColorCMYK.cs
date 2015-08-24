using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the CMYK color model
    /// </summary>
    public class ColorCMYK : Color
    {
        #region Variables

        /// <summary>
        /// Cyan-Channel
        /// </summary>
        public double C
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// Magenta-Channel
        /// </summary>
        public double M
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// Yellow-Channel
        /// </summary>
        public double Y
        {
            get { return this[2]; }
            set { this[2] = value; }
        }
        /// <summary>
        /// Key(Black)-Channel
        /// </summary>
        public double K
        {
            get { return this[3]; }
            set { this[3] = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "CMYK"; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get { return 4; }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min_C, Min_M, Min_Y, Min_K }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_C, Max_M, Max_Y, Max_K }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "C", "M", "Y", "K" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Cyan", "Magenta", "Yellow", "Key" }; }
        }

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_C, Min_M, Min_Y, Min_K }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_C, Max_M, Max_Y, Max_K }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="C"/> channel
        /// </summary>
        public static readonly double Min_C = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="M"/> channel
        /// </summary>
        public static readonly double Min_M = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="Y"/> channel
        /// </summary>
        public static readonly double Min_Y = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="K"/> channel
        /// </summary>
        public static readonly double Min_K = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="C"/> channel
        /// </summary>
        public static readonly double Max_C = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="M"/> channel
        /// </summary>
        public static readonly double Max_M = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="Y"/> channel
        /// </summary>
        public static readonly double Max_Y = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="K"/> channel
        /// </summary>
        public static readonly double Max_K = 1.0;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMYK"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorCMYK(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMYK"/> class
        /// </summary>
        /// <param name="C">Value for the Cyan channel</param>
        /// <param name="M">Value for the Magenta channel</param>
        /// <param name="Y">Value for the Yellow channel</param>
        /// <param name="K">Value for the Key (Black) channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorCMYK(double C, double M, double Y, double K, ICCProfile profile)
            : base(new ColorspaceICC(profile), C, M, Y, K)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMYK"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorCMYK(ColorspaceICC space)
            : base(space, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMYK"/> class
        /// </summary>
        /// <param name="C">Value for the Cyan channel</param>
        /// <param name="M">Value for the Magenta channel</param>
        /// <param name="Y">Value for the Yellow channel</param>
        /// <param name="K">Value for the Key (Black) channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorCMYK(double C, double M, double Y, double K, ColorspaceICC space)
            : base(space, C, M, Y, K)
        { }
    }
}
