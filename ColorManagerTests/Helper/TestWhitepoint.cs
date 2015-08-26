using ColorManager;

namespace ColorManagerTests
{
    public sealed class TestWhitepoint : Whitepoint
    {
        public override string Name
        {
            get { return "Test"; }
        }
        public override double Cx
        {
            get { return _Cx; }
        }
        public override double Cy
        {
            get { return _Cy; }
        }
        public override double X
        {
            get { return _X; }
        }
        public override double Y
        {
            get { return _Y; }
        }
        public override double Z
        {
            get { return _Z; }
        }

        private double _X, _Y, _Z, _Cx, _Cy;

        public TestWhitepoint()
        {
            _X = _Y = _Z = _Cx = _Cy = 0.5;
        }

        public TestWhitepoint(double X, double Y, double Z, double Cx, double Cy)
        {
            _X = X;
            _Y = Y;
            _Z = Z;
            _Cx = Cx;
            _Cy = Cy;
        }
    }
}
