using System;

namespace ColorManager
{
    /// <summary>
    /// A collection of matrix calculations
    /// </summary>
    public static unsafe class UMath
    {
        /// <summary>
        /// Inverts a 3x3 matrix
        /// </summary>
        /// <param name="M">Input matrix</param>
        /// <param name="result">Inverse input matrix</param>
        public static void InvertMatrix_3x3(double* M, double* result)
        {
            double determinant = M[0] * (M[4] * M[8] - M[7] * M[5])
                               - M[1] * (M[3] * M[8] - M[5] * M[6])
                               + M[2] * (M[3] * M[7] - M[4] * M[6]);

            result[0] = (M[4] * M[8] - M[7] * M[5]) / determinant;
            result[1] = -(M[1] * M[8] - M[2] * M[7]) / determinant;
            result[2] = (M[1] * M[5] - M[2] * M[4]) / determinant;
            result[3] = -(M[3] * M[8] - M[5] * M[6]) / determinant;
            result[4] = (M[0] * M[8] - M[2] * M[6]) / determinant;
            result[5] = -(M[0] * M[5] - M[3] * M[2]) / determinant;
            result[6] = (M[3] * M[7] - M[6] * M[4]) / determinant;
            result[7] = -(M[0] * M[7] - M[6] * M[1]) / determinant;
            result[8] = (M[0] * M[4] - M[3] * M[1]) / determinant;
        }
        
        /// <summary>
        /// Multiplies two multi-dimensional matrices
        /// </summary>
        /// <param name="a">A multi-dimensional matrix</param>
        /// <param name="b">A multi-dimensional matrix</param>
        /// <param name="result">The result matrix (size: ax * ay)</param>
        /// <param name="ax">X-Length of first matrix</param>
        /// <param name="ay">Y-Length of first matrix</param>
        /// <param name="bx">X-Length of second matrix</param>
        /// <param name="by">Y-Length of second matrix</param>
        public static void MultiplyMatrix(double* a, double* b, double* result, int ax, int ay, int bx, int by)
        {
            int i1, i2;
            int length = ax * ay;
            double* m = stackalloc double[length];
            for (int i = 0; i < ay; i++)
            {
                i1 = i * ax;
                for (int j = 0; j < bx; j++)
                {
                    i2 = j;
                    double res = 0;
                    for (int k = 0; k < ax; k++, i2 += bx)
                    {
                        res += a[i1 + k] * b[i2];
                    }
                    m[i * bx + j] = res;
                }
            }
            for (int i = 0; i < length; i++) result[i] = m[i];
        }

        /// <summary>
        /// Multiplies a 3x3 matrix with a 3x1 matrix
        /// </summary>
        /// <param name="a">A 3x3 matrix</param>
        /// <param name="b">A 3x1 matrix</param>
        /// <param name="result">The result as a 3x1 matrix</param>
        public static void MultiplyMatrix_3x3_3x1(double* a, double* b, double* result)
        {
            //Create temporary variable because a or b could be the same pointer as result
            double* c = stackalloc double[3];
            c[0] = b[0] * a[0] + b[1] * a[1] + b[2] * a[2];
            c[1] = b[0] * a[3] + b[1] * a[4] + b[2] * a[5];
            c[2] = b[0] * a[6] + b[1] * a[7] + b[2] * a[8];
            result[0] = c[0];
            result[1] = c[1];
            result[2] = c[2];
        }

