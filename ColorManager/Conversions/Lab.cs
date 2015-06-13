using System;

namespace ColorManager.Conversion
{
    public sealed unsafe class Path_XYZ_Lab : ConversionPath<ColorXYZ, ColorLab>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.Vars[0] = inColor[1] / data.OutWP[1];
            if (data.Vars[0] <= Const.Epsilon) data.Vars[0] = ((Const.Kappa * data.Vars[0]) + 16d) / 116d;
            else data.Vars[0] = Math.Pow(data.Vars[0], Const.div1_3);

            data.Vars[1] = inColor[0] / data.OutWP[0];
            if (data.Vars[1] <= Const.Epsilon) data.Vars[1] = ((Const.Kappa * data.Vars[1]) + 16d) / 116d;
            else data.Vars[1] = Math.Pow(data.Vars[1], Const.div1_3);

            data.Vars[2] = inColor[2] / data.OutWP[2];
            if (data.Vars[2] <= Const.Epsilon) data.Vars[2] = ((Const.Kappa * data.Vars[2]) + 16d) / 116d;
            else data.Vars[2] = Math.Pow(data.Vars[2], Const.div1_3);

            outColor[0] = 116d * data.Vars[0] - 16d;
            outColor[1] = 500d * (data.Vars[1] - data.Vars[0]);
            outColor[2] = 200d * (data.Vars[0] - data.Vars[2]);
        }
    }

    public sealed unsafe class Path_Lab_XYZ : ConversionPath<ColorLab, ColorXYZ>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.Vars[0] = (inColor[0] + 16d) / 116d;       //fy
            data.Vars[1] = data.Vars[0] + inColor[1] / 500d;//fx
            data.Vars[2] = data.Vars[0] - inColor[2] / 200d;//fz

            data.Vars[3] = data.Vars[1] * data.Vars[1] * data.Vars[1];//fx^3
            data.Vars[4] = data.Vars[2] * data.Vars[2] * data.Vars[2];//fz^3

            //Y
            if (inColor[0] > Const.KapEps)
            {
                data.Vars[5] = (inColor[0] + 16d) / 116d;
                outColor[1] = data.Vars[5] * data.Vars[5] * data.Vars[5] * data.InWP[1];
            }
            else outColor[1] = (inColor[0] / Const.Kappa) * data.InWP[1];
            //X
            if (data.Vars[3] > Const.Epsilon) outColor[0] = data.Vars[3] * data.InWP[0];
            else outColor[0] = ((116d * data.Vars[1] - 16d) / Const.Kappa) * data.InWP[0];
            //Z
            if (data.Vars[4] > Const.Epsilon) outColor[2] = data.Vars[4] * data.InWP[2];
            else outColor[2] = ((116d * data.Vars[2] - 16d) / Const.Kappa) * data.InWP[2];
        }
    }
}
