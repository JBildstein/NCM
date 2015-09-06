using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorXYZ"/> to <see cref="ColorYxy"/>
    /// </summary>
    public sealed unsafe class Path_XYZ_Yxy : ConversionPath<ColorXYZ, ColorYxy>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorXYZ"/> to <see cref="ColorYxy"/>
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

            outColor[0] = inColor[1];
            vs[0] = inColor[0] + inColor[1] + inColor[2];
            if (Math.Abs(vs[0]) < Const.Delta)
            {
                outColor[1] = data.OutWPCr[0];
                outColor[2] = data.OutWPCr[1];
            }
            else
            {
                vs[0] = 1d / vs[0];
                outColor[2] = inColor[1] * vs[0];
                outColor[1] = inColor[0] * vs[0];
            }
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorYxy"/> to <see cref="ColorXYZ"/>
    /// </summary>
    public sealed unsafe class Path_Yxy_XYZ : ConversionPath<ColorYxy, ColorXYZ>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorYxy"/> to <see cref="ColorXYZ"/>
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
            if (inColor[2] < Const.Delta) outColor[0] = outColor[1] = outColor[2] = 0;
            else
            {
                double* vs = data.Vars;
                vs[0] = 1d / inColor[2];
                outColor[1] = inColor[0];
                outColor[0] = (inColor[1] * inColor[0]) * vs[0];
                outColor[2] = ((1 - inColor[1] - inColor[2]) * inColor[0]) * vs[0];
            }
        }
    }
}
