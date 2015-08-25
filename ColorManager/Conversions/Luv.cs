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
            if (inColor[1] < Const.Delta) outColor[0] = outColor[1] = outColor[2] = 0;
            else
            {
                //yr
                data.Vars[0] = inColor[1] / data.OutWP[1];

                //uv' tmp
                data.Vars[1] = inColor[0] + 15d * inColor[1] + 3d * inColor[2];
                //uv'r tmp
                data.Vars[2] = data.OutWP[0] + 15d * data.OutWP[1] + 3d * data.OutWP[2];

                //u' = 4d * inColor[0] / data.Vars[1];
                //v' = 9d * inColor[1] / data.Vars[1];

                //u'r = 4d * data.OutWP[0] / data.Vars[2];
                //v'r = 9d * data.OutWP[1] / data.Vars[2];

                //L
                if (data.Vars[0] > Const.Epsilon) outColor[0] = 116d * Math.Pow(data.Vars[0], Const.div1_3) - 16d;
                else outColor[0] = Const.Kappa * data.Vars[0];
                //uv tmp
                data.Vars[3] = 13d * outColor[0];
                outColor[2] = data.Vars[3] * ((9d * inColor[1] / data.Vars[1]) - (9d * data.OutWP[1] / data.Vars[2]));  //v
                outColor[1] = data.Vars[3] * ((4d * inColor[0] / data.Vars[1]) - (4d * data.OutWP[0] / data.Vars[2]));  //u
            }
        }
    }

    //LTODO: Somewhere in here is a problem with negative u/v values (when L and v is low. The lower v, the less low L has to be)

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
            const double div1_116 = 1 / 116d;

            //Y
            if (inColor[0] > Const.KapEps)
            {
                outColor[1] = (inColor[0] + 16) * div1_116;
                outColor[1] *= outColor[1] * outColor[1];    // == outColor[1]^3
            }
            else outColor[1] = inColor[0] / Const.Kappa;

            if (outColor[1] < Const.Delta) outColor[0] = outColor[1] = outColor[2] = 0;
            else
            {
                //uv0 tmp
                data.Vars[5] = data.InWP[0] + 15d * data.InWP[1] + 3d * data.InWP[2];

                //u0
                data.Vars[1] = 4d * data.InWP[0] / data.Vars[5];
                //v0
                data.Vars[2] = 9d * data.InWP[1] / data.Vars[5];

                //a
                data.Vars[3] = Const.div1_3 * ((52d * inColor[0] / (inColor[1] + 13d * inColor[0] * data.Vars[1])) - 1d);
                //b
                data.Vars[4] = -5d * outColor[1];
                //c = -1/3;
                //d
                data.Vars[6] = outColor[1] * ((39d * inColor[0] / (inColor[2] + 13d * inColor[0] * data.Vars[2])) - 5d);//I think the problem lies in this line here
                
                outColor[0] = (data.Vars[6] - data.Vars[4]) / (data.Vars[3] + Const.div1_3);    //X
                outColor[2] = outColor[0] * data.Vars[3] + data.Vars[4];                        //Z
            }
        }
    }
}
