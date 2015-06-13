using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_Lab_LCHab : ConversionPath<ColorLab, ColorLCHab>
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

    public sealed unsafe class Path_LCHab_Lab : ConversionPath<ColorLCHab, ColorLab>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = inColor[0];
            outColor[1] = inColor[1] * Math.Cos(inColor[2] * Const.Pi180);
            outColor[2] = inColor[1] * Math.Sin(inColor[2] * Const.Pi180);
        }
    }
}
