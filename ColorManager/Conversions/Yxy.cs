using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_XYZ_Yxy : ConversionPath<ColorXYZ, ColorYxy>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            outColor[0] = inColor[1];
            data.Vars[0] = inColor[0] + inColor[1] + inColor[2];
            if (Math.Abs(data.Vars[0]) < Const.Delta)
            {
                outColor[1] = data.OutWPCr[0];
                outColor[2] = data.OutWPCr[1];
            }
            else
            {
                outColor[2] = inColor[1] / data.Vars[0];
                outColor[1] = inColor[0] / data.Vars[0];
            }
        }
    }

    public sealed unsafe class Path_Yxy_XYZ : ConversionPath<ColorYxy, ColorXYZ>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            if (Math.Abs(inColor[2]) < Const.Delta) outColor[0] = outColor[1] = outColor[2] = 0;
            else
            {
                outColor[1] = inColor[0];
                outColor[0] = (inColor[1] * inColor[0]) / inColor[2];
                outColor[2] = ((1 - inColor[1] - inColor[2]) * inColor[0]) / inColor[2];
            }
        }
    }
}
