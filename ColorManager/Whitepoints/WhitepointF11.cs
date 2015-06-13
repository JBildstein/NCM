using System;

namespace ColorManager
{
    public sealed class WhitepointF11 : Whitepoint
    {
        public override string Name { get { return "F11"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 1.00962;
        private const double y = 1.0;
        private const double z = 0.6435;
        private const double cx = 0.38052;
        private const double cy = 0.37713;
    }
}
