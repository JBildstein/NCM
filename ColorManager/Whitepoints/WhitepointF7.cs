using System;

namespace ColorManager
{
    public sealed class WhitepointF7 : Whitepoint
    {
        public override string Name { get { return "F7"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 0.95041;
        private const double y = 1.0;
        private const double z = 1.08747;
        private const double cx = 0.31292;
        private const double cy = 0.32933;
    }
}
