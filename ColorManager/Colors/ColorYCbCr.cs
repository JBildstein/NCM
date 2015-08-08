using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the YCbCr color model
    /// </summary>
    public class ColorYCbCr : Color
    {
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
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { 1.0, 1.0, 1.0 }; }
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
