
namespace ColorManagerTests.ICC.Data
{
    public static class MatrixData
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
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_4,
            PrimitivesData.Fix16_7,

            PrimitivesData.Fix16_2,
            PrimitivesData.Fix16_5,
            PrimitivesData.Fix16_8,

            PrimitivesData.Fix16_3,
            PrimitivesData.Fix16_6,
            PrimitivesData.Fix16_9
        );

        /// <summary>
        /// 3x3 Matrix
        /// </summary>
        public static readonly byte[] Single_2D_Grad = ArrayHelper.Concat
        (
            PrimitivesData.Single_1,
            PrimitivesData.Single_4,
            PrimitivesData.Single_7,

            PrimitivesData.Single_2,
            PrimitivesData.Single_5,
            PrimitivesData.Single_8,

            PrimitivesData.Single_3,
            PrimitivesData.Single_6,
            PrimitivesData.Single_9
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
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_4,
            PrimitivesData.Fix16_7
        );

        /// <summary>
        /// 3x1 Matrix
        /// </summary>
        public static readonly byte[] Single_1D_Grad = ArrayHelper.Concat
        (
            PrimitivesData.Single_1,
            PrimitivesData.Single_4,
            PrimitivesData.Single_7
        );

        #endregion
    }
}
