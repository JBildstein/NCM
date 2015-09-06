using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorRGB"/> to <see cref="ColorHSL"/>
    /// </summary>
    public sealed unsafe class Path_RGB_HSL : ConversionPath<ColorRGB, ColorHSL>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorRGB"/> to <see cref="ColorHSL"/>
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

            //Max = vs[0] and Min = vs[1]
            UMath.MinMax_3(inColor, vs);

            vs[2] = vs[0] - vs[1];
            vs[3] = vs[0] + vs[1];
            outColor[2] = vs[3] * 0.5;

            if (Math.Round(vs[0], 6) == Math.Round(vs[1], 6)) { outColor[1] = outColor[0] = 0; }
            else
            {
                if (outColor[2] <= 0.5d) { outColor[1] = vs[2] / vs[3]; }
                else { outColor[1] = vs[2] / (2 - vs[2]); }

                if (inColor[0] == vs[0]) { outColor[0] = (inColor[1] - inColor[2]) / vs[2]; }
                else if (inColor[1] == vs[0]) { outColor[0] = 2d + (inColor[2] - inColor[0]) / vs[2]; }
                else { outColor[0] = 4d + (inColor[0] - inColor[1]) / vs[2]; }    //inColor[2] == max
                outColor[0] *= 60d;
            }
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorHSL"/> to <see cref="ColorRGB"/>
    /// </summary>
    public sealed unsafe class Path_HSL_RGB : ConversionPath<ColorHSL, ColorRGB>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorHSL"/> to <see cref="ColorRGB"/>
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
            if (Math.Abs(inColor[1]) < Const.Delta)
            {
                outColor[0] = inColor[2];
                outColor[1] = inColor[2];
                outColor[2] = inColor[2];
            }
            else
            {
                const double div1_60 = 1 / 60d;
                double* vs = data.Vars;

                if (inColor[2] < 0.5d) { vs[0] = inColor[2] * (1d + inColor[1]); }
                else { vs[0] = (inColor[2] + inColor[1]) - (inColor[1] * inColor[2]); }

                vs[1] = 2d * inColor[2] - vs[0];
                
                if (inColor[0] > 360d) { inColor[0] -= 360d; }
                else if (inColor[0] < 0) { inColor[0] += 360d; }
                if (inColor[0] < 60d) { outColor[1] = vs[1] + (vs[0] - vs[1]) * inColor[0] * div1_60; }
                else if (inColor[0] < 180d) { outColor[1] = vs[0]; }
                else if (inColor[0] < 240d) { outColor[1] = vs[1] + (vs[0] - vs[1]) * (240d - inColor[0]) * div1_60; }
                else outColor[1] = vs[1];

                vs[2] = inColor[0] - 120d;
                if (vs[2] > 360d) { vs[2] -= 360d; }
                else if (vs[2] < 0) { vs[2] += 360d; }
                if (vs[2] < 60d) { outColor[2] = vs[1] + (vs[0] - vs[1]) * vs[2] * div1_60; }
                else if (vs[2] < 180d) { outColor[2] = vs[0]; }
                else if (vs[2] < 240d) { outColor[2] = vs[1] + (vs[0] - vs[1]) * (240d - vs[2]) * div1_60; }
                else outColor[2] = vs[1];

                vs[2] = inColor[0] + 120d;
                if (vs[2] > 360d) { vs[2] -= 360d; }
                else if (vs[2] < 0) { vs[2] += 360d; }
                if (vs[2] < 60d) { outColor[0] = vs[1] + (vs[0] - vs[1]) * vs[2] * div1_60; }
                else if (vs[2] < 180d) { outColor[0] = vs[0]; }
                else if (vs[2] < 240d) { outColor[0] = vs[1] + (vs[0] - vs[1]) * (240d - vs[2]) * div1_60; }
                else outColor[0] = vs[1];
            }
        }
    }
}
