
namespace ColorManager
{
    public abstract class ColorLCHBase : Color
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
        /// C-Channel
        /// </summary>
        public double C
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// H-Channel
        /// </summary>
        public double H
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public override int CylinderChannel
        {
            get { return 2; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "L", "C", "H" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Lightness", "Chromaticity", "Hue" }; }
        }
        
        protected ColorLCHBase()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        protected ColorLCHBase(double L, double C, double H)
            : base(Colorspace.Default, L, C, H)
        { }

        protected ColorLCHBase(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        protected ColorLCHBase(double L, double C, double H, Whitepoint wp)
            : base(new Colorspace(wp), L, C, H)
        { }
    }
}
