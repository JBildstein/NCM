namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an D55 whitepoint
    /// </summary>
    public sealed class WhitepointD55 : Whitepoint
    {
        /// <summary>
        /// The name of this whitepoint
        /// </summary>
        public override string Name { get { return "D55"; } }

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

        private const double x = 0.95682;
        private const double y = 1.0;
        private const double z = 0.92149;
        private const double cx = 0.33242;
        private const double cy = 0.34743;
    }
}
