using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the YCbCr color model
    /// </summary>
    public class ColorYCbCr : Color
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
        /// Cb-Channel
        /// </summary>
        public double Cb
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// Cr-Channel
        /// </summary>
        public double Cr
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
            get { return "YCbCr"; }
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
            get { return new double[] { Min_Y, Min_Cb, Min_Cr }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_Y, Max_Cb, Max_Cr }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "Y", "Cb", "Cr" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Y", "Cb", "Cr" }; }
        }

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_Y, Min_Cb, Min_Cr }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_Y, Max_Cb, Max_Cr }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="Y"/> channel
        /// </summary>
        public static readonly double Min_Y = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="Cb"/> channel
        /// </summary>
        public static readonly double Min_Cb = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="Cr"/> channel
        /// </summary>
        public static readonly double Min_Cr = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="Y"/> channel
        /// </summary>
        public static readonly double Max_Y = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="Cb"/> channel
        /// </summary>
        public static readonly double Max_Cb = 1.0;
        /// <summary>
        /// Maximum value for the <see cref="Cr"/> channel
        /// </summary>
        public static readonly double Max_Cr = 1.0;

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYCbCr"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorYCbCr(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYCbCr"/> class
        /// </summary>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="Cr">Value for the Cr channel</param>
        /// <param name="Cb">Value for the Cb channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorYCbCr(double Y, double Cb, double Cr, ICCProfile profile)
            : base(new ColorspaceICC(profile), Y, Cb, Cr)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYCbCr"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorYCbCr(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorYCbCr"/> class
        /// </summary>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="Cr">Value for the Cr channel</param>
        /// <param name="Cb">Value for the Cb channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorYCbCr(double Y, double Cb, double Cr, ColorspaceICC space)
            : base(space, Y, Cb, Cr)
        { }

        #endregion
    }
}
