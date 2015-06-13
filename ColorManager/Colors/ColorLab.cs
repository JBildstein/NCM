using ColorManager.ICC;

namespace ColorManager
{
    public sealed class ColorLab : Color
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
        /// a-Channel
        /// </summary>
        public double a
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// b-Channel
        /// </summary>
        public double b
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public override string Name
        {
            get { return "CIE Lab"; }
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
            get { return new string[] { "L", "a", "b" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Lightness", "a", "b" }; }
        }
        
        #region Constructor

        public ColorLab()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorLab(double L, double a, double b)
            : base(Colorspace.Default, L, a, b)
        { }
        
        public ColorLab(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorLab(double L, double a, double b, Whitepoint wp)
            : base(new Colorspace(wp), L, a, b)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorLab(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorLab(double L, double a, double b, ICCProfile profile)
            : base(new ColorspaceICC(profile), L, a, b)
        { }

        public ColorLab(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorLab(double L, double a, double b, ColorspaceICC space)
            : base(space, L, a, b)
        { }

        #endregion
    }
}
