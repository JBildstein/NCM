using ColorManager.ICC;

namespace ColorManager
{
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

        public override string Name
        {
            get { return "RGB"; }
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
            get { return new string[] { "R", "G", "B" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Red", "Green", "Blue" }; }
        }

        #region Constructor

        public ColorRGB()
            : base(ColorspaceRGB.Default, 0, 0, 0)
        { }

        public ColorRGB(double R, double G, double B)
            : base(ColorspaceRGB.Default, R, G, B)
        { }

        public ColorRGB(ColorspaceRGB colorspace)
            : base(colorspace, 0, 0, 0)
        { }

        public ColorRGB(double R, double G, double B, ColorspaceRGB colorspace)
            : base(colorspace, R, G, B)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorRGB(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorRGB(double R, double G, double B, ICCProfile profile)
            : base(new ColorspaceICC(profile), R, G, B)
        { }

        public ColorRGB(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorRGB(double R, double G, double B, ColorspaceICC space)
            : base(space, R, G, B)
        { }

        #endregion
    }
}
