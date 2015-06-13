using ColorManager.ICC;

namespace ColorManager
{
    public sealed class ColorLuv : Color
    {
        /// <summary>
        /// L-Channel
        /// </summary>
        public double L
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// u-Channel
        /// </summary>
        public double u
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// v-Channel
        /// </summary>
        public double v
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public override string Name
        {
            get { return "CIE Luv"; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, -255.0, -255.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 100.0, 255.0, 255.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "L", "u", "v" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Lightness", "u", "v" }; }
        }
        
        #region Constructor

        public ColorLuv()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorLuv(double L, double u, double v)
            : base(Colorspace.Default, L, u, v)
        { }
        
        public ColorLuv(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorLuv(double L, double u, double v, Whitepoint wp)
            : base(new Colorspace(wp), L, u, v)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorLuv(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorLuv(double L, double u, double v, ICCProfile profile)
            : base(new ColorspaceICC(profile), L, u, v)
        { }

        public ColorLuv(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorLuv(double L, double u, double v, ColorspaceICC space)
            : base(space, L, u, v)
        { }

        #endregion
    }
}
