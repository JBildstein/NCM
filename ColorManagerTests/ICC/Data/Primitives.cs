
namespace ColorManagerTests.ICC.Data
{
    public static class Primitives
    {
        public const double Fix16_ValMin = short.MinValue;
        public const double Fix16_ValMax = short.MaxValue + 65535d / 65536d;

        public const double UFix16_ValMin = 0;
        public const double UFix16_ValMax = ushort.MaxValue + 65535d / 65536d;

        public const double U1Fix15_ValMin = 0;
        public const double U1Fix15_ValMax = 1d + 32767d / 32768d;

        public const double UFix8_ValMin = 0;
        public const double UFix8_ValMax = byte.MaxValue + 255d / 256d;
        

        public static readonly byte[] UInt16_0 = { 0x00, 0x00 };
        public static readonly byte[] UInt16_1 = { 0x00, 0x01 };
        public static readonly byte[] UInt16_Max = { 0xFF, 0xFF };

        public static readonly byte[] Int16_Min = { 0x80, 0x00 };
        public static readonly byte[] Int16_0 = { 0x00, 0x00 };
        public static readonly byte[] Int16_1 = { 0x00, 0x01 };
        public static readonly byte[] Int16_Max = { 0x7F, 0xFF };

        public static readonly byte[] UInt32_0 = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] UInt32_1 = { 0x00, 0x00, 0x00, 0x01 };
        public static readonly byte[] UInt32_Max = { 0xFF, 0xFF, 0xFF, 0xFF };

        public static readonly byte[] Int32_Min = { 0x80, 0x00, 0x00, 0x00 };
        public static readonly byte[] Int32_0 = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Int32_1 = { 0x00, 0x00, 0x00, 0x01 };
        public static readonly byte[] Int32_Max = { 0x7F, 0xFF, 0xFF, 0xFF };

        public static readonly byte[] UInt64_0 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] UInt64_1 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
        public static readonly byte[] UInt64_Max = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        public static readonly byte[] Int64_Min = { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Int64_0 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Int64_1 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
        public static readonly byte[] Int64_Max = { 0x7F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        
        public static readonly byte[] Single_Min = { 0xFF, 0x7F, 0xFF, 0xFF };
        public static readonly byte[] Single_0 = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Single_1 = { 0x3F, 0x80, 0x00, 0x00 };
        public static readonly byte[] Single_Max = { 0x7F, 0x7F, 0xFF, 0xFF };

        public static readonly byte[] Double_Min = { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        public static readonly byte[] Double_0 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Double_1 = { 0x3F, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Double_Max = { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        public static readonly byte[] Fix16_Min = { 0x80, 0x00, 0x00, 0x00 };
        public static readonly byte[] Fix16_0 = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] Fix16_1 = { 0x00, 0x01, 0x00, 0x00 };
        public static readonly byte[] Fix16_Max = { 0x7F, 0xFF, 0xFF, 0xFF };
        
        public static readonly byte[] UFix16_0 = { 0x00, 0x00, 0x00, 0x00 };
        public static readonly byte[] UFix16_1 = { 0x00, 0x01, 0x00, 0x00 };
        public static readonly byte[] UFix16_Max = { 0xFF, 0xFF, 0xFF, 0xFF };

        public static readonly byte[] U1Fix15_0 = { 0x00, 0x00 };
        public static readonly byte[] U1Fix15_1 = { 0x80, 0x00 };
        public static readonly byte[] U1Fix15_Max = { 0xFF, 0xFF };

        public static readonly byte[] UFix8_0 = { 0x00, 0x00 };
        public static readonly byte[] UFix8_1 = { 0x01, 0x00 };
        public static readonly byte[] UFix8_Max = { 0xFF, 0xFF };
    }
}
