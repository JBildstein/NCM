using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the RGB color model
    /// </summary>
    public class ColorRGB : Color
    {
        /// <summary>
        /// R-Channel
        /// </summary>
        public double R
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// G-Channel
        /// </summary>
        public double G
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// B-Channel
        /// </summary>
        public double B
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "RGB"; }
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
            get { return new string[] { "R", "G", "B" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Red", "Green", "Blue" }; }
        }

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        public ColorRGB()
            : base(ColorspaceRGB.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="R">Value for the Red channel</param>
        /// <param name="G">Value for the Green channel</param>
        /// <param name="B">Value for the Blue channel</param>
        public ColorRGB(double R, double G, double B)
            : base(ColorspaceRGB.Default, R, G, B)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorRGB(ColorspaceRGB colorspace)
            : base(colorspace, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="R">Value for the Red channel</param>
        /// <param name="G">Value for the Green channel</param>
        /// <param name="B">Value for the Blue channel</param>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorRGB(double R, double G, double B, ColorspaceRGB colorspace)
            : base(colorspace, R, G, B)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorRGB(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="R">Value for the Red channel</param>
        /// <param name="G">Value for the Green channel</param>
        /// <param name="B">Value for the Blue channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorRGB(double R, double G, double B, ICCProfile profile)
            : base(new ColorspaceICC(profile), R, G, B)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorRGB(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorRGB"/> class
        /// </summary>
        /// <param name="R">Value for the Red channel</param>
        /// <param name="G">Value for the Green channel</param>
        /// <param name="B">Value for the Blue channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorRGB(double R, double G, double B, ColorspaceICC space)
            : base(space, R, G, B)
        { }

        #endregion
    }
}
