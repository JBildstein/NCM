using System;

namespace ColorManager
{
    public sealed class Colorspace_EktaSpacePS5 : ColorspaceRGB
    {
        public override string Name
        {
            get { return "EktaSpacePS5"; }
        }
        public override double Gamma
        {
            get { return g; }
        }
        public override double[] Cr
        {
            get { return new double[] { 0.695, 0.305 }; }
        }
        public override double[] Cg
        {
            get { return new double[] { 0.26, 0.7 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.11, 0.005 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.5938914, 0.2729801, 0.0973485, 0.2606286, 0.7349465, 0.0044249, 0.0, 0.0419969, 0.7832131 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 2.0043819, -0.7304844, -0.2450052, -0.7110285, 1.6202126, 0.0792227, 0.0381263, -0.086878, 1.2725438 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_EktaSpacePS5()
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
