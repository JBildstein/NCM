
namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLab"/> to <see cref="ColorGray"/>
    /// </summary>
    public sealed unsafe class Path_Lab_Gray : ConversionPath<ColorLab, ColorGray>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLab"/> to <see cref="ColorGray"/>
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
            outColor[0] = inColor[0] / 100d;
            data.OutTransform(outColor, outColor);//Apply gamma
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorGray"/> to <see cref="ColorLab"/>
    /// </summary>
    public sealed unsafe class Path_Gray_Lab : ConversionPath<ColorGray, ColorLab>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorGray"/> to <see cref="ColorLab"/>
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
            data.InTransform(inColor, outColor);//Linearize gamma
            outColor[0] *= 100;
            outColor[1] = outColor[2] = 0;
        }
    }
}
