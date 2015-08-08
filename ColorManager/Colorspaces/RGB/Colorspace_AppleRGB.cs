using System;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an AppleRGB colorspace
    /// </summary>
    public sealed class Colorspace_AppleRGB : ColorspaceRGB
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public override string Name
        {
            get { return "AppleRGB"; }
        }
        /// <summary>
        /// The gamma value
        /// </summary>
        public override double Gamma
        {
            get { return g; }
        }
        /// <summary>
        /// Red primary
        /// </summary>
        public override double[] Cr
        {
            get { return new double[] { 0.625, 0.34 }; }
        }
        /// <summary>
        /// Green primary
        /// </summary>
        public override double[] Cg
        {
            get { return new double[] { 0.28, 0.595 }; }
        }
        /// <summary>
        /// Blue primary
        /// </summary>
        public override double[] Cb
        {
            get { return new double[] { 0.155, 0.07 }; }
        }
        /// <summary>
        /// The conversion matrix (3x3)
        /// </summary>
        public override double[] CM
        {
            get { return new double[] { 0.4497288, 0.3162486, 0.1844926, 0.2446525, 0.6720283, 0.0833192, 0.0251848, 0.1411824, 0.9224628 }; }
        }
        /// <summary>
        /// The inverse conversion matrix (3x3)
        /// </summary>
        public override double[] ICM
        {
            get { return new double[] { 2.9515373, -1.2894116, -0.4738445, -1.0851093, 1.9908566, 0.0372026, 0.0854934, -0.2694964, 1.0912975 }; }
        }

        private const double g = 1.8d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace_AppleRGB"/> class
        /// </summary>
        public Colorspace_AppleRGB()
            : base(wp)
        { }

        /// <summary>
        /// Linearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to non-Linear input values</param>
        /// <param name="outVal">Pointer to linear output values</param>
        public unsafe override void ToLinear(double* inVal, double* outVal)
        {
            outVal[0] = Math.Pow(inVal[0], g);
            outVal[1] = Math.Pow(inVal[1], g);
            outVal[2] = Math.Pow(inVal[2], g);
        }

        /// <summary>
        /// Delinearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to linear input values</param>
        /// <param name="outVal">Pointer to non-Linear output values</param>
        public unsafe override void ToNonLinear(double* inVal, double* outVal)
        {
            outVal[0] = Math.Pow(inVal[0], g1);
            outVal[1] = Math.Pow(inVal[1], g1);
            outVal[2] = Math.Pow(inVal[2], g1);
        }
    }
}
