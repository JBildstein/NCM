using ColorManager.ICC;

namespace ColorManagerTests.ICC.Data
{
    public static class LUTData
    {
        #region LUT8

        public static readonly LUT LUT8_ValGrad = CreateLUT8Val();
        public static readonly byte[] LUT8_Grad = CreateLUT8();

        private static LUT CreateLUT8Val()
        {
            var result = new double[256];
            for (int i = 0; i < 256; i++) { result[i] = i / 255d; }
            return new LUT(result);
        }

        private static byte[] CreateLUT8()
        {
            var result = new byte[256];
            for (int i = 0; i < 256; i++) { result[i] = (byte)i; }
            return result;
        }

        #endregion

        #region LUT16

        public static readonly LUT LUT16_ValGrad = new LUT(new double[]
        {
            1d / ushort.MaxValue,
            2d / ushort.MaxValue,
            3d / ushort.MaxValue,
            4d / ushort.MaxValue,
            5d / ushort.MaxValue,
            6d / ushort.MaxValue,
            7d / ushort.MaxValue,
            8d / ushort.MaxValue,
            9d / ushort.MaxValue,
            32768d / ushort.MaxValue,
            1d
        });

        public static readonly byte[] LUT16_Grad = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_1,
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_3,
            PrimitivesData.UInt16_4,
            PrimitivesData.UInt16_5,
            PrimitivesData.UInt16_6,
            PrimitivesData.UInt16_7,
            PrimitivesData.UInt16_8,
            PrimitivesData.UInt16_9,
            PrimitivesData.UInt16_32768,
            PrimitivesData.UInt16_Max
        );

        #endregion

        #region CLUT8

        public static readonly CLUT CLUT8_ValGrad = new CLUT
        (
            new double[][]
            {
                new double[] { 1d / byte.MaxValue, 2d / byte.MaxValue, 3d / byte.MaxValue },
                new double[] { 4d / byte.MaxValue, 5d / byte.MaxValue, 6d / byte.MaxValue },
                new double[] { 7d / byte.MaxValue, 8d / byte.MaxValue, 9d / byte.MaxValue },
                
                new double[] { 10d / byte.MaxValue, 11d / byte.MaxValue, 12d / byte.MaxValue },
                new double[] { 13d / byte.MaxValue, 14d / byte.MaxValue, 15d / byte.MaxValue },
                new double[] { 16d / byte.MaxValue, 17d / byte.MaxValue, 18d / byte.MaxValue },

                new double[] { 19d / byte.MaxValue, 20d / byte.MaxValue, 21d / byte.MaxValue },
                new double[] { 22d / byte.MaxValue, 23d / byte.MaxValue, 24d / byte.MaxValue },
                new double[] { 25d / byte.MaxValue, 26d / byte.MaxValue, 27d / byte.MaxValue },
            },
            2, 3, new byte[] { 3, 3 }, CLUTDataType.UInt8
        );

        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// <para>Grid-point Count: { 3, 3 }</para>
        /// </summary>
        public static readonly byte[] CLUT8_Grad =
        {
            0x01, 0x02, 0x03,
            0x04, 0x05, 0x06,
            0x07, 0x08, 0x09,
            
            0x0A, 0x0B, 0x0C,
            0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12,
            
            0x13, 0x14, 0x15,
            0x16, 0x17, 0x18,
            0x19, 0x1A, 0x1B,
        };

        #endregion

        #region CLUT16

