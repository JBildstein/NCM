using System;

namespace ColorManager.ICC
{
    //TODO: remove MatrixMath and replace references with UMath

    public static class MatrixMath
    {
        public static double[,] MultiplyMatrix(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) { throw new ArgumentException("Cannot multiply: Size of matrices do not match"); }

            double[,] output = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    for (int k = 0; k < a.GetLength(1); k++) { output[i, j] += a[i, k] * b[k, j]; }
                }
            }
            return output;
        }

        public static double[] MultiplyMatrix(double[,] a, double[] b)
        {
            if (a.GetLength(1) != b.Length) { throw new ArgumentException("Cannot multiply: Size of matrices do not match"); }

            double[] output = new double[a.GetLength(0)];
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int k = 0; k < a.GetLength(1); k++) { output[i] += a[i, k] * b[k]; }
            }
            return output;
        }

        public static double[] AddMatrix(double[] a, double[] b)
        {
            if (a.Length != b.Length) { throw new ArgumentException("Cannot add: Size of matrices do not match"); }

            double[] output = new double[a.Length];
            for (int i = 0; i < a.Length; i++) { output[i] = a[i] + b[i]; }
            return output;
        }

        public static double[] MultiplyMatrix3x3(double[,] a, double[] b)
        {
            double[] c = new double[3];
            c[0] = b[0] * a[0, 0] + b[1] * a[0, 1] + b[2] * a[0, 2];
            c[1] = b[0] * a[1, 0] + b[1] * a[1, 1] + b[2] * a[1, 2];
            c[2] = b[0] * a[2, 0] + b[1] * a[2, 1] + b[2] * a[2, 2];
            return c;
        }
    }
}
