using System;
using ColorManager.Conversion;

namespace ColorManager
{
    public unsafe sealed class CA_XYZ : ChromaticAdaption
    {
        public override Type ColorType { get { return typeof(ColorXYZ); } }
        public override ConversionDelegate Method { get { return CAMethod; } }

        public override CustomData GetCAData(ConversionData data)
        {
            var matrix = DefaultMethod.CalculateMatrix(data.InWP, data.OutWP);
            return new CA_XYZ_Data(matrix);
        }

        public static CA_XYZ_Method DefaultMethod
        {
            get { return _DefaultMethod; }
            set { if (value != null) _DefaultMethod = value; }
        }
        private static CA_XYZ_Method _DefaultMethod = new CA_XYZ_Bradford();

        public static void CAMethod(double* inColor, double* outColor, ConversionData data)
        {
            UMath.MultiplyMatrix_3x3_3x1((double*)data.CAData, inColor, outColor);
        }
    }
}
