using ColorManager.ICC;

namespace ColorManager
{
    public class ColorGray : Color
    {
        /// <summary>
        /// G-Channel
        /// </summary>
        public double G
        {
            get { return this[0]; }
            set { this[0] = value; }
        }

        public override string Name
        {
            get { return "Gray"; }
        }
        public override int ChannelCount
        {
            get { return 1; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 1.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "G" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Gray" }; }
        }

        #region Constructor

        public ColorGray()
            : base(ColorspaceGray.Default, 0)
        { }

        public ColorGray(double G)
            : base(ColorspaceGray.Default, G)
        { }

        public ColorGray(ColorspaceGray colorspace)
            : base(colorspace, 0)
        { }

        public ColorGray(double G, ColorspaceGray colorspace)
            : base(colorspace, G)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorGray(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0)
        { }

        public ColorGray(double G, ICCProfile profile)
            : base(new ColorspaceICC(profile), G)
        { }

        public ColorGray(ColorspaceICC space)
            : base(space, 0)
        { }

        public ColorGray(double G, ColorspaceICC space)
            : base(space, G)
        { }

        #endregion
    }
}
