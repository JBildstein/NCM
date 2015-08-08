using System;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an ColorMatchRGB colorspace
    /// </summary>
    public sealed class Colorspace_ColorMatchRGB : ColorspaceRGB
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public override string Name
        {
            get { return "ColorMatchRGB"; }
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
            get { return new double[] { 0.63, 0.34 }; }
        }
        /// <summary>
        /// Green primary
        /// </summary>
        public override double[] Cg
        {
            get { return new double[] { 0.295, 0.605 }; }
        }
        /// <summary>
        /// Blue primary
        /// </summary>
        public override double[] Cb
        {
            get { return new double[] { 0.15, 0.075 }; }
        }
        /// <summary>
        /// The conversion matrix (3x3)
        /// </summary>
        public override double[] CM
        {
            get { return new double[] { 0.5093439, 0.3209071, 0.1339691, 0.2748840, 0.6581315, 0.0669845, 0.0242545, 0.1087821, 0.6921735 }; }
        }
        /// <summary>
        /// The inverse conversion matrix (3x3)
        /// </summary>
        public override double[] ICM
        {
            get { return new double[] { 2.6422874, -1.2234270, -0.3930143, -1.1119763, 2.0590183, 0.0159614, 0.0821699, -0.2807254, 1.4559877 }; }
        }

        private const double g = 1.8d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace_ColorMatchRGB"/> class
        /// </summary>
        public Colorspace_ColorMatchRGB()
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
