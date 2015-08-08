using System;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an DonRGB4 colorspace
    /// </summary>
    public sealed class Colorspace_DonRGB4 : ColorspaceRGB
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public override string Name
        {
            get { return "DonRGB4"; }
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
            get { return new double[] { 0.696, 0.3 }; }
        }
        /// <summary>
        /// Green primary
        /// </summary>
        public override double[] Cg
        {
            get { return new double[] { 0.215, 0.765 }; }
        }
        /// <summary>
        /// Blue primary
        /// </summary>
        public override double[] Cb
        {
            get { return new double[] { 0.13, 0.035 }; }
        }
        /// <summary>
        /// The conversion matrix (3x3)
        /// </summary>
        public override double[] CM
        {
            get { return new double[] { 0.6457711, 0.1933511, 0.1250978, 0.2783496, 0.6879702, 0.0336802, 0.0037113, 0.0179861, 0.8035125 }; }
        }
        /// <summary>
        /// The inverse conversion matrix (3x3)
        /// </summary>
        public override double[] ICM
        {
            get { return new double[] { 1.7603902, -0.4881198, -0.2536126, -0.7126288, 1.6527432, 0.0416715, 0.0078207, -0.0347411, 1.2447743 }; }
        }

        private const double g = 2.19921875d;
        private const double g1 = 1 / g;

        private static readonly Whitepoint wp = new WhitepointD50();

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace_DonRGB4"/> class
        /// </summary>
        public Colorspace_DonRGB4()
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
