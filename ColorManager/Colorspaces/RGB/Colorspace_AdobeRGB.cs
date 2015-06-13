using System;

namespace ColorManager
{
    public sealed class Colorspace_AdobeRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "AdobeRGB"; }
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
            get { return new double[] { 0.21, 0.71 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.15, 0.06 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.5767309, 0.185554, 0.1881852, 0.2973769, 0.6273491, 0.0752741, 0.0270343, 0.0706872, 0.9911085 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 2.041369, -0.5649464, -0.3446944, -0.969266, 1.8760108, 0.041556, 0.0134474, -0.1183897, 1.0154096 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        public Colorspace_AdobeRGB()
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
