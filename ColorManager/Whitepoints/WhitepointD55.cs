using System;

namespace ColorManager
{
    public sealed class WhitepointD55 : Whitepoint
    {
        public override string Name { get { return "D55"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 0.95682;
        private const double y = 1.0;
        private const double z = 0.92149;
        private const double cx = 0.33242;
        private const double cy = 0.34743;
    }
}
