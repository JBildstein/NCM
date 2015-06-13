using System;

namespace ColorManager
{
    public sealed class Colorspace_AppleRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "AppleRGB"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.625, 0.34 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.28, 0.595 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.155, 0.07 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.4497288, 0.3162486, 0.1844926, 0.2446525, 0.6720283, 0.0833192, 0.0251848, 0.1411824, 0.9224628 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 2.9515373, -1.2894116, -0.4738445, -1.0851093, 1.9908566, 0.0372026, 0.0854934, -0.2694964, 1.0912975 }; }
        }

        private const double g = 1.8d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        public Colorspace_AppleRGB()
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
