using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorManager.ICC;
using ColorManagerTests.ICC.Data;

namespace ColorManagerTests.ICC
{
    [TestClass]
    public class ICCDataReaderTest
    {
        #region Read Primitives

        [TestMethod]
        public void ReadUInt16()
        {
            var reader = new ICCDataReader(Primitives.UInt16_0);
            ushort value = reader.ReadUInt16();
            Assert.IsTrue(value == ushort.MinValue, "Read Zero");
            
            reader = new ICCDataReader(Primitives.UInt16_1);
            value = reader.ReadUInt16();
            Assert.IsTrue(value == 1, "Read 1");
            
            reader = new ICCDataReader(Primitives.UInt16_Max);
            value = reader.ReadUInt16();
            Assert.IsTrue(value == ushort.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt16()
        {
            var reader = new ICCDataReader(Primitives.Int16_Min);
            short value = reader.ReadInt16();
            Assert.IsTrue(value == short.MinValue, "Read Min");
            
            reader = new ICCDataReader(Primitives.Int16_0);
            value = reader.ReadInt16();
            Assert.IsTrue(value == 0, "Read Zero");

            reader = new ICCDataReader(Primitives.Int16_1);
            value = reader.ReadInt16();
            Assert.IsTrue(value == 1, "Read One");

            reader = new ICCDataReader(Primitives.Int16_Max);
            value = reader.ReadInt16();
            Assert.IsTrue(value == short.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadUInt32()
        {
            var reader = new ICCDataReader(Primitives.UInt32_0);
            uint value = reader.ReadUInt32();
            Assert.IsTrue(value == uint.MinValue, "Read Zero");
            
            reader = new ICCDataReader(Primitives.UInt32_1);
            value = reader.ReadUInt32();
            Assert.IsTrue(value == 1, "Read One");
            
            reader = new ICCDataReader(Primitives.UInt32_Max);
            value = reader.ReadUInt32();
            Assert.IsTrue(value == uint.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt32()
        {
            var reader = new ICCDataReader(Primitives.Int32_Min);
            int value = reader.ReadInt32();
            Assert.IsTrue(value == int.MinValue, "Read Min");
            
            reader = new ICCDataReader(Primitives.Int32_0);
            value = reader.ReadInt32();
            Assert.IsTrue(value == 0, "Read Zero");
            
            reader = new ICCDataReader(Primitives.Int32_1);
            value = reader.ReadInt32();
            Assert.IsTrue(value == 1, "Read One");
            
            reader = new ICCDataReader(Primitives.Int32_Max);
            value = reader.ReadInt32();
            Assert.IsTrue(value == int.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadUInt64()
        {
            var reader = new ICCDataReader(Primitives.UInt64_0);
            ulong value = reader.ReadUInt64();
            Assert.IsTrue(value == ulong.MinValue, "Read Zero");
            
            reader = new ICCDataReader(Primitives.UInt64_1);
            value = reader.ReadUInt64();
            Assert.IsTrue(value == 1, "Read One");
            
            reader = new ICCDataReader(Primitives.UInt64_Max);
            value = reader.ReadUInt64();
            Assert.IsTrue(value == ulong.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt64()
        {
            var reader = new ICCDataReader(Primitives.Int64_Min);
            long value = reader.ReadInt64();
            Assert.IsTrue(value == long.MinValue, "Read Min");
            
            reader = new ICCDataReader(Primitives.Int64_0);
            value = reader.ReadInt64();
            Assert.IsTrue(value == 0, "Read Zero");
            
            reader = new ICCDataReader(Primitives.Int64_1);
            value = reader.ReadInt64();
            Assert.IsTrue(value == 1, "Read One");

            reader = new ICCDataReader(Primitives.Int64_Max);
            value = reader.ReadInt64();
            Assert.IsTrue(value == long.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadSingle()
        {
            var reader = new ICCDataReader(Primitives.Single_Min);
            float value = reader.ReadSingle();
            Assert.AreEqual(float.MinValue, value, "Read Min");
            
            reader = new ICCDataReader(Primitives.Single_0);
            value = reader.ReadSingle();
            Assert.AreEqual(0f, value, "Read Zero");
            
            reader = new ICCDataReader(Primitives.Single_1);
            value = reader.ReadSingle();
            Assert.AreEqual(1f, value, "Read One");
            
            reader = new ICCDataReader(Primitives.Single_Max);
            value = reader.ReadSingle();
            Assert.AreEqual(float.MaxValue, value, "Read Max");
        }

        [TestMethod]
        public void ReadDouble()
        {
            var reader = new ICCDataReader(Primitives.Double_Min);
            double value = reader.ReadDouble();
            Assert.AreEqual(double.MinValue, value, "Read Min");
            
            reader = new ICCDataReader(Primitives.Double_0);
            value = reader.ReadDouble();
            Assert.AreEqual(0d, value, "Read Zero");
            
            reader = new ICCDataReader(Primitives.Double_1);
            value = reader.ReadDouble();
            Assert.AreEqual(1d, value, "Read One");
            
            reader = new ICCDataReader(Primitives.Double_Max);
            value = reader.ReadDouble();
            Assert.AreEqual(double.MaxValue, value, "Read Max");
        }

        [TestMethod]
        public void ReadFix16()
        {
            var reader = new ICCDataReader(Primitives.Fix16_Min);
            double value = reader.ReadFix16();
            Assert.AreEqual(Primitives.Fix16_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Primitives.Fix16_0);
            value = reader.ReadFix16();
            Assert.AreEqual(0, value, "Read Zero");
            
            reader = new ICCDataReader(Primitives.Fix16_1);
            value = reader.ReadFix16();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(Primitives.Fix16_Max);
            value = reader.ReadFix16();
            Assert.AreEqual(Primitives.Fix16_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadUFix16()
        {
            var reader = new ICCDataReader(Primitives.UFix16_0);
            double value = reader.ReadUFix16();
            Assert.AreEqual(Primitives.UFix16_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Primitives.UFix16_1);
            value = reader.ReadUFix16();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(Primitives.UFix16_Max);
            value = reader.ReadUFix16();
            Assert.AreEqual(Primitives.UFix16_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadU1Fix15()
        {
            var reader = new ICCDataReader(Primitives.U1Fix15_0);
            double value = reader.ReadU1Fix15();
            Assert.AreEqual(Primitives.U1Fix15_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Primitives.U1Fix15_1);
            value = reader.ReadU1Fix15();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(Primitives.U1Fix15_Max);
            value = reader.ReadU1Fix15();
            Assert.AreEqual(Primitives.U1Fix15_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadUFix8()
        {
            var reader = new ICCDataReader(Primitives.UFix8_0);
            double value = reader.ReadUFix8();
            Assert.AreEqual(Primitives.UFix8_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Primitives.UFix8_1);
            value = reader.ReadUFix8();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(Primitives.UFix8_Max);
            value = reader.ReadUFix8();
            Assert.AreEqual(Primitives.UFix8_ValMax, value, "Read Max");
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
            var reader = new ICCDataReader(Structs.DateTime_Min);
            var value = reader.ReadDateTime();
            Assert.AreEqual(Structs.DateTime_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.DateTime_Max);
            value = reader.ReadDateTime();
            Assert.AreEqual(Structs.DateTime_ValMax, value, "Read Max");
            
            reader = new ICCDataReader(Structs.DateTime_Invalid);
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
            var reader = new ICCDataReader(Structs.VersionNumber_Min);
            var value = reader.ReadVersionNumber();
            Assert.AreEqual(Structs.VersionNumber_ValMin, value, "Read Min");
            Assert.AreEqual(Structs.VersionNumber_StrMin, value.ToString(), "Read Min ToString");
            
            reader = new ICCDataReader(Structs.VersionNumber_211);
            value = reader.ReadVersionNumber();
            Assert.AreEqual(Structs.VersionNumber_Val211, value, "Read Version 2.1.1");
            Assert.AreEqual(Structs.VersionNumber_Str211, value.ToString(), "Read Version 2.1.1 ToString");

            reader = new ICCDataReader(Structs.VersionNumber_430);
            value = reader.ReadVersionNumber();
            Assert.AreEqual(Structs.VersionNumber_Val430, value, "Read Version 4.3");
            Assert.AreEqual(Structs.VersionNumber_Str430, value.ToString(), "Read Version 4.3 ToString");
            
            reader = new ICCDataReader(Structs.VersionNumber_Max);
            value = reader.ReadVersionNumber();
            Assert.AreEqual(Structs.VersionNumber_ValMax, value, "Read Max");
            Assert.AreEqual(Structs.VersionNumber_StrMax, value.ToString(), "Read Max ToString");
        }

        [TestMethod]
        public void ReadProfileFlag()
        {
            var reader = new ICCDataReader(Structs.ProfileFlag_Min);
            var value = reader.ReadProfileFlag();
            Assert.AreEqual(Structs.ProfileFlag_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.ProfileFlag_Embedded);
            value = reader.ReadProfileFlag();
            Assert.AreEqual(Structs.ProfileFlag_ValEmbedded, value, "Read Flag: Embedded");
            
            reader = new ICCDataReader(Structs.ProfileFlag_NotIndependent);
            value = reader.ReadProfileFlag();
            Assert.AreEqual(Structs.ProfileFlag_ValNotIndependent, value, "Read Flag: Not Independent");
            
            reader = new ICCDataReader(Structs.ProfileFlag_Max);
            value = reader.ReadProfileFlag();
            Assert.AreEqual(Structs.ProfileFlag_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadDeviceAttribute()
        {
            var reader = new ICCDataReader(Structs.DeviceAttribute_Min);
            var value = reader.ReadDeviceAttribute();
            Assert.AreEqual(Structs.DeviceAttribute_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.DeviceAttribute_Var1);
            value = reader.ReadDeviceAttribute();
            Assert.AreEqual(Structs.DeviceAttribute_ValVar1, value, "Read Var1");
            
            reader = new ICCDataReader(Structs.DeviceAttribute_Var2);
            value = reader.ReadDeviceAttribute();
            Assert.AreEqual(Structs.DeviceAttribute_ValVar2, value, "Read Var2");
            
            reader = new ICCDataReader(Structs.DeviceAttribute_Max);
            value = reader.ReadDeviceAttribute();
            Assert.AreEqual(Structs.DeviceAttribute_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadXYZNumber()
        {
            var reader = new ICCDataReader(Structs.XYZNumber_Min);
            var value = reader.ReadXYZNumber();
            Assert.AreEqual(Structs.XYZNumber_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.XYZNumber_0);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(Structs.XYZNumber_Val0, value, "Read Zero");
            
            reader = new ICCDataReader(Structs.XYZNumber_1);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(Structs.XYZNumber_Val1, value, "Read One");

            reader = new ICCDataReader(Structs.XYZNumber_Var);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(Structs.XYZNumber_ValVar, value, "Read Var");

            reader = new ICCDataReader(Structs.XYZNumber_Max);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(Structs.XYZNumber_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadProfileID()
        {
            var reader = new ICCDataReader(Structs.ProfileID_Min);
            var value = reader.ReadProfileID();
            Assert.AreEqual(Structs.ProfileID_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.ProfileID_Rand);
            value = reader.ReadProfileID();
            Assert.AreEqual(Structs.ProfileID_ValRand, value, "Read Random");
            
            reader = new ICCDataReader(Structs.ProfileID_Max);
            value = reader.ReadProfileID();
            Assert.AreEqual(Structs.ProfileID_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadPositionNumber()
        {
            var reader = new ICCDataReader(Structs.PositionNumber_Min);
            var value = reader.ReadPositionNumber();
            Assert.AreEqual(Structs.PositionNumber_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.PositionNumber_Rand);
            value = reader.ReadPositionNumber();
            Assert.AreEqual(Structs.PositionNumber_ValRand, value, "Read Random");
            
            reader = new ICCDataReader(Structs.PositionNumber_Max);
            value = reader.ReadPositionNumber();
            Assert.AreEqual(Structs.PositionNumber_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadResponseNumber()
        {
            var reader = new ICCDataReader(Structs.ResponseNumber_Min);
            var value = reader.ReadResponseNumber();
            Assert.AreEqual(Structs.ResponseNumber_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.ResponseNumber_1);
            value = reader.ReadResponseNumber();
            Assert.AreEqual(Structs.ResponseNumber_Val1, value, "Read One");
            
            reader = new ICCDataReader(Structs.ResponseNumber_Max);
            value = reader.ReadResponseNumber();
            Assert.AreEqual(Structs.ResponseNumber_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadNamedColor()
        {
            var reader = new ICCDataReader(Structs.NamedColor_Min);
            var value = reader.ReadNamedColor(Structs.NamedColor_ValMin.DeviceCoordinates.Length);
            Assert.AreEqual(Structs.NamedColor_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(Structs.NamedColor_Max);
            value = reader.ReadNamedColor(Structs.NamedColor_ValMax.DeviceCoordinates.Length);
            Assert.AreEqual(Structs.NamedColor_ValMax, value, "Read Max");
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
            var reader = new ICCDataReader(Matrix.Fix16_2D_Grad);
            var value = reader.ReadMatrix(3, 3, false);
            CollectionAssert.AreEqual(Matrix.Fix16_2D_ValGrad, value, "Read Fix16");
            
            reader = new ICCDataReader(Matrix.Single_2D_Grad);
            value = reader.ReadMatrix(3, 3, true);
            CollectionAssert.AreEqual(Matrix.Single_2D_ValGrad, value, "Read Single");
        }

        [TestMethod]
        public void ReadMatrix1D()
        {
            var reader = new ICCDataReader(Matrix.Fix16_1D_Grad);
            var value = reader.ReadMatrix(3, false);
            CollectionAssert.AreEqual(Matrix.Fix16_1D_ValGrad, value, "Read Fix16");
            
            reader = new ICCDataReader(Matrix.Single_1D_Grad);
            value = reader.ReadMatrix(3, true);
            CollectionAssert.AreEqual(Matrix.Single_1D_ValGrad, value, "Read Single");
        }

        #endregion

        #region Read (C)LUT

        [TestMethod]
        public void ReadLUT8()
        {
            var reader = new ICCDataReader(LUTData.LUT8_Grad);
            var value = reader.ReadLUT8();
            Assert.AreEqual(LUTData.LUT8_ValGrad, value);
        }

        [TestMethod]
        public void ReadLUT16()
        {
            var reader = new ICCDataReader(LUTData.LUT16_Grad);
            var value = reader.ReadLUT16(LUTData.LUT16_ValGrad.Values.Length);
            Assert.AreEqual(LUTData.LUT16_ValGrad, value);
        }

        [TestMethod]
        public void ReadCLUT()
        {
            var reader = new ICCDataReader(LUTData.CLUT_8);
            var value = reader.ReadCLUT(2, 3, false);
            Assert.AreEqual(LUTData.CLUT_Val8, value, "Read CLUT 8");

            reader = new ICCDataReader(LUTData.CLUT_16);
            value = reader.ReadCLUT(2, 3, false);
            Assert.AreEqual(LUTData.CLUT_Val16, value, "Read CLUT 16");

            reader = new ICCDataReader(LUTData.CLUT_f32);
            value = reader.ReadCLUT(2, 3, true);
            Assert.AreEqual(LUTData.CLUT_Valf32, value, "Read CLUT f32");
        }

        [TestMethod]
        public void ReadCLUT8()
        {
            var reader = new ICCDataReader(LUTData.CLUT8_Grad);
            var value = reader.ReadCLUT8(2, 3, new byte[] { 3, 3 });
            Assert.AreEqual(LUTData.CLUT8_ValGrad, value);
        }

        [TestMethod]
        public void ReadCLUT16()
        {
            var reader = new ICCDataReader(LUTData.CLUT16_Grad);
            var value = reader.ReadCLUT16(2, 3, new byte[] { 3, 3 });
            Assert.AreEqual(LUTData.CLUT16_ValGrad, value);
        }

        [TestMethod]
        public void ReadCLUTf32()
        {
            var reader = new ICCDataReader(LUTData.CLUTf32_Grad);
            var value = reader.ReadCLUTf32(2, 3, new byte[] { 3, 3 });
            Assert.AreEqual(LUTData.CLUTf32_ValGrad, value);
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