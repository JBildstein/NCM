using ColorManager.ICC;

namespace ColorManager
{
    public sealed class ColorHSL : ColorHSx
    {
        /// <summary>
        /// L-Channel
        /// </summary>
        public double L
        {
            get { return x; }
            set { x = value; }
        }

        public override string Name
        {
            get { return "HSL"; }
        }
        protected override string ChannelXNameShort
        {
            get { return "L"; }
        }
        protected override string ChannelXNameFull
        {
            get { return "Lightness"; }
        }

        #region Constructor

        public ColorHSL()
            : base()
        { }

        public ColorHSL(double H, double S, double L)
            : base(H, S, L)
        { }

        public ColorHSL(ColorspaceRGB colorspace)
            : base(colorspace)
        { }

        public ColorHSL(double H, double S, double L, ColorspaceRGB colorspace)
            : base(H, S, L, colorspace)
        { }

        #endregion

        #region Constructor ICC
        
        public ColorHSL(ICCProfile profile)
            : base(profile)
        { }

        public ColorHSL(double H, double S, double L, ICCProfile profile)
            : base(H, S, L, profile)
        { }

        public ColorHSL(ColorspaceICC space)
            : base(space)
        { }

        public ColorHSL(double H, double S, double L, ColorspaceICC space)
            : base(H, S, L, space)
        { }

        #endregion
    }
}
