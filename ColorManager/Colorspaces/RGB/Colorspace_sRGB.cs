using System;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an sRGB colorspace
    /// </summary>
    public sealed class Colorspace_sRGB : ColorspaceRGB
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public override string Name
        {
            get { return "sRGB"; }
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
            get { return new double[] { 0.3, 0.6 }; }
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
            get { return new double[] { 0.4124564, 0.3575761, 0.1804375, 0.2126729, 0.7151522, 0.072175, 0.0193339, 0.119192, 0.9503041 }; }
        }
        /// <summary>
        /// The inverse conversion matrix (3x3)
        /// </summary>
        public override double[] ICM
        {
            get { return new double[] { 3.2404542, -1.5371385, -0.4985314, -0.969266, 1.8760108, 0.041556, 0.0556434, -0.2040259, 1.0572252 }; }
        }

        private const double g = 2.4;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD65();

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace_sRGB"/> class
        /// </summary>
        public Colorspace_sRGB()
            : base(wp)
        { }

        /// <summary>
        /// Linearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to non-Linear input values</param>
        /// <param name="outVal">Pointer to linear output values</param>
        public unsafe override void ToLinear(double* inVal, double* outVal)
        {
            if (inVal[0] > 0.04045) { outVal[0] = Math.Pow((inVal[0] + 0.055) / 1.055, g); }
            else { outVal[0] = (inVal[0] / 12.92); }

            if (inVal[1] > 0.04045) { outVal[1] = Math.Pow((inVal[1] + 0.055) / 1.055, g); }
            else { outVal[1] = (inVal[1] / 12.92); }

            if (inVal[2] > 0.04045) { outVal[2] = Math.Pow((inVal[2] + 0.055) / 1.055, g); }
            else { outVal[2] = (inVal[2] / 12.92); }
        }

        /// <summary>
        /// Delinearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to linear input values</param>
        /// <param name="outVal">Pointer to non-Linear output values</param>
        public unsafe override void ToNonLinear(double* inVal, double* outVal)
        {
            if (inVal[0] <= 0.0031308) { outVal[0] = 12.92 * inVal[0]; }
            else { outVal[0] = 1.055 * Math.Pow(inVal[0], g1) - 0.055; }

            if (inVal[1] <= 0.0031308) { outVal[1] = 12.92 * inVal[1]; }
            else { outVal[1] = 1.055 * Math.Pow(inVal[1], g1) - 0.055; }

            if (inVal[2] <= 0.0031308) { outVal[2] = 12.92 * inVal[2]; }
            else { outVal[2] = 1.055 * Math.Pow(inVal[2], g1) - 0.055; }
        }
    }
}
