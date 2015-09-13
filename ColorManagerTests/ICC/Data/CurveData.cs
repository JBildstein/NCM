using ColorManager.ICC;

namespace ColorManagerTests.ICC.Data
{
    public static class CurveData
    {
        #region Response

        /// <summary>
        /// Channels: 3
        /// </summary>
        public static readonly ResponseCurve Response_ValGrad = new ResponseCurve
        (
            CurveMeasurementEncodings.StatusA,
            new XYZNumber[]
            {
                StructsData.XYZNumber_ValVar1,
                StructsData.XYZNumber_ValVar2,
                StructsData.XYZNumber_ValVar3,
            },
            new ResponseNumber[][]
            {
                new ResponseNumber[] { StructsData.ResponseNumber_Val1, StructsData.ResponseNumber_Val2 },
                new ResponseNumber[] { StructsData.ResponseNumber_Val3, StructsData.ResponseNumber_Val4 },
                new ResponseNumber[] { StructsData.ResponseNumber_Val5, StructsData.ResponseNumber_Val6 },
            }
        );

        /// <summary>
        /// Channels: 3
        /// </summary>
        public static readonly byte[] Response_Grad = ArrayHelper.Concat
        (
            new byte[] { 0x53, 0x74, 0x61, 0x41 },
            PrimitivesData.UInt32_2,
            PrimitivesData.UInt32_2,
            PrimitivesData.UInt32_2,

            StructsData.XYZNumber_Var1,
            StructsData.XYZNumber_Var2,
            StructsData.XYZNumber_Var3,

            StructsData.ResponseNumber_1,
            StructsData.ResponseNumber_2,

            StructsData.ResponseNumber_3,
            StructsData.ResponseNumber_4,

            StructsData.ResponseNumber_5,
            StructsData.ResponseNumber_6
        );

        #endregion

        #region Parametric

        public static readonly ParametricCurve Parametric_ValVar1 = new ParametricCurve(1);
        public static readonly ParametricCurve Parametric_ValVar2 = new ParametricCurve(1, 2, 3);
        public static readonly ParametricCurve Parametric_ValVar3 = new ParametricCurve(1, 2, 3, 4);
        public static readonly ParametricCurve Parametric_ValVar4 = new ParametricCurve(1, 2, 3, 4, 5);
        public static readonly ParametricCurve Parametric_ValVar5 = new ParametricCurve(1, 2, 3, 4, 5, 6, 7);

