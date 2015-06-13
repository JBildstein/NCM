using System;

namespace ColorManager
{
    public sealed class Colorspace_BestRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "BestRGB"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.7347, 0.2653 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.215, 0.775 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.13, 0.035 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.6326696, 0.2045558, 0.1269946, 0.2284569, 0.7373523, 0.0341908, 0.0000000, 0.0095142, 0.8156958 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 1.7552599, -0.4836786, -0.2530000, -0.5441336, 1.5068789, 0.0215528, 0.0063467, -0.0175761, 1.2256959 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_BestRGB()
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
