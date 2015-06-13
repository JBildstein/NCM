using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_XYZ_LCH99c : ConversionPath<ColorXYZ, ColorLCH99c>
    {
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

        public static void Adapt(double* inColor, double* outColor)
        {
            //X'= 1.1 * X − 0.1 * Z
            outColor[0] = inColor[0] * 1.1 - inColor[2] * 0.1;
            outColor[1] = inColor[1];
            outColor[2] = inColor[2];
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = Const.LCH99c_L1 * Math.Log(1 + Const.LCH99c_L2 * inColor[0]);                                                         //L
            data.Vars[0] = Const.LCH99c_f * inColor[2];                                                                                         //f
            outColor[1] = Math.Log(1 + Const.LCH99c_CG * Math.Sqrt(inColor[1] * inColor[1] + data.Vars[0] * data.Vars[0])) * Const.LCH99c_Cd;   //C
            outColor[2] = Math.Atan2(data.Vars[0], inColor[1]) * Const.Pi180_1;                                                                 //H
        }
    }

    public sealed unsafe class Path_LCH99c_XYZ : ConversionPath<ColorLCH99c, ColorXYZ>
    {
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

        public static void Adapt(double* outColor)
        {
            outColor[0] = (outColor[0] + 0.1 * outColor[2]) / 1.1;
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.Vars[0] = (Math.Exp(inColor[1] / Const.LCH99c_Cd) - 1) / Const.LCH99c_CG;  //G
            data.Vars[2] = inColor[2] * Const.Pi180;
            outColor[1] = data.Vars[0] * Math.Cos(data.Vars[2]);                            //e
            data.Vars[1] = data.Vars[0] * Math.Sin(data.Vars[2]);                           //f
            outColor[2] = data.Vars[1] / Const.LCH99c_f;
            outColor[0] = (Math.Exp(inColor[0] / Const.LCH99c_L1) - 1) / Const.LCH99c_L2;
        }
    }
}