        public static readonly byte[] Parametric_Var1 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x00,
                0x00, 0x00,
            },
            PrimitivesData.Fix16_1
        );

        public static readonly byte[] Parametric_Var2 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x01,
                0x00, 0x00,
            },
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_2,
            PrimitivesData.Fix16_3
        );

        public static readonly byte[] Parametric_Var3 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x02,
                0x00, 0x00,
            },
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_2,
            PrimitivesData.Fix16_3,
            PrimitivesData.Fix16_4
        );

        public static readonly byte[] Parametric_Var4 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x03,
                0x00, 0x00,
            },
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_2,
            PrimitivesData.Fix16_3,
            PrimitivesData.Fix16_4,
            PrimitivesData.Fix16_5
        );

        public static readonly byte[] Parametric_Var5 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x04,
                0x00, 0x00,
            },
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_2,
            PrimitivesData.Fix16_3,
            PrimitivesData.Fix16_4,
            PrimitivesData.Fix16_5,
            PrimitivesData.Fix16_6,
            PrimitivesData.Fix16_7
        );

        #endregion

        #region Formula Segment

        public static readonly FormulaCurveElement Formula_ValVar1 = new FormulaCurveElement(0, 1, 2, 3, 4, 0, 0);
        public static readonly FormulaCurveElement Formula_ValVar2 = new FormulaCurveElement(1, 1, 2, 3, 4, 5, 0);
        public static readonly FormulaCurveElement Formula_ValVar3 = new FormulaCurveElement(2, 0, 2, 3, 4, 5, 6);

        public static readonly byte[] Formula_Var1 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x00,
                0x00, 0x00,
            },
            PrimitivesData.Single_1,
            PrimitivesData.Single_2,
            PrimitivesData.Single_3,
            PrimitivesData.Single_4
        );

        public static readonly byte[] Formula_Var2 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x01,
                0x00, 0x00,
            },
            PrimitivesData.Single_1,
            PrimitivesData.Single_2,
            PrimitivesData.Single_3,
            PrimitivesData.Single_4,
            PrimitivesData.Single_5
        );

        public static readonly byte[] Formula_Var3 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x02,
                0x00, 0x00,
            },
            PrimitivesData.Single_2,
            PrimitivesData.Single_3,
            PrimitivesData.Single_4,
            PrimitivesData.Single_5,
            PrimitivesData.Single_6
        );

        #endregion

        #region Sampled Segment

        public static readonly SampledCurveElement Sampled_ValGrad1 = new SampledCurveElement(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        public static readonly SampledCurveElement Sampled_ValGrad2 = new SampledCurveElement(new double[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 });

        public static readonly byte[] Sampled_Grad1 = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_9,

            PrimitivesData.Single_1,
            PrimitivesData.Single_2,
            PrimitivesData.Single_3,
            PrimitivesData.Single_4,
            PrimitivesData.Single_5,
            PrimitivesData.Single_6,
            PrimitivesData.Single_7,
            PrimitivesData.Single_8,
            PrimitivesData.Single_9
        );

        public static readonly byte[] Sampled_Grad2 = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_9,

            PrimitivesData.Single_9,
            PrimitivesData.Single_8,
            PrimitivesData.Single_7,
            PrimitivesData.Single_6,
            PrimitivesData.Single_5,
            PrimitivesData.Single_4,
            PrimitivesData.Single_3,
            PrimitivesData.Single_2,
            PrimitivesData.Single_1
        );

        #endregion

        #region Segment

        public static readonly CurveSegment Segment_ValFormula1 = Formula_ValVar1;
        public static readonly CurveSegment Segment_ValFormula2 = Formula_ValVar2;
        public static readonly CurveSegment Segment_ValFormula3 = Formula_ValVar3;
        public static readonly CurveSegment Segment_ValSampled1 = Sampled_ValGrad1;
        public static readonly CurveSegment Segment_ValSampled2 = Sampled_ValGrad2;

        public static readonly byte[] Segment_Formula1 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x70, 0x61, 0x72, 0x66,
                0x00, 0x00, 0x00, 0x00,
            },
            Formula_Var1
        );

        public static readonly byte[] Segment_Formula2 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x70, 0x61, 0x72, 0x66,
                0x00, 0x00, 0x00, 0x00,
            },
            Formula_Var2
        );

        public static readonly byte[] Segment_Formula3 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x70, 0x61, 0x72, 0x66,
                0x00, 0x00, 0x00, 0x00,
            },
            Formula_Var3
        );

        public static readonly byte[] Segment_Sampled1 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x73, 0x61, 0x6D, 0x66,
                0x00, 0x00, 0x00, 0x00,
            },
            Sampled_Grad1
        );

        public static readonly byte[] Segment_Sampled2 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x73, 0x61, 0x6D, 0x66,
                0x00, 0x00, 0x00, 0x00,
            },
            Sampled_Grad2
        );

        #endregion

        #region One Dimensional

        public static readonly OneDimensionalCurve OneDimensional_ValFormula1 = new OneDimensionalCurve
        (
            new double[] { 0, 1 },
            new CurveSegment[] { Segment_ValFormula1, Segment_ValFormula2, Segment_ValFormula3 }
        );
        public static readonly OneDimensionalCurve OneDimensional_ValFormula2 = new OneDimensionalCurve
        (
            new double[] { 0, 1 },
            new CurveSegment[] { Segment_ValFormula3, Segment_ValFormula2, Segment_ValFormula1 }
        );
        public static readonly OneDimensionalCurve OneDimensional_ValSampled = new OneDimensionalCurve
        (
            new double[] { 0, 1 },
            new CurveSegment[] { Segment_ValSampled1, Segment_ValSampled2, Segment_ValSampled1 }
        );

        public static readonly byte[] OneDimensional_Formula1 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x03,
                0x00, 0x00,
            },
            PrimitivesData.Single_0,
            PrimitivesData.Single_1,
            Segment_Formula1,
            Segment_Formula2,
            Segment_Formula3
        );

        public static readonly byte[] OneDimensional_Formula2 = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x03,
                0x00, 0x00,
            },
            PrimitivesData.Single_0,
            PrimitivesData.Single_1,
            Segment_Formula3,
            Segment_Formula2,
            Segment_Formula1
        );

        public static readonly byte[] OneDimensional_Sampled = ArrayHelper.Concat
        (
            new byte[]
            {
                0x00, 0x03,
                0x00, 0x00,
            },
            PrimitivesData.Single_0,
            PrimitivesData.Single_1,
            Segment_Sampled1,
            Segment_Sampled2,
            Segment_Sampled1
        );

        #endregion
    }
}
