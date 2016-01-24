
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the Bef color model
    /// </summary>
    public sealed class ColorBef : Color
    {
        #region Variables

        /// <summary>
        /// B-Channel
        /// </summary>
        public double B
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// e-Channel
        /// </summary>
        public double e
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// f-Channel
        /// </summary>
        public double f
        {
            get { return this[2]; }
            set { this[2] = value; }
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
            get { return "Bef"; }
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
            get { return new double[] { Min_B, Min_e, Min_f }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_B, Max_e, Max_f }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "B", "e", "f" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "B", "e", "f" }; }
        }

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_B, Min_e, Min_f }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_B, Max_e, Max_f }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="B"/> channel
        /// </summary>
        public static readonly double Min_B = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="e"/> channel
        /// </summary>
        public static readonly double Min_e = -1.0;
        /// <summary>
        /// Minimum value for the <see cref="f"/> channel
        /// </summary>
        public static readonly double Min_f = -1.0;

        /// <summary>
        /// Maximum value for the <see cref="B"/> channel
        /// </summary>
        public static readonly double Max_B = 2.0;
        /// <summary>
        /// Maximum value for the <see cref="e"/> channel
        /// </summary>
        public static readonly double Max_e = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="f"/> channel
        /// </summary>
        public static readonly double Max_f = 1.0;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBef"/> class
        /// </summary>
        public ColorBef()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBef"/> class
        /// </summary>
        /// <param name="B">Value for the Brightness channel</param>
        /// <param name="e">Value for the e channel</param>
        /// <param name="f">Value for the f channel</param>
        public ColorBef(double B, double e, double f)
            : base(Colorspace.Default, B, e, f)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBef"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorBef(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBef"/> class
        /// </summary>
        /// <param name="B">Value for the Brightness channel</param>
        /// <param name="e">Value for the e channel</param>
        /// <param name="f">Value for the f channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorBef(double B, double e, double f, Whitepoint wp)
            : base(new Colorspace(wp), B, e, f)
        { }
    }
}
