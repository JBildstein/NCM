using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the CMY color model
    /// </summary>
    public class ColorCMY : Color
    {
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
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "CMY"; }
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
            get { return new string[] { "C", "M", "Y" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Cyan", "Magenta", "Yellow" }; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMY"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorCMY(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMY"/> class
        /// </summary>
        /// <param name="C">Value for the Cyan channel</param>
        /// <param name="M">Value for the Magenta channel</param>
        /// <param name="Y">Value for the Yellow channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorCMY(double C, double M, double Y, ICCProfile profile)
            : base(new ColorspaceICC(profile), C, M, Y)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMY"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorCMY(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorCMY"/> class
        /// </summary>
        /// <param name="C">Value for the Cyan channel</param>
        /// <param name="M">Value for the Magenta channel</param>
        /// <param name="Y">Value for the Yellow channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorCMY(double C, double M, double Y, ColorspaceICC space)
            : base(space, C, M, Y)
        { }
    }
}
