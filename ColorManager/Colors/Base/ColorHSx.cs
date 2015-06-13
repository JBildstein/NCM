using ColorManager.ICC;

namespace ColorManager
{
    public abstract class ColorHSx : Color
    {
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

        public override int CylinderChannel
        {
            get { return 0; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 360.0, 1.0, 1.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "H", "S", ChannelXNameShort }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Hue", "Saturation", ChannelXNameFull }; }
        }

        protected abstract string ChannelXNameShort { get; }
        protected abstract string ChannelXNameFull { get; }
        
        #region Constructor

        protected ColorHSx()
            : base(ColorspaceRGB.Default, 0, 0, 0)
        { }

        protected ColorHSx(double H, double S, double x)
            : base(ColorspaceRGB.Default, H, S, x)
        { }

        protected ColorHSx(ColorspaceRGB colorspace)
            : base(colorspace, 0, 0, 0)
        { }

        protected ColorHSx(double H, double S, double x, ColorspaceRGB colorspace)
            : base(colorspace, H, S, x)
        { }

        #endregion

        #region Constructor ICC

        protected ColorHSx(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        protected ColorHSx(double H, double S, double x, ICCProfile profile)
            : base(new ColorspaceICC(profile), H, S, x)
        { }

        protected ColorHSx(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        protected ColorHSx(double H, double S, double x, ColorspaceICC space)
            : base(space, H, S, x)
        { }

        #endregion
    }
}
