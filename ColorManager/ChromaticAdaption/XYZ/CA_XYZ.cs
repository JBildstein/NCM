using System;
using ColorManager.Conversion;

namespace ColorManager
{
    /// <summary>
    /// Represents an XYZ color chromatic adaption
    /// </summary>
    public unsafe sealed class CA_XYZ : ChromaticAdaption
    {
        /// <summary>
        /// The type of color this chromatic adaption is performed on (always typeof(<see cref="ColorXYZ"/>))
        /// </summary>
        public override Type ColorType { get { return typeof(ColorXYZ); } }
        /// <summary>
        /// The method to execute to do the chromatic adaption
        /// </summary>
        public override ConversionDelegate Method { get { return CAMethod; } }

        /// <summary>
        /// Gets the conversion data necessary for the chromatic adaption
        /// </summary>
        /// <param name="data">The data that is used to perform the chromatic adaption</param>
        /// <returns>The conversion data necessary for the chromatic adaption</returns>
        public override CustomData GetCAData(ConversionData data)
        {
            var matrix = DefaultMethod.CalculateMatrix(data.InWP, data.OutWP);
            return new CA_XYZ_Data(matrix);
        }

        /// <summary>
        /// The default XYZ chromatic adaption method
        /// </summary>
        public static CA_XYZ_Method DefaultMethod
        {
            get { return _DefaultMethod; }
            set { if (value != null) _DefaultMethod = value; }
        }
        private static CA_XYZ_Method _DefaultMethod = new CA_XYZ_Bradford();

        /// <summary>
        /// The method that does the chromatic adaption
        /// </summary>
        /// <param name="inColor">The pointer to the input color values</param>
        /// <param name="outColor">The pointer to the output color values</param>
        /// <param name="data">The data that is used to perform the chromatic adaption</param>
        public static void CAMethod(double* inColor, double* outColor, ConversionData data)
        {
            UMath.MultiplyMatrix_3x3_3x1((double*)data.CAData, inColor, outColor);
        }
    }
}