        /// <summary>
        /// Multiplies a 3x3 matrix with another 3x3 matrix
        /// </summary>
        /// <param name="a">A 3x3 matrix</param>
        /// <param name="b">A 3x3 matrix</param>
        /// <param name="result">The result as a 3x3 matrix</param>
        public static void MultiplyMatrix_3x3_3x3(double* a, double* b, double* result)
        {
            double* m = stackalloc double[23];

            m[0] = (a[0] + a[1] + a[2] - a[3] - a[4] - a[7] - a[8]) * b[4];
            m[1] = (a[0] - a[3]) * (-b[1] + b[4]);
            m[2] = a[4] * (-b[0] + b[1] + b[3] - b[4] - b[5] - b[6] + b[8]);
            m[3] = (-a[0] + a[3] + a[4]) * (b[0] - b[1] + b[4]);
            m[4] = (a[3] + a[4]) * (-b[0] + b[1]);
            m[5] = a[0] * b[0];
            m[6] = (-a[0] + a[6] + a[7]) * (b[0] - b[2] + b[5]);
            m[7] = (-a[0] + a[6]) * (b[2] - b[5]);
            m[8] = (a[6] + a[7]) * (-b[0] + b[2]);
            m[9] = (a[0] + a[1] + a[2] - a[4] - a[5] - a[6] - a[7]) * b[5];
            m[10] = a[7] * (-b[0] + b[2] + b[3] - b[4] - b[5] - b[6] + b[7]);
            m[11] = (-a[2] + a[7] + a[8]) * (b[4] + b[6] - b[7]);
            m[12] = (a[2] - a[8]) * (b[4] - b[7]);
            m[13] = a[2] * b[6];
            m[14] = (a[7] + a[8]) * (-b[6] + b[7]);
            m[15] = (-a[2] + a[4] + a[5]) * (b[5] + b[6] - b[8]);
            m[16] = (a[2] - a[5]) * (b[5] - b[8]);
            m[17] = (a[4] + a[5]) * (-b[6] + b[8]);
            m[18] = a[1] * b[3];
            m[19] = a[5] * b[7];
            m[20] = a[3] * b[2];
            m[21] = a[6] * b[1];
            m[22] = a[8] * b[8];

            result[0] = m[5] + m[13] + m[18];
            result[1] = m[0] + m[3] + m[4] + m[5] + m[11] + m[13] + m[14];
            result[2] = m[5] + m[6] + m[8] + m[9] + m[13] + m[15] + m[17];
            result[3] = m[1] + m[2] + m[3] + m[5] + m[13] + m[15] + m[16];
            result[4] = m[1] + m[3] + m[4] + m[5] + m[19];
            result[5] = m[13] + m[15] + m[16] + m[17] + m[20];
            result[6] = m[5] + m[6] + m[7] + m[10] + m[11] + m[12] + m[13];
            result[7] = m[11] + m[12] + m[13] + m[14] + m[21];
            result[8] = m[5] + m[6] + m[7] + m[8] + m[22];
        }
        
        /// <summary>
        /// Adds two one dimensional matrices together
        /// </summary>
        /// <param name="a">A one dimensional matrix</param>
        /// <param name="b">A one dimensional matrix</param>
        /// <param name="result">The result matrix</param>
        /// <param name="la">Length of first matrix</param>
        /// <param name="lb">Length of second matrix</param>
        public static void AddMatrix(double* a, double* b, double* result, int la, int lb)
        {
            if (la != lb) { throw new ArgumentException("Cannot multiply: Size of matrices do not match"); }

            for (int i = 0; i < la; i++) { result[i] = a[i] + b[i]; }
        }

        /// <summary>
        /// Adds a 3x1 matrix with another 3x1 matrix
        /// </summary>
        /// <param name="a">A one dimensional matrix</param>
        /// <param name="b">A one dimensional matrix</param>
        /// <param name="result">The result matrix</param>
        public static void AddMatrix_3x1(double* a, double* b, double* result)
        {
            result[0] = a[0] + b[0];
            result[1] = a[1] + b[1];
            result[2] = a[2] + b[2];
        }

        /// <summary>
        /// Finds the maximum and minimum value in a set of three values 
        /// </summary>
        /// <param name="input">Input values</param>
        /// <param name="result">pointer to maximum value is index 0 and minimum value is index 1</param>
        public static void MinMax_3(double* input, double* result)
        {
            if (input[0] > input[1])
            {
                if (input[0] > input[2])
                {
                    result[0] = input[0];
                    if (input[2] > input[1]) result[1] = input[1];
                    else result[1] = input[2];
                }
                else { result[0] = input[2]; result[1] = input[1]; }
            }
            else
            {
                if (input[1] > input[2])
                {
                    result[0] = input[1];
                    if (input[2] > input[0]) result[1] = input[0];
                    else result[1] = input[2];
                }
                else { result[0] = input[2]; result[1] = input[0]; }
            }
        }
    }
}
