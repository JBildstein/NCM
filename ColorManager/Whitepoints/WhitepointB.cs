using System;

namespace ColorManager
{
    public sealed class WhitepointB : Whitepoint
    {
        public override string Name { get { return "B"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 0.99072;
        private const double y = 1.0;
        private const double z = 0.85223;
        private const double cx = 0.34842;
        private const double cy = 0.35161;
    }
}
