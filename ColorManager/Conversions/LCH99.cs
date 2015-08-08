using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLab"/> to <see cref="ColorLCH99"/>
    /// </summary>
    public sealed unsafe class Path_Lab_LCH99 : ConversionPath<ColorLab, ColorLCH99>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLab"/> to <see cref="ColorLCH99"/>
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
            outColor[0] = Const.LCH99_L1 * Math.Log(1 + Const.LCH99_L2 * inColor[0]);                                                           //L
            data.Vars[1] = inColor[1] * Const.cos16 + inColor[2] * Const.sin16;                                                                 //e
            data.Vars[2] = Const.LCH99_f * (-inColor[1] * Const.sin16 + inColor[2] * Const.cos16);                                              //f
            outColor[1] = Math.Log(1 + Const.LCH99_CG * Math.Sqrt(data.Vars[1] * data.Vars[1] + data.Vars[2] * data.Vars[2])) * Const.LCH99_Cd; //C
            outColor[2] = Math.Atan2(data.Vars[2], data.Vars[1]) * Const.Pi180_1;                                                               //H
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLCH99"/> to <see cref="ColorLab"/>
    /// </summary>
    public sealed unsafe class Path_LCH99_Lab : ConversionPath<ColorLCH99, ColorLab>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLCH99"/> to <see cref="ColorLab"/>
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
            data.Vars[0] = (Math.Exp(inColor[1] / Const.LCH99_Cd) - 1) / Const.LCH99_CG;    //G
            data.Vars[3] = inColor[2] * Const.Pi180;
            data.Vars[1] = data.Vars[0] * Math.Cos(data.Vars[3]);                           //e
            data.Vars[2] = (data.Vars[0] * Math.Sin(data.Vars[3])) / Const.LCH99_f;         //f
            outColor[1] = data.Vars[1] * Const.cos16 - data.Vars[2] * Const.sin16;
            outColor[2] = data.Vars[1] * Const.sin16 + data.Vars[2] * Const.cos16;
            outColor[0] = (Math.Exp(inColor[0] / Const.LCH99_L1) - 1) / Const.LCH99_L2;
        }
    }
}
