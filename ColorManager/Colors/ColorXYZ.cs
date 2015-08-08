using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the XYZ color model
    /// </summary>
    public class ColorXYZ : Color
    {
        /// <summary>
        /// X-Channel
        /// </summary>
        public double X
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// Y-Channel
        /// </summary>
        public double Y
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// Z-Channel
        /// </summary>
        public double Z
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "CIE XYZ"; }
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
            get { return new string[] { "X", "Y", "Z" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "X", "Y", "Z" }; }
        }

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        public ColorXYZ()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="X">Value for the X channel</param>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="Z">Value for the Z channel</param>
        public ColorXYZ(double X, double Y, double Z)
            : base(Colorspace.Default, X, Y, Z)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorXYZ(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="X">Value for the X channel</param>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="Z">Value for the Z channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorXYZ(double X, double Y, double Z, Whitepoint wp)
            : base(new Colorspace(wp), X, Y, Z)
        { }

        #endregion

        #region Constructor ICC

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorXYZ(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="X">Value for the X channel</param>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="Z">Value for the Z channel</param>
        /// <param name="profile">The ICC profile for this color</param>
        public ColorXYZ(double X, double Y, double Z, ICCProfile profile)
            : base(new ColorspaceICC(profile), X, Y, Z)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        public ColorXYZ(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorXYZ"/> class
        /// </summary>
        /// <param name="X">Value for the X channel</param>
        /// <param name="Y">Value for the Y channel</param>
        /// <param name="Z">Value for the Z channel</param>
        /// <param name="space">The ICC space for this color</param>
        public ColorXYZ(double X, double Y, double Z, ColorspaceICC space)
            : base(space, X, Y, Z)
        { }

        #endregion
    }
}
