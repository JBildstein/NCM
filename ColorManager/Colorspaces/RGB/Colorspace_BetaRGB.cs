using System;

namespace ColorManager
{
    public sealed class Colorspace_BetaRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "BetaRGB"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.6888, 0.3112 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.1986, 0.7551 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.1265, 0.0352 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.6712537, 0.1745834, 0.1183829, 0.3032726, 0.6637861, 0.0329413, 0.0, 0.040701, 0.784509 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 1.683227, -0.4282363, -0.2360185, -0.7710229, 1.7065571, 0.04469, 0.0400013, -0.0885376, 1.272364 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_BetaRGB()
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
