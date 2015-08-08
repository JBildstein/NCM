using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLuv"/> to <see cref="ColorLCHuv"/>
    /// </summary>
    public sealed unsafe class Path_Luv_LCHuv : ConversionPath<ColorLuv, ColorLCHuv>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLuv"/> to <see cref="ColorLCHuv"/>
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
            outColor[0] = inColor[0];
            outColor[1] = Math.Sqrt(inColor[1] * inColor[1] + inColor[2] * inColor[2]);
            outColor[2] = Math.Atan2(inColor[2], inColor[1]) * Const.Pi180_1;
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLCHuv"/> to <see cref="ColorLuv"/>
    /// </summary>
    public sealed unsafe class Path_LCHuv_Luv : ConversionPath<ColorLCHuv, ColorLuv>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLCHuv"/> to <see cref="ColorLuv"/>
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
            outColor[0] = Math.Abs(inColor[0]);
            outColor[1] = inColor[1] * Math.Cos(inColor[2] * Const.Pi180);
            outColor[2] = inColor[1] * Math.Sin(inColor[2] * Const.Pi180);
        }
    }
}
