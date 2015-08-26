using System;
using ColorManager;

namespace ColorManagerTests
{
    public sealed class TestColorspaceRGB : ColorspaceRGB
    {
        public override string Name
        {
            get { return "Test"; }
        }
        public override double Gamma
        {
            get { return _Gamma; }
        }
        public override double[] Cr
        {
            get { return _Cr; }
        }
        public override double[] Cg
        {
            get { return _Cg; }
        }
        public override double[] Cb
        {
            get { return _Cb; }
        }

        public override double[] CM
        {
            get { throw new NotImplementedException(); }
        }
        public override double[] ICM
        {
            get { throw new NotImplementedException(); }
        }

        private double[] _Cr, _Cg, _Cb;
        private double _Gamma;

        public TestColorspaceRGB()
            : base(Whitepoint.Default)
        {
            _Cr = new double[] { 0.5, 0.5 };
            _Cg = new double[] { 0.5, 0.5 };
            _Cb = new double[] { 0.5, 0.5 };
            _Gamma = 2.2;
        }

        public TestColorspaceRGB(Whitepoint wp, double[] Cr, double[] Cg, double[] Cb, double Gamma)
            : base(wp)
        {
            _Cr = Cr;
            _Cg = Cg;
            _Cb = Cb;
            _Gamma = Gamma;
        }

        public override unsafe void ToLinear(double* inVal, double* outVal)
        {
            throw new NotImplementedException();
        }

        public override unsafe void ToNonLinear(double* inVal, double* outVal)
        {
            throw new NotImplementedException();
        }
    }
}
