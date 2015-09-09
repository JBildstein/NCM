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
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadVersionNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadProfileFlag()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadDeviceAttribute()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadXYZNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadProfileID()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadPositionNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadResponseNumber()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadNamedColor()
        {
            Assert.Inconclusive("Not implemented");
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
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadMatrix1D()
        {
            Assert.Inconclusive("Not implemented");
        }

        #endregion

        #region Read (C)LUT

        [TestMethod]
        public void ReadLUT16()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ReadLUT8()
        {
            Assert.Inconclusive("Not implemented");
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