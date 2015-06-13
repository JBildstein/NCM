using System;

namespace ColorManager
{
    public sealed class WhitepointC : Whitepoint
    {
        public override string Name { get { return "C"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 0.98074;
        private const double y = 1.0;
        private const double z = 1.18232;
        private const double cx = 0.31006;
        private const double cy = 0.31616;
    }
}
