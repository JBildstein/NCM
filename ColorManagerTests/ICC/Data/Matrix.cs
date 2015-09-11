
namespace ColorManagerTests.ICC.Data
{
    public static class Matrix
    {
        #region 2D

        /// <summary>
        /// 3x3 Matrix
        /// </summary>
        public static readonly double[,] Fix16_2D_ValGrad =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 },
        };
        /// <summary>
        /// 3x3 Matrix
        /// </summary>
        public static readonly double[,] Single_2D_ValGrad =
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 },
        };

        /// <summary>
        /// 3x3 Matrix
        /// </summary>
        public static readonly byte[] Fix16_2D_Grad = ArrayHelper.Concat
        (
            Primitives.Fix16_1,
            Primitives.Fix16_4,
            Primitives.Fix16_7,

            Primitives.Fix16_2,
            Primitives.Fix16_5,
            Primitives.Fix16_8,

            Primitives.Fix16_3,
            Primitives.Fix16_6,
            Primitives.Fix16_9
        );

        /// <summary>
        /// 3x3 Matrix
        /// </summary>
        public static readonly byte[] Single_2D_Grad = ArrayHelper.Concat
        (
            Primitives.Single_1,
            Primitives.Single_4,
            Primitives.Single_7,

            Primitives.Single_2,
            Primitives.Single_5,
            Primitives.Single_8,

            Primitives.Single_3,
            Primitives.Single_6,
            Primitives.Single_9
        );

        #endregion

        #region 1D

        /// <summary>
        /// 3x1 Matrix
        /// </summary>
        public static readonly double[] Fix16_1D_ValGrad = { 1, 4, 7 };
        /// <summary>
        /// 3x1 Matrix
        /// </summary>
        public static readonly double[] Single_1D_ValGrad = { 1, 4, 7 };

        /// <summary>
        /// 3x1 Matrix
        /// </summary>
        public static readonly byte[] Fix16_1D_Grad = ArrayHelper.Concat
        (
            Primitives.Fix16_1,
            Primitives.Fix16_4,
            Primitives.Fix16_7
        );

        /// <summary>
        /// 3x1 Matrix
        /// </summary>
        public static readonly byte[] Single_1D_Grad = ArrayHelper.Concat
        (
            Primitives.Single_1,
            Primitives.Single_4,
            Primitives.Single_7
        );

        #endregion
    }
}
