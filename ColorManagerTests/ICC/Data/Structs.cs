using System;
using ColorManager.ICC;

namespace ColorManagerTests.ICC.Data
{
    public static class Structs
    {
        #region DateTime

        public static readonly DateTime DateTime_ValMin = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime DateTime_ValMax = new DateTime(9999, 12, 31, 23, 59, 59, DateTimeKind.Utc);

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
        public static readonly byte[] DeviceAttribute_Max = { 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF };

        #endregion

        #region XYZNumber

        public static readonly XYZNumber XYZNumber_ValMin = new XYZNumber(Primitives.Fix16_ValMin, Primitives.Fix16_ValMin, Primitives.Fix16_ValMin);
        public static readonly XYZNumber XYZNumber_Val0 = new XYZNumber(0, 0, 0);
        public static readonly XYZNumber XYZNumber_Val1 = new XYZNumber(1, 1, 1);
        public static readonly XYZNumber XYZNumber_ValVar = new XYZNumber(Primitives.Fix16_ValMin, 1, Primitives.Fix16_ValMax);
        public static readonly XYZNumber XYZNumber_ValMax = new XYZNumber(Primitives.Fix16_ValMax, Primitives.Fix16_ValMax, Primitives.Fix16_ValMax);

        public static readonly byte[] XYZNumber_Min = ArrayHelper.Concat(Primitives.Fix16_Min, Primitives.Fix16_Min, Primitives.Fix16_Min);
        public static readonly byte[] XYZNumber_0 = ArrayHelper.Concat(Primitives.Fix16_0, Primitives.Fix16_0, Primitives.Fix16_0);
        public static readonly byte[] XYZNumber_1 = ArrayHelper.Concat(Primitives.Fix16_1, Primitives.Fix16_1, Primitives.Fix16_1);
        public static readonly byte[] XYZNumber_Var = ArrayHelper.Concat(Primitives.Fix16_Min, Primitives.Fix16_1, Primitives.Fix16_Max);
        public static readonly byte[] XYZNumber_Max = ArrayHelper.Concat(Primitives.Fix16_Max, Primitives.Fix16_Max, Primitives.Fix16_Max);

        #endregion

        #region ProfileID

        public static readonly ProfileID ProfileID_ValMin = new ProfileID(0, 0, 0, 0);
        public static readonly ProfileID ProfileID_ValRand = new ProfileID(Primitives.UInt32_ValRand1, Primitives.UInt32_ValRand2, Primitives.UInt32_ValRand3, Primitives.UInt32_ValRand4);
        public static readonly ProfileID ProfileID_ValMax = new ProfileID(uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue);

        public static readonly byte[] ProfileID_Min = ArrayHelper.Concat(Primitives.UInt32_0, Primitives.UInt32_0, Primitives.UInt32_0, Primitives.UInt32_0);
        public static readonly byte[] ProfileID_Rand = ArrayHelper.Concat(Primitives.UInt32_Rand1, Primitives.UInt32_Rand2, Primitives.UInt32_Rand3, Primitives.UInt32_Rand4);
        public static readonly byte[] ProfileID_Max = ArrayHelper.Concat(Primitives.UInt32_Max, Primitives.UInt32_Max, Primitives.UInt32_Max, Primitives.UInt32_Max);

        #endregion

        #region PositionNumber

        public static readonly PositionNumber PositionNumber_ValMin = new PositionNumber(0, 0);
        public static readonly PositionNumber PositionNumber_ValRand = new PositionNumber(Primitives.UInt32_ValRand1, Primitives.UInt32_ValRand2);
        public static readonly PositionNumber PositionNumber_ValMax = new PositionNumber(uint.MaxValue, uint.MaxValue);

        public static readonly byte[] PositionNumber_Min = ArrayHelper.Concat(Primitives.UInt32_0, Primitives.UInt32_0);
        public static readonly byte[] PositionNumber_Rand = ArrayHelper.Concat(Primitives.UInt32_Rand1, Primitives.UInt32_Rand2);
        public static readonly byte[] PositionNumber_Max = ArrayHelper.Concat(Primitives.UInt32_Max, Primitives.UInt32_Max);

        #endregion

        #region ResponseNumber

        public static readonly ResponseNumber ResponseNumber_ValMin = new ResponseNumber(0, Primitives.Fix16_ValMin);
        public static readonly ResponseNumber ResponseNumber_Val1 = new ResponseNumber(1, 1);
        public static readonly ResponseNumber ResponseNumber_ValMax = new ResponseNumber(ushort.MaxValue, Primitives.Fix16_ValMax);

        public static readonly byte[] ResponseNumber_Min = ArrayHelper.Concat(Primitives.UInt16_0, Primitives.Fix16_Min);
        public static readonly byte[] ResponseNumber_1 = ArrayHelper.Concat(Primitives.UInt16_1, Primitives.Fix16_1);
        public static readonly byte[] ResponseNumber_Max = ArrayHelper.Concat(Primitives.UInt16_Max, Primitives.Fix16_Max);

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
    }
}
