using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from <see cref="ColorRGB"/> to <see cref="ColorHSV"/>
    /// </summary>
    public sealed unsafe class Path_RGB_HSV : ConversionPath<ColorRGB, ColorHSV>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorRGB"/> to <see cref="ColorHSV"/>
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

            if (Math.Round(vs[0], 6) == Math.Round(vs[1], 6)) { outColor[2] = vs[1]; outColor[0] = 0; }
            else
            {
                if (inColor[0] == vs[1])
                {
                    vs[2] = inColor[1] - inColor[2];
                    vs[3] = 3d;
                }
                else if (inColor[2] == vs[1])
                {
                    vs[2] = inColor[0] - inColor[1];
                    vs[3] = 1d;
                }
                else //inColor[0] == vs[1] (== min)
                {
                    vs[2] = inColor[2] - inColor[0];
                    vs[3] = 5d;
                }

                outColor[0] = 60d * (vs[3] - vs[2] / (vs[0] - vs[1]));
                outColor[2] = vs[0];
            }
            if (Math.Abs(vs[0]) < Const.Delta) outColor[1] = 0;
            else outColor[1] = (vs[0] - vs[1]) / vs[0];
        }
    }

    /// <summary>
    /// Stores data about a conversion from <see cref="ColorHSV"/> to <see cref="ColorRGB"/>
    /// </summary>
    public sealed unsafe class Path_HSV_RGB : ConversionPath<ColorHSV, ColorRGB>
    {
        /// <summary>
        /// An array of commands that convert from <see cref="ColorHSV"/> to <see cref="ColorRGB"/>
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
                const double div1_360 = 1d / 360d;
                double* vs = data.Vars;

                vs[0] = (inColor[0] * div1_360) * 6d;
                vs[1] = (int)vs[0]; //== Math.Floor
                
                vs[2] = inColor[2] * inColor[1];
                vs[3] = vs[2] * (vs[0] - vs[1]);

                vs[4] = inColor[2] - vs[2];
                vs[5] = inColor[2] - vs[3];
                vs[6] = vs[4] + vs[3];

                switch ((int)(vs[1] + 0.5)) //Since the value is always positive, this is the fastest way to round to an even number
                {
                    case 6:
                    case 0: outColor[0] = inColor[2]; outColor[1] = vs[6]; outColor[2] = vs[4]; break;
                    case 1: outColor[0] = vs[5]; outColor[1] = inColor[2]; outColor[2] = vs[4]; break;
                    case 2: outColor[0] = vs[4]; outColor[1] = inColor[2]; outColor[2] = vs[6]; break;
                    case 3: outColor[0] = vs[4]; outColor[1] = vs[5]; outColor[2] = inColor[2]; break;
                    case 4: outColor[0] = vs[6]; outColor[1] = vs[4]; outColor[2] = inColor[2]; break;
                    default: outColor[0] = inColor[2]; outColor[1] = vs[4]; outColor[2] = vs[5]; break;
                }
            }
        }
    }
}
