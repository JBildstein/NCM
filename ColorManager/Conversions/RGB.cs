
namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorRGB"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_RGB : ConversionPath<ColorXYZ, ColorRGB>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorRGB"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            UMath.MultiplyMatrix_3x3_3x1((double*)data.OutSpaceData, inColor, outColor);
            if (outColor[0] < 0) outColor[0] = 0;
            if (outColor[1] < 0) outColor[1] = 0;
            if (outColor[2] < 0) outColor[2] = 0;
            data.OutTransform(outColor, outColor);
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorRGB"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_RGB_XYZ : ConversionPath<ColorRGB, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorRGB"/> to <see cref="ColorXYZ"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.InTransform(inColor, outColor);
            UMath.MultiplyMatrix_3x3_3x1((double*)data.InSpaceData, outColor, outColor);
        }
    }
}
