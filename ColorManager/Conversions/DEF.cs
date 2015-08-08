using System;

namespace ColorManager.Conversion
{
    //LTODO: DEF/XYZ Matrix is currently fixed for every call. It should be pre-pinned

    public sealed unsafe class Path_XYZ_DEF : ConversionPath<ColorXYZ, ColorDEF>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        private static double[] XYZ_DEF_Matrix = { 0.2053, 0.7125, 0.4670, 1.8537, -1.2797, -0.4429, -0.3655, 1.0120, -0.6104 };
        
        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            fixed (double* XYZ_DEF_MatrixP = XYZ_DEF_Matrix)
            {
                UMath.MultiplyMatrix_3x3_3x1(XYZ_DEF_MatrixP, inColor, outColor);
            }
        }
    }

    public sealed unsafe class Path_DEF_XYZ : ConversionPath<ColorDEF, ColorXYZ>
    {
        public override IConversionCommand[] Commands
        {
            get { return new IConversionCommand[] { new CC_ExecuteMethod(Convert) }; }
        }

        private static double[] DEF_XYZ_Matrix = { 0.671203, 0.495489, 0.153997, 0.706165, 0.0247732, 0.522292, 0.768864, -0.255621, -0.864558 };

        public static void Convert(double* inColor, double* outColor, ConversionData data)
        {
            fixed (double* DEF_XYZ_MatrixP = DEF_XYZ_Matrix)
            {
                UMath.MultiplyMatrix_3x3_3x1(DEF_XYZ_MatrixP, inColor, outColor);
            }
        }
    }
}
