using System;

namespace ColorManager
{
    public sealed class WhitepointE : Whitepoint
    {
        public override string Name { get { return "E"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 1.0;
        private const double y = 1.0;
        private const double z = 1.0;
        private const double cx = 1.0 / 3d;
        private const double cy = 1.0 / 3d;
    }
}
