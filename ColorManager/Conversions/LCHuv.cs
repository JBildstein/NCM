using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_Luv_LCHuv : ConversionPath<ColorLuv, ColorLCHuv>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = inColor[0];
            outColor[1] = Math.Sqrt(inColor[1] * inColor[1] + inColor[2] * inColor[2]);
            outColor[2] = Math.Atan2(inColor[2], inColor[1]) * Const.Pi180_1;
        }
    }

    public sealed unsafe class Path_LCHuv_Luv : ConversionPath<ColorLCHuv, ColorLuv>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = Math.Abs(inColor[0]);
            outColor[1] = inColor[1] * Math.Cos(inColor[2] * Const.Pi180);
            outColor[2] = inColor[1] * Math.Sin(inColor[2] * Const.Pi180);
        }
    }
}
