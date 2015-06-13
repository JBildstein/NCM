using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_Lab_Gray : ConversionPath<ColorLab, ColorGray>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }
        
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = inColor[0] / 100d;
            data.OutTransform(outColor, outColor);//Apply gamma
        }
    }

    public sealed unsafe class Path_Gray_Lab : ConversionPath<ColorGray, ColorLab>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.InTransform(inColor, outColor);//Linearize gamma
            outColor[0] *= 100;
            outColor[1] = outColor[2] = 0;
        }
    }
}
