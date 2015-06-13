using ColorManager.ICC;

namespace ColorManager
{
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

        public override string Name
        {
            get { return "CIE XYZ"; }
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
            get { return new double[] { 1.0, 1.0, 1.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "X", "Y", "Z" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "X", "Y", "Z" }; }
        }
        
        #region Constructor

        public ColorXYZ()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorXYZ(double X, double Y, double Z)
            : base(Colorspace.Default, X, Y, Z)
        { }

        public ColorXYZ(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorXYZ(double X, double Y, double Z, Whitepoint wp)
            : base(new Colorspace(wp), X, Y, Z)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorXYZ(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorXYZ(double X, double Y, double Z, ICCProfile profile)
            : base(new ColorspaceICC(profile), X, Y, Z)
        { }

        public ColorXYZ(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorXYZ(double X, double Y, double Z, ColorspaceICC space)
            : base(space, X, Y, Z)
        { }

        #endregion
    }
}
