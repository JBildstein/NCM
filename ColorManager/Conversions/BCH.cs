using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_DEF_BCH : ConversionPath<ColorDEF, ColorBCH>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }
        
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            //inColor[2]^2
            data.Vars[0] = inColor[2] * inColor[2];
            //inColor[1]^2
            data.Vars[1] = inColor[1] * inColor[1];
            
            //B
            outColor[0] = Math.Sqrt(inColor[0] * inColor[0] + data.Vars[1] + data.Vars[0]);

            //C
            if (Math.Abs(outColor[0]) < Const.Delta) outColor[1] = 0;
            else
            {
                outColor[1] = Math.Sqrt(inColor[1] * inColor[1] + data.Vars[0]) / outColor[0];
                if (inColor[2] < 0) outColor[1] = Math.Asin(-outColor[1]);
                else outColor[1] = Math.Asin(outColor[1]);
            }
            //H
            data.Vars[2] = Math.Sqrt(data.Vars[1] + data.Vars[0]);
            if (Math.Abs(inColor[1]) < Const.Delta) outColor[2] = 0;
            else
            {
                outColor[2] = inColor[1] / data.Vars[2];
                if (inColor[2] < 0) outColor[2] = Math.Acos(-outColor[2]) * Const.Pi180_1;
                else outColor[2] = Math.Acos(outColor[2]) * Const.Pi180_1;
            }
        }
    }

    public sealed unsafe class Path_BCH_DEF : ConversionPath<ColorBCH, ColorDEF>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.Vars[0] = inColor[0] * Math.Sin(inColor[1]);
            outColor[0] = inColor[0] * Math.Sin(Const.Pi2 - inColor[1]);
            data.Vars[1] = inColor[2] * Const.Pi180;
            outColor[1] = data.Vars[0] * Math.Cos(data.Vars[1]);
            outColor[2] = data.Vars[0] * Math.Sin(data.Vars[1]);
        }
    }
}
