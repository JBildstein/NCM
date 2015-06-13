using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_RGB_HSL : ConversionPath<ColorRGB, ColorHSL>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }
        
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            //Max = data.Vars[0] and Min = data.Vars[1]
            UMath.MinMax_3(inColor, data.Vars);

            if (Math.Round(data.Vars[0], 6) == Math.Round(data.Vars[1], 6)) { outColor[1] = outColor[0] = 0; }
            else
            {
                if ((data.Vars[0] + data.Vars[1]) / 2d <= 0.5d) { outColor[1] = (data.Vars[0] - data.Vars[1]) / (data.Vars[0] + data.Vars[1]); }
                else { outColor[1] = (data.Vars[0] - data.Vars[1]) / (2 - data.Vars[0] - data.Vars[1]); }

                if (inColor[0] == data.Vars[0]) { outColor[0] = (inColor[1] - inColor[2]) / (data.Vars[0] - data.Vars[1]); }
                else if (inColor[1] == data.Vars[0]) { outColor[0] = 2d + (inColor[2] - inColor[0]) / (data.Vars[0] - data.Vars[1]); }
                else { outColor[0] = 4d + (inColor[0] - inColor[1]) / (data.Vars[0] - data.Vars[1]); }    //inColor[2] == max
                outColor[0] *= 60d;
            }
            outColor[2] = (data.Vars[0] + data.Vars[1]) / 2d;
        }
    }

    public sealed unsafe class Path_HSL_RGB : ConversionPath<ColorHSL, ColorRGB>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            if (Math.Abs(inColor[1]) < Const.Delta)
            {
                outColor[0] = inColor[2];
                outColor[1] = inColor[2];
                outColor[2] = inColor[2];
            }
            else
            {
                if (inColor[2] < 0.5d) { data.Vars[0] = inColor[2] * (1d + inColor[1]); }
                else { data.Vars[0] = (inColor[2] + inColor[1]) - (inColor[1] * inColor[2]); }

                data.Vars[1] = 2d * inColor[2] - data.Vars[0];
                
                if (inColor[0] > 360d) { inColor[0] -= 360d; }
                else if (inColor[0] < 0) { inColor[0] += 360d; }
                if (inColor[0] < 60d) { outColor[1] = data.Vars[1] + (data.Vars[0] - data.Vars[1]) * inColor[0] / 60d; }
                else if (inColor[0] < 180d) { outColor[1] = data.Vars[0]; }
                else if (inColor[0] < 240d) { outColor[1] = data.Vars[1] + (data.Vars[0] - data.Vars[1]) * (240d - inColor[0]) / 60d; }
                else outColor[1] = data.Vars[1];

                data.Vars[2] = inColor[0] - 120d;
                if (data.Vars[2] > 360d) { data.Vars[2] -= 360d; }
                else if (data.Vars[2] < 0) { data.Vars[2] += 360d; }
                if (data.Vars[2] < 60d) { outColor[2] = data.Vars[1] + (data.Vars[0] - data.Vars[1]) * data.Vars[2] / 60d; }
                else if (data.Vars[2] < 180d) { outColor[2] = data.Vars[0]; }
                else if (data.Vars[2] < 240d) { outColor[2] = data.Vars[1] + (data.Vars[0] - data.Vars[1]) * (240d - data.Vars[2]) / 60d; }
                else outColor[2] = data.Vars[1];

                data.Vars[2] = inColor[0] + 120d;
                if (data.Vars[2] > 360d) { data.Vars[2] -= 360d; }
                else if (data.Vars[2] < 0) { data.Vars[2] += 360d; }
                if (data.Vars[2] < 60d) { outColor[0] = data.Vars[1] + (data.Vars[0] - data.Vars[1]) * data.Vars[2] / 60d; }
                else if (data.Vars[2] < 180d) { outColor[0] = data.Vars[0]; }
                else if (data.Vars[2] < 240d) { outColor[0] = data.Vars[1] + (data.Vars[0] - data.Vars[1]) * (240d - data.Vars[2]) / 60d; }
                else outColor[0] = data.Vars[1];
            }
        }
    }
}
