using System;

namespace ColorManager
{
    public sealed class Colorspace_SMPTE_C_RGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "SMPTE_C_RGB"; }
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
            get { return new double[] { 0.31, 0.595 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.155, 0.07 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.3935891, 0.3652497, 0.1916313, 0.2124132, 0.7010437, 0.0865432, 0.0187423, 0.1119313, 0.9581563 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 3.505396, -1.7394894, -0.5439640, -1.0690722, 1.9778245, 0.0351722, 0.05632, -0.1970226, 1.0502026 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        public Colorspace_SMPTE_C_RGB()
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
