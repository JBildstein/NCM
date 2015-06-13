using System;

namespace ColorManager
{
    public sealed class Colorspace_CIERGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "CIERGB"; }
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
            get { return new double[] { 0.274, 0.717 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.167, 0.009 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.488718, 0.3106803, 0.2006017, 0.1762044, 0.8129847, 0.0108109, 0.0, 0.0102048, 0.9897952 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 2.3706743, -0.9000405, -0.4706338, -0.513885, 1.4253036, 0.0885814, 0.0052982, -0.0146949, 1.0093968 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointE();

        public Colorspace_CIERGB()
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