        public static readonly CLUT CLUT16_ValGrad = new CLUT
        (
            new double[][]
            {
                new double[] { 1d / ushort.MaxValue, 2d / ushort.MaxValue, 3d / ushort.MaxValue },
                new double[] { 4d / ushort.MaxValue, 5d / ushort.MaxValue, 6d / ushort.MaxValue },
                new double[] { 7d / ushort.MaxValue, 8d / ushort.MaxValue, 9d / ushort.MaxValue },

                new double[] { 10d / ushort.MaxValue, 11d / ushort.MaxValue, 12d / ushort.MaxValue },
                new double[] { 13d / ushort.MaxValue, 14d / ushort.MaxValue, 15d / ushort.MaxValue },
                new double[] { 16d / ushort.MaxValue, 17d / ushort.MaxValue, 18d / ushort.MaxValue },

                new double[] { 19d / ushort.MaxValue, 20d / ushort.MaxValue, 21d / ushort.MaxValue },
                new double[] { 22d / ushort.MaxValue, 23d / ushort.MaxValue, 24d / ushort.MaxValue },
                new double[] { 25d / ushort.MaxValue, 26d / ushort.MaxValue, 27d / ushort.MaxValue },
            },
            2, 3, new byte[] { 3, 3 }, CLUTDataType.UInt16
        );

        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// <para>Grid-point Count: { 3, 3 }</para>
        /// </summary>
        public static readonly byte[] CLUT16_Grad =
        {
            0x00, 0x01, 0x00, 0x02, 0x00, 0x03,
            0x00, 0x04, 0x00, 0x05, 0x00, 0x06,
            0x00, 0x07, 0x00, 0x08, 0x00, 0x09,

            0x00, 0x0A, 0x00, 0x0B, 0x00, 0x0C,
            0x00, 0x0D, 0x00, 0x0E, 0x00, 0x0F,
            0x00, 0x10, 0x00, 0x11, 0x00, 0x12,

            0x00, 0x13, 0x00, 0x14, 0x00, 0x15,
            0x00, 0x16, 0x00, 0x17, 0x00, 0x18,
            0x00, 0x19, 0x00, 0x1A, 0x00, 0x1B,
        };

        #endregion

        #region CLUTf32

        public static readonly CLUT CLUTf32_ValGrad = new CLUT
        (
            new double[][]
            {
                new double[] { 1f, 2f, 3f },
                new double[] { 4f, 5f, 6f },
                new double[] { 7f, 8f, 9f },

                new double[] { 1f, 2f, 3f },
                new double[] { 4f, 5f, 6f },
                new double[] { 7f, 8f, 9f },

                new double[] { 1f, 2f, 3f },
                new double[] { 4f, 5f, 6f },
                new double[] { 7f, 8f, 9f },
            },
            2, 3, new byte[] { 3, 3 }, CLUTDataType.UInt16
        );

        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// <para>Grid-point Count: { 3, 3 }</para>
        /// </summary>
        public static readonly byte[] CLUTf32_Grad = ArrayHelper.Concat
        (
            PrimitivesData.Single_1, PrimitivesData.Single_2, PrimitivesData.Single_3,
            PrimitivesData.Single_4, PrimitivesData.Single_5, PrimitivesData.Single_6,
            PrimitivesData.Single_7, PrimitivesData.Single_8, PrimitivesData.Single_9,

            PrimitivesData.Single_1, PrimitivesData.Single_2, PrimitivesData.Single_3,
            PrimitivesData.Single_4, PrimitivesData.Single_5, PrimitivesData.Single_6,
            PrimitivesData.Single_7, PrimitivesData.Single_8, PrimitivesData.Single_9,

            PrimitivesData.Single_1, PrimitivesData.Single_2, PrimitivesData.Single_3,
            PrimitivesData.Single_4, PrimitivesData.Single_5, PrimitivesData.Single_6,
            PrimitivesData.Single_7, PrimitivesData.Single_8, PrimitivesData.Single_9
        );

        #endregion

        #region CLUT

        public static readonly CLUT CLUT_Val8 = CLUT8_ValGrad;
        public static readonly CLUT CLUT_Val16 = CLUT16_ValGrad;
        public static readonly CLUT CLUT_Valf32 = CLUTf32_ValGrad;

        public static readonly byte[] CLUT_8 = ArrayHelper.Concat
        (
            new byte[16] { 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            new byte[4] { 0x01, 0x00, 0x00, 0x00 },
            CLUT8_Grad
        );

        public static readonly byte[] CLUT_16 = ArrayHelper.Concat
        (
            new byte[16] { 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            new byte[4] { 0x02, 0x00, 0x00, 0x00 },
            CLUT16_Grad
        );

        public static readonly byte[] CLUT_f32 = ArrayHelper.Concat
        (
            new byte[16] { 0x03, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            CLUTf32_Grad
        );

        #endregion
    }
}
