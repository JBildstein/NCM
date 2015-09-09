using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorManager.ICC;

namespace ColorManagerTests.ICC
{
    [TestClass]
    public class ICCDataWriterTest
    {
        #region Write Primitives
        
        [TestMethod]
        public void WriteByte()
        {
            using (var stream = new MemoryStream(1))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteByte(byte.MinValue);
                Assert.IsTrue(c == 1, "Write length incorrect");

                var result = new byte[] { byte.MinValue };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(1))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteByte(128);
                Assert.IsTrue(c == 1, "Write length incorrect");

                var result = new byte[] { 128 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(1))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteByte(byte.MaxValue);
                Assert.IsTrue(c == 1, "Write length incorrect");

                var result = new byte[] { byte.MaxValue };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUInt16()
        {
            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16(ushort.MinValue);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16(1 + ushort.MaxValue / 2);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16(ushort.MaxValue);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteInt16()
        {
            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt16(short.MinValue);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt16(0);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt16(short.MaxValue);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x7F, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUInt32()
        {
            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32(uint.MinValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32(1 + uint.MaxValue / 2);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32(uint.MaxValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteInt32()
        {
            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt32(int.MinValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt32(0);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt32(int.MaxValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x7F, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUInt64()
        {
            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64(ulong.MinValue);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64(1 + ulong.MaxValue / 2);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64(ulong.MaxValue);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteInt64()
        {
            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt64(long.MinValue);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt64(0);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt64(long.MaxValue);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x7F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteSingle()
        {
            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(float.MinValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0xFF, 0x7F, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(0f);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(1f);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x3F, 0x80, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(float.MaxValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x7F, 0x7F, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteDouble()
        {
            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(double.MinValue);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(0d);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(1d);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x3F, 0xF0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream(8))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(double.MaxValue);
                Assert.IsTrue(c == 8, "Write length incorrect");

                var result = new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteFix16()
        {
            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(short.MinValue);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(0d);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(1d);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x01, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(short.MaxValue + 65535d / 65536d);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x7F, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUFix16()
        {
            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16(0d);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16(1d);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0x00, 0x01, 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream(4))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16(ushort.MaxValue + 65535d / 65536d);
                Assert.IsTrue(c == 4, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteU1Fix15()
        {
            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteU1Fix15(0d);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteU1Fix15(1d);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x80, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteU1Fix15(1d + 32767d / 32768d);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUFix8()
        {
            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix8(0d);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x00, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix8(1d);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0x01, 0x00 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream(2))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix8(byte.MaxValue + 255d / 256d);
                Assert.IsTrue(c == 2, "Write length incorrect");

                var result = new byte[] { 0xFF, 0xFF };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteASCIIString()
        {
            using (var stream = new MemoryStream(128))
            {
                var writer = new ICCDataWriter(stream);

                var result = new byte[128];
                var dataArr = new char[128];
                for (int i = 0; i < 128; i++)
                {
                    result[i] = (byte)i;
                    dataArr[i] = (char)i;
                }
                var data = new string(dataArr);
                
                int c = writer.WriteASCIIString(data);
                Assert.IsTrue(c == 128, "Write length incorrect");
                CollectionAssert.AreEqual(result, stream.ToArray(), "Written string is not the same");
            }
        }

        [TestMethod]
        public void WriteASCIIStringLength()
        {
            using (var stream = new MemoryStream(128))
            {
                var writer = new ICCDataWriter(stream);

                var result = new byte[128];
                var dataArr = new char[132];
                for (int i = 0; i < 132; i++)
                {
                    if (i < 128) result[i] = (byte)i;
                    dataArr[i] = (char)i;
                }
                var data = new string(dataArr);

                int c = writer.WriteASCIIString(data, 128);
                Assert.IsTrue(c == 128, "Write length incorrect");
                CollectionAssert.AreEqual(result, stream.ToArray(), "Written string is not the same");
            }
        }

        [TestMethod]
        public void WriteUnicodeString()
        {
            using (var stream = new MemoryStream(26))
            {
                var writer = new ICCDataWriter(stream);
                var data = ".6Abäñ$€β𐐷𤭢";
                var result = new byte[]
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

                int c = writer.WriteUnicodeString(data);
                Assert.IsTrue(c == 26, "Write length incorrect");
                CollectionAssert.AreEqual(result, stream.ToArray(), "Written string is not the same");
            }
        }

        #endregion

        #region Write Structs

        [TestMethod]
        public void WriteDateTime()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteVersionNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteProfileFlag()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteDeviceAttribute()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteXYZNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteProfileID()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WritePositionNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteResponseNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteNamedColor()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteProfileDescription()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write Tag Data Entries

        [TestMethod]
        public void WriteTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteTagDataEntryTest1()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteTagDataEntryHeader()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteUnknownTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteChromaticityTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteColorantOrderTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteColorantTableTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCurveTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteDataTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteDateTimeTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteLut16TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteLut8TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteLutAToBTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteLutBToATagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteMeasurementTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteMultiLocalizedUnicodeTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteMultiProcessElementsTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteNamedColor2TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteParametricCurveTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteProfileSequenceDescTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteProfileSequenceIdentifierTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteResponseCurveSet16TagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteFix16ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteSignatureTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteTextTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteUFix16ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteUInt16ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteUInt32ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteUInt64ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteUInt8ArrayTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteViewingConditionsTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteXYZTagDataEntry()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write Matrix

        [TestMethod]
        public void WriteMatrixTest2D()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteMatrix1D()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write (C)LUT

        [TestMethod]
        public void WriteLUT16()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteLUT8()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCLUT()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCLUT8()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCLUT16()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCLUTf32()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write MultiProcessElement

        [TestMethod]
        public void WriteMultiProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCurveSetProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteMatrixProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCLUTProcessElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write Curves
        
        [TestMethod]
        public void WriteOneDimensionalCurve()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteResponseCurve()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteParametricCurve()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteCurveSegment()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteFormulaCurveElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteSampledCurveElement()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write Array

        [TestMethod]
        public void WriteArrayByte()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteArrayUInt16()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteArrayInt16()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteArrayUInt32()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteArrayInt32()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WriteArrayUInt64()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Write Misc

        [TestMethod]
        public void WriteEmpty()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void WritePadding()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion
    }
}