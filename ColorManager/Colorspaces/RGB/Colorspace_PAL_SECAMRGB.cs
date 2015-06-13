using System;

namespace ColorManager
{
    public sealed class Colorspace_PAL_SECAMRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "PAL_SECAMRGB"; }
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
            get { return new double[] { 0.29, 0.6 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.15, 0.06 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.430619, 0.3415419, 0.1783091, 0.2220379, 0.7066384, 0.0713236, 0.0201853, 0.1295504, 0.9390944 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 3.0628971, -1.3931791, -0.4757517, -0.969266, 1.8760108, 0.041556, 0.0678775, -0.2288548, 1.069349 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        public Colorspace_PAL_SECAMRGB()
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
