
namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorDEF"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_DEF : ConversionPath<ColorXYZ, ColorDEF>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorDEF"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert, new double[][] { XYZ_DEF_Matrix }) }; }
        }

        private static double[] XYZ_DEF_Matrix = { 0.2053, 0.7125, 0.4670, 1.8537, -1.2797, -0.4429, -0.3655, 1.0120, -0.6104 };

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        /// <param name="matrix">The matrix that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data, double** matrix)
        {
            UMath.MultiplyMatrix_3x3_3x1(matrix[0], inColor, outColor);
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorDEF"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_DEF_XYZ : ConversionPath<ColorDEF, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorDEF"/> to <see cref="ColorXYZ"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert, new double[][] { DEF_XYZ_Matrix }) }; }
        }

        private static double[] DEF_XYZ_Matrix = { 0.671203, 0.495489, 0.153997, 0.706165, 0.0247732, 0.522292, 0.768864, -0.255621, -0.864558 };

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        /// <param name="matrix">The matrix that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data, double** matrix)
        {
            UMath.MultiplyMatrix_3x3_3x1(matrix[0], inColor, outColor);
        }
    }
}
