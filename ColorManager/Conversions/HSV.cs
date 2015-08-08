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
            //Max = data.Vars[0] and Min = data.Vars[1]
            UMath.MinMax_3(inColor, data.Vars);

            if (Math.Round(data.Vars[0], 6) == Math.Round(data.Vars[1], 6)) { outColor[2] = data.Vars[1]; outColor[0] = 0; }
            else
            {
                if (inColor[0] == data.Vars[1])
                {
                    data.Vars[2] = inColor[1] - inColor[2];
                    data.Vars[3] = 3d;
                }
                else if (inColor[2] == data.Vars[1])
                {
                    data.Vars[2] = inColor[0] - inColor[1];
                    data.Vars[3] = 1d;
                }
                else //inColor[0] == data.Vars[1] (== min)
                {
                    data.Vars[2] = inColor[2] - inColor[0];
                    data.Vars[3] = 5d;
                }

                outColor[0] = 60d * (data.Vars[3] - data.Vars[2] / (data.Vars[0] - data.Vars[1]));
                outColor[2] = data.Vars[0];
            }
            if (Math.Abs(data.Vars[0]) < Const.Delta) outColor[1] = 0;
            else outColor[1] = (data.Vars[0] - data.Vars[1]) / data.Vars[0];
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
                data.Vars[0] = (inColor[0] / 360d) * 6d;
                data.Vars[1] = Math.Floor(data.Vars[0]);
                
                data.Vars[2] = inColor[2] * inColor[1];
                data.Vars[3] = data.Vars[2] * (data.Vars[0] - data.Vars[1]);

                data.Vars[4] = inColor[2] - data.Vars[2];
                data.Vars[5] = inColor[2] - data.Vars[3];
                data.Vars[6] = data.Vars[4] + data.Vars[3];

                switch ((int)(data.Vars[1] + 0.5))//Since the value is always positive, this is the fastest way to round to an even number
                {
                    case 6:
                    case 0: outColor[0] = inColor[2]; outColor[1] = data.Vars[6]; outColor[2] = data.Vars[4]; break;
                    case 1: outColor[0] = data.Vars[5]; outColor[1] = inColor[2]; outColor[2] = data.Vars[4]; break;
                    case 2: outColor[0] = data.Vars[4]; outColor[1] = inColor[2]; outColor[2] = data.Vars[6]; break;
                    case 3: outColor[0] = data.Vars[4]; outColor[1] = data.Vars[5]; outColor[2] = inColor[2]; break;
                    case 4: outColor[0] = data.Vars[6]; outColor[1] = data.Vars[4]; outColor[2] = inColor[2]; break;
                    default: outColor[0] = inColor[2]; outColor[1] = data.Vars[4]; outColor[2] = data.Vars[5]; break;
                }
            }
        }
    }
}
