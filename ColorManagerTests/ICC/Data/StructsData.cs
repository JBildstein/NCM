using System;
using System.Globalization;
using ColorManager.ICC;

namespace ColorManagerTests.ICC.Data
{
    public static class StructsData
    {
        #region DateTime

        public static readonly DateTime DateTime_ValMin = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime DateTime_ValMax = new DateTime(9999, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        public static readonly DateTime DateTime_ValRand1 = new DateTime(1990, 11, 26, 3, 19, 47, DateTimeKind.Utc);

        public static readonly byte[] DateTime_Min =
        {
            0x00, 0x01, //Year      1
            0x00, 0x01, //Month     1
            0x00, 0x01, //Day       1
            0x00, 0x00, //Hour      0
            0x00, 0x00, //Minute    0
            0x00, 0x00, //Second    0
        };

        public static readonly byte[] DateTime_Max =
        {
                0x27, 0x0F, //Year      9999
                0x00, 0x0C, //Month     12
                0x00, 0x1F, //Day       31
                0x00, 0x17, //Hour      23
                0x00, 0x3B, //Minute    59
                0x00, 0x3B, //Second    59
        };

        public static readonly byte[] DateTime_Invalid =
        {
                0xFF, 0xFF, //Year      65535
                0x00, 0x0E, //Month     14
                0x00, 0x21, //Day       33
                0x00, 0x19, //Hour      25
                0x00, 0x3D, //Minute    61
                0x00, 0x3D, //Second    61
        };

        public static readonly byte[] DateTime_Rand1 =
        {
                0x07, 0xC6, //Year      1990
                0x00, 0x0B, //Month     11
                0x00, 0x1A, //Day       26
                0x00, 0x03, //Hour      3
                0x00, 0x13, //Minute    19
                0x00, 0x2F, //Second    47
        };

        #endregion

        #region VersionNumber

        public static readonly VersionNumber VersionNumber_ValMin = new VersionNumber(0, 0, 0);
        public static readonly VersionNumber VersionNumber_Val211 = new VersionNumber(2, 1, 1);
        public static readonly VersionNumber VersionNumber_Val430 = new VersionNumber(4, 3, 0);
        public static readonly VersionNumber VersionNumber_ValMax = new VersionNumber(255, 15, 15);

        public static readonly string VersionNumber_StrMin = "0.0.0.0";
        public static readonly string VersionNumber_Str211 = "2.1.1.0";
        public static readonly string VersionNumber_Str430 = "4.3.0.0";
        public static readonly string VersionNumber_StrMax = "255.15.15.0";

        public static readonly byte[] VersionNumber_Min = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] VersionNumber_211 = { 0x02, 0x11, 0x00, 0x00 };
        public static readonly byte[] VersionNumber_430 = { 0x04, 0x30, 0x00, 0x00 };
        public static readonly byte[] VersionNumber_Max = { 0xFF, 0xFF, 0x00, 0x00 };

        #endregion

        #region ProfileFlag

        public static readonly ProfileFlag ProfileFlag_ValMin = new ProfileFlag(new bool[]
        {
            false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false,
        });
        public static readonly ProfileFlag ProfileFlag_ValEmbedded = new ProfileFlag(new bool[]
        {
            true, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false,
        });
        public static readonly ProfileFlag ProfileFlag_ValNotIndependent = new ProfileFlag(new bool[]
        {
            false, true, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false,
        });
        public static readonly ProfileFlag ProfileFlag_ValMax = new ProfileFlag(new bool[]
        {
            true, true, true, true, true, true, true, true,
            true, true, true, true, true, true, true, true,
        });

        public static readonly byte[] ProfileFlag_Min = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] ProfileFlag_Embedded = { 0x00, 0x01, 0x00, 0x00 };
        public static readonly byte[] ProfileFlag_NotIndependent = { 0x00, 0x02, 0x00, 0x00 };
        public static readonly byte[] ProfileFlag_Max = { 0xFF, 0xFF, 0x00, 0x00 };

        #endregion

        #region DeviceAttribute

