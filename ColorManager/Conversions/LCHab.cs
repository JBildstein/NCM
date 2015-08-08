using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLab"/> to <see cref="ColorLCHab"/>
    /// </summary>
    public sealed unsafe class Path_Lab_LCHab : ConversionPath<ColorLab, ColorLCHab>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLab"/> to <see cref="ColorLCHab"/>
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
    /// Stores data about a conversion from <see cref="ColorLCHab"/> to <see cref="ColorLab"/>
    /// </summary>
    public sealed unsafe class Path_LCHab_Lab : ConversionPath<ColorLCHab, ColorLab>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLCHab"/> to <see cref="ColorLab"/>
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
            outColor[1] = inColor[1] * Math.Cos(inColor[2] * Const.Pi180);
            outColor[2] = inColor[1] * Math.Sin(inColor[2] * Const.Pi180);
        }
    }
}
