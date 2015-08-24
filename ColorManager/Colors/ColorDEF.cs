
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the DEF color model
    /// </summary>
    public sealed class ColorDEF : Color
    {
        #region Variables

        /// <summary>
        /// D-Channel
        /// </summary>
        public double D
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// E-Channel
        /// </summary>
        public double E
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// F-Channel
        /// </summary>
        public double F
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "DEF"; }
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
            get { return new double[] { Min_D, Min_E, Min_F }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_D, Max_E, Max_F }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "D", "E", "F" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "D", "E", "F" }; }
        }

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_D, Min_E, Min_F }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_D, Max_E, Max_F }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="D"/> channel
        /// </summary>
        public static readonly double Min_D = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="E"/> channel
        /// </summary>
        public static readonly double Min_E = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="F"/> channel
        /// </summary>
        public static readonly double Min_F = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="D"/> channel
        /// </summary>
        public static readonly double Max_D = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="E"/> channel
        /// </summary>
        public static readonly double Max_E = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="F"/> channel
        /// </summary>
        public static readonly double Max_F = 1.0;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDEF"/> class
        /// </summary>
        public ColorDEF()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDEF"/> class
        /// </summary>
        /// <param name="D">Value for the D channel</param>
        /// <param name="E">Value for the E channel</param>
        /// <param name="F">Value for the F channel</param>
        public ColorDEF(double D, double E, double F)
            : base(Colorspace.Default, D, E, F)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDEF"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorDEF(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDEF"/> class
        /// </summary>
        /// <param name="D">Value for the D channel</param>
        /// <param name="E">Value for the E channel</param>
        /// <param name="F">Value for the F channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorDEF(double D, double E, double F, Whitepoint wp)
            : base(new Colorspace(wp), D, E, F)
        { }
    }
}
