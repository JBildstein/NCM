using System;

namespace ColorManager
{
    public sealed class Colorspace_ProPhotoRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "ProPhotoRGB"; }
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
            get { return new double[] { 0.1596, 0.8404 }; }
        }
        public override double[] Cb
        {
            get { return new double[] { 0.0366, 0.0001 }; }
        }
        public override double[] CM
        {
            get { return new double[] { 0.7976749, 0.1351917, 0.0313534, 0.2880402, 0.7118741, 0.0000857, 0.0, 0.0, 0.8252100 }; }
        }
        public override double[] ICM
        {
            get { return new double[] { 1.3459433, -0.2556075, -0.0511118, -0.5445989, 1.5081673, 0.0205351, 0.0, 0.0, 1.2118128 }; }
        }

        private const double g = 1.8d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        public Colorspace_ProPhotoRGB()
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
