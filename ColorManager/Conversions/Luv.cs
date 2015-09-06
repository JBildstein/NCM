using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorLuv"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_Luv : ConversionPath<ColorXYZ, ColorLuv>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorLuv"/>
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
            double* vs = data.Vars;
            double* wp = data.OutWP;

            if (inColor[1] < Const.Delta) outColor[0] = outColor[1] = outColor[2] = 0;
            else
            {
                //yr
                vs[0] = inColor[1] / wp[1];

                //uv' tmp
                vs[1] = 1 / (inColor[0] + 15d * inColor[1] + 3d * inColor[2]);
                //uv'r tmp
                vs[2] = 1d / (wp[0] + 15d * wp[1] + 3d * wp[2]);

                //L
                if (vs[0] > Const.Epsilon) outColor[0] = 116d * Math.Pow(vs[0], Const.div1_3) - 16d;
                else outColor[0] = Const.Kappa * vs[0];

                //uv tmp
                vs[0] = 13d * outColor[0];
                outColor[2] = vs[0] * ((9d * inColor[1] * vs[1]) - (9d * wp[1] * vs[2]));  //v
                outColor[1] = vs[0] * ((4d * inColor[0] * vs[1]) - (4d * wp[0] * vs[2]));  //u
            }
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorLuv"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_Luv_XYZ : ConversionPath<ColorLuv, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorLuv"/> to <see cref="ColorXYZ"/>
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
            double* vs = data.Vars;
            double* wp = data.InWP;

            vs[0] = 1d / (wp[0] + 15d * wp[1] + 3d * wp[2]);
            vs[1] = 4d * wp[0] * vs[0];     //u'n
            vs[2] = 9d * wp[1] * vs[0];     //v'n

            const double f = 3d / 29d;
            const double g = f * f * f;

            if (inColor[0] <= Const.KapEps) outColor[1] = wp[1] * inColor[0] * g;
            else
            {
                vs[0] = (inColor[0] + 16d) * (1d / 116d);
                outColor[1] = wp[1] * vs[0] * vs[0] * vs[0];
            }

            //TODO: if v(<0) and L(~0) are small, there is a problem because of a division with a very small number

            vs[5] = 13d * inColor[0];

            if (vs[5] > Const.Delta)
            {
                vs[0] = 1 / vs[5];
                vs[3] = (inColor[1] * vs[0]) + vs[1];   //u'
                vs[4] = (inColor[2] * vs[0]) + vs[2];   //v'

                vs[0] = 1d / (4d * vs[4]);
                outColor[0] = outColor[1] * ((9d * vs[3]) * vs[0]);
                outColor[2] = outColor[1] * ((12d - 3d * vs[3] - 20d * vs[4]) * vs[0]);
            }
            else outColor[0] = outColor[2] = 0;

            if (outColor[0] < 0) outColor[0] = 0;
            else if (outColor[0] > 1) outColor[0] = 1;

            if (outColor[2] < 0) outColor[2] = 0;
            else if (outColor[2] > 1) outColor[2] = 1;
        }
    }
}
