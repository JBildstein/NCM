using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorLCH99c"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_LCH99c : ConversionPath<ColorXYZ, ColorLCH99c>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorLCH99c"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get
            {
                return new IConversionCommand[]
                {
                    new CC_ExecuteMethod(Adapt),
                    new CC_Convert(From, typeof(ColorLab)),
                    new CC_ExecuteMethod(Convert),
                };
            }
        }

        /// <summary>
        /// The adaption method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        public static void Adapt(double* inColor, double* outColor)
        {
            //X'= 1.1 * X − 0.1 * Z
            outColor[0] = inColor[0] * 1.1 - inColor[2] * 0.1;
            outColor[1] = inColor[1];
            outColor[2] = inColor[2];
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = Const.LCH99c_L1 * Math.Log(1 + Const.LCH99c_L2 * inColor[0]);                                                         //L
            data.Vars[0] = Const.LCH99c_f * inColor[2];                                                                                         //f
            outColor[1] = Math.Log(1 + Const.LCH99c_CG * Math.Sqrt(inColor[1] * inColor[1] + data.Vars[0] * data.Vars[0])) * Const.LCH99c_Cd;   //C
            outColor[2] = Math.Atan2(data.Vars[0], inColor[1]) * Const.Pi180_1;                                                                 //H
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLCH99c"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_LCH99c_XYZ : ConversionPath<ColorLCH99c, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLCH99c"/> to <see cref="ColorXYZ"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get
            {
                return new IConversionCommand[]
                {
                    new CC_ExecuteMethod(Convert),
                    new CC_Convert(typeof(ColorLab), To),
                    new CC_ExecuteMethod(Adapt),
                };
            }
        }

        /// <summary>
        /// The adaption method
        /// </summary>
        /// <param name="outColor">The pointer to the output color values</param>
        public static void Adapt(double* outColor)
        {
            const double div1_11 = 1 / 1.1;
            outColor[0] = (outColor[0] + 0.1 * outColor[2]) * div1_11;
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            const double div1_Cd = 1 / Const.LCH99c_Cd;
            const double div1_CG = 1 / Const.LCH99c_CG;
            const double div1_f = 1 / Const.LCH99c_f;
            const double div1_L1 = 1 / Const.LCH99c_L1;
            const double div1_L2 = 1 / Const.LCH99c_L2;

            data.Vars[0] = (Math.Exp(inColor[1] * div1_Cd) - 1) * div1_CG;  //G
            data.Vars[2] = inColor[2] * Const.Pi180;
            outColor[1] = data.Vars[0] * Math.Cos(data.Vars[2]);            //e
            data.Vars[1] = data.Vars[0] * Math.Sin(data.Vars[2]);           //f
            outColor[2] = data.Vars[1] * div1_f;
            outColor[0] = (Math.Exp(inColor[0] * div1_L1) - 1) * div1_L2;
        }
    }
}
