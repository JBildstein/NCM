using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Base class for HS(x) colors (abstract class)
    /// </summary>
    public abstract class ColorHSx : Color
    {
        #region Variables

        /// <summary>
        /// L-Channel
        /// </summary>
        public double H
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// C-Channel
        /// </summary>
        public double S
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// X-Channel
        /// </summary>
        protected double x
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The index of the channel that is cylindrical
        /// <para>If the value is bigger than 360° or smaller than 0, it will start over</para>
        /// <para>If not used, it's simply -1</para>
        /// </summary>
        public override int CylinderChannel
        {
            get { return 0; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get { return 3; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "H", "S", ChannelXNameShort }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Hue", "Saturation", ChannelXNameFull }; }
        }
        /// <summary>
        /// Short name for the X-Channel
        /// </summary>
        protected abstract string ChannelXNameShort { get; }
        /// <summary>
        /// Full name for the X-Channel
        /// </summary>
        protected abstract string ChannelXNameFull { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        protected ColorHSx()
            : base(ColorspaceRGB.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="x">Value for the X channel</param>
        protected ColorHSx(double H, double S, double x)
            : base(ColorspaceRGB.Default, H, S, x)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="colorspace">The colorspace for this color</param>
        protected ColorHSx(ColorspaceRGB colorspace)
            : base(colorspace, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="x">Value for the X channel</param>
        /// <param name="colorspace">The colorspace for this color</param>
        protected ColorHSx(double H, double S, double x, ColorspaceRGB colorspace)
            : base(colorspace, H, S, x)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        protected ColorHSx(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="x">Value for the X channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        protected ColorHSx(double H, double S, double x, ICCProfile profile)
            : base(new ColorspaceICC(profile), H, S, x)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        protected ColorHSx(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorHSx"/> class
        /// </summary>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="S">Value for the Saturation channel</param>
        /// <param name="x">Value for the X channel</param>
        /// <param name="space">The ICC space for this color</param>
        protected ColorHSx(double H, double S, double x, ColorspaceICC space)
            : base(space, H, S, x)
        { }

        #endregion
    }
}
