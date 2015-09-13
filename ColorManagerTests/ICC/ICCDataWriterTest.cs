using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorManager.ICC;
using ColorManagerTests.ICC.Data;

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
                Assert.IsTrue(c == 1, "Write length incorrect Min");

                var result = new byte[] { byte.MinValue };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream(1))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteByte(128);
                Assert.IsTrue(c == 1, "Write length incorrect Mid");

                var result = new byte[] { 128 };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream(1))
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteByte(byte.MaxValue);
                Assert.IsTrue(c == 1, "Write length incorrect Max");

                var result = new byte[] { byte.MaxValue };
                CollectionAssert.AreEqual(result, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUInt16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16(ushort.MinValue);
                Assert.IsTrue(c == PrimitivesData.UInt16_0.Length, "Write length incorrect Min");                
                CollectionAssert.AreEqual(PrimitivesData.UInt16_0, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16(32768);
                Assert.IsTrue(c == PrimitivesData.UInt16_32768.Length, "Write length incorrect Mid");
                CollectionAssert.AreEqual(PrimitivesData.UInt16_32768, stream.ToArray(), "Write Mid");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16(ushort.MaxValue);
                Assert.IsTrue(c == PrimitivesData.UInt16_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.UInt16_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteInt16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt16(short.MinValue);
                Assert.IsTrue(c == PrimitivesData.Int16_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.Int16_Min, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt16(0);
                Assert.IsTrue(c == PrimitivesData.Int16_0.Length, "Write length incorrect Zero");
                CollectionAssert.AreEqual(PrimitivesData.Int16_0, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt16(short.MaxValue);
                Assert.IsTrue(c == PrimitivesData.Int16_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.Int16_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUInt32()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32(uint.MinValue);
                Assert.IsTrue(c == PrimitivesData.UInt32_0.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.UInt32_0, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32(PrimitivesData.UInt32_ValRand1);
                Assert.IsTrue(c == PrimitivesData.UInt32_Rand1.Length, "Write length incorrect Rand");
                CollectionAssert.AreEqual(PrimitivesData.UInt32_Rand1, stream.ToArray(), "Write Rand");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32(uint.MaxValue);
                Assert.IsTrue(c == PrimitivesData.UInt32_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.UInt32_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteInt32()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt32(int.MinValue);
                Assert.IsTrue(c == PrimitivesData.Int32_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.Int32_Min, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt32(0);
                Assert.IsTrue(c == PrimitivesData.Int32_0.Length, "Write length incorrect Zero");
                CollectionAssert.AreEqual(PrimitivesData.Int32_0, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt32(int.MaxValue);
                Assert.IsTrue(c == PrimitivesData.Int32_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.Int32_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUInt64()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64(ulong.MinValue);
                Assert.IsTrue(c == PrimitivesData.UInt64_0.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.UInt64_0, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64(9);
                Assert.IsTrue(c == PrimitivesData.UInt64_9.Length, "Write length incorrect 9");
                CollectionAssert.AreEqual(PrimitivesData.UInt64_9, stream.ToArray(), "Write 9");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64(ulong.MaxValue);
                Assert.IsTrue(c == PrimitivesData.UInt64_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.UInt64_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteInt64()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt64(long.MinValue);
                Assert.IsTrue(c == PrimitivesData.Int64_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.Int64_Min, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt64(0);
                Assert.IsTrue(c == PrimitivesData.Int64_0.Length, "Write length incorrect Zero");
                CollectionAssert.AreEqual(PrimitivesData.Int64_0, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteInt64(long.MaxValue);
                Assert.IsTrue(c == PrimitivesData.Int64_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.Int64_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteSingle()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(float.MinValue);
                Assert.IsTrue(c == PrimitivesData.Single_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.Single_Min, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(0f);
                Assert.IsTrue(c == PrimitivesData.Single_0.Length, "Write length incorrect Zero");
                CollectionAssert.AreEqual(PrimitivesData.Single_0, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(1f);
                Assert.IsTrue(c == PrimitivesData.Single_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(PrimitivesData.Single_1, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSingle(float.MaxValue);
                Assert.IsTrue(c == PrimitivesData.Single_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.Single_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteDouble()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(double.MinValue);
                Assert.IsTrue(c == PrimitivesData.Double_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.Double_Min, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(0d);
                Assert.IsTrue(c == PrimitivesData.Double_0.Length, "Write length incorrect Zero");
                CollectionAssert.AreEqual(PrimitivesData.Double_0, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(1d);
                Assert.IsTrue(c == PrimitivesData.Double_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(PrimitivesData.Double_1, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDouble(double.MaxValue);
                Assert.IsTrue(c == PrimitivesData.Double_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.Double_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteFix16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(PrimitivesData.Fix16_ValMin);
                Assert.IsTrue(c == PrimitivesData.Fix16_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.Fix16_Min, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(0d);
                Assert.IsTrue(c == PrimitivesData.Fix16_0.Length, "Write length incorrect Zero");
                CollectionAssert.AreEqual(PrimitivesData.Fix16_0, stream.ToArray(), "Write Zero");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(1d);
                Assert.IsTrue(c == PrimitivesData.Fix16_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(PrimitivesData.Fix16_1, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16(PrimitivesData.Fix16_ValMax);
                Assert.IsTrue(c == PrimitivesData.Fix16_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.Fix16_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUFix16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16(PrimitivesData.UFix16_ValMin);
                Assert.IsTrue(c == PrimitivesData.UFix16_0.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.UFix16_0, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16(1d);
                Assert.IsTrue(c == PrimitivesData.UFix16_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(PrimitivesData.UFix16_1, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16(PrimitivesData.UFix16_ValMax);
                Assert.IsTrue(c == PrimitivesData.UFix16_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.UFix16_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteU1Fix15()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteU1Fix15(0d);
                Assert.IsTrue(c == PrimitivesData.U1Fix15_0.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.U1Fix15_0, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteU1Fix15(1d);
                Assert.IsTrue(c == PrimitivesData.U1Fix15_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(PrimitivesData.U1Fix15_1, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteU1Fix15(PrimitivesData.U1Fix15_ValMax);
                Assert.IsTrue(c == PrimitivesData.U1Fix15_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.U1Fix15_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteUFix8()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix8(0d);
                Assert.IsTrue(c == PrimitivesData.UFix8_0.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(PrimitivesData.UFix8_0, stream.ToArray(), "Write Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix8(1d);
                Assert.IsTrue(c == PrimitivesData.UFix8_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(PrimitivesData.UFix8_1, stream.ToArray(), "Write One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix8(PrimitivesData.UFix8_ValMax);
                Assert.IsTrue(c == PrimitivesData.UFix8_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(PrimitivesData.UFix8_Max, stream.ToArray(), "Write Max");
            }
        }

        [TestMethod]
        public void WriteASCIIString()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteASCIIString(PrimitivesData.ASCII_ValAll);
                Assert.IsTrue(c == PrimitivesData.ASCII_All.Length, "Write length incorrect");
                CollectionAssert.AreEqual(PrimitivesData.ASCII_All, stream.ToArray(), "Written string is not the same");
            }
        }

        [TestMethod]
        public void WriteASCIIStringLength()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteASCIIString(PrimitivesData.ASCII_ValAll, PrimitivesData.ASCII_ValAll.Length);
                Assert.IsTrue(c == PrimitivesData.ASCII_All.Length, "Write length incorrect");
                CollectionAssert.AreEqual(PrimitivesData.ASCII_All, stream.ToArray(), "Written string is not the same");
            }
        }

        [TestMethod]
        public void WriteUnicodeString()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUnicodeString(PrimitivesData.Unicode_ValRand1);
                Assert.IsTrue(c == PrimitivesData.Unicode_Rand1.Length, "Write length incorrect");
                CollectionAssert.AreEqual(PrimitivesData.Unicode_Rand1, stream.ToArray(), "Written string is not the same");
            }
        }

        #endregion

        #region Write Structs

        [TestMethod]
        public void WriteDateTime()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDateTime(StructsData.DateTime_ValRand1);
                Assert.IsTrue(c == StructsData.DateTime_Rand1.Length, "Write length incorrect Rand");
                CollectionAssert.AreEqual(StructsData.DateTime_Rand1, stream.ToArray(), "Written value is not the same Rand");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDateTime(StructsData.DateTime_ValMin);
                Assert.IsTrue(c == StructsData.DateTime_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.DateTime_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDateTime(StructsData.DateTime_ValMax);
                Assert.IsTrue(c == StructsData.DateTime_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.DateTime_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteVersionNumber()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteVersionNumber(StructsData.VersionNumber_ValMin);
                Assert.IsTrue(c == StructsData.VersionNumber_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.VersionNumber_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteVersionNumber(StructsData.VersionNumber_Val211);
                Assert.IsTrue(c == StructsData.VersionNumber_211.Length, "Write length incorrect 2.1.1");
                CollectionAssert.AreEqual(StructsData.VersionNumber_211, stream.ToArray(), "Written value is not the same 2.1.1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteVersionNumber(StructsData.VersionNumber_Val430);
                Assert.IsTrue(c == StructsData.VersionNumber_430.Length, "Write length incorrect 4.3.0");
                CollectionAssert.AreEqual(StructsData.VersionNumber_430, stream.ToArray(), "Written value is not the same 4.3.0");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteVersionNumber(StructsData.VersionNumber_ValMax);
                Assert.IsTrue(c == StructsData.VersionNumber_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.VersionNumber_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteProfileFlag()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileFlag(StructsData.ProfileFlag_ValMin);
                Assert.IsTrue(c == StructsData.ProfileFlag_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.ProfileFlag_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileFlag(StructsData.ProfileFlag_ValEmbedded);
                Assert.IsTrue(c == StructsData.ProfileFlag_Embedded.Length, "Write length incorrect Embedded");
                CollectionAssert.AreEqual(StructsData.ProfileFlag_Embedded, stream.ToArray(), "Written value is not the same Embedded");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileFlag(StructsData.ProfileFlag_ValNotIndependent);
                Assert.IsTrue(c == StructsData.ProfileFlag_NotIndependent.Length, "Write length incorrect NotIndependent");
                CollectionAssert.AreEqual(StructsData.ProfileFlag_NotIndependent, stream.ToArray(), "Written value is not the same NotIndependent");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileFlag(StructsData.ProfileFlag_ValMax);
                Assert.IsTrue(c == StructsData.ProfileFlag_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.ProfileFlag_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteDeviceAttribute()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDeviceAttribute(StructsData.DeviceAttribute_ValMin);
                Assert.IsTrue(c == StructsData.DeviceAttribute_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.DeviceAttribute_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDeviceAttribute(StructsData.DeviceAttribute_ValVar1);
                Assert.IsTrue(c == StructsData.DeviceAttribute_Var1.Length, "Write length incorrect Var1");
                CollectionAssert.AreEqual(StructsData.DeviceAttribute_Var1, stream.ToArray(), "Written value is not the same Var1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDeviceAttribute(StructsData.DeviceAttribute_ValVar2);
                Assert.IsTrue(c == StructsData.DeviceAttribute_Var2.Length, "Write length incorrect Var2");
                CollectionAssert.AreEqual(StructsData.DeviceAttribute_Var2, stream.ToArray(), "Written value is not the same Var2");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDeviceAttribute(StructsData.DeviceAttribute_ValMax);
                Assert.IsTrue(c == StructsData.DeviceAttribute_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.DeviceAttribute_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteXYZNumber()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteXYZNumber(StructsData.XYZNumber_ValMin);
                Assert.IsTrue(c == StructsData.XYZNumber_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.XYZNumber_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteXYZNumber(StructsData.XYZNumber_Val1);
                Assert.IsTrue(c == StructsData.XYZNumber_1.Length, "Write length incorrect One");
                CollectionAssert.AreEqual(StructsData.XYZNumber_1, stream.ToArray(), "Written value is not the same One");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteXYZNumber(StructsData.XYZNumber_ValVar1);
                Assert.IsTrue(c == StructsData.XYZNumber_Var1.Length, "Write length incorrect Var1");
                CollectionAssert.AreEqual(StructsData.XYZNumber_Var1, stream.ToArray(), "Written value is not the same Var1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteXYZNumber(StructsData.XYZNumber_ValMax);
                Assert.IsTrue(c == StructsData.XYZNumber_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.XYZNumber_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteProfileID()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileID(StructsData.ProfileID_ValMin);
                Assert.IsTrue(c == StructsData.ProfileID_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.ProfileID_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileID(StructsData.ProfileID_ValRand);
                Assert.IsTrue(c == StructsData.ProfileID_Rand.Length, "Write length incorrect Rand");
                CollectionAssert.AreEqual(StructsData.ProfileID_Rand, stream.ToArray(), "Written value is not the same Rand");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileID(StructsData.ProfileID_ValMax);
                Assert.IsTrue(c == StructsData.ProfileID_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.ProfileID_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WritePositionNumber()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WritePositionNumber(StructsData.PositionNumber_ValMin);
                Assert.IsTrue(c == StructsData.PositionNumber_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.PositionNumber_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WritePositionNumber(StructsData.PositionNumber_ValRand);
                Assert.IsTrue(c == StructsData.PositionNumber_Rand.Length, "Write length incorrect Rand");
                CollectionAssert.AreEqual(StructsData.PositionNumber_Rand, stream.ToArray(), "Written value is not the same Rand");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WritePositionNumber(StructsData.PositionNumber_ValMax);
                Assert.IsTrue(c == StructsData.PositionNumber_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.PositionNumber_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteResponseNumber()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteResponseNumber(StructsData.ResponseNumber_ValMin);
                Assert.IsTrue(c == StructsData.ResponseNumber_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.ResponseNumber_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteResponseNumber(StructsData.ResponseNumber_Val3);
                Assert.IsTrue(c == StructsData.ResponseNumber_3.Length, "Write length incorrect Three");
                CollectionAssert.AreEqual(StructsData.ResponseNumber_3, stream.ToArray(), "Written value is not the same Three");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteResponseNumber(StructsData.ResponseNumber_ValMax);
                Assert.IsTrue(c == StructsData.ResponseNumber_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.ResponseNumber_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteNamedColor()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteNamedColor(StructsData.NamedColor_ValMin);
                Assert.IsTrue(c == StructsData.NamedColor_Min.Length, "Write length incorrect Min");
                CollectionAssert.AreEqual(StructsData.NamedColor_Min, stream.ToArray(), "Written value is not the same Min");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteNamedColor(StructsData.NamedColor_ValMax);
                Assert.IsTrue(c == StructsData.NamedColor_Max.Length, "Write length incorrect Max");
                CollectionAssert.AreEqual(StructsData.NamedColor_Max, stream.ToArray(), "Written value is not the same Max");
            }
        }

        [TestMethod]
        public void WriteProfileDescription()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileDescription(StructsData.ProfileDescription_ValRand1);
                Assert.IsTrue(c == StructsData.ProfileDescription_Rand1.Length, "Write length incorrect");
                CollectionAssert.AreEqual(StructsData.ProfileDescription_Rand1, stream.ToArray(), "Written value is not the same");
            }
        }

        #endregion

        #region Write Tag Data Entries

        [TestMethod]
        public void WriteTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteTagDataEntry(TagDataEntryData.TagDataEntry_CurveVal);
                c += writer.WritePadding();
                Assert.IsTrue(c == TagDataEntryData.TagDataEntry_CurveArr.Length, "Write length incorrect Curve");
                CollectionAssert.AreEqual(TagDataEntryData.TagDataEntry_CurveArr, stream.ToArray(), "Written value is not the same Curve");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteTagDataEntry(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeVal);
                c += writer.WritePadding();
                Assert.IsTrue(c == TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeArr.Length, "Write length incorrect MultiLocalizedUnicode");
                CollectionAssert.AreEqual(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeArr, stream.ToArray(), "Written value is not the same MultiLocalizedUnicode");
            }
        }

        [TestMethod]
        public void WriteTagDataEntryOutTable()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                TagTableEntry table;
                int c = writer.WriteTagDataEntry(TagDataEntryData.TagDataEntry_CurveVal, out table);
                Assert.IsTrue(c == TagDataEntryData.TagDataEntry_CurveArr.Length - 2, "Write length incorrect");
                Assert.AreEqual(TagDataEntryData.TagDataEntry_CurveTable, table, "TagTableEntry incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.TagDataEntry_CurveArr, stream.ToArray(), "Written value is not the same");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                TagTableEntry table;
                int c = writer.WriteTagDataEntry(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeVal, out table);
                Assert.IsTrue(c == TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeArr.Length - 2, "Write length incorrect");
                Assert.AreEqual(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeTable, table, "TagTableEntry incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeArr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteTagDataEntryHeader()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteTagDataEntryHeader(TagDataEntryData.TagDataEntryHeader_UnknownVal);
                Assert.IsTrue(c == TagDataEntryData.TagDataEntryHeader_UnknownArr.Length, "Write length incorrect Unknown");
                CollectionAssert.AreEqual(TagDataEntryData.TagDataEntryHeader_UnknownArr, stream.ToArray(), "Written value is not the same Unknown");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteTagDataEntryHeader(TagDataEntryData.TagDataEntryHeader_CurveVal);
                Assert.IsTrue(c == TagDataEntryData.TagDataEntryHeader_CurveArr.Length, "Write length incorrect Curve");
                CollectionAssert.AreEqual(TagDataEntryData.TagDataEntryHeader_CurveArr, stream.ToArray(), "Written value is not the same Curve");
            }
        }

        [TestMethod]
        public void WriteUnknownTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUnknownTagDataEntry(TagDataEntryData.Unknown_Val);
                Assert.IsTrue(c == TagDataEntryData.Unknown_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Unknown_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteChromaticityTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteChromaticityTagDataEntry(TagDataEntryData.Chromaticity_Val1);
                Assert.IsTrue(c == TagDataEntryData.Chromaticity_Arr1.Length, "Write length incorrect Arr1");
                CollectionAssert.AreEqual(TagDataEntryData.Chromaticity_Arr1, stream.ToArray(), "Written value is not the same Arr1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteChromaticityTagDataEntry(TagDataEntryData.Chromaticity_Val2);
                Assert.IsTrue(c == TagDataEntryData.Chromaticity_Arr2.Length, "Write length incorrect Arr2");
                CollectionAssert.AreEqual(TagDataEntryData.Chromaticity_Arr2, stream.ToArray(), "Written value is not the same Arr2");
            }
        }

        [TestMethod]
        public void WriteColorantOrderTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteColorantOrderTagDataEntry(TagDataEntryData.ColorantOrder_Val);
                Assert.IsTrue(c == TagDataEntryData.ColorantOrder_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ColorantOrder_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteColorantTableTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteColorantTableTagDataEntry(TagDataEntryData.ColorantTable_Val);
                Assert.IsTrue(c == TagDataEntryData.ColorantTable_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ColorantTable_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteCurveTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveTagDataEntry(TagDataEntryData.Curve_Val_0);
                Assert.IsTrue(c == TagDataEntryData.Curve_Arr_0.Length, "Write length incorrect Var0");
                CollectionAssert.AreEqual(TagDataEntryData.Curve_Arr_0, stream.ToArray(), "Written value is not the same Var0");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveTagDataEntry(TagDataEntryData.Curve_Val_1);
                Assert.IsTrue(c == TagDataEntryData.Curve_Arr_1.Length, "Write length incorrect Var1");
                CollectionAssert.AreEqual(TagDataEntryData.Curve_Arr_1, stream.ToArray(), "Written value is not the same Var1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveTagDataEntry(TagDataEntryData.Curve_Val_2);
                Assert.IsTrue(c == TagDataEntryData.Curve_Arr_2.Length, "Write length incorrect Var2");
                CollectionAssert.AreEqual(TagDataEntryData.Curve_Arr_2, stream.ToArray(), "Written value is not the same Var2");
            }
        }

        [TestMethod]
        public void WriteDataTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDataTagDataEntry(TagDataEntryData.Data_ValASCII);
                Assert.IsTrue(c == TagDataEntryData.Data_ArrASCII.Length, "Write length incorrect ASCII");
                CollectionAssert.AreEqual(TagDataEntryData.Data_ArrASCII, stream.ToArray(), "Written value is not the same ASCII");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDataTagDataEntry(TagDataEntryData.Data_ValNoASCII);
                Assert.IsTrue(c == TagDataEntryData.Data_ArrNoASCII.Length, "Write length incorrect NoASCII");
                CollectionAssert.AreEqual(TagDataEntryData.Data_ArrNoASCII, stream.ToArray(), "Written value is not the same NoASCII");
            }
        }

        [TestMethod]
        public void WriteDateTimeTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteDateTimeTagDataEntry(TagDataEntryData.DateTime_Val);
                Assert.IsTrue(c == TagDataEntryData.DateTime_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.DateTime_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteLut16TagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteLut16TagDataEntry(TagDataEntryData.Lut16_Val);
                Assert.IsTrue(c == TagDataEntryData.Lut16_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Lut16_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteLut8TagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteLut8TagDataEntry(TagDataEntryData.Lut8_Val);
                Assert.IsTrue(c == TagDataEntryData.Lut8_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Lut8_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteLutAToBTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteLutAToBTagDataEntry(TagDataEntryData.LutAToB_Val);
                Assert.IsTrue(c == TagDataEntryData.LutAToB_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.LutAToB_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteLutBToATagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteLutBToATagDataEntry(TagDataEntryData.LutBToA_Val);
                Assert.IsTrue(c == TagDataEntryData.LutBToA_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.LutBToA_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteMeasurementTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMeasurementTagDataEntry(TagDataEntryData.Measurement_Val);
                Assert.IsTrue(c == TagDataEntryData.Measurement_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Measurement_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteMultiLocalizedUnicodeTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiLocalizedUnicodeTagDataEntry(TagDataEntryData.MultiLocalizedUnicode_Val);
                Assert.IsTrue(c == TagDataEntryData.MultiLocalizedUnicode_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.MultiLocalizedUnicode_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteMultiProcessElementsTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiProcessElementsTagDataEntry(TagDataEntryData.MultiProcessElements_Val);
                Assert.IsTrue(c == TagDataEntryData.MultiProcessElements_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.MultiProcessElements_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteNamedColor2TagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteNamedColor2TagDataEntry(TagDataEntryData.NamedColor2_Val);
                Assert.IsTrue(c == TagDataEntryData.NamedColor2_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.NamedColor2_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteParametricCurveTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteParametricCurveTagDataEntry(TagDataEntryData.ParametricCurve_Val);
                Assert.IsTrue(c == TagDataEntryData.ParametricCurve_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ParametricCurve_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteProfileSequenceDescTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileSequenceDescTagDataEntry(TagDataEntryData.ProfileSequenceDesc_Val);
                Assert.IsTrue(c == TagDataEntryData.ProfileSequenceDesc_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ProfileSequenceDesc_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteProfileSequenceIdentifierTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteProfileSequenceIdentifierTagDataEntry(TagDataEntryData.ProfileSequenceIdentifier_Val);
                Assert.IsTrue(c == TagDataEntryData.ProfileSequenceIdentifier_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ProfileSequenceIdentifier_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteResponseCurveSet16TagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteResponseCurveSet16TagDataEntry(TagDataEntryData.ResponseCurveSet16_Val);
                Assert.IsTrue(c == TagDataEntryData.ResponseCurveSet16_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ResponseCurveSet16_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteFix16ArrayTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFix16ArrayTagDataEntry(TagDataEntryData.Fix16Array_Val);
                Assert.IsTrue(c == TagDataEntryData.Fix16Array_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Fix16Array_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteSignatureTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSignatureTagDataEntry(TagDataEntryData.Signature_Val);
                Assert.IsTrue(c == TagDataEntryData.Signature_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Signature_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteTextTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteTextTagDataEntry(TagDataEntryData.Text_Val);
                Assert.IsTrue(c == TagDataEntryData.Text_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.Text_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteUFix16ArrayTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUFix16ArrayTagDataEntry(TagDataEntryData.UFix16Array_Val);
                Assert.IsTrue(c == TagDataEntryData.UFix16Array_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.UFix16Array_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteUInt16ArrayTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt16ArrayTagDataEntry(TagDataEntryData.UInt16Array_Val);
                Assert.IsTrue(c == TagDataEntryData.UInt16Array_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.UInt16Array_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteUInt32ArrayTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt32ArrayTagDataEntry(TagDataEntryData.UInt32Array_Val);
                Assert.IsTrue(c == TagDataEntryData.UInt32Array_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.UInt32Array_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteUInt64ArrayTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt64ArrayTagDataEntry(TagDataEntryData.UInt64Array_Val);
                Assert.IsTrue(c == TagDataEntryData.UInt64Array_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.UInt64Array_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteUInt8ArrayTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteUInt8ArrayTagDataEntry(TagDataEntryData.UInt8Array_Val);
                Assert.IsTrue(c == TagDataEntryData.UInt8Array_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.UInt8Array_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteViewingConditionsTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteViewingConditionsTagDataEntry(TagDataEntryData.ViewingConditions_Val);
                Assert.IsTrue(c == TagDataEntryData.ViewingConditions_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.ViewingConditions_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteXYZTagDataEntry()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteXYZTagDataEntry(TagDataEntryData.XYZ_Val);
                Assert.IsTrue(c == TagDataEntryData.XYZ_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(TagDataEntryData.XYZ_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        #endregion

        #region Write Matrix

        [TestMethod]
        public void WriteMatrix2D()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMatrix(MatrixData.Fix16_2D_ValGrad, false);
                Assert.IsTrue(c == MatrixData.Fix16_2D_Grad.Length, "Write length incorrect Fix16");
                CollectionAssert.AreEqual(MatrixData.Fix16_2D_Grad, stream.ToArray(), "Written value is not the same Fix16");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMatrix(MatrixData.Single_2D_ValGrad, true);
                Assert.IsTrue(c == MatrixData.Single_2D_Grad.Length, "Write length incorrect Single");
                CollectionAssert.AreEqual(MatrixData.Single_2D_Grad, stream.ToArray(), "Written value is not the same Single");
            }
        }

        [TestMethod]
        public void WriteMatrix1D()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMatrix(MatrixData.Fix16_1D_ValGrad, false);
                Assert.IsTrue(c == MatrixData.Fix16_1D_Grad.Length, "Write length incorrect Fix16");
                CollectionAssert.AreEqual(MatrixData.Fix16_1D_Grad, stream.ToArray(), "Written value is not the same Fix16");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMatrix(MatrixData.Single_1D_ValGrad, true);
                Assert.IsTrue(c == MatrixData.Single_1D_Grad.Length, "Write length incorrect Single");
                CollectionAssert.AreEqual(MatrixData.Single_1D_Grad, stream.ToArray(), "Written value is not the same Single");
            }
        }

        #endregion

        #region Write (C)LUT

        [TestMethod]
        public void WriteLUT16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteLUT16(LUTData.LUT16_ValGrad);
                Assert.IsTrue(c == LUTData.LUT16_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(LUTData.LUT16_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteLUT8()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteLUT8(LUTData.LUT8_ValGrad);
                Assert.IsTrue(c == LUTData.LUT8_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(LUTData.LUT8_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteCLUT()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUT(LUTData.CLUT_Val8);
                Assert.IsTrue(c == LUTData.CLUT_8.Length, "Write length incorrect CLUT8");
                CollectionAssert.AreEqual(LUTData.CLUT_8, stream.ToArray(), "Written value is not the same CLUT8");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUT(LUTData.CLUT_Val16);
                Assert.IsTrue(c == LUTData.CLUT_16.Length, "Write length incorrect CLUT16");
                CollectionAssert.AreEqual(LUTData.CLUT_16, stream.ToArray(), "Written value is not the same CLUT16");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUT(LUTData.CLUT_Valf32);
                Assert.IsTrue(c == LUTData.CLUT_f32.Length, "Write length incorrect CLUTf32");
                CollectionAssert.AreEqual(LUTData.CLUT_f32, stream.ToArray(), "Written value is not the same CLUTf32");
            }
        }

        [TestMethod]
        public void WriteCLUT8()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUT8(LUTData.CLUT8_ValGrad);
                Assert.IsTrue(c == LUTData.CLUT8_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(LUTData.CLUT8_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteCLUT16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUT16(LUTData.CLUT16_ValGrad);
                Assert.IsTrue(c == LUTData.CLUT16_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(LUTData.CLUT16_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteCLUTf32()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUTf32(LUTData.CLUTf32_ValGrad);
                Assert.IsTrue(c == LUTData.CLUTf32_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(LUTData.CLUTf32_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        #endregion

        #region Write MultiProcessElement

        [TestMethod]
        public void WriteMultiProcessElement()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiProcessElement(MultiProcessElementData.MPE_ValCLUT);
                Assert.IsTrue(c == MultiProcessElementData.MPE_CLUT.Length, "Write length incorrect CLUT");
                CollectionAssert.AreEqual(MultiProcessElementData.MPE_CLUT, stream.ToArray(), "Written value is not the same CLUT");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiProcessElement(MultiProcessElementData.MPE_ValCurve);
                Assert.IsTrue(c == MultiProcessElementData.MPE_Curve.Length, "Write length incorrect Curve");
                CollectionAssert.AreEqual(MultiProcessElementData.MPE_Curve, stream.ToArray(), "Written value is not the same Curve");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiProcessElement(MultiProcessElementData.MPE_ValMatrix);
                Assert.IsTrue(c == MultiProcessElementData.MPE_Matrix.Length, "Write length incorrect Matrix");
                CollectionAssert.AreEqual(MultiProcessElementData.MPE_Matrix, stream.ToArray(), "Written value is not the same Matrix");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiProcessElement(MultiProcessElementData.MPE_ValbACS);
                Assert.IsTrue(c == MultiProcessElementData.MPE_bACS.Length, "Write length incorrect bACS");
                CollectionAssert.AreEqual(MultiProcessElementData.MPE_bACS, stream.ToArray(), "Written value is not the same bACS");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMultiProcessElement(MultiProcessElementData.MPE_ValeACS);
                Assert.IsTrue(c == MultiProcessElementData.MPE_eACS.Length, "Write length incorrect eACS");
                CollectionAssert.AreEqual(MultiProcessElementData.MPE_eACS, stream.ToArray(), "Written value is not the same eACS");
            }
        }

        [TestMethod]
        public void WriteCurveSetProcessElement()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveSetProcessElement(MultiProcessElementData.CurvePE_ValGrad);
                Assert.IsTrue(c == MultiProcessElementData.CurvePE_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(MultiProcessElementData.CurvePE_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteMatrixProcessElement()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteMatrixProcessElement(MultiProcessElementData.MatrixPE_ValGrad);
                Assert.IsTrue(c == MultiProcessElementData.MatrixPE_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(MultiProcessElementData.MatrixPE_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteCLUTProcessElement()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCLUTProcessElement(MultiProcessElementData.CLUTPE_ValGrad);
                Assert.IsTrue(c == MultiProcessElementData.CLUTPE_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(MultiProcessElementData.CLUTPE_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        #endregion

        #region Write Curves
        
        [TestMethod]
        public void WriteOneDimensionalCurve()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteOneDimensionalCurve(CurveData.OneDimensional_ValFormula1);
                Assert.IsTrue(c == CurveData.OneDimensional_Formula1.Length, "Write length incorrect Formula1");
                CollectionAssert.AreEqual(CurveData.OneDimensional_Formula1, stream.ToArray(), "Written value is not the same Formula1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteOneDimensionalCurve(CurveData.OneDimensional_ValFormula2);
                Assert.IsTrue(c == CurveData.OneDimensional_Formula2.Length, "Write length incorrect Formula2");
                CollectionAssert.AreEqual(CurveData.OneDimensional_Formula2, stream.ToArray(), "Written value is not the same Formula2");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteOneDimensionalCurve(CurveData.OneDimensional_ValSampled);
                Assert.IsTrue(c == CurveData.OneDimensional_Sampled.Length, "Write length incorrect Sampled");
                CollectionAssert.AreEqual(CurveData.OneDimensional_Sampled, stream.ToArray(), "Written value is not the same Sampled");
            }
        }

        [TestMethod]
        public void WriteResponseCurve()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteResponseCurve(CurveData.Response_ValGrad);
                Assert.IsTrue(c == CurveData.Response_Grad.Length, "Write length incorrect");
                CollectionAssert.AreEqual(CurveData.Response_Grad, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteParametricCurve()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteParametricCurve(CurveData.Parametric_ValVar1);
                Assert.IsTrue(c == CurveData.Parametric_Var1.Length, "Write length incorrect Var1");
                CollectionAssert.AreEqual(CurveData.Parametric_Var1, stream.ToArray(), "Written value is not the same Var1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteParametricCurve(CurveData.Parametric_ValVar2);
                Assert.IsTrue(c == CurveData.Parametric_Var2.Length, "Write length incorrect Var2");
                CollectionAssert.AreEqual(CurveData.Parametric_Var2, stream.ToArray(), "Written value is not the same Var2");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteParametricCurve(CurveData.Parametric_ValVar3);
                Assert.IsTrue(c == CurveData.Parametric_Var3.Length, "Write length incorrect Var3");
                CollectionAssert.AreEqual(CurveData.Parametric_Var3, stream.ToArray(), "Written value is not the same Var3");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteParametricCurve(CurveData.Parametric_ValVar4);
                Assert.IsTrue(c == CurveData.Parametric_Var4.Length, "Write length incorrect Var4");
                CollectionAssert.AreEqual(CurveData.Parametric_Var4, stream.ToArray(), "Written value is not the same Var4");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteParametricCurve(CurveData.Parametric_ValVar5);
                Assert.IsTrue(c == CurveData.Parametric_Var5.Length, "Write length incorrect Var5");
                CollectionAssert.AreEqual(CurveData.Parametric_Var5, stream.ToArray(), "Written value is not the same Var5");
            }
        }

        [TestMethod]
        public void WriteCurveSegment()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveSegment(CurveData.Segment_ValFormula1);
                Assert.IsTrue(c == CurveData.Segment_Formula1.Length, "Write length incorrect Formula1");
                CollectionAssert.AreEqual(CurveData.Segment_Formula1, stream.ToArray(), "Written value is not the same Formula1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveSegment(CurveData.Segment_ValFormula2);
                Assert.IsTrue(c == CurveData.Segment_Formula2.Length, "Write length incorrect Formula2");
                CollectionAssert.AreEqual(CurveData.Segment_Formula2, stream.ToArray(), "Written value is not the same Formula2");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveSegment(CurveData.Segment_ValFormula3);
                Assert.IsTrue(c == CurveData.Segment_Formula3.Length, "Write length incorrect Formula3");
                CollectionAssert.AreEqual(CurveData.Segment_Formula3, stream.ToArray(), "Written value is not the same Formula3");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveSegment(CurveData.Segment_ValSampled1);
                Assert.IsTrue(c == CurveData.Segment_Sampled1.Length, "Write length incorrect Sampled1");
                CollectionAssert.AreEqual(CurveData.Segment_Sampled1, stream.ToArray(), "Written value is not the same Sampled1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteCurveSegment(CurveData.Segment_ValSampled2);
                Assert.IsTrue(c == CurveData.Segment_Sampled2.Length, "Write length incorrect Sampled2");
                CollectionAssert.AreEqual(CurveData.Segment_Sampled2, stream.ToArray(), "Written value is not the same Sampled2");
            }
        }

        [TestMethod]
        public void WriteFormulaCurveElement()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFormulaCurveElement(CurveData.Formula_ValVar1);
                Assert.IsTrue(c == CurveData.Formula_Var1.Length, "Write length incorrect Var1");
                CollectionAssert.AreEqual(CurveData.Formula_Var1, stream.ToArray(), "Written value is not the same Var1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFormulaCurveElement(CurveData.Formula_ValVar2);
                Assert.IsTrue(c == CurveData.Formula_Var2.Length, "Write length incorrect Var2");
                CollectionAssert.AreEqual(CurveData.Formula_Var2, stream.ToArray(), "Written value is not the same Var2");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteFormulaCurveElement(CurveData.Formula_ValVar3);
                Assert.IsTrue(c == CurveData.Formula_Var3.Length, "Write length incorrect Var3");
                CollectionAssert.AreEqual(CurveData.Formula_Var3, stream.ToArray(), "Written value is not the same Var3");
            }
        }

        [TestMethod]
        public void WriteSampledCurveElement()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSampledCurveElement(CurveData.Sampled_ValGrad1);
                Assert.IsTrue(c == CurveData.Sampled_Grad1.Length, "Write length incorrect Grad1");
                CollectionAssert.AreEqual(CurveData.Sampled_Grad1, stream.ToArray(), "Written value is not the same Grad1");
            }

            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteSampledCurveElement(CurveData.Sampled_ValGrad2);
                Assert.IsTrue(c == CurveData.Sampled_Grad2.Length, "Write length incorrect Grad2");
                CollectionAssert.AreEqual(CurveData.Sampled_Grad2, stream.ToArray(), "Written value is not the same Grad2");
            }
        }

        #endregion

        #region Write Array

        [TestMethod]
        public void WriteArrayByte()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteArray(ArrayData.UInt8);
                Assert.IsTrue(c == ArrayData.UInt8.Length, "Write length incorrect");
                CollectionAssert.AreEqual(ArrayData.UInt8, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteArrayUInt16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteArray(ArrayData.UInt16_Val);
                Assert.IsTrue(c == ArrayData.UInt16_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(ArrayData.UInt16_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteArrayInt16()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteArray(ArrayData.Int16_Val);
                Assert.IsTrue(c == ArrayData.Int16_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(ArrayData.Int16_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteArrayUInt32()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteArray(ArrayData.UInt32_Val);
                Assert.IsTrue(c == ArrayData.UInt32_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(ArrayData.UInt32_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteArrayInt32()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteArray(ArrayData.Int32_Val);
                Assert.IsTrue(c == ArrayData.Int32_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(ArrayData.Int32_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WriteArrayUInt64()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteArray(ArrayData.UInt64_Val);
                Assert.IsTrue(c == ArrayData.UInt64_Arr.Length, "Write length incorrect");
                CollectionAssert.AreEqual(ArrayData.UInt64_Arr, stream.ToArray(), "Written value is not the same");
            }
        }

        #endregion

        #region Write Misc

        [TestMethod]
        public void WriteEmpty()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                int c = writer.WriteEmpty(3);
                Assert.IsTrue(c == 3, "Write length incorrect");
                CollectionAssert.AreEqual(new byte[] { 0x00, 0x00, 0x00 }, stream.ToArray(), "Written value is not the same");
            }
        }

        [TestMethod]
        public void WritePadding()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new ICCDataWriter(stream);
                stream.Write(new byte[] { 0x00, 0x00, 0x00 }, 0, 3);
                int c = writer.WritePadding();
                Assert.IsTrue(c == 1, "Write length incorrect");
                CollectionAssert.AreEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 }, stream.ToArray(), "Written value is not the same");
            }
        }

        #endregion
    }
}