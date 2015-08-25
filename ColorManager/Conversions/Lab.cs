using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorLab"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_Lab : ConversionPath<ColorXYZ, ColorLab>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorLab"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            const double div1_116 = 1 / 116d;

            data.Vars[0] = inColor[1] / data.OutWP[1];
            if (data.Vars[0] <= Const.Epsilon) data.Vars[0] = ((Const.Kappa * data.Vars[0]) + 16d) * div1_116;
            else data.Vars[0] = Math.Pow(data.Vars[0], Const.div1_3);

            data.Vars[1] = inColor[0] / data.OutWP[0];
            if (data.Vars[1] <= Const.Epsilon) data.Vars[1] = ((Const.Kappa * data.Vars[1]) + 16d) * div1_116;
            else data.Vars[1] = Math.Pow(data.Vars[1], Const.div1_3);

            data.Vars[2] = inColor[2] / data.OutWP[2];
            if (data.Vars[2] <= Const.Epsilon) data.Vars[2] = ((Const.Kappa * data.Vars[2]) + 16d) * div1_116;
            else data.Vars[2] = Math.Pow(data.Vars[2], Const.div1_3);

            outColor[0] = 116d * data.Vars[0] - 16d;
            outColor[1] = 500d * (data.Vars[1] - data.Vars[0]);
            outColor[2] = 200d * (data.Vars[0] - data.Vars[2]);
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLab"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_Lab_XYZ : ConversionPath<ColorLab, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLab"/> to <see cref="ColorXYZ"/>
        /// </summary>
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        /// <summary>
        /// The conversion method
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the conversion</param>
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            const double div1_116 = 1 / 116d;
            const double div1_500 = 1 / 500d;
            const double div1_200 = 1 / 200d;
            const double div1_Kappa = 1 / Const.Kappa;

            data.Vars[0] = (inColor[0] + 16d) * div1_116;           //fy
            data.Vars[1] = data.Vars[0] + inColor[1] * div1_500;    //fx
            data.Vars[2] = data.Vars[0] - inColor[2] * div1_200;    //fz

            data.Vars[3] = data.Vars[1] * data.Vars[1] * data.Vars[1];//fx^3
            data.Vars[4] = data.Vars[2] * data.Vars[2] * data.Vars[2];//fz^3

            //Y
            if (inColor[0] > Const.KapEps)
            {
                data.Vars[5] = (inColor[0] + 16d) * div1_116;
                outColor[1] = data.Vars[5] * data.Vars[5] * data.Vars[5] * data.InWP[1];
            }
            else outColor[1] = inColor[0] * div1_Kappa * data.InWP[1];
            //X
            if (data.Vars[3] > Const.Epsilon) outColor[0] = data.Vars[3] * data.InWP[0];
            else outColor[0] = ((116d * data.Vars[1] - 16d) * div1_Kappa) * data.InWP[0];
            //Z
            if (data.Vars[4] > Const.Epsilon) outColor[2] = data.Vars[4] * data.InWP[2];
            else outColor[2] = ((116d * data.Vars[2] - 16d) * div1_Kappa) * data.InWP[2];
        }
    }
}
