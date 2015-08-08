namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an E whitepoint
    /// </summary>
    public sealed class WhitepointE : Whitepoint
    {
        /// <summary>
        /// The name of this whitepoint
        /// </summary>
        public override string Name { get { return "E"; } }

        /// <summary>
        /// X value
        /// </summary>
        public override double X { get { return x; } }
        /// <summary>
        /// Y value
        /// </summary>
        public override double Y { get { return y; } }
        /// <summary>
        /// Z value
        /// </summary>
        public override double Z { get { return z; } }

        /// <summary>
        /// x chromaticity value
        /// </summary>
        public override double Cx { get { return cx; } }
        /// <summary>
        /// y chromaticity value
        /// </summary>
        public override double Cy { get { return cy; } }

        private const double x = 1.0;
        private const double y = 1.0;
        private const double z = 1.0;
        private const double cx = 1.0 / 3d;
        private const double cy = 1.0 / 3d;
    }
}
