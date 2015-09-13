
namespace ColorManagerTests.ICC.Data
{
    public static class ArrayData
    {
        #region Byte

        public static readonly byte[] UInt8 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        #endregion

        #region UInt16

        public static readonly ushort[] UInt16_Val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static readonly byte[] UInt16_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_0,
            PrimitivesData.UInt16_1,
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_3,
            PrimitivesData.UInt16_4,
            PrimitivesData.UInt16_5,
            PrimitivesData.UInt16_6,
            PrimitivesData.UInt16_7,
            PrimitivesData.UInt16_8,
            PrimitivesData.UInt16_9
        );

        #endregion

        #region Int16

        public static readonly short[] Int16_Val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static readonly byte[] Int16_Arr = ArrayHelper.Concat
        (
            PrimitivesData.Int16_0,
            PrimitivesData.Int16_1,
            PrimitivesData.Int16_2,
            PrimitivesData.Int16_3,
            PrimitivesData.Int16_4,
            PrimitivesData.Int16_5,
            PrimitivesData.Int16_6,
            PrimitivesData.Int16_7,
            PrimitivesData.Int16_8,
            PrimitivesData.Int16_9
        );

        #endregion

        #region UInt32

        public static readonly uint[] UInt32_Val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static readonly byte[] UInt32_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_0,
            PrimitivesData.UInt32_1,
            PrimitivesData.UInt32_2,
            PrimitivesData.UInt32_3,
            PrimitivesData.UInt32_4,
            PrimitivesData.UInt32_5,
            PrimitivesData.UInt32_6,
            PrimitivesData.UInt32_7,
            PrimitivesData.UInt32_8,
            PrimitivesData.UInt32_9
        );

        #endregion

        #region Int32

        public static readonly int[] Int32_Val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static readonly byte[] Int32_Arr = ArrayHelper.Concat
        (
            PrimitivesData.Int32_0,
            PrimitivesData.Int32_1,
            PrimitivesData.Int32_2,
            PrimitivesData.Int32_3,
            PrimitivesData.Int32_4,
            PrimitivesData.Int32_5,
            PrimitivesData.Int32_6,
            PrimitivesData.Int32_7,
            PrimitivesData.Int32_8,
            PrimitivesData.Int32_9
        );

        #endregion

        #region UInt64

        public static readonly ulong[] UInt64_Val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static readonly byte[] UInt64_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt64_0,
            PrimitivesData.UInt64_1,
            PrimitivesData.UInt64_2,
            PrimitivesData.UInt64_3,
            PrimitivesData.UInt64_4,
            PrimitivesData.UInt64_5,
            PrimitivesData.UInt64_6,
            PrimitivesData.UInt64_7,
            PrimitivesData.UInt64_8,
            PrimitivesData.UInt64_9
        );

        #endregion
    }
}
