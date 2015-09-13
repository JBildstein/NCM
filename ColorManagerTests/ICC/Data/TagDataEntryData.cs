using System.Globalization;
using ColorManager.ICC;

namespace ColorManagerTests.ICC.Data
{
    public static class TagDataEntryData
    {
        #region TagDataEntry Header

        public static readonly TypeSignature TagDataEntryHeader_UnknownVal = TypeSignature.Unknown;
        public static readonly byte[] TagDataEntryHeader_UnknownArr =
        {
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
        };

        public static readonly TypeSignature TagDataEntryHeader_MultiLocalizedUnicodeVal = TypeSignature.MultiLocalizedUnicode;
        public static readonly byte[] TagDataEntryHeader_MultiLocalizedUnicodeArr =
        {
            0x6D, 0x6C, 0x75, 0x63,
            0x00, 0x00, 0x00, 0x00,
        };

        public static readonly TypeSignature TagDataEntryHeader_CurveVal = TypeSignature.Curve;
        public static readonly byte[] TagDataEntryHeader_CurveArr =
        {
            0x63, 0x75, 0x72, 0x76,
            0x00, 0x00, 0x00, 0x00,
        };

        #endregion

        #region UnknownTagDataEntry

        public static readonly UnknownTagDataEntry Unknown_Val = new UnknownTagDataEntry(new byte[] { 0x00, 0x01, 0x02, 0x03 });
        public static readonly byte[] Unknown_Arr = { 0x00, 0x01, 0x02, 0x03 };

        #endregion

        #region ChromaticityTagDataEntry

        public static readonly ChromaticityTagDataEntry Chromaticity_Val1 = new ChromaticityTagDataEntry(3, ColorantEncoding.ITU_R_BT_709_2);
        public static readonly byte[] Chromaticity_Arr1 = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_3,
            PrimitivesData.UInt16_1
        );

        public static readonly ChromaticityTagDataEntry Chromaticity_Val2 = new ChromaticityTagDataEntry
        (
            2, ColorantEncoding.Unknown,
            new double[][]
            {
                new double[] { 1, 2 },
                new double[] { 3, 4 },
            }
        );
        public static readonly byte[] Chromaticity_Arr2 = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_0,

            PrimitivesData.UFix16_1,
            PrimitivesData.UFix16_2,

