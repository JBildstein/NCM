using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorManager.ICC;

namespace ColorManagerTests.ICC
{
    [TestClass]
    public class ICCDataReaderTest
    {
        #region Read Primitives

        [TestMethod]
        public void ReadUInt16()
        {
            var data = new byte[] { 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            ushort value = reader.ReadUInt16();
            Assert.IsTrue(value == ushort.MinValue, "Read Min");

            data = new byte[] { 0x80, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadUInt16();
            Assert.IsTrue(value == 1 + ushort.MaxValue / 2, "Read Mid");

            data = new byte[] { 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadUInt16();
            Assert.IsTrue(value == ushort.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt16()
        {
            var data = new byte[] { 0x80, 0x00 };
            var reader = new ICCDataReader(data);
            short value = reader.ReadInt16();
            Assert.IsTrue(value == short.MinValue, "Read Min");

            data = new byte[] { 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadInt16();
            Assert.IsTrue(value == 0, "Read Mid");

            data = new byte[] { 0x7F, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadInt16();
            Assert.IsTrue(value == short.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadUInt32()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            uint value = reader.ReadUInt32();
            Assert.IsTrue(value == uint.MinValue, "Read Min");

            data = new byte[] { 0x80, 0x00, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadUInt32();
            Assert.IsTrue(value == 1 + uint.MaxValue / 2, "Read Mid");

            data = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadUInt32();
            Assert.IsTrue(value == uint.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt32()
        {
            var data = new byte[] { 0x80, 0x00, 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            int value = reader.ReadInt32();
            Assert.IsTrue(value == int.MinValue, "Read Min");

            data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadInt32();
            Assert.IsTrue(value == 0, "Read Mid");

            data = new byte[] { 0x7F, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadInt32();
            Assert.IsTrue(value == int.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadUInt64()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            ulong value = reader.ReadUInt64();
            Assert.IsTrue(value == ulong.MinValue, "Read Min");

            data = new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadUInt64();
            Assert.IsTrue(value == 1 + ulong.MaxValue / 2, "Read Mid");

            data = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadUInt64();
            Assert.IsTrue(value == ulong.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt64()
        {
            var data = new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            long value = reader.ReadInt64();
            Assert.IsTrue(value == long.MinValue, "Read Min");

            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadInt64();
            Assert.IsTrue(value == 0, "Read Mid");

            data = new byte[] { 0x7F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadInt64();
            Assert.IsTrue(value == long.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadSingle()
        {
            var data = new byte[] { 0xFF, 0x7F, 0xFF, 0xFF };
            var reader = new ICCDataReader(data);
            float value = reader.ReadSingle();
            Assert.AreEqual(float.MinValue, value, "Read Min");

            data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadSingle();
            Assert.AreEqual(0f, value, "Read Zero");

            data = new byte[] {  0x3F, 0x80, 0x00, 0x00  };
            reader = new ICCDataReader(data);
            value = reader.ReadSingle();
            Assert.AreEqual(1f, value, "Read One");

            data = new byte[] { 0x7F, 0x7F, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadSingle();
            Assert.AreEqual(float.MaxValue, value, "Read Max");
        }

        [TestMethod]
        public void ReadDouble()
        {
            var data = new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            var reader = new ICCDataReader(data);
            double value = reader.ReadDouble();
            Assert.AreEqual(double.MinValue, value, "Read Min");

            data = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            reader = new ICCDataReader(data);
            value = reader.ReadDouble();
            Assert.AreEqual(0d, value, "Read Zero");

            data = new byte[] { 0x3F, 0xF0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            reader = new ICCDataReader(data);
            value = reader.ReadDouble();
            Assert.AreEqual(1d, value, "Read One");

            data = new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadDouble();
            Assert.AreEqual(double.MaxValue, value, "Read Max");
        }

        [TestMethod]
        public void ReadFix16()
        {
            var data = new byte[] { 0x80, 0x00, 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            double value = reader.ReadFix16();
            Assert.AreEqual(short.MinValue, value, "Read Min");

            data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadFix16();
            Assert.AreEqual(0, value, "Read Zero");

            data = new byte[] { 0x00, 0x01, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadFix16();
            Assert.AreEqual(1, value, "Read One");

            data = new byte[] { 0x7F, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadFix16();
            Assert.AreEqual(short.MaxValue + 65535d / 65536d, value, "Read Max");
        }

        [TestMethod]
        public void ReadUFix16()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            double value = reader.ReadUFix16();
            Assert.AreEqual(0, value, "Read Min");
            
            data = new byte[] { 0x00, 0x01, 0x00, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadUFix16();
            Assert.AreEqual(1, value, "Read One");

            data = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadUFix16();
            Assert.AreEqual(ushort.MaxValue + 65535d / 65536d, value, "Read Max");
        }

        [TestMethod]
        public void ReadU1Fix15()
        {
            var data = new byte[] { 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            double value = reader.ReadU1Fix15();
            Assert.AreEqual(0, value, "Read Min");

            data = new byte[] { 0x80, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadU1Fix15();
            Assert.AreEqual(1, value, "Read One");

            data = new byte[] { 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadU1Fix15();
            Assert.AreEqual(1d + 32767d / 32768d, value, "Read Max");
        }

        [TestMethod]
        public void ReadUFix8()
        {
            var data = new byte[] { 0x00, 0x00 };
            var reader = new ICCDataReader(data);
            double value = reader.ReadUFix8();
            Assert.AreEqual(0, value, "Read Min");

            data = new byte[] { 0x01, 0x00 };
            reader = new ICCDataReader(data);
            value = reader.ReadUFix8();
            Assert.AreEqual(1, value, "Read One");

            data = new byte[] { 0xFF, 0xFF };
            reader = new ICCDataReader(data);
            value = reader.ReadUFix8();
            Assert.AreEqual(byte.MaxValue + 255d / 256d, value, "Read Max");
        }
        
        [TestMethod]
        public void ReadASCIIString()
        {
            var data = new byte[128];
            var resultArr = new char[128];
            for (int i = 0; i < 128; i++)
            {
                data[i] = (byte)i;
                resultArr[i] = (char)i;
            }
            var result = new string(resultArr);

            var reader = new ICCDataReader(data);
            string value = reader.ReadASCIIString(128);

            Assert.IsFalse(value == null, "Read string is null");
            Assert.IsTrue(value.Length == result.Length, "Read length does not match");
            Assert.AreEqual(result, value, false);
        }

        [TestMethod]
        public void ReadUnicodeString()
        {
            var data = new byte[]
            {
                0x00, 0x2e, //.
                0x00, 0x36, //6
                0x00, 0x41, //A
                0x00, 0x62, //b
                0x00, 0xe4, //ä
                0x00, 0xf1, //ñ
                0x00, 0x24, //$
                0x20, 0xAC, //€
                0x03, 0xb2, //β
                0xD8, 0x01, 0xDC, 0x37, //𐐷
                0xD8, 0x52, 0xDF, 0x62, //𤭢
            };
            var result = ".6Abäñ$€β𐐷𤭢";

            var reader = new ICCDataReader(data);
            string value = reader.ReadUnicodeString(data.Length);

            Assert.IsFalse(value == null, "Read string is null");
            Assert.IsTrue(value.Length == result.Length, "Read length does not match");
            Assert.AreEqual(result, value, false);
        }

        #endregion

        #region Read Structs

        [TestMethod]
        public void ReadDateTime()
        {
            //Year (1-9999)
            //Month 1-12
            //Day 1-31
            //Hour 0-23
            //Minute 0-59
            //Second 0-59

            var data = new byte[]
            {
                0x00, 0x01, //Year      1
                0x00, 0x01, //Month     1
                0x00, 0x01, //Day       1
                0x00, 0x00, //Hour      0
                0x00, 0x00, //Minute    0
                0x00, 0x00, //Second    0
            };
            var result = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var reader = new ICCDataReader(data);
            var value = reader.ReadDateTime();
            Assert.IsTrue(value == result, "Read Min");

            data = new byte[]
            {
                0x27, 0x0F, //Year      9999
                0x00, 0x0C, //Month     12
                0x00, 0x1F, //Day       31
                0x00, 0x17, //Hour      23
                0x00, 0x3B, //Minute    59
                0x00, 0x3B, //Second    59
            };
            result = new DateTime(9999, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            reader = new ICCDataReader(data);
            value = reader.ReadDateTime();
            Assert.IsTrue(value == result, "Read Max");
            
            data = new byte[]
            {
                0xFF, 0xFF, //Year      65535
                0x00, 0x0E, //Month     14
                0x00, 0x21, //Day       33
                0x00, 0x19, //Hour      25
                0x00, 0x3D, //Minute    61
                0x00, 0x3D, //Second    61
            };
            reader = new ICCDataReader(data);
            try
            {
                value = reader.ReadDateTime();
                Assert.Fail("Reading invalid value did not throw exception");
            }
            catch (InvalidProfileException) { Assert.IsTrue(true); }
        }

        [TestMethod]
        public void ReadVersionNumber()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var result = new VersionNumber(0, 0, 0);
            var reader = new ICCDataReader(data);
            var value = reader.ReadVersionNumber();
            Assert.IsTrue(value == result, "Read Min");
            Assert.AreEqual("0.0.0.0", value.ToString(), "Read Min ToString");

            data = new byte[] { 0x04, 0x30, 0x00, 0x00 };
            result = new VersionNumber(4, 3, 0);
            reader = new ICCDataReader(data);
            value = reader.ReadVersionNumber();
            Assert.IsTrue(value == result, "Read Version 4.3");
            Assert.AreEqual("4.3.0.0", value.ToString(), "Read Version 4.3 ToString");

            data = new byte[] { 0x02, 0x11, 0x00, 0x00 };
            result = new VersionNumber(2, 1, 1);
            reader = new ICCDataReader(data);
            value = reader.ReadVersionNumber();
            Assert.IsTrue(value == result, "Read Version 2.1.1");
            Assert.AreEqual("2.1.1.0", value.ToString(), "Read Version 2.1.1 ToString");

            data = new byte[] { 0xFF, 0xFF, 0x00, 0x00 };
            result = new VersionNumber(255, 15, 15);
            reader = new ICCDataReader(data);
            value = reader.ReadVersionNumber();
            Assert.IsTrue(value == result, "Read Max");
            Assert.AreEqual("255.15.15.0", value.ToString(), "Read Max ToString");
        }

        [TestMethod]
        public void ReadProfileFlag()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var arr = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            var result = new ProfileFlag(arr);
            var reader = new ICCDataReader(data);
            var value = reader.ReadProfileFlag();
            Assert.IsTrue(value == result, "Read Min");

            data = new byte[] { 0x00, 0x01, 0x00, 0x00 };
            arr =new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            result = new ProfileFlag(arr);
            reader = new ICCDataReader(data);
            value = reader.ReadProfileFlag();
            Assert.IsTrue(value == result, "Read Flag: Embedded");

            data = new byte[] { 0x00, 0x02, 0x00, 0x00 };
            arr = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            result = new ProfileFlag(arr);
            reader = new ICCDataReader(data);
            value = reader.ReadProfileFlag();
            Assert.IsTrue(value == result, "Read Flag: Not Independent");

            data = new byte[] { 0xFF, 0xFF, 0x00, 0x00 };
            arr = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
            result = new ProfileFlag(arr);
            reader = new ICCDataReader(data);
            value = reader.ReadProfileFlag();
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadDeviceAttribute()
        {
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var result = new DeviceAttribute(false, false, false, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            var reader = new ICCDataReader(data);
            var value = reader.ReadDeviceAttribute();
            Assert.IsTrue(value == result, "Read Min");

            data = new byte[] { 0x50, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0xFF };
            result = new DeviceAttribute(false, true, false, true, new byte[] { 0x00, 0xFF, 0x00, 0xFF });
            reader = new ICCDataReader(data);
            value = reader.ReadDeviceAttribute();
            Assert.IsTrue(value == result, "Read Var1");

            data = new byte[] { 0xA0, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x0F, 0x00 };
            result = new DeviceAttribute(true, false, true, false, new byte[] { 0x0F, 0x00, 0x0F, 0x00 });
            reader = new ICCDataReader(data);
            value = reader.ReadDeviceAttribute();
            Assert.IsTrue(value == result, "Read Var2");

            data = new byte[] { 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF };
            result = new DeviceAttribute(true, true, true, true, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            reader = new ICCDataReader(data);
            value = reader.ReadDeviceAttribute();
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadXYZNumber()
        {
            var data = new byte[]
            {
                0x80, 0x00, 0x00, 0x00, //X
                0x80, 0x00, 0x00, 0x00, //Y
                0x80, 0x00, 0x00, 0x00, //Z
            };
            var result = new XYZNumber(short.MinValue, short.MinValue, short.MinValue);
            var reader = new ICCDataReader(data);
            var value = reader.ReadXYZNumber();
            Assert.IsTrue(value == result, "Read Min");

            data = new byte[]
            {
                0x00, 0x00, 0x00, 0x00, //X
                0x00, 0x00, 0x00, 0x00, //Y
                0x00, 0x00, 0x00, 0x00, //Z
            };
            result = new XYZNumber(0, 0, 0);
            reader = new ICCDataReader(data);
            value = reader.ReadXYZNumber();
            Assert.IsTrue(value == result, "Read Zero");

            data = new byte[]
            {
                0x00, 0x01, 0x00, 0x00, //X
                0x00, 0x01, 0x00, 0x00, //Y
                0x00, 0x01, 0x00, 0x00, //Z
            };
            result = new XYZNumber(1, 1, 1);
            reader = new ICCDataReader(data);
            value = reader.ReadXYZNumber();
            Assert.IsTrue(value == result, "Read One");

            const double max = short.MaxValue + 65535d / 65536d;
            data = new byte[]
            {
                0x7F, 0xFF, 0xFF, 0xFF, //X
                0x7F, 0xFF, 0xFF, 0xFF, //Y
                0x7F, 0xFF, 0xFF, 0xFF, //Z
            };
            result = new XYZNumber(max, max, max);
            reader = new ICCDataReader(data);
            value = reader.ReadXYZNumber();
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadProfileID()
        {
            var data = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            };
            var result = new ProfileID(0, 0, 0, 0);
            var reader = new ICCDataReader(data);
            var value = reader.ReadProfileID();
            Assert.IsTrue(value == result, "Read Min");
            
            data = new byte[]
            {
                0x68, 0x3F, 0xD6, 0x6B,
                0xE6, 0xB4, 0x12, 0xDD,
                0x3E, 0x97, 0x1B, 0x5E,
                0xD3, 0x9C, 0x8F, 0x4A,
            };
            result = new ProfileID(1749014123, 3870560989, 1050090334, 3550252874);
            reader = new ICCDataReader(data);
            value = reader.ReadProfileID();
            Assert.IsTrue(value == result, "Read Random");
            
            data = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
            };
            result = new ProfileID(uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue);
            reader = new ICCDataReader(data);
            value = reader.ReadProfileID();
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadPositionNumber()
        {
            var data = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            };
            var result = new PositionNumber(0, 0);
            var reader = new ICCDataReader(data);
            var value = reader.ReadPositionNumber();
            Assert.IsTrue(value == result, "Read Min");

            data = new byte[]
            {
                0x68, 0x3F, 0xD6, 0x6B,
                0xE6, 0xB4, 0x12, 0xDD,
            };
            result = new PositionNumber(1749014123, 3870560989);
            reader = new ICCDataReader(data);
            value = reader.ReadPositionNumber();
            Assert.IsTrue(value == result, "Read Random");

            data = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF,
            };
            result = new PositionNumber(uint.MaxValue, uint.MaxValue);
            reader = new ICCDataReader(data);
            value = reader.ReadPositionNumber();
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadResponseNumber()
        {
            var data = new byte[]
            {
                0x00, 0x00,
                0x80, 0x00, 0x00, 0x00,
            };
            var result = new ResponseNumber(0, short.MinValue);
            var reader = new ICCDataReader(data);
            var value = reader.ReadResponseNumber();
            Assert.IsTrue(value == result, "Read Min");

            data = new byte[]
            {
                0x00, 0x01,
                0x00, 0x01, 0x00, 0x00,
            };
            result = new ResponseNumber(1, 1);
            reader = new ICCDataReader(data);
            value = reader.ReadResponseNumber();
            Assert.IsTrue(value == result, "Read One");

            data = new byte[]
            {
                0xFF, 0xFF,
                0x7F, 0xFF, 0xFF, 0xFF,
            };
            result = new ResponseNumber(ushort.MaxValue, short.MaxValue + 65535d / 65536d);
            reader = new ICCDataReader(data);
            value = reader.ReadResponseNumber();
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadNamedColor()
        {
            const int devCoordCount = 3;

            var data = new byte[32 + 6 + devCoordCount * 2];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < 32) data[i] = 0x41;         //Name (0x41==A)
                else if (i < 32 + 6) data[i] = 0x00;//PCS Coordinates
                else data[i] = 0x00;                //Device Coordinates
            }
            var r_name = "".PadRight(32, 'A');
            var r_pcs = new ushort[] { 0, 0, 0 };
            var r_dev = new ushort[devCoordCount] { 0, 0, 0 };            
            var result = new NamedColor(r_name, r_pcs, r_dev);
            var reader = new ICCDataReader(data);
            var value = reader.ReadNamedColor(devCoordCount);
            Assert.IsTrue(value == result, "Read Min");


            data = new byte[32 + 6 + devCoordCount * 2];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < 32) data[i] = 0x34;         //Name (0x34==4)
                else if (i < 32 + 6) data[i] = 0xFF;//PCS Coordinates
                else data[i] = 0xFF;                //Device Coordinates
            }
            r_name = "".PadRight(32, '4');
            r_pcs = new ushort[] { ushort.MaxValue, ushort.MaxValue, ushort.MaxValue };
            r_dev = new ushort[devCoordCount] { ushort.MaxValue, ushort.MaxValue, ushort.MaxValue };
            result = new NamedColor(r_name, r_pcs, r_dev);
            reader = new ICCDataReader(data);
            value = reader.ReadNamedColor(devCoordCount);
            Assert.IsTrue(value == result, "Read Max");
        }

        [TestMethod]
        public void ReadProfileDescription()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Read Tag Data Entries
        
        [TestMethod]
        public void ReadTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadTagDataEntryHeader()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadUnknownTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadChromaticityTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadColorantOrderTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadColorantTableTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCurveTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadDataTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadDateTimeTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadLut16TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadLut8TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadLutAToBTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadLutBToATagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadMeasurementTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadMultiLocalizedUnicodeTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadMultiProcessElementsTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadNamedColor2TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadParametricCurveTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadProfileSequenceDescTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadProfileSequenceIdentifierTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadResponseCurveSet16TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadFix16ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadSignatureTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadTextTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadUFix16ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadUInt16ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadUInt32ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadUInt64ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadUInt8ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadViewingConditionsTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadXYZTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Read Matrix

        [TestMethod]
        public void ReadMatrix2D()
        {
            var data = new byte[]
            {
                0x00, 0x01, 0x00, 0x00, //1
                0x00, 0x04, 0x00, 0x00, //4
                0x00, 0x07, 0x00, 0x00, //7

                0x00, 0x02, 0x00, 0x00, //2
                0x00, 0x05, 0x00, 0x00, //5
                0x00, 0x08, 0x00, 0x00, //8

                0x00, 0x03, 0x00, 0x00, //3
                0x00, 0x06, 0x00, 0x00, //6
                0x00, 0x09, 0x00, 0x00, //9
            };
            var result = new double[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            };
            var reader = new ICCDataReader(data);
            var value = reader.ReadMatrix(3, 3, false);
            CollectionAssert.AreEqual(result, value, "Read Fix16");

            data = new byte[]
            {
                0x3F, 0x80, 0x00, 0x00, //1
                0x40, 0x80, 0x00, 0x00, //4
                0x40, 0xE0, 0x00, 0x00, //7

                0x40, 0x00, 0x00, 0x00, //2
                0x40, 0xA0, 0x00, 0x00, //5
                0x41, 0x00, 0x00, 0x00, //8

                0x40, 0x40, 0x00, 0x00, //3
                0x40, 0xC0, 0x00, 0x00, //6
                0x41, 0x10, 0x00, 0x00, //9
            };
            result = new double[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            };
            reader = new ICCDataReader(data);
            value = reader.ReadMatrix(3, 3, true);
            CollectionAssert.AreEqual(result, value, "Read Single");
        }

        [TestMethod]
        public void ReadMatrix1D()
        {
            var data = new byte[]
            {
                0x00, 0x01, 0x00, 0x00, //1
                0x00, 0x04, 0x00, 0x00, //4
                0x00, 0x07, 0x00, 0x00, //7
            };
            var result = new double[] { 1, 4, 7 };
            var reader = new ICCDataReader(data);
            var value = reader.ReadMatrix(3, false);
            CollectionAssert.AreEqual(result, value, "Read Fix16");

            data = new byte[]
            {
                0x3F, 0x80, 0x00, 0x00, //1
                0x40, 0x80, 0x00, 0x00, //4
                0x40, 0xE0, 0x00, 0x00, //7
            };
            result = new double[] { 1, 4, 7 };
            reader = new ICCDataReader(data);
            value = reader.ReadMatrix(3, true);
            CollectionAssert.AreEqual(result, value, "Read Single");
        }

        #endregion

        #region Read (C)LUT

        [TestMethod]
        public void ReadLUT16()
        {
            var data = new byte[]
            {
                0x00, 0x01, //1
                0x80, 0x00, //32768
                0xFF, 0xFF, //65535
            };
            var result = new LUT(new double[] { 1d / ushort.MaxValue, 32768d / ushort.MaxValue, 1d });
            var reader = new ICCDataReader(data);
            var value = reader.ReadLUT16(3);
            Assert.IsTrue(result == value);
        }

        [TestMethod]
        public void ReadLUT8()
        {
            var data = new byte[256];
            var resultArr = new double[256];
            for (int i = 0; i < 256; i++)
            {
                data[i] = (byte)i;
                resultArr[i] = i / 255d;
            }
            var result = new LUT(resultArr);
            var reader = new ICCDataReader(data);
            var value = reader.ReadLUT8();
            Assert.IsTrue(result == value);
        }

        [TestMethod]
        public void ReadCLUT()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCLUT8()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCLUT16()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCLUTf32()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Read MultiProcessElement

        [TestMethod]
        public void ReadMultiProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCurveSetProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadMatrixProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCLUTProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Read Curves

        [TestMethod]
        public void ReadOneDimensionalCurve()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadResponseCurve()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadParametricCurve()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadCurveSegment()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadFormulaCurveElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadSampledCurveElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion
    }
}