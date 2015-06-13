using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_DEF_Bef : ConversionPath<ColorDEF, ColorBef>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }
        
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = Math.Sqrt(inColor[0] * inColor[0] + inColor[1] * inColor[1] + inColor[2] * inColor[2]);
            if (Math.Abs(outColor[0]) < Const.Delta) outColor[1] = outColor[2] = 0;
            else
            {
                outColor[1] = inColor[1] / outColor[0];
                outColor[2] = inColor[2] / outColor[0];
            }
        }
    }

    public sealed unsafe class Path_Bef_DEF : ConversionPath<ColorBef, ColorDEF>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[1] = inColor[1] * inColor[0];
            outColor[2] = inColor[2] * inColor[0];
            data.Vars[0] = inColor[0] * inColor[0] - outColor[1] * outColor[1] - outColor[2] * outColor[2];
            if (data.Vars[0] > 0) outColor[0] = Math.Sqrt(data.Vars[0]);
            else outColor[0] = 0;
        }
    }
}
