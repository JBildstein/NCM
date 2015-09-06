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
            double* vs = data.Vars;
            double* wp = data.OutWP;

            vs[0] = inColor[1] / wp[1];
            if (vs[0] <= Const.Epsilon) vs[0] = ((Const.Kappa * vs[0]) + 16d) * div1_116;
            else vs[0] = Math.Pow(vs[0], Const.div1_3);

            vs[1] = inColor[0] / wp[0];
            if (vs[1] <= Const.Epsilon) vs[1] = ((Const.Kappa * vs[1]) + 16d) * div1_116;
            else vs[1] = Math.Pow(vs[1], Const.div1_3);

            vs[2] = inColor[2] / wp[2];
            if (vs[2] <= Const.Epsilon) vs[2] = ((Const.Kappa * vs[2]) + 16d) * div1_116;
            else vs[2] = Math.Pow(vs[2], Const.div1_3);

            outColor[0] = 116d * vs[0] - 16d;
            outColor[1] = 500d * (vs[1] - vs[0]);
            outColor[2] = 200d * (vs[0] - vs[2]);
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

            double* vs = data.Vars;
            double* wp = data.InWP;

            vs[0] = (inColor[0] + 16d) * div1_116;  //fy
            vs[1] = vs[0] + inColor[1] * div1_500;  //fx
            vs[2] = vs[0] - inColor[2] * div1_200;  //fz

            vs[3] = vs[1] * vs[1] * vs[1];          //fx^3
            vs[4] = vs[2] * vs[2] * vs[2];          //fz^3

            //Y
            if (inColor[0] > Const.KapEps)
            {
                vs[5] = (inColor[0] + 16d) * div1_116;
                outColor[1] = vs[5] * vs[5] * vs[5] * wp[1];
            }
            else outColor[1] = inColor[0] * div1_Kappa * wp[1];

            //X
            if (vs[3] > Const.Epsilon) outColor[0] = vs[3] * wp[0];
            else outColor[0] = ((116d * vs[1] - 16d) * div1_Kappa) * wp[0];

            //Z
            if (vs[4] > Const.Epsilon) outColor[2] = vs[4] * wp[2];
            else outColor[2] = ((116d * vs[2] - 16d) * div1_Kappa) * wp[2];
        }
    }
}
