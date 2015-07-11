using ColorManager.ICC;

namespace ColorManager
{
    public class ColorCMYK : Color
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
        /// <summary>
        /// Key(Black)-Channel
        /// </summary>
        public double K
        {
            get { return this[3]; }
            set { this[3] = value; }
        }

        public override string Name
        {
            get { return "CMYK"; }
        }
        public override int ChannelCount
        {
            get { return 4; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0, 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 1.0, 1.0, 1.0, 1.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "C", "M", "Y", "K" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Cyan", "Magenta", "Yellow", "Key" }; }
        }

        public ColorCMYK(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0, 0)
        { }

        public ColorCMYK(double C, double M, double Y, double K, ICCProfile profile)
            : base(new ColorspaceICC(profile), C, M, Y, K)
        { }

        public ColorCMYK(ColorspaceICC space)
            : base(space, 0, 0, 0, 0)
        { }

        public ColorCMYK(double C, double M, double Y, double K, ColorspaceICC space)
            : base(space, C, M, Y, K)
        { }
    }
}