        public static readonly DeviceAttribute DeviceAttribute_ValMin = new DeviceAttribute(false, false, false, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });
        public static readonly DeviceAttribute DeviceAttribute_ValVar1 = new DeviceAttribute(false, true, false, true, new byte[] { 0x00, 0xFF, 0x00, 0xFF });
        public static readonly DeviceAttribute DeviceAttribute_ValVar2 = new DeviceAttribute(true, false, true, false, new byte[] { 0x0F, 0x00, 0x0F, 0x00 });
        public static readonly DeviceAttribute DeviceAttribute_ValMax = new DeviceAttribute(true, true, true, true, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

        public static readonly byte[] DeviceAttribute_Min = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] DeviceAttribute_Var1 = { 0x50, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0xFF };
        public static readonly byte[] DeviceAttribute_Var2 = { 0xA0, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x0F, 0x00 };
        public static readonly byte[] DeviceAttribute_Max = { 0xF0, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF };

        #endregion

        #region XYZNumber

        public static readonly XYZNumber XYZNumber_ValMin = new XYZNumber(PrimitivesData.Fix16_ValMin, PrimitivesData.Fix16_ValMin, PrimitivesData.Fix16_ValMin);
        public static readonly XYZNumber XYZNumber_Val0 = new XYZNumber(0, 0, 0);
        public static readonly XYZNumber XYZNumber_Val1 = new XYZNumber(1, 1, 1);
        public static readonly XYZNumber XYZNumber_ValVar1 = new XYZNumber(1, 2, 3);
        public static readonly XYZNumber XYZNumber_ValVar2 = new XYZNumber(4, 5, 6);
        public static readonly XYZNumber XYZNumber_ValVar3 = new XYZNumber(7, 8, 9);
        public static readonly XYZNumber XYZNumber_ValMax = new XYZNumber(PrimitivesData.Fix16_ValMax, PrimitivesData.Fix16_ValMax, PrimitivesData.Fix16_ValMax);

        public static readonly byte[] XYZNumber_Min = ArrayHelper.Concat(PrimitivesData.Fix16_Min, PrimitivesData.Fix16_Min, PrimitivesData.Fix16_Min);
        public static readonly byte[] XYZNumber_0 = ArrayHelper.Concat(PrimitivesData.Fix16_0, PrimitivesData.Fix16_0, PrimitivesData.Fix16_0);
        public static readonly byte[] XYZNumber_1 = ArrayHelper.Concat(PrimitivesData.Fix16_1, PrimitivesData.Fix16_1, PrimitivesData.Fix16_1);
        public static readonly byte[] XYZNumber_Var1 = ArrayHelper.Concat(PrimitivesData.Fix16_1, PrimitivesData.Fix16_2, PrimitivesData.Fix16_3);
        public static readonly byte[] XYZNumber_Var2 = ArrayHelper.Concat(PrimitivesData.Fix16_4, PrimitivesData.Fix16_5, PrimitivesData.Fix16_6);
        public static readonly byte[] XYZNumber_Var3 = ArrayHelper.Concat(PrimitivesData.Fix16_7, PrimitivesData.Fix16_8, PrimitivesData.Fix16_9);
        public static readonly byte[] XYZNumber_Max = ArrayHelper.Concat(PrimitivesData.Fix16_Max, PrimitivesData.Fix16_Max, PrimitivesData.Fix16_Max);

        #endregion

        #region ProfileID

        public static readonly ProfileID ProfileID_ValMin = new ProfileID(0, 0, 0, 0);
        public static readonly ProfileID ProfileID_ValRand = new ProfileID(PrimitivesData.UInt32_ValRand1, PrimitivesData.UInt32_ValRand2, PrimitivesData.UInt32_ValRand3, PrimitivesData.UInt32_ValRand4);
        public static readonly ProfileID ProfileID_ValMax = new ProfileID(uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue);

        public static readonly byte[] ProfileID_Min = ArrayHelper.Concat(PrimitivesData.UInt32_0, PrimitivesData.UInt32_0, PrimitivesData.UInt32_0, PrimitivesData.UInt32_0);
        public static readonly byte[] ProfileID_Rand = ArrayHelper.Concat(PrimitivesData.UInt32_Rand1, PrimitivesData.UInt32_Rand2, PrimitivesData.UInt32_Rand3, PrimitivesData.UInt32_Rand4);
        public static readonly byte[] ProfileID_Max = ArrayHelper.Concat(PrimitivesData.UInt32_Max, PrimitivesData.UInt32_Max, PrimitivesData.UInt32_Max, PrimitivesData.UInt32_Max);

        #endregion

        #region PositionNumber

        public static readonly PositionNumber PositionNumber_ValMin = new PositionNumber(0, 0);
        public static readonly PositionNumber PositionNumber_ValRand = new PositionNumber(PrimitivesData.UInt32_ValRand1, PrimitivesData.UInt32_ValRand2);
        public static readonly PositionNumber PositionNumber_ValMax = new PositionNumber(uint.MaxValue, uint.MaxValue);

        public static readonly byte[] PositionNumber_Min = ArrayHelper.Concat(PrimitivesData.UInt32_0, PrimitivesData.UInt32_0);
        public static readonly byte[] PositionNumber_Rand = ArrayHelper.Concat(PrimitivesData.UInt32_Rand1, PrimitivesData.UInt32_Rand2);
        public static readonly byte[] PositionNumber_Max = ArrayHelper.Concat(PrimitivesData.UInt32_Max, PrimitivesData.UInt32_Max);

        #endregion

        #region ResponseNumber

        public static readonly ResponseNumber ResponseNumber_ValMin = new ResponseNumber(0, PrimitivesData.Fix16_ValMin);
        public static readonly ResponseNumber ResponseNumber_Val1 = new ResponseNumber(1, 1);
        public static readonly ResponseNumber ResponseNumber_Val2 = new ResponseNumber(2, 2);
        public static readonly ResponseNumber ResponseNumber_Val3 = new ResponseNumber(3, 3);
        public static readonly ResponseNumber ResponseNumber_Val4 = new ResponseNumber(4, 4);
        public static readonly ResponseNumber ResponseNumber_Val5 = new ResponseNumber(5, 5);
        public static readonly ResponseNumber ResponseNumber_Val6 = new ResponseNumber(6, 6);
        public static readonly ResponseNumber ResponseNumber_Val7 = new ResponseNumber(7, 7);
        public static readonly ResponseNumber ResponseNumber_Val8 = new ResponseNumber(8, 8);
        public static readonly ResponseNumber ResponseNumber_Val9 = new ResponseNumber(9, 9);
        public static readonly ResponseNumber ResponseNumber_ValMax = new ResponseNumber(ushort.MaxValue, PrimitivesData.Fix16_ValMax);

        public static readonly byte[] ResponseNumber_Min = ArrayHelper.Concat(PrimitivesData.UInt16_0, PrimitivesData.Fix16_Min);
        public static readonly byte[] ResponseNumber_1 = ArrayHelper.Concat(PrimitivesData.UInt16_1, PrimitivesData.Fix16_1);
        public static readonly byte[] ResponseNumber_2 = ArrayHelper.Concat(PrimitivesData.UInt16_2, PrimitivesData.Fix16_2);
        public static readonly byte[] ResponseNumber_3 = ArrayHelper.Concat(PrimitivesData.UInt16_3, PrimitivesData.Fix16_3);
        public static readonly byte[] ResponseNumber_4 = ArrayHelper.Concat(PrimitivesData.UInt16_4, PrimitivesData.Fix16_4);
        public static readonly byte[] ResponseNumber_5 = ArrayHelper.Concat(PrimitivesData.UInt16_5, PrimitivesData.Fix16_5);
        public static readonly byte[] ResponseNumber_6 = ArrayHelper.Concat(PrimitivesData.UInt16_6, PrimitivesData.Fix16_6);
        public static readonly byte[] ResponseNumber_7 = ArrayHelper.Concat(PrimitivesData.UInt16_7, PrimitivesData.Fix16_7);
        public static readonly byte[] ResponseNumber_8 = ArrayHelper.Concat(PrimitivesData.UInt16_8, PrimitivesData.Fix16_8);
        public static readonly byte[] ResponseNumber_9 = ArrayHelper.Concat(PrimitivesData.UInt16_9, PrimitivesData.Fix16_9);
        public static readonly byte[] ResponseNumber_Max = ArrayHelper.Concat(PrimitivesData.UInt16_Max, PrimitivesData.Fix16_Max);

        #endregion

        #region NamedColor

        public static readonly NamedColor NamedColor_ValMin = new NamedColor
        (
            ArrayHelper.Fill('A', 32),
            new ushort[] { 0, 0, 0 },
            new ushort[] { 0, 0, 0 }
        );
        public static readonly NamedColor NamedColor_ValMax = new NamedColor
        (
            ArrayHelper.Fill('4', 32),
            new ushort[] { ushort.MaxValue, ushort.MaxValue, ushort.MaxValue },
            new ushort[] { ushort.MaxValue, ushort.MaxValue, ushort.MaxValue, ushort.MaxValue }
        );

        public static readonly byte[] NamedColor_Min = CreateNamedColor(3, 0x41, 0x00, 0x00);
        public static readonly byte[] NamedColor_Max = CreateNamedColor(4, 0x34, 0xFF, 0xFF);
        
        private static byte[] CreateNamedColor(int devCoordCount, byte name, byte PCS, byte device)
        {
            var data = new byte[32 + 6 + devCoordCount * 2];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < 32) data[i] = name;         //Name
                else if (i < 32 + 6) data[i] = PCS; //PCS Coordinates
                else data[i] = device;              //Device Coordinates
            }
            return data;
        }

        #endregion

        #region ProfileDescription

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

        private static readonly MultiLocalizedUnicodeTagDataEntry MultiLocalizedUnicode_Val = new MultiLocalizedUnicodeTagDataEntry(LocalizedString_RandArr1);
        private static readonly byte[] MultiLocalizedUnicode_Arr = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_2,
            //Size: 8(header) + 8(count/size) + 2 * 12(culture/length/offset) + 12(record1) + 14(record2)
            new byte[] { 0x00, 0x00, 0x00, 0x4A }, //66

            new byte[] { (byte)'e', (byte)'n', (byte)'U', (byte)'S' },
            new byte[] { 0x00, 0x00, 0x00, 0x0C },  //12
            new byte[] { 0x00, 0x00, 0x00, 0x28 },  //40

            new byte[] { (byte)'d', (byte)'e', (byte)'D', (byte)'E' },
            new byte[] { 0x00, 0x00, 0x00, 0x0E },  //14
            new byte[] { 0x00, 0x00, 0x00, 0x34 },  //52

            PrimitivesData.Unicode_Rand2,
            PrimitivesData.Unicode_Rand3
        );

        public static readonly ProfileDescription ProfileDescription_ValRand1 = new ProfileDescription
        (
            1, 2,
            DeviceAttribute_ValVar1,
            TagSignature.ProfileDescription,
            MultiLocalizedUnicode_Val.Text,
            MultiLocalizedUnicode_Val.Text
        );

        public static readonly byte[] ProfileDescription_Rand1 = ArrayHelper.Concat
        (
            PrimitivesData.UInt32_1,
            PrimitivesData.UInt32_2,
            DeviceAttribute_Var1,
            new byte[] { 0x64, 0x65, 0x73, 0x63 },

            new byte[] { 0x6D, 0x6C, 0x75, 0x63 },
            new byte[] { 0x00, 0x00, 0x00, 0x00 },
            MultiLocalizedUnicode_Arr,
            new byte[] { 0x6D, 0x6C, 0x75, 0x63 },
            new byte[] { 0x00, 0x00, 0x00, 0x00 },
            MultiLocalizedUnicode_Arr
        );

        #endregion

        #region ColorantTableEntry

        public static readonly ColorantTableEntry ColorantTableEntry_ValRand1 = new ColorantTableEntry(ArrayHelper.Fill('A', 32), 1, 2, 3);
        public static readonly ColorantTableEntry ColorantTableEntry_ValRand2 = new ColorantTableEntry(ArrayHelper.Fill('4', 32), 4, 5, 6);

        public static readonly byte[] ColorantTableEntry_Rand1 = ArrayHelper.Concat
        (
            ArrayHelper.Fill((byte)0x41, 32),
            PrimitivesData.UInt16_1,
            PrimitivesData.UInt16_2,
            PrimitivesData.UInt16_3
        );

        public static readonly byte[] ColorantTableEntry_Rand2 = ArrayHelper.Concat
        (
            ArrayHelper.Fill((byte)0x34, 32),
            PrimitivesData.UInt16_4,
            PrimitivesData.UInt16_5,
            PrimitivesData.UInt16_6
        );

        #endregion
    }
}
