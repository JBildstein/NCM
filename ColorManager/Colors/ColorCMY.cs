using ColorManager.ICC;

namespace ColorManager
{
    public class ColorCMY : Color
    {
        /// <summary>
        /// Cyan-Channel
        /// </summary>
        public double C
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// Magenta-Channel
        /// </summary>
        public double M
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// Yellow-Channel
        /// </summary>
        public double Y
        {
            get { return this[2]; }
            set { this[2] = value; }
        }
        //TODO: check min/max values of ColorCMY
        public override string Name
        {
            get { return "CMY"; }
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
            get { return new string[] { "C", "M", "Y" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Cyan", "Magenta", "Yellow" }; }
        }
        
        public ColorCMY(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorCMY(double C, double M, double Y, ICCProfile profile)
            : base(new ColorspaceICC(profile), C, M, Y)
        { }

        public ColorCMY(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorCMY(double C, double M, double Y, ColorspaceICC space)
            : base(space, C, M, Y)
        { }
    }
}
