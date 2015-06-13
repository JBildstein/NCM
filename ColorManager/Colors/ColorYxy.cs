using ColorManager.ICC;

namespace ColorManager
{
    public sealed class ColorYxy : Color
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
        /// x-Channel
        /// </summary>
        public double x
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// y-Channel
        /// </summary>
        public double y
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public override string Name
        {
            get { return "Yxy"; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, double.MinValue, double.MinValue }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 1.0, double.MaxValue, double.MaxValue }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "Y", "x", "y" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Y", "x", "y" }; }
        }
        
        #region Constructor

        public ColorYxy()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorYxy(double Y, double x, double y)
            : base(Colorspace.Default, Y, x, y)
        { }
        
        public ColorYxy(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorYxy(double Y, double x, double y, Whitepoint wp)
            : base(new Colorspace(wp), Y, x, y)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorYxy(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorYxy(double Y, double x, double y, ICCProfile profile)
            : base(new ColorspaceICC(profile), Y, x, y)
        { }

        public ColorYxy(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorYxy(double Y, double x, double y, ColorspaceICC space)
            : base(space, Y, x, y)
        { }

        #endregion
    }
}
