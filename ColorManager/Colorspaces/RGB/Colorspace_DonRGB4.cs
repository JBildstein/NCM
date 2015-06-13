using System;

namespace ColorManager
{
    public sealed class Colorspace_DonRGB4 : ColorspaceRGB
    {
        public override string Name
        {
            get { return "DonRGB4"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.696, 0.3 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.215, 0.765 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.13, 0.035 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.6457711, 0.1933511, 0.1250978, 0.2783496, 0.6879702, 0.0336802, 0.0037113, 0.0179861, 0.8035125 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 1.7603902, -0.4881198, -0.2536126, -0.7126288, 1.6527432, 0.0416715, 0.0078207, -0.0347411, 1.2447743 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_DonRGB4()
            : base(wp)
        { }

        public unsafe override void ToLinear(double* inVal, double* outVal)
        {
            outVal[0] = Math.Pow(inVal[0], g);
            outVal[1] = Math.Pow(inVal[1], g);
            outVal[2] = Math.Pow(inVal[2], g);
        }

        public unsafe override void ToNonLinear(double* inVal, double* outVal)
        {
            outVal[0] = Math.Pow(inVal[0], g1);
            outVal[1] = Math.Pow(inVal[1], g1);
            outVal[2] = Math.Pow(inVal[2], g1);
        }
    }
}
