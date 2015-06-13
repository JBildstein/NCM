
namespace ColorManager
{
    public sealed unsafe class ColorBCH : Color
    {
        /// <summary>
        /// B-Channel
        /// </summary>
        public double B
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
        public override string Name
        {
            get { return "BCH"; }
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
            get { return new double[] { 1.5, 1.5, 360.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "B", "C", "H" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Brightness", "Chromaticity", "Hue"}; }
        }

        public ColorBCH()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorBCH(double B, double C, double H)
            : base(Colorspace.Default, B, C, H)
        { }

        public ColorBCH(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorBCH(double B, double C, double H, Whitepoint wp)
            : base(new Colorspace(wp), B, C, H)
        { }
    }
}
