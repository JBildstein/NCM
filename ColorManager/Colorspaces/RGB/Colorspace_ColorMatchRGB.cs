using System;

namespace ColorManager
{
    public sealed class Colorspace_ColorMatchRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "ColorMatchRGB"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.63, 0.34 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.295, 0.605 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.15, 0.075 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.5093439, 0.3209071, 0.1339691, 0.2748840, 0.6581315, 0.0669845, 0.0242545, 0.1087821, 0.6921735 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 2.6422874, -1.2234270, -0.3930143, -1.1119763, 2.0590183, 0.0159614, 0.0821699, -0.2807254, 1.4559877 }; }
        }

        private const double g = 1.8d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_ColorMatchRGB()
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
