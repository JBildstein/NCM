using System;

namespace ColorManager
{
    public sealed class WhitepointA : Whitepoint
    {
        public override string Name { get { return "A"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 1.0985;
        private const double y = 1.0;
        private const double z = 0.35585;
        private const double cx = 0.44757;
        private const double cy = 0.40745;
    }
}
