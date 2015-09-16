using ColorManager.ICC;

namespace ColorManagerTests.ICC.Data
{
    public static class MultiProcessElementData
    {
        #region CurveSet

        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// </summary>
        public static readonly CurveSetProcessElement CurvePE_ValGrad = new CurveSetProcessElement(new OneDimensionalCurve[]
        {
            CurveData.OneDimensional_ValFormula1,
            CurveData.OneDimensional_ValFormula2,
            CurveData.OneDimensional_ValFormula1
        });
        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// </summary>
        public static readonly byte[] CurvePE_Grad = ArrayHelper.Concat
        (
            CurveData.OneDimensional_Formula1,
            CurveData.OneDimensional_Formula2,
            CurveData.OneDimensional_Formula1
        );

        #endregion

        #region Matrix

        /// <summary>
        /// <para>Input Channel Count: 3</para>
        /// <para>Output Channel Count: 3</para>
        /// </summary>
        public static readonly MatrixProcessElement MatrixPE_ValGrad = new MatrixProcessElement
        (
            MatrixData.Single_2D_ValGrad,
            MatrixData.Single_1D_ValGrad
        );
        /// <summary>
        /// <para>Input Channel Count: 3</para>
        /// <para>Output Channel Count: 3</para>
        /// </summary>
        public static readonly byte[] MatrixPE_Grad = ArrayHelper.Concat
        (
            MatrixData.Single_2D_Grad,
            MatrixData.Single_1D_Grad
        );

        #endregion

        #region CLUT

        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// </summary>
        public static readonly CLUTProcessElement CLUTPE_ValGrad = new CLUTProcessElement(LUTData.CLUT_Valf32);
        /// <summary>
        /// <para>Input Channel Count: 2</para>
        /// <para>Output Channel Count: 3</para>
        /// </summary>
        public static readonly byte[] CLUTPE_Grad = LUTData.CLUT_f32;

        #endregion

        #region MultiProcessElement

        public static readonly MultiProcessElement MPE_ValMatrix = MatrixPE_ValGrad;
        public static readonly MultiProcessElement MPE_ValCLUT = CLUTPE_ValGrad;
        public static readonly MultiProcessElement MPE_ValCurve = CurvePE_ValGrad;
        public static readonly MultiProcessElement MPE_ValbACS = new bACSProcessElement(3, 3);
        public static readonly MultiProcessElement MPE_ValeACS = new eACSProcessElement(3, 3);

        public static readonly byte[] MPE_Matrix = ArrayHelper.Concat
        (
            new byte[]
            {
                0x6D, 0x61, 0x74, 0x66,
                0x00, 0x03,
                0x00, 0x03,
            },
            MatrixPE_Grad
        );

        public static readonly byte[] MPE_CLUT = ArrayHelper.Concat
        (
            new byte[]
            {
                0x63, 0x6C, 0x75, 0x74,
                0x00, 0x02,
                0x00, 0x03,
            },
            CLUTPE_Grad
        );

        public static readonly byte[] MPE_Curve = ArrayHelper.Concat
        (
            new byte[]
            {
                0x6D, 0x66, 0x6C, 0x74,
                0x00, 0x03,
                0x00, 0x03,
            },
            CurvePE_Grad
        );

        public static readonly byte[] MPE_bACS =
        {
            0x62, 0x41, 0x43, 0x53,
            0x00, 0x03,
            0x00, 0x03,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        public static readonly byte[] MPE_eACS =
        {
            0x65, 0x41, 0x43, 0x53,
            0x00, 0x03,
            0x00, 0x03,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        #endregion
    }
}
