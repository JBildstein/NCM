using System;

namespace ColorManager
{
    public sealed class WhitepointD50 : Whitepoint
    {
        public override string Name { get { return "D50"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 0.96422;
        private const double y = 1.0;
        private const double z = 0.82521;
        private const double cx = 0.34567;
        private const double cy = 0.3585;
    }
}
