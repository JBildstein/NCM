using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the Gray color model
    /// </summary>
    public class ColorGray : Color
    {
        #region Variables

        /// <summary>
        /// G-Channel
        /// </summary>
        public double G
        {
            get { return this[0]; }
            set { this[0] = value; }
        }

        /// <summary>
        /// The colorspace of this color
        /// </summary>
        public new ColorspaceGray Space
        {
            get { return base.Space as ColorspaceGray; }
            protected set
            {
                ColorspaceGray sp = value as ColorspaceGray;
                if (sp != null) base.Space = sp;
            }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "Gray"; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get { return 1; }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "G" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Gray" }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="G"/> channel
        /// </summary>
        public static readonly double Min = 0.0;
        /// <summary>
        /// Maximum value for the <see cref="G"/> channel
        /// </summary>
        public static readonly double Max = 1.0;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        public ColorGray()
            : base(ColorspaceGray.Default, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="G">Value for the Gray channel</param>
        public ColorGray(double G)
            : base(ColorspaceGray.Default, G)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorGray(ColorspaceGray colorspace)
            : base(colorspace, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="G">Value for the Gray channel</param>
        /// <param name="colorspace">The colorspace for this color</param>
        public ColorGray(double G, ColorspaceGray colorspace)
            : base(colorspace, G)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorGray(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="G">Value for the Gray channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorGray(double G, ICCProfile profile)
            : base(new ColorspaceICC(profile), G)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorGray(ColorspaceICC space)
            : base(space, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorGray"/> class
        /// </summary>
        /// <param name="G">Value for the Gray channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorGray(double G, ColorspaceICC space)
            : base(space, G)
        { }

        #endregion
    }
}
