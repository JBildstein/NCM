
namespace ColorManager.Conversion
{
    public sealed unsafe class Path_XYZ_RGB : ConversionPath<ColorXYZ, ColorRGB>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            UMath.MultiplyMatrix_3x3_3x1((double*)data.OutSpaceData, inColor, outColor);
            if (outColor[0] < 0) outColor[0] = 0;
            if (outColor[1] < 0) outColor[1] = 0;
            if (outColor[2] < 0) outColor[2] = 0;
            data.OutTransform(outColor, outColor);
        }
    }

    public sealed unsafe class Path_RGB_XYZ : ConversionPath<ColorRGB, ColorXYZ>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            data.InTransform(inColor, outColor);
            UMath.MultiplyMatrix_3x3_3x1((double*)data.InSpaceData, outColor, outColor);
        }
    }
}
