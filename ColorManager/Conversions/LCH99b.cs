using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLab"/> to <see cref="ColorLCH99b"/>
    /// </summary>
    public sealed unsafe class Path_Lab_LCH99b : ConversionPath<ColorLab, ColorLCH99b>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLab"/> to <see cref="ColorLCH99b"/>
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
            outColor[0] = Const.LCH99b_L1 * Math.Log(1d + Const.LCH99b_L2 * inColor[0]);                                                            //L
            data.Vars[1] = inColor[1] * Const.cos26 + inColor[2] * Const.sin26;                                                                     //e
            data.Vars[2] = Const.LCH99b_f * (-inColor[1] * Const.sin26 + inColor[2] * Const.cos26);                                                 //f
            outColor[1] = Math.Log(1d + Const.LCH99b_CG * Math.Sqrt(data.Vars[1] * data.Vars[1] + data.Vars[2] * data.Vars[2])) * Const.LCH99b_Cd;  //C
            outColor[2] = (Math.Atan2(data.Vars[2], data.Vars[1]) * Const.Pi180_1) + Const.LCH99b_angle;                                            //H
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLCH99b"/> to <see cref="ColorLab"/>
    /// </summary>
    public sealed unsafe class Path_LCH99b_Lab : ConversionPath<ColorLCH99b, ColorLab>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLCH99b"/> to <see cref="ColorLab"/>
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
            data.Vars[0] = (Math.Exp(inColor[1] / Const.LCH99b_Cd) - 1) / Const.LCH99b_CG;  //G
            data.Vars[3] = (inColor[2] - Const.LCH99b_angle) * Const.Pi180;
            data.Vars[1] = data.Vars[0] * Math.Cos(data.Vars[3]);                           //e
            data.Vars[2] = (data.Vars[0] * Math.Sin(data.Vars[3])) / Const.LCH99b_f;        //f
            outColor[1] = data.Vars[1] * Const.cos26 - data.Vars[2] * Const.sin26;
            outColor[2] = data.Vars[1] * Const.sin26 + data.Vars[2] * Const.cos26;
            outColor[0] = (Math.Exp(inColor[0] / Const.LCH99b_L1) - 1) / Const.LCH99b_L2;
        }
    }
}
