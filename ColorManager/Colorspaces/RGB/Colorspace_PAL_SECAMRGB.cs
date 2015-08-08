using System;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an PAL SECAMRGB colorspace
    /// </summary>
    public sealed class Colorspace_PAL_SECAMRGB : ColorspaceRGB
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public override string Name
        {
            get { return "PAL_SECAMRGB"; }
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
            get { return new double[] { 0.64, 0.33 }; }
        }
        /// <summary>
        /// Green primary
        /// </summary>
        public override double[] Cg
        {
            get { return new double[] { 0.29, 0.6 }; }
        }
        /// <summary>
        /// Blue primary
        /// </summary>
        public override double[] Cb
        {
            get { return new double[] { 0.15, 0.06 }; }
        }
        /// <summary>
        /// The conversion matrix (3x3)
        /// </summary>
        public override double[] CM
        {
            get { return new double[] { 0.430619, 0.3415419, 0.1783091, 0.2220379, 0.7066384, 0.0713236, 0.0201853, 0.1295504, 0.9390944 }; }
        }
        /// <summary>
        /// The inverse conversion matrix (3x3)
        /// </summary>
        public override double[] ICM
        {
            get { return new double[] { 3.0628971, -1.3931791, -0.4757517, -0.969266, 1.8760108, 0.041556, 0.0678775, -0.2288548, 1.069349 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace_PAL_SECAMRGB"/> class
        /// </summary>
        public Colorspace_PAL_SECAMRGB()
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
