using ColorManager.ICC;

namespace ColorManager
{
    public sealed class ColorHSV : ColorHSx
    {
        /// <summary>
        /// V-Channel
        /// </summary>
        public double V
        {
            get { return x; }
            set { x = value; }
        }

        public override string Name
        {
            get { return "HSV"; }
        }
        protected override string ChannelXNameShort
        {
            get { return "V"; }
        }
        protected override string ChannelXNameFull
        {
            get { return "Value"; }
        }

        #region Constructor

        public ColorHSV()
            : base()
        { }

        public ColorHSV(double H, double S, double V)
            : base(H, S, V)
        { }

        public ColorHSV(ColorspaceRGB colorspace)
            : base(colorspace)
        { }

        public ColorHSV(double H, double S, double V, ColorspaceRGB colorspace)
            : base(H, S, V, colorspace)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorHSV(ICCProfile profile)
            : base(profile)
        { }

        public ColorHSV(double H, double S, double V, ICCProfile profile)
            : base(H, S, V, profile)
        { }

        public ColorHSV(ColorspaceICC space)
            : base(space)
        { }

        public ColorHSV(double H, double S, double V, ColorspaceICC space)
            : base(H, S, V, space)
        { }

        #endregion
    }
}
