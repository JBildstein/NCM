using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorLCH99d"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_LCH99d : ConversionPath<ColorXYZ, ColorLCH99d>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorLCH99d"/>
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
            //X'= 1.12 * X − 0.12 * Z
            outColor[0] = inColor[0] * 1.12 - inColor[2] * 0.12;
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
            double* vs = data.Vars;

            outColor[0] = Const.LCH99d_L1 * Math.Log(1 + Const.LCH99d_L2 * inColor[0]);                                 //L
            vs[0] = inColor[1] * Const.cos50 + inColor[2] * Const.sin50;                                                //e
            vs[1] = Const.LCH99d_f * (-inColor[1] * Const.sin50 + inColor[2] * Const.cos50);                            //f
            outColor[1] = Math.Log(1 + Const.LCH99d_CG * Math.Sqrt(vs[0] * vs[0] + vs[1] * vs[1])) * Const.LCH99d_Cd;   //C
            outColor[2] = (Math.Atan2(vs[1], vs[0]) * Const.Pi180_1) + Const.LCH99d_angle;                              //H
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLCH99d"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_LCH99d_XYZ : ConversionPath<ColorLCH99d, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLCH99d"/> to <see cref="ColorXYZ"/>
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
            const double div1_112 = 1 / 1.12;
            outColor[0] = (outColor[0] + 0.12 * outColor[2]) * div1_112;
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            const double div1_Cd = 1 / Const.LCH99d_Cd;
            const double div1_CG = 1 / Const.LCH99d_CG;
            const double div1_f = 1 / Const.LCH99d_f;
            const double div1_L1 = 1 / Const.LCH99d_L1;
            const double div1_L2 = 1 / Const.LCH99d_L2;
            
            double* vs = data.Vars;

            vs[0] = (Math.Exp(inColor[1] * div1_Cd) - 1) * div1_CG;     //G
            vs[3] = (inColor[2] - Const.LCH99d_angle) * Const.Pi180;
            vs[1] = vs[0] * Math.Cos(vs[3]);                            //e
            vs[2] = vs[0] * Math.Sin(vs[3]);                            //f
            outColor[1] = vs[1] * Const.cos50 - (vs[2] * div1_f) * Const.sin50;
            outColor[2] = vs[1] * Const.sin50 + (vs[2] * div1_f) * Const.cos50;
            outColor[0] = (Math.Exp(inColor[0] * div1_L1) - 1) * div1_L2;
        }
    }
}
