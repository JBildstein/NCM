using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the Yxy color model
    /// </summary>
    public sealed class ColorYxy : Color
    {
        #region Variables

        /// <summary>
        /// Y-Channel
        /// </summary>
        public double Y
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// x-Channel
        /// </summary>
        public double x
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// y-Channel
        /// </summary>
        public double y
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
            get { return "Yxy"; }
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
            get { return new double[] { Min_Y, Min_x, Min_y }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_Y, Max_x, Max_y }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "Y", "x", "y" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Y", "x", "y" }; }
        }


        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_Y, Min_x, Min_y }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_Y, Max_x, Max_y }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="Y"/> channel
        /// </summary>
        public static readonly double Min_Y = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="x"/> channel
        /// </summary>
        public static readonly double Min_x = double.NaN;
        /// <summary>
        /// Minimum value for the <see cref="y"/> channel
        /// </summary>
        public static readonly double Min_y = double.NaN;

        /// <summary>
        /// Maximum value for the <see cref="Y"/> channel
        /// </summary>
        public static readonly double Max_Y = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="x"/> channel
        /// </summary>
        public static readonly double Max_x = double.NaN;
        /// <summary>
        /// Maximum value for the <see cref="y"/> channel
        /// </summary>
        public static readonly double Max_y = double.NaN;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        public ColorYxy()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="x">Value for the x channel</param>
        /// <param name="y">Value for the y channel</param>
        public ColorYxy(double Y, double x, double y)
            : base(Colorspace.Default, Y, x, y)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorYxy(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="x">Value for the x channel</param>
        /// <param name="y">Value for the y channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorYxy(double Y, double x, double y, Whitepoint wp)
            : base(new Colorspace(wp), Y, x, y)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorYxy(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="x">Value for the x channel</param>
        /// <param name="y">Value for the y channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorYxy(double Y, double x, double y, ICCProfile profile)
            : base(new ColorspaceICC(profile), Y, x, y)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorYxy(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYxy"/> class
        /// </summary>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="x">Value for the x channel</param>
        /// <param name="y">Value for the y channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorYxy(double Y, double x, double y, ColorspaceICC space)
            : base(space, Y, x, y)
        { }

        #endregion
    }
}
