using System;

namespace ColorManager
{
    public sealed class Colorspace_WideGamutRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "WideGamutRGB"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.735, 0.265 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.115, 0.826 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.157, 0.018 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.7161046, 0.1009296, 0.1471858, 0.2581874, 0.7249378, 0.0168748, 0.0, 0.0517813, 0.7734287 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 1.4628067, -0.1840623, -0.2743606, -0.5217933, 1.4472381, 0.0677227, 0.0349342, -0.096893, 1.2884099 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_WideGamutRGB()
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
