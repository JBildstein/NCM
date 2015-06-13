
namespace ColorManager
{
    public sealed class ColorBef : Color
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
        /// e-Channel
        /// </summary>
        public double e
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// f-Channel
        /// </summary>
        public double f
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public override string Name
        {
            get { return "Bef"; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, -1.0, -1.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 2.0, 1.0, 1.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "B", "e", "f" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "B", "e", "f" }; }
        }

        public ColorBef()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorBef(double B, double e, double f)
            : base(Colorspace.Default, B, e, f)
        { }
        
        public ColorBef(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorBef(double B, double e, double f, Whitepoint wp)
            : base(new Colorspace(wp), B, e, f)
        { }
    }
}