            PrimitivesData.UFix16_3,
            PrimitivesData.UFix16_4
        );

        /// <summary>
        /// <see cref="CorruptProfileException"/>: channel count must be 3 for any enum other than <see cref="ColorantEncoding.Unknown"/>
        /// </summary>
        public static readonly byte[] Chromaticity_ArrInvalid1 = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_5,
            PrimitivesData.UInt16_1
        );

        /// <summary>
        /// <see cref="CorruptProfileException"/>: invalid enum value
        /// </summary>
        public static readonly byte[] Chromaticity_ArrInvalid2 = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_3,
            PrimitivesData.UInt16_9
        );

        #endregion

        #region ColorantOrderTagDataEntry

        public static readonly ColorantOrderTagDataEntry ColorantOrder_Val = new ColorantOrderTagDataEntry(3, new byte[] { 0x00, 0x01, 0x02 });
        public static readonly byte[] ColorantOrder_Arr = ArrayHelper.Concat(PrimitivesData.UInt32_3, new byte[] { 0x00, 0x01, 0x02 });

        #endregion

        #region ColorantTableTagDataEntry

        public static readonly ColorantTableTagDataEntry ColorantTable_Val = new ColorantTableTagDataEntry
        (
            2,
            new ColorantTableEntry[]
            {
                StructsData.ColorantTableEntry_ValRand1,
                StructsData.ColorantTableEntry_ValRand2
            }
        );
        public static readonly byte[] ColorantTable_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_2,
            StructsData.ColorantTableEntry_Rand1,
            StructsData.ColorantTableEntry_Rand2
        );

        #endregion

        #region CurveTagDataEntry

        public static readonly CurveTagDataEntry Curve_Val_0 = new CurveTagDataEntry();
        public static readonly byte[] Curve_Arr_0 = PrimitivesData.UInt32_0;

        public static readonly CurveTagDataEntry Curve_Val_1 = new CurveTagDataEntry(1d);
        public static readonly byte[] Curve_Arr_1 = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_1,
            PrimitivesData.UFix8_1
        );

        public static readonly CurveTagDataEntry Curve_Val_2 = new CurveTagDataEntry(new double[] { 1 / 65535d, 2 / 65535d, 3 / 65535d });
        public static readonly byte[] Curve_Arr_2 = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_3,
            PrimitivesData.UInt16_1,
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_3
        );

        #endregion

        #region DataTagDataEntry

        public static readonly DataTagDataEntry Data_ValNoASCII = new DataTagDataEntry
        (
            new byte[] { 0x01, 0x02, 0x03, 0x04 },
            false
        );
        public static readonly byte[] Data_ArrNoASCII =
        {
            0x00, 0x00, 0x00, 0x00,
            0x01, 0x02, 0x03, 0x04
        };

        public static readonly DataTagDataEntry Data_ValASCII = new DataTagDataEntry
        (
            new byte[] { (byte)'A', (byte)'S', (byte)'C', (byte)'I', (byte)'I' },
            true
        );
        public static readonly byte[] Data_ArrASCII =
        {
            0x00, 0x00, 0x00, 0x01,
            (byte)'A', (byte)'S', (byte)'C', (byte)'I', (byte)'I'
        };

        #endregion

        #region DateTimeTagDataEntry

        public static readonly DateTimeTagDataEntry DateTime_Val = new DateTimeTagDataEntry(StructsData.DateTime_ValRand1);
        public static readonly byte[] DateTime_Arr = StructsData.DateTime_Rand1;

        #endregion

        #region Lut16TagDataEntry

        public static readonly Lut16TagDataEntry Lut16_Val = new Lut16TagDataEntry
        (
            MatrixData.Fix16_2D_ValGrad,
            new LUT[] { LUTData.LUT16_ValGrad, LUTData.LUT16_ValGrad },
            LUTData.CLUT16_ValGrad,
            new LUT[] { LUTData.LUT16_ValGrad, LUTData.LUT16_ValGrad, LUTData.LUT16_ValGrad }
        );
        public static readonly byte[] Lut16_Arr = ArrayHelper.Concat
        (
            new byte[] { 0x02, 0x03, 0x03, 0x00 },
            MatrixData.Fix16_2D_Grad,
            new byte[] { 0x00, (byte)LUTData.LUT16_ValGrad.Values.Length, 0x00, (byte)LUTData.LUT16_ValGrad.Values.Length },

            LUTData.LUT16_Grad,
            LUTData.LUT16_Grad,

            LUTData.CLUT16_Grad,

            LUTData.LUT16_Grad,
            LUTData.LUT16_Grad,
            LUTData.LUT16_Grad
        );

        #endregion

        #region Lut8TagDataEntry

        public static readonly Lut8TagDataEntry Lut8_Val = new Lut8TagDataEntry
        (
            MatrixData.Fix16_2D_ValGrad,
            new LUT[] { LUTData.LUT8_ValGrad, LUTData.LUT8_ValGrad },
            LUTData.CLUT8_ValGrad,
            new LUT[] { LUTData.LUT8_ValGrad, LUTData.LUT8_ValGrad, LUTData.LUT8_ValGrad }
        );
        public static readonly byte[] Lut8_Arr = ArrayHelper.Concat
        (
            new byte[] { 0x02, 0x03, 0x03, 0x00 },
            MatrixData.Fix16_2D_Grad,

            LUTData.LUT8_Grad,
            LUTData.LUT8_Grad,

            LUTData.CLUT8_Grad,

            LUTData.LUT8_Grad,
            LUTData.LUT8_Grad,
            LUTData.LUT8_Grad
        );

        #endregion

        #region LutAToBTagDataEntry

        private static readonly byte[] CurveFull_0 = ArrayHelper.Concat
        (
            TagDataEntryHeader_CurveArr,
            Curve_Arr_0
        );
        private static readonly byte[] CurveFull_1 = ArrayHelper.Concat
        (
            TagDataEntryHeader_CurveArr,
            Curve_Arr_1
        );
        private static readonly byte[] CurveFull_2 = ArrayHelper.Concat
        (
            TagDataEntryHeader_CurveArr,
            Curve_Arr_2
        );

        public static readonly LutAToBTagDataEntry LutAToB_Val = new LutAToBTagDataEntry
        (
            2, 3,
            MatrixData.Fix16_2D_ValGrad,
            MatrixData.Fix16_1D_ValGrad,
            LUTData.CLUT_Val16,
            new TagDataEntry[]
            {
                Curve_Val_0,
                Curve_Val_1,
                Curve_Val_2,
            },
            new TagDataEntry[]
            {
                Curve_Val_1,
                Curve_Val_2,
                Curve_Val_0,
            },
            new TagDataEntry[]
            {
                Curve_Val_2,
                Curve_Val_1,
            }
        );
        public static readonly byte[] LutAToB_Arr = ArrayHelper.Concat
        (
            new byte[] { 0x02, 0x03, 0x00, 0x00 },

            new byte[] { 0x00, 0x00, 0x00, 0x20 },  //b:        32
            new byte[] { 0x00, 0x00, 0x00, 0x50 },  //matrix:   80
            new byte[] { 0x00, 0x00, 0x00, 0x80 },  //m:        128
            new byte[] { 0x00, 0x00, 0x00, 0xB0 },  //clut:     176
            new byte[] { 0x00, 0x00, 0x00, 0xFC },  //a:        252

            //B
            CurveFull_0,    //12 bytes
            CurveFull_1,    //14 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_2,    //18 bytes
            new byte[] { 0x00, 0x00 }, // Padding

            //Matrix
            MatrixData.Fix16_2D_Grad,   //36 bytes
            MatrixData.Fix16_1D_Grad,   //12 bytes

            //M
            CurveFull_1,    //14 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_2,    //18 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_0,    //12 bytes

            //CLUT
            LUTData.CLUT_16,           //74 bytes
            new byte[] { 0x00, 0x00 }, // Padding

            //A
            CurveFull_2,    //18 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_1,    //14 bytes
            new byte[] { 0x00, 0x00 }  // Padding
        );

        #endregion

        #region LutBToATagDataEntry

        public static readonly LutBToATagDataEntry LutBToA_Val = new LutBToATagDataEntry
        (
            2, 3,
            MatrixData.Fix16_2D_ValGrad,
            MatrixData.Fix16_1D_ValGrad,
            LUTData.CLUT_Val16,
            new TagDataEntry[]
            {
                Curve_Val_0,
                Curve_Val_1,
            },
            new TagDataEntry[]
            {
                Curve_Val_1,
                Curve_Val_2,
            },
            new TagDataEntry[]
            {
                Curve_Val_2,
                Curve_Val_1,
                Curve_Val_0,
            }
        );
        public static readonly byte[] LutBToA_Arr = ArrayHelper.Concat
        (
            new byte[] { 0x02, 0x03, 0x00, 0x00 },

            new byte[] { 0x00, 0x00, 0x00, 0x20 },  //b:        32
            new byte[] { 0x00, 0x00, 0x00, 0x3C },  //matrix:   60
            new byte[] { 0x00, 0x00, 0x00, 0x6C },  //m:        108
            new byte[] { 0x00, 0x00, 0x00, 0x90 },  //clut:     144
            new byte[] { 0x00, 0x00, 0x00, 0xDC },  //a:        220

            //B
            CurveFull_0,    //12 bytes
            CurveFull_1,    //14 bytes
            new byte[] { 0x00, 0x00 }, // Padding

            //Matrix
            MatrixData.Fix16_2D_Grad,   //36 bytes
            MatrixData.Fix16_1D_Grad,    //12 bytes

            //M
            CurveFull_1,    //14 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_2,    //18 bytes
            new byte[] { 0x00, 0x00 }, // Padding

            //CLUT
            LUTData.CLUT_16,           //74 bytes
            new byte[] { 0x00, 0x00 }, // Padding

            //A
            CurveFull_2,    //18 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_1,    //14 bytes
            new byte[] { 0x00, 0x00 }, // Padding
            CurveFull_0     //12 bytes
        );

        #endregion

        #region MeasurementTagDataEntry

        public static readonly MeasurementTagDataEntry Measurement_Val = new MeasurementTagDataEntry
        (
            StandardObserver.CIE1931_Observer, StructsData.XYZNumber_ValVar1,
            MeasurementGeometry.MG_0d_d0, 1d, StandardIlluminant.D50
        );
        public static readonly byte[] Measurement_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_1,
            StructsData.XYZNumber_Var1,
            PrimitivesData.UInt32_2,
            PrimitivesData.UFix16_1,
            PrimitivesData.UInt32_1
        );

        #endregion

        #region MultiLocalizedUnicodeTagDataEntry

        private static readonly LocalizedString LocalizedString_Rand1 = new LocalizedString(new CultureInfo("en-US"), PrimitivesData.Unicode_ValRand2);
        private static readonly LocalizedString LocalizedString_Rand2 = new LocalizedString(new CultureInfo("de-DE"), PrimitivesData.Unicode_ValRand3);

        private static readonly LocalizedString[] LocalizedString_RandArr1 = new LocalizedString[]
        {
            LocalizedString_Rand1,
            LocalizedString_Rand2,
        };
        private static readonly LocalizedString[] LocalizedString_RandArr2 = new LocalizedString[]
        {
            LocalizedString_Rand2,
            LocalizedString_Rand1,
        };

        public static readonly MultiLocalizedUnicodeTagDataEntry MultiLocalizedUnicode_Val = new MultiLocalizedUnicodeTagDataEntry(LocalizedString_RandArr1);
        public static readonly byte[] MultiLocalizedUnicode_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_2,
            new byte[] { 0x00, 0x00, 0x00, 0x0C }, //12

            new byte[] { (byte)'e', (byte)'n', (byte)'U', (byte)'S' },
            new byte[] { 0x00, 0x00, 0x00, 0x0C },  //12
            new byte[] { 0x00, 0x00, 0x00, 0x28 },  //40

            new byte[] { (byte)'d', (byte)'e', (byte)'D', (byte)'E' },
            new byte[] { 0x00, 0x00, 0x00, 0x0E },  //14
            new byte[] { 0x00, 0x00, 0x00, 0x34 },  //52

            PrimitivesData.Unicode_Rand2,
            PrimitivesData.Unicode_Rand3
        );

        #endregion

        #region MultiProcessElementsTagDataEntry

        public static readonly MultiProcessElementsTagDataEntry MultiProcessElements_Val = new MultiProcessElementsTagDataEntry
        (
            2, 3,
            new MultiProcessElement[]
            {
                MultiProcessElementData.MPE_ValCLUT,
                MultiProcessElementData.MPE_ValCLUT,
            }
        );
        public static readonly byte[] MultiProcessElements_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_3,
            PrimitivesData.UInt32_2,

            new byte[] { 0x00, 0x00, 0x00, 0x20 },  //32
            new byte[] { 0x00, 0x00, 0x00, 0x84 },  //132

            new byte[] { 0x00, 0x00, 0x00, 0xA4 },  //164
            new byte[] { 0x00, 0x00, 0x00, 0x84 },  //132

            MultiProcessElementData.MPE_CLUT,
            MultiProcessElementData.MPE_CLUT
        );

        #endregion

        #region NamedColor2TagDataEntry

        public static readonly NamedColor2TagDataEntry NamedColor2_Val = new NamedColor2TagDataEntry
        (
            new byte[] { 0x01, 0x02, 0x03, 0x04 },
            3, ArrayHelper.Fill('A', 32), ArrayHelper.Fill('4', 32),
            new NamedColor[]
            {
                StructsData.NamedColor_ValMin,
                StructsData.NamedColor_ValMin
            }
        );
        public static readonly byte[] NamedColor2_Arr = ArrayHelper.Concat
        (
            new byte[] { 0x01, 0x02, 0x03, 0x04 },
            PrimitivesData.UInt32_2,
            PrimitivesData.UInt32_3,
            ArrayHelper.Fill((byte)0x41, 32),
            ArrayHelper.Fill((byte)0x34, 32),
            StructsData.NamedColor_Min,
            StructsData.NamedColor_Min
        );

        #endregion

        #region ParametricCurveTagDataEntry

        public static readonly ParametricCurveTagDataEntry ParametricCurve_Val = new ParametricCurveTagDataEntry(CurveData.Parametric_ValVar1);
        public static readonly byte[] ParametricCurve_Arr = CurveData.Parametric_Var1;

        #endregion

        #region ProfileSequenceDescTagDataEntry

        public static readonly ProfileSequenceDescTagDataEntry ProfileSequenceDesc_Val = new ProfileSequenceDescTagDataEntry
        (
            new ProfileDescription[]
            {
                StructsData.ProfileDescription_ValRand1,
                StructsData.ProfileDescription_ValRand1
            }
        );
        public static readonly byte[] ProfileSequenceDesc_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_2,
            StructsData.ProfileDescription_Rand1,
            StructsData.ProfileDescription_Rand1
        );

        #endregion

        #region ProfileSequenceIdentifierTagDataEntry

        public static readonly ProfileSequenceIdentifierTagDataEntry ProfileSequenceIdentifier_Val = new ProfileSequenceIdentifierTagDataEntry
        (
            new ProfileSequenceIdentifier[]
            {
                new ProfileSequenceIdentifier(StructsData.ProfileID_ValRand, LocalizedString_RandArr1),
                new ProfileSequenceIdentifier(StructsData.ProfileID_ValRand, LocalizedString_RandArr1),
            }
        );
        public static readonly byte[] ProfileSequenceIdentifier_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_2,

            new byte[] { 0x00, 0x00, 0x00, 0x1C },  //28
            new byte[] { 0x00, 0x00, 0x00, 0x54 },  //84

            new byte[] { 0x00, 0x00, 0x00, 0x70 },  //112
            new byte[] { 0x00, 0x00, 0x00, 0x54 },  //84

            StructsData.ProfileID_Rand,                 //16 bytes
            TagDataEntryHeader_MultiLocalizedUnicodeArr,//8  bytes
            MultiLocalizedUnicode_Arr,                  //58 bytes
            new byte[] { 0x00, 0x00 },                  //2  bytes (padding)

            StructsData.ProfileID_Rand,
            TagDataEntryHeader_MultiLocalizedUnicodeArr,
            MultiLocalizedUnicode_Arr,
            new byte[] { 0x00, 0x00 }
        );

        #endregion

        #region ResponseCurveSet16TagDataEntry

        public static readonly ResponseCurveSet16TagDataEntry ResponseCurveSet16_Val = new ResponseCurveSet16TagDataEntry
        (
            3,
            new ResponseCurve[]
            {
                CurveData.Response_ValGrad,
                CurveData.Response_ValGrad,
            }
        );
        public static readonly byte[] ResponseCurveSet16_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_3,
            PrimitivesData.UInt16_2,

            new byte[] { 0x00, 0x00, 0x00, 0x14 },  //20
            new byte[] { 0x00, 0x00, 0x00, 0x6C },  //108

            CurveData.Response_Grad, //88 bytes
            CurveData.Response_Grad  //88 bytes
        );

        #endregion

        #region Fix16ArrayTagDataEntry

        public static readonly Fix16ArrayTagDataEntry Fix16Array_Val = new Fix16ArrayTagDataEntry(new double[] { 1 / 256d, 2 / 256d, 3 / 256d });
        public static readonly byte[] Fix16Array_Arr = ArrayHelper.Concat
        (
            PrimitivesData.Fix16_1,
            PrimitivesData.Fix16_2,
            PrimitivesData.Fix16_3
        );

        #endregion

        #region SignatureTagDataEntry

        public static readonly SignatureTagDataEntry Signature_Val = new SignatureTagDataEntry("ABCD");
        public static readonly byte[] Signature_Arr = { 0x41, 0x42, 0x43, 0x44, };

        #endregion

        #region TextTagDataEntry

        public static readonly TextTagDataEntry Text_Val = new TextTagDataEntry("ABCD");
        public static readonly byte[] Text_Arr = { 0x41, 0x42, 0x43, 0x44 };

        #endregion

        #region UFix16ArrayTagDataEntry

        public static readonly UFix16ArrayTagDataEntry UFix16Array_Val = new UFix16ArrayTagDataEntry(new double[] { 1, 2, 3 });
        public static readonly byte[] UFix16Array_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UFix16_1,
            PrimitivesData.UFix16_2,
            PrimitivesData.UFix16_3
        );

        #endregion

        #region UInt16ArrayTagDataEntry

        public static readonly UInt16ArrayTagDataEntry UInt16Array_Val = new UInt16ArrayTagDataEntry(new ushort[] { 1, 2, 3 });
        public static readonly byte[] UInt16Array_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt16_1,
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_3
        );

        #endregion

        #region UInt32ArrayTagDataEntry

        public static readonly UInt32ArrayTagDataEntry UInt32Array_Val = new UInt32ArrayTagDataEntry(new uint[] { 1, 2, 3 });
        public static readonly byte[] UInt32Array_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_1,
            PrimitivesData.UInt32_2,
            PrimitivesData.UInt32_3
        );

        #endregion

        #region UInt64ArrayTagDataEntry

        public static readonly UInt64ArrayTagDataEntry UInt64Array_Val = new UInt64ArrayTagDataEntry(new ulong[] { 1, 2, 3 });
        public static readonly byte[] UInt64Array_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt64_1,
            PrimitivesData.UInt64_2,
            PrimitivesData.UInt64_3
        );

        #endregion

        #region UInt8ArrayTagDataEntry

        public static readonly UInt8ArrayTagDataEntry UInt8Array_Val = new UInt8ArrayTagDataEntry(new byte[] { 1, 2, 3 });
        public static readonly byte[] UInt8Array_Arr = { 1, 2, 3 };

        #endregion

        #region ViewingConditionsTagDataEntry

        public static readonly ViewingConditionsTagDataEntry ViewingConditions_Val = new ViewingConditionsTagDataEntry
        (
            StructsData.XYZNumber_ValVar1,
            StructsData.XYZNumber_ValVar2,
            StandardIlluminant.D50
        );
        public static readonly byte[] ViewingConditions_Arr = ArrayHelper.Concat
        (
            StructsData.XYZNumber_Var1,
            StructsData.XYZNumber_Var2,
            PrimitivesData.UInt32_1
        );

        #endregion

        #region XYZTagDataEntry

        public static readonly XYZTagDataEntry XYZ_Val = new XYZTagDataEntry(new XYZNumber[]
        {
            StructsData.XYZNumber_ValVar1,
            StructsData.XYZNumber_ValVar2,
            StructsData.XYZNumber_ValVar3,
        });
        public static readonly byte[] XYZ_Arr = ArrayHelper.Concat
        (
            StructsData.XYZNumber_Var1,
            StructsData.XYZNumber_Var2,
            StructsData.XYZNumber_Var3
        );

        #endregion

        #region TextDescriptionTagDataEntry

        public static readonly TextDescriptionTagDataEntry TextDescription_Val = new TextDescriptionTagDataEntry
        (
            PrimitivesData.ASCII_ValAll, PrimitivesData.Unicode_ValRand1, ArrayHelper.Fill('A', 66),
            9, 2
        );
        public static readonly byte[] TextDescription_Arr = ArrayHelper.Concat
        (
            new byte[] { 0x00, 0x00, 0x00, 0x81 },  //129
            PrimitivesData.ASCII_All,
            new byte[] { 0x00 },                    //Null terminator

            PrimitivesData.UInt32_9,
            new byte[] { 0x00, 0x00, 0x00, 0x0E },  //14
            PrimitivesData.Unicode_Rand1,
            new byte[] { 0x00, 0x00 },              //Null terminator

            new byte[] { 0x00, 0x02, 0x43 },        //2, 67
            ArrayHelper.Fill((byte)0x41, 66),
            new byte[] { 0x00 }                     //Null terminator
        );

        #endregion

        #region TagDataEntry

        public static readonly TagDataEntry TagDataEntry_CurveVal = Curve_Val_2;
        public static readonly byte[] TagDataEntry_CurveArr = ArrayHelper.Concat
        (
            TagDataEntryHeader_CurveArr,
            Curve_Arr_2,
            new byte[] { 0x00, 0x00 }//padding
        );

        public static readonly TagDataEntry TagDataEntry_MultiLocalizedUnicodeVal = MultiLocalizedUnicode_Val;
        public static readonly byte[] TagDataEntry_MultiLocalizedUnicodeArr = ArrayHelper.Concat
        (
            TagDataEntryHeader_MultiLocalizedUnicodeArr,
            MultiLocalizedUnicode_Arr,
            new byte[] { 0x00, 0x00 }//padding
        );

        public static readonly TagTableEntry TagDataEntry_MultiLocalizedUnicodeTable = new TagTableEntry
        (
            TagSignature.Unknown, 0,
            (uint)TagDataEntry_MultiLocalizedUnicodeArr.Length - 2
        );

        public static readonly TagTableEntry TagDataEntry_CurveTable = new TagTableEntry
        (
            TagSignature.Unknown, 0,
            (uint)TagDataEntry_CurveArr.Length - 2
        );

        #endregion
    }
}
