
namespace ColorManager
{
    public sealed class ColorDEF : Color
    {
        /// <summary>
        /// D-Channel
        /// </summary>
        public double D
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// E-Channel
        /// </summary>
        public double E
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// F-Channel
        /// </summary>
        public double F
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public override string Name
        {
            get { return "DEF"; }
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
            get { return new string[] { "D", "E", "F" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "D", "E", "F" }; }
        }

        public ColorDEF()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public ColorDEF(double D, double E, double F)
            : base(Colorspace.Default, D, E, F)
        { }
        
        public ColorDEF(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        public ColorDEF(double D, double E, double F, Whitepoint wp)
            : base(new Colorspace(wp), D, E, F)
        { }
    }
}
