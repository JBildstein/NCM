using System;

namespace ColorManager
{
    /// <summary>
    /// Represents an XYZ color chromatic adaption method (abstract class)
    /// </summary>
    public abstract unsafe class CA_XYZ_Method
    {
        /// <summary>
        /// The name of the chromatic adaption method
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// The adaption matrix (3x3)
        /// </summary>
        public abstract double[] MA { get; }
        /// <summary>
        /// The inverse adaption matrix (3x3)
        /// </summary>
        public abstract double[] MA1 { get; }

        /// <summary>
        /// Calculates the matrix for the chromatic adaption
        /// </summary>
        /// <param name="inXYZ">The input whitepoint XYZ values</param>
        /// <param name="outXYZ">The output whitepoint XYZ values</param>
        /// <returns>The matrix for the chromatic adaption</returns>
        public double[] CalculateMatrix(double[] inXYZ, double[] outXYZ)
        {
            if (inXYZ.Length < 3 || outXYZ.Length < 3) throw new ArgumentException("XYZ array has to have a length of 3");

            fixed (double* source = inXYZ)
            fixed (double* dest = outXYZ)
            {
                return CalculateMatrix(source, dest);
            }
        }

        /// <summary>
        /// Calculates the matrix for the chromatic adaption
        /// </summary>
        /// <param name="inXYZ">The pointer to the input whitepoint XYZ values</param>
        /// <param name="outXYZ">The pointer to the output whitepoint XYZ values</param>
        /// <returns>The matrix for the chromatic adaption</returns>
        public double[] CalculateMatrix(double* inXYZ, double* outXYZ)
        {
            double[] result = new double[9];

            fixed (double* ma = MA)
            fixed (double* ma1 = MA1)
            fixed (double* res = result)
            {
                double* M = stackalloc double[9];
                double* M2 = stackalloc double[9];
                double* S = stackalloc double[3];
                double* D = stackalloc double[3];

                UMath.MultiplyMatrix_3x3_3x1(ma, inXYZ, S);
                UMath.MultiplyMatrix_3x3_3x1(ma, outXYZ, D);
                M[0] = D[0] / S[0];
                M[4] = D[1] / S[1];
                M[8] = D[2] / S[2];
                UMath.MultiplyMatrix_3x3_3x3(ma1, M, M2);
                UMath.MultiplyMatrix_3x3_3x3(M2, ma, res);
            }

            return result;
        }
    }

    /// <summary>
    /// Represents the Bradford XYZ color chromatic adaption method
    /// </summary>
    public sealed class CA_XYZ_Bradford : CA_XYZ_Method
    {
        /// <summary>
        /// The name of the chromatic adaption method
        /// </summary>
        public override string Name { get { return "Bradford"; } }
        /// <summary>
        /// The adaption matrix (3x3)
        /// </summary>
        public override double[] MA
        {
            get { return new double[] { 0.8951, 0.2664, -0.1614, -0.7502, 1.7135, 0.0367, 0.0389, -0.0685, 1.0296 }; }
        }
        /// <summary>
        /// The inverse adaption matrix (3x3)
        /// </summary>
        public override double[] MA1
        {
            get { return new double[] { 0.9869929, -0.1470543, 0.1599627, 0.4323053, 0.5183603, 0.0492912, -0.0085287, 0.0400428, 0.9684867 }; }
        }
    }

    /// <summary>
    /// Represents the XYZScaling XYZ color chromatic adaption method
    /// </summary>
    public sealed class CA_XYZ_XYZScaling : CA_XYZ_Method
    {
        /// <summary>
        /// The name of the chromatic adaption method
        /// </summary>
        public override string Name { get { return "XYZScaling"; } }
        /// <summary>
        /// The adaption matrix (3x3)
        /// </summary>
        public override double[] MA
        {
            get { return new double[] { 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0 }; }
        }
        /// <summary>
        /// The inverse adaption matrix (3x3)
        /// </summary>
        public override double[] MA1
        {
            get { return new double[] { 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0 }; }
        }
    }

    /// <summary>
    /// Represents the VonKries XYZ color chromatic adaption method
    /// </summary>
    public sealed class CA_XYZ_VonKries : CA_XYZ_Method
    {
        /// <summary>
        /// The name of the chromatic adaption method
        /// </summary>
        public override string Name { get { return "VonKries"; } }
        /// <summary>
        /// The adaption matrix (3x3)
        /// </summary>
        public override double[] MA
        {
            get { return new double[] { 0.40024, 0.7076, -0.08081, -0.2263, 1.16532, 0.0457, 0.0, 0.0, 0.91822 }; }
        }
        /// <summary>
        /// The inverse adaption matrix (3x3)
        /// </summary>
        public override double[] MA1
        {
            get { return new double[] { 1.8599364, -1.1293816, 0.2198974, 0.3611914, 0.6388125, -0.0000064, 0.0, 0.0, 1.0890636 }; }
        }
    }
}
