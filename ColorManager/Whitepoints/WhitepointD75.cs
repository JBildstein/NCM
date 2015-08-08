namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an D75 whitepoint
    /// </summary>
    public sealed class WhitepointD75 : Whitepoint
    {
        /// <summary>
        /// The name of this whitepoint
        /// </summary>
        public override string Name { get { return "D75"; } }

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

        private const double x = 0.94972;
        private const double y = 1.0;
        private const double z = 1.22638;
        private const double cx = 0.29902;
        private const double cy = 0.31485;
    }
}
