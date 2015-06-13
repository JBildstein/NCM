using System;

namespace ColorManager
{
    public sealed class WhitepointD75 : Whitepoint
    {
        public override string Name { get { return "D75"; } }

        public override double X { get { return x; } }
        public override double Y { get { return y; } }
        public override double Z { get { return z; } }

        public override double Cx { get { return cx; } }
        public override double Cy { get { return cy; } }

        private const double x = 0.94972;
        private const double y = 1.0;
        private const double z = 1.22638;
        private const double cx = 0.29902;
        private const double cy = 0.31485;
    }
}
