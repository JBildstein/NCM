using System;

namespace ColorManager
{
    public sealed class Colorspace_BruceRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "NTSCRGB"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.64, 0.33 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.28, 0.65 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.15, 0.06 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.4674162, 0.2944512, 0.1886026, 0.2410115, 0.6835475, 0.075441, 0.0219101, 0.0736128, 0.9933071 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 2.7454669, -1.1358136, -0.4350269, -0.969266, 1.8760108, 0.041556, 0.0112723, -0.1139754, 1.0132541 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        public Colorspace_BruceRGB()
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
