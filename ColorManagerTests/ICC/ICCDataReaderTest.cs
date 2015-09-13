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
            var reader = new ICCDataReader(PrimitivesData.UInt16_0);
            ushort value = reader.ReadUInt16();
            Assert.IsTrue(value == ushort.MinValue, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.UInt16_1);
            value = reader.ReadUInt16();
            Assert.IsTrue(value == 1, "Read 1");
            
            reader = new ICCDataReader(PrimitivesData.UInt16_Max);
            value = reader.ReadUInt16();
            Assert.IsTrue(value == ushort.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt16()
        {
            var reader = new ICCDataReader(PrimitivesData.Int16_Min);
            short value = reader.ReadInt16();
            Assert.IsTrue(value == short.MinValue, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.Int16_0);
            value = reader.ReadInt16();
            Assert.IsTrue(value == 0, "Read Zero");

            reader = new ICCDataReader(PrimitivesData.Int16_1);
            value = reader.ReadInt16();
            Assert.IsTrue(value == 1, "Read One");

            reader = new ICCDataReader(PrimitivesData.Int16_Max);
            value = reader.ReadInt16();
            Assert.IsTrue(value == short.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadUInt32()
        {
            var reader = new ICCDataReader(PrimitivesData.UInt32_0);
            uint value = reader.ReadUInt32();
            Assert.IsTrue(value == uint.MinValue, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.UInt32_1);
            value = reader.ReadUInt32();
            Assert.IsTrue(value == 1, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.UInt32_Max);
            value = reader.ReadUInt32();
            Assert.IsTrue(value == uint.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt32()
        {
            var reader = new ICCDataReader(PrimitivesData.Int32_Min);
            int value = reader.ReadInt32();
            Assert.IsTrue(value == int.MinValue, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.Int32_0);
            value = reader.ReadInt32();
            Assert.IsTrue(value == 0, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.Int32_1);
            value = reader.ReadInt32();
            Assert.IsTrue(value == 1, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.Int32_Max);
            value = reader.ReadInt32();
            Assert.IsTrue(value == int.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadUInt64()
        {
            var reader = new ICCDataReader(PrimitivesData.UInt64_0);
            ulong value = reader.ReadUInt64();
            Assert.IsTrue(value == ulong.MinValue, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.UInt64_1);
            value = reader.ReadUInt64();
            Assert.IsTrue(value == 1, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.UInt64_Max);
            value = reader.ReadUInt64();
            Assert.IsTrue(value == ulong.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadInt64()
        {
            var reader = new ICCDataReader(PrimitivesData.Int64_Min);
            long value = reader.ReadInt64();
            Assert.IsTrue(value == long.MinValue, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.Int64_0);
            value = reader.ReadInt64();
            Assert.IsTrue(value == 0, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.Int64_1);
            value = reader.ReadInt64();
            Assert.IsTrue(value == 1, "Read One");

            reader = new ICCDataReader(PrimitivesData.Int64_Max);
            value = reader.ReadInt64();
            Assert.IsTrue(value == long.MaxValue, "Read Max");
        }

        [TestMethod]
        public void ReadSingle()
        {
            var reader = new ICCDataReader(PrimitivesData.Single_Min);
            float value = reader.ReadSingle();
            Assert.AreEqual(float.MinValue, value, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.Single_0);
            value = reader.ReadSingle();
            Assert.AreEqual(0f, value, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.Single_1);
            value = reader.ReadSingle();
            Assert.AreEqual(1f, value, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.Single_Max);
            value = reader.ReadSingle();
            Assert.AreEqual(float.MaxValue, value, "Read Max");
        }

        [TestMethod]
        public void ReadDouble()
        {
            var reader = new ICCDataReader(PrimitivesData.Double_Min);
            double value = reader.ReadDouble();
            Assert.AreEqual(double.MinValue, value, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.Double_0);
            value = reader.ReadDouble();
            Assert.AreEqual(0d, value, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.Double_1);
            value = reader.ReadDouble();
            Assert.AreEqual(1d, value, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.Double_Max);
            value = reader.ReadDouble();
            Assert.AreEqual(double.MaxValue, value, "Read Max");
        }

        [TestMethod]
        public void ReadFix16()
        {
            var reader = new ICCDataReader(PrimitivesData.Fix16_Min);
            double value = reader.ReadFix16();
            Assert.AreEqual(PrimitivesData.Fix16_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.Fix16_0);
            value = reader.ReadFix16();
            Assert.AreEqual(0, value, "Read Zero");
            
            reader = new ICCDataReader(PrimitivesData.Fix16_1);
            value = reader.ReadFix16();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.Fix16_Max);
            value = reader.ReadFix16();
            Assert.AreEqual(PrimitivesData.Fix16_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadUFix16()
        {
            var reader = new ICCDataReader(PrimitivesData.UFix16_0);
            double value = reader.ReadUFix16();
            Assert.AreEqual(PrimitivesData.UFix16_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.UFix16_1);
            value = reader.ReadUFix16();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.UFix16_Max);
            value = reader.ReadUFix16();
            Assert.AreEqual(PrimitivesData.UFix16_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadU1Fix15()
        {
            var reader = new ICCDataReader(PrimitivesData.U1Fix15_0);
            double value = reader.ReadU1Fix15();
            Assert.AreEqual(PrimitivesData.U1Fix15_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.U1Fix15_1);
            value = reader.ReadU1Fix15();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.U1Fix15_Max);
            value = reader.ReadU1Fix15();
            Assert.AreEqual(PrimitivesData.U1Fix15_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadUFix8()
        {
            var reader = new ICCDataReader(PrimitivesData.UFix8_0);
            double value = reader.ReadUFix8();
            Assert.AreEqual(PrimitivesData.UFix8_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(PrimitivesData.UFix8_1);
            value = reader.ReadUFix8();
            Assert.AreEqual(1, value, "Read One");
            
            reader = new ICCDataReader(PrimitivesData.UFix8_Max);
            value = reader.ReadUFix8();
            Assert.AreEqual(PrimitivesData.UFix8_ValMax, value, "Read Max");
        }
        
        [TestMethod]
        public void ReadASCIIString()
        {
            var reader = new ICCDataReader(PrimitivesData.ASCII_All);
            string value = reader.ReadASCIIString(128);

            Assert.IsFalse(value == null, "Read string is null");
            Assert.IsTrue(value.Length == PrimitivesData.ASCII_ValAll.Length, "Read length does not match");
            Assert.AreEqual(PrimitivesData.ASCII_ValAll, value, false);
        }

        [TestMethod]
        public void ReadUnicodeString()
        {
            var reader = new ICCDataReader(PrimitivesData.Unicode_Rand1);
            string value = reader.ReadUnicodeString(PrimitivesData.Unicode_Rand1.Length);

            Assert.IsFalse(value == null, "Read string is null");
            Assert.IsTrue(value.Length == PrimitivesData.Unicode_ValRand1.Length, "Read length does not match");
            Assert.AreEqual(PrimitivesData.Unicode_ValRand1, value, false);
        }

        #endregion

        #region Read Structs

        [TestMethod]
        public void ReadDateTime()
        {
            var reader = new ICCDataReader(StructsData.DateTime_Min);
            var value = reader.ReadDateTime();
            Assert.AreEqual(StructsData.DateTime_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.DateTime_Max);
            value = reader.ReadDateTime();
            Assert.AreEqual(StructsData.DateTime_ValMax, value, "Read Max");
            
            reader = new ICCDataReader(StructsData.DateTime_Invalid);
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
            var reader = new ICCDataReader(StructsData.VersionNumber_Min);
            var value = reader.ReadVersionNumber();
            Assert.AreEqual(StructsData.VersionNumber_ValMin, value, "Read Min");
            Assert.AreEqual(StructsData.VersionNumber_StrMin, value.ToString(), "Read Min ToString");
            
            reader = new ICCDataReader(StructsData.VersionNumber_211);
            value = reader.ReadVersionNumber();
            Assert.AreEqual(StructsData.VersionNumber_Val211, value, "Read Version 2.1.1");
            Assert.AreEqual(StructsData.VersionNumber_Str211, value.ToString(), "Read Version 2.1.1 ToString");

            reader = new ICCDataReader(StructsData.VersionNumber_430);
            value = reader.ReadVersionNumber();
            Assert.AreEqual(StructsData.VersionNumber_Val430, value, "Read Version 4.3");
            Assert.AreEqual(StructsData.VersionNumber_Str430, value.ToString(), "Read Version 4.3 ToString");
            
            reader = new ICCDataReader(StructsData.VersionNumber_Max);
            value = reader.ReadVersionNumber();
            Assert.AreEqual(StructsData.VersionNumber_ValMax, value, "Read Max");
            Assert.AreEqual(StructsData.VersionNumber_StrMax, value.ToString(), "Read Max ToString");
        }

        [TestMethod]
        public void ReadProfileFlag()
        {
            var reader = new ICCDataReader(StructsData.ProfileFlag_Min);
            var value = reader.ReadProfileFlag();
            Assert.AreEqual(StructsData.ProfileFlag_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.ProfileFlag_Embedded);
            value = reader.ReadProfileFlag();
            Assert.AreEqual(StructsData.ProfileFlag_ValEmbedded, value, "Read Flag: Embedded");
            
            reader = new ICCDataReader(StructsData.ProfileFlag_NotIndependent);
            value = reader.ReadProfileFlag();
            Assert.AreEqual(StructsData.ProfileFlag_ValNotIndependent, value, "Read Flag: Not Independent");
            
            reader = new ICCDataReader(StructsData.ProfileFlag_Max);
            value = reader.ReadProfileFlag();
            Assert.AreEqual(StructsData.ProfileFlag_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadDeviceAttribute()
        {
            var reader = new ICCDataReader(StructsData.DeviceAttribute_Min);
            var value = reader.ReadDeviceAttribute();
            Assert.AreEqual(StructsData.DeviceAttribute_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.DeviceAttribute_Var1);
            value = reader.ReadDeviceAttribute();
            Assert.AreEqual(StructsData.DeviceAttribute_ValVar1, value, "Read Var1");
            
            reader = new ICCDataReader(StructsData.DeviceAttribute_Var2);
            value = reader.ReadDeviceAttribute();
            Assert.AreEqual(StructsData.DeviceAttribute_ValVar2, value, "Read Var2");
            
            reader = new ICCDataReader(StructsData.DeviceAttribute_Max);
            value = reader.ReadDeviceAttribute();
            Assert.AreEqual(StructsData.DeviceAttribute_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadXYZNumber()
        {
            var reader = new ICCDataReader(StructsData.XYZNumber_Min);
            var value = reader.ReadXYZNumber();
            Assert.AreEqual(StructsData.XYZNumber_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.XYZNumber_0);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(StructsData.XYZNumber_Val0, value, "Read Zero");
            
            reader = new ICCDataReader(StructsData.XYZNumber_1);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(StructsData.XYZNumber_Val1, value, "Read One");

            reader = new ICCDataReader(StructsData.XYZNumber_Var1);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(StructsData.XYZNumber_ValVar1, value, "Read Var1");

            reader = new ICCDataReader(StructsData.XYZNumber_Max);
            value = reader.ReadXYZNumber();
            Assert.AreEqual(StructsData.XYZNumber_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadProfileID()
        {
            var reader = new ICCDataReader(StructsData.ProfileID_Min);
            var value = reader.ReadProfileID();
            Assert.AreEqual(StructsData.ProfileID_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.ProfileID_Rand);
            value = reader.ReadProfileID();
            Assert.AreEqual(StructsData.ProfileID_ValRand, value, "Read Random");
            
            reader = new ICCDataReader(StructsData.ProfileID_Max);
            value = reader.ReadProfileID();
            Assert.AreEqual(StructsData.ProfileID_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadPositionNumber()
        {
            var reader = new ICCDataReader(StructsData.PositionNumber_Min);
            var value = reader.ReadPositionNumber();
            Assert.AreEqual(StructsData.PositionNumber_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.PositionNumber_Rand);
            value = reader.ReadPositionNumber();
            Assert.AreEqual(StructsData.PositionNumber_ValRand, value, "Read Random");
            
            reader = new ICCDataReader(StructsData.PositionNumber_Max);
            value = reader.ReadPositionNumber();
            Assert.AreEqual(StructsData.PositionNumber_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadResponseNumber()
        {
            var reader = new ICCDataReader(StructsData.ResponseNumber_Min);
            var value = reader.ReadResponseNumber();
            Assert.AreEqual(StructsData.ResponseNumber_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.ResponseNumber_1);
            value = reader.ReadResponseNumber();
            Assert.AreEqual(StructsData.ResponseNumber_Val1, value, "Read One");
            
            reader = new ICCDataReader(StructsData.ResponseNumber_Max);
            value = reader.ReadResponseNumber();
            Assert.AreEqual(StructsData.ResponseNumber_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadNamedColor()
        {
            var reader = new ICCDataReader(StructsData.NamedColor_Min);
            var value = reader.ReadNamedColor(StructsData.NamedColor_ValMin.DeviceCoordinates.Length);
            Assert.AreEqual(StructsData.NamedColor_ValMin, value, "Read Min");
            
            reader = new ICCDataReader(StructsData.NamedColor_Max);
            value = reader.ReadNamedColor(StructsData.NamedColor_ValMax.DeviceCoordinates.Length);
            Assert.AreEqual(StructsData.NamedColor_ValMax, value, "Read Max");
        }

        [TestMethod]
        public void ReadProfileDescription()
        {
            var reader = new ICCDataReader(StructsData.ProfileDescription_Rand1);
            var value = reader.ReadProfileDescription();
            Assert.AreEqual(StructsData.ProfileDescription_ValRand1, value);
        }

        [TestMethod]
        public void ReadColorantTableEntry()
        {
            var reader = new ICCDataReader(StructsData.ColorantTableEntry_Rand1);
            var value = reader.ReadColorantTableEntry();
            Assert.AreEqual(StructsData.ColorantTableEntry_ValRand1, value);

            reader = new ICCDataReader(StructsData.ColorantTableEntry_Rand2);
            value = reader.ReadColorantTableEntry();
            Assert.AreEqual(StructsData.ColorantTableEntry_ValRand2, value);
        }

        #endregion

        #region Read Tag Data Entries

        [TestMethod]
        public void ReadTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeArr);
            var value = reader.ReadTagDataEntry(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeTable);
            Assert.AreEqual(TagDataEntryData.TagDataEntry_MultiLocalizedUnicodeVal, value, "Read MultiLocalizedUnicode");

            reader = new ICCDataReader(TagDataEntryData.TagDataEntry_CurveArr);
            value = reader.ReadTagDataEntry(TagDataEntryData.TagDataEntry_CurveTable);
            Assert.AreEqual(TagDataEntryData.TagDataEntry_CurveVal, value, "Read Curve");
        }

        [TestMethod]
        public void ReadTagDataEntryHeader()
        {
            var reader = new ICCDataReader(TagDataEntryData.TagDataEntryHeader_MultiLocalizedUnicodeArr);
            var value = reader.ReadTagDataEntryHeader();
            Assert.AreEqual(TagDataEntryData.TagDataEntryHeader_MultiLocalizedUnicodeVal, value, "Read MultiLocalizedUnicode");

            reader = new ICCDataReader(TagDataEntryData.TagDataEntryHeader_CurveArr);
            value = reader.ReadTagDataEntryHeader();
            Assert.AreEqual(TagDataEntryData.TagDataEntryHeader_CurveVal, value, "Read Curve");
        }



        [TestMethod]
        public void ReadUnknownTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Unknown_Arr);
            var value = reader.ReadUnknownTagDataEntry((uint)TagDataEntryData.Unknown_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.Unknown_Val, value);
        }

        [TestMethod]
        public void ReadChromaticityTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Chromaticity_Arr1);
            var value = reader.ReadChromaticityTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Chromaticity_Val1, value, "Read Var 1");

            reader = new ICCDataReader(TagDataEntryData.Chromaticity_Arr2);
            value = reader.ReadChromaticityTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Chromaticity_Val2, value, "Read Var 2");

            try
            {
                reader = new ICCDataReader(TagDataEntryData.Chromaticity_ArrInvalid1);
                value = reader.ReadChromaticityTagDataEntry();
                Assert.Fail("Reading invalid Chromaticity TagDataEntry 1 did not throw exception");
            }
            catch (CorruptProfileException) { }

            try
            {
                reader = new ICCDataReader(TagDataEntryData.Chromaticity_ArrInvalid2);
                value = reader.ReadChromaticityTagDataEntry();
                Assert.Fail("Reading invalid Chromaticity TagDataEntry 2 did not throw exception");
            }
            catch (CorruptProfileException) { }
        }

        [TestMethod]
        public void ReadColorantOrderTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ColorantOrder_Arr);
            var value = reader.ReadColorantOrderTagDataEntry();
            Assert.AreEqual(TagDataEntryData.ColorantOrder_Val, value);
        }

        [TestMethod]
        public void ReadColorantTableTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ColorantTable_Arr);
            var value = reader.ReadColorantTableTagDataEntry();
            Assert.AreEqual(TagDataEntryData.ColorantTable_Val, value);
        }

        [TestMethod]
        public void ReadCurveTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Curve_Arr_0);
            var value = reader.ReadCurveTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Curve_Val_0, value, "Read Curve 0");

            reader = new ICCDataReader(TagDataEntryData.Curve_Arr_1);
            value = reader.ReadCurveTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Curve_Val_1, value, "Read Curve 1");

            reader = new ICCDataReader(TagDataEntryData.Curve_Arr_2);
            value = reader.ReadCurveTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Curve_Val_2, value, "Read Curve 2");
        }

        [TestMethod]
        public void ReadDataTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Data_ArrASCII);
            var value = reader.ReadDataTagDataEntry((uint)TagDataEntryData.Data_ArrASCII.Length + 8);
            Assert.AreEqual(TagDataEntryData.Data_ValASCII, value, "Read ASCII");

            reader = new ICCDataReader(TagDataEntryData.Data_ArrNoASCII);
            value = reader.ReadDataTagDataEntry((uint)TagDataEntryData.Data_ArrNoASCII.Length + 8);
            Assert.AreEqual(TagDataEntryData.Data_ValNoASCII, value, "Read No ASCII");
        }

        [TestMethod]
        public void ReadDateTimeTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.DateTime_Arr);
            var value = reader.ReadDateTimeTagDataEntry();
            Assert.AreEqual(TagDataEntryData.DateTime_Val, value);
        }

        [TestMethod]
        public void ReadLut16TagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Lut16_Arr);
            var value = reader.ReadLut16TagDataEntry();
            Assert.AreEqual(TagDataEntryData.Lut16_Val, value);
        }

        [TestMethod]
        public void ReadLut8TagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Lut8_Arr);
            var value = reader.ReadLut8TagDataEntry();
            Assert.AreEqual(TagDataEntryData.Lut8_Val, value);
        }

        [TestMethod]
        public void ReadLutAToBTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.LutAToB_Arr);
            var value = reader.ReadLutAToBTagDataEntry();
            Assert.AreEqual(TagDataEntryData.LutAToB_Val, value);
        }

        [TestMethod]
        public void ReadLutBToATagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.LutBToA_Arr);
            var value = reader.ReadLutBToATagDataEntry();
            Assert.AreEqual(TagDataEntryData.LutBToA_Val, value);
        }

        [TestMethod]
        public void ReadMeasurementTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Measurement_Arr);
            var value = reader.ReadMeasurementTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Measurement_Val, value);
        }

        [TestMethod]
        public void ReadMultiLocalizedUnicodeTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.MultiLocalizedUnicode_Arr);
            var value = reader.ReadMultiLocalizedUnicodeTagDataEntry();
            Assert.AreEqual(TagDataEntryData.MultiLocalizedUnicode_Val, value);
        }

        [TestMethod]
        public void ReadMultiProcessElementsTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.MultiProcessElements_Arr);
            var value = reader.ReadMultiProcessElementsTagDataEntry();
            Assert.AreEqual(TagDataEntryData.MultiProcessElements_Val, value);
        }

        [TestMethod]
        public void ReadNamedColor2TagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.NamedColor2_Arr);
            var value = reader.ReadNamedColor2TagDataEntry();
            Assert.AreEqual(TagDataEntryData.NamedColor2_Val, value);
        }

        [TestMethod]
        public void ReadParametricCurveTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ParametricCurve_Arr);
            var value = reader.ReadParametricCurveTagDataEntry();
            Assert.AreEqual(TagDataEntryData.ParametricCurve_Val, value);
        }

        [TestMethod]
        public void ReadProfileSequenceDescTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ProfileSequenceDesc_Arr);
            var value = reader.ReadProfileSequenceDescTagDataEntry();
            Assert.AreEqual(TagDataEntryData.ProfileSequenceDesc_Val, value);
        }

        [TestMethod]
        public void ReadProfileSequenceIdentifierTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ProfileSequenceIdentifier_Arr);
            var value = reader.ReadProfileSequenceIdentifierTagDataEntry();
            Assert.AreEqual(TagDataEntryData.ProfileSequenceIdentifier_Val, value);
        }

        [TestMethod]
        public void ReadResponseCurveSet16TagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ResponseCurveSet16_Arr);
            var value = reader.ReadResponseCurveSet16TagDataEntry();
            Assert.AreEqual(TagDataEntryData.ResponseCurveSet16_Val, value);
        }

        [TestMethod]
        public void ReadFix16ArrayTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Fix16Array_Arr);
            var value = reader.ReadFix16ArrayTagDataEntry((uint)TagDataEntryData.Fix16Array_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.Fix16Array_Val, value);
        }

        [TestMethod]
        public void ReadSignatureTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Signature_Arr);
            var value = reader.ReadSignatureTagDataEntry();
            Assert.AreEqual(TagDataEntryData.Signature_Val, value);
        }

        [TestMethod]
        public void ReadTextTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.Text_Arr);
            var value = reader.ReadTextTagDataEntry((uint)TagDataEntryData.Text_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.Text_Val, value);
        }

        [TestMethod]
        public void ReadUFix16ArrayTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.UFix16Array_Arr);
            var value = reader.ReadUFix16ArrayTagDataEntry((uint)TagDataEntryData.UFix16Array_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.UFix16Array_Val, value);
        }

        [TestMethod]
        public void ReadUInt16ArrayTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.UInt16Array_Arr);
            var value = reader.ReadUInt16ArrayTagDataEntry((uint)TagDataEntryData.UInt16Array_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.UInt16Array_Val, value);
        }

        [TestMethod]
        public void ReadUInt32ArrayTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.UInt32Array_Arr);
            var value = reader.ReadUInt32ArrayTagDataEntry((uint)TagDataEntryData.UInt32Array_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.UInt32Array_Val, value);
        }

        [TestMethod]
        public void ReadUInt64ArrayTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.UInt64Array_Arr);
            var value = reader.ReadUInt64ArrayTagDataEntry((uint)TagDataEntryData.UInt64Array_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.UInt64Array_Val, value);
        }

        [TestMethod]
        public void ReadUInt8ArrayTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.UInt8Array_Arr);
            var value = reader.ReadUInt8ArrayTagDataEntry((uint)TagDataEntryData.UInt8Array_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.UInt8Array_Val, value);
        }

        [TestMethod]
        public void ReadViewingConditionsTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.ViewingConditions_Arr);
            var value = reader.ReadViewingConditionsTagDataEntry((uint)TagDataEntryData.ViewingConditions_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.ViewingConditions_Val, value);
        }

        [TestMethod]
        public void ReadXYZTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.XYZ_Arr);
            var value = reader.ReadXYZTagDataEntry((uint)TagDataEntryData.XYZ_Arr.Length + 8);
            Assert.AreEqual(TagDataEntryData.XYZ_Val, value);
        }

        [TestMethod]
        public void ReadTextDescriptionTagDataEntry()
        {
            var reader = new ICCDataReader(TagDataEntryData.TextDescription_Arr);
            var value = reader.ReadTextDescriptionTagDataEntry();
            Assert.AreEqual(TagDataEntryData.TextDescription_Val, value);
        }

        #endregion

        #region Read Matrix

        [TestMethod]
        public void ReadMatrix2D()
        {
            var reader = new ICCDataReader(MatrixData.Fix16_2D_Grad);
            var value = reader.ReadMatrix(3, 3, false);
            CollectionAssert.AreEqual(MatrixData.Fix16_2D_ValGrad, value, "Read Fix16");
            
            reader = new ICCDataReader(MatrixData.Single_2D_Grad);
            value = reader.ReadMatrix(3, 3, true);
            CollectionAssert.AreEqual(MatrixData.Single_2D_ValGrad, value, "Read Single");
        }

        [TestMethod]
        public void ReadMatrix1D()
        {
            var reader = new ICCDataReader(MatrixData.Fix16_1D_Grad);
            var value = reader.ReadMatrix(3, false);
            CollectionAssert.AreEqual(MatrixData.Fix16_1D_ValGrad, value, "Read Fix16");
            
            reader = new ICCDataReader(MatrixData.Single_1D_Grad);
            value = reader.ReadMatrix(3, true);
            CollectionAssert.AreEqual(MatrixData.Single_1D_ValGrad, value, "Read Single");
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
            var reader = new ICCDataReader(MultiProcessElementData.MPE_Matrix);
            var value = reader.ReadMultiProcessElement();
            Assert.AreEqual(MultiProcessElementData.MPE_ValMatrix, value, "Read Matrix Element");

            reader = new ICCDataReader(MultiProcessElementData.MPE_CLUT);
            value = reader.ReadMultiProcessElement();
            Assert.AreEqual(MultiProcessElementData.MPE_ValCLUT, value, "Read CLUT Element");

            reader = new ICCDataReader(MultiProcessElementData.MPE_Curve);
            value = reader.ReadMultiProcessElement();
            Assert.AreEqual(MultiProcessElementData.MPE_ValCurve, value, "Read Curve Element");

            reader = new ICCDataReader(MultiProcessElementData.MPE_bACS);
            value = reader.ReadMultiProcessElement();
            Assert.AreEqual(MultiProcessElementData.MPE_ValbACS, value, "Read bACS Element");

            reader = new ICCDataReader(MultiProcessElementData.MPE_eACS);
            value = reader.ReadMultiProcessElement();
            Assert.AreEqual(MultiProcessElementData.MPE_ValeACS, value, "Read eACS Element");
        }

        [TestMethod]
        public void ReadCurveSetProcessElement()
        {
            var reader = new ICCDataReader(MultiProcessElementData.CurvePE_Grad);
            var value = reader.ReadCurveSetProcessElement(2, 3);
            Assert.AreEqual(MultiProcessElementData.CurvePE_ValGrad, value);
        }

        [TestMethod]
        public void ReadMatrixProcessElement()
        {
            var reader = new ICCDataReader(MultiProcessElementData.MatrixPE_Grad);
            var value = reader.ReadMatrixProcessElement(3, 3);
            Assert.AreEqual(MultiProcessElementData.MatrixPE_ValGrad, value);
        }

        [TestMethod]
        public void ReadCLUTProcessElement()
        {
            var reader = new ICCDataReader(MultiProcessElementData.CLUTPE_Grad);
            var value = reader.ReadCLUTProcessElement(2, 3);
            Assert.AreEqual(MultiProcessElementData.CLUTPE_ValGrad, value);
        }

        #endregion

        #region Read Curves

        [TestMethod]
        public void ReadOneDimensionalCurve()
        {
            var reader = new ICCDataReader(CurveData.OneDimensional_Formula1);
            var value = reader.ReadOneDimensionalCurve();
            Assert.AreEqual(CurveData.OneDimensional_ValFormula1, value, "Read Formula1");

            reader = new ICCDataReader(CurveData.OneDimensional_Formula2);
            value = reader.ReadOneDimensionalCurve();
            Assert.AreEqual(CurveData.OneDimensional_ValFormula2, value, "Read Formula2");

            reader = new ICCDataReader(CurveData.OneDimensional_Sampled);
            value = reader.ReadOneDimensionalCurve();
            Assert.AreEqual(CurveData.OneDimensional_ValSampled, value, "Read Sampled");
        }

        [TestMethod]
        public void ReadResponseCurve()
        {
            var reader = new ICCDataReader(CurveData.Response_Grad);
            var value = reader.ReadResponseCurve(3);
            Assert.AreEqual(CurveData.Response_ValGrad, value);
        }

        [TestMethod]
        public void ReadParametricCurve()
        {
            var reader = new ICCDataReader(CurveData.Parametric_Var1);
            var value = reader.ReadParametricCurve();
            Assert.AreEqual(CurveData.Parametric_ValVar1, value, "Read Var1");

            reader = new ICCDataReader(CurveData.Parametric_Var2);
            value = reader.ReadParametricCurve();
            Assert.AreEqual(CurveData.Parametric_ValVar2, value, "Read Var2");

            reader = new ICCDataReader(CurveData.Parametric_Var3);
            value = reader.ReadParametricCurve();
            Assert.AreEqual(CurveData.Parametric_ValVar3, value, "Read Var3");

            reader = new ICCDataReader(CurveData.Parametric_Var4);
            value = reader.ReadParametricCurve();
            Assert.AreEqual(CurveData.Parametric_ValVar4, value, "Read Var4");

            reader = new ICCDataReader(CurveData.Parametric_Var5);
            value = reader.ReadParametricCurve();
            Assert.AreEqual(CurveData.Parametric_ValVar5, value, "Read Var5");
        }

        [TestMethod]
        public void ReadCurveSegment()
        {
            var reader = new ICCDataReader(CurveData.Segment_Formula1);
            var value = reader.ReadCurveSegment();
            Assert.AreEqual(CurveData.Segment_ValFormula1, value, "Read Formula1");

            reader = new ICCDataReader(CurveData.Segment_Formula2);
            value = reader.ReadCurveSegment();
            Assert.AreEqual(CurveData.Segment_ValFormula2, value, "Read Formula2");

            reader = new ICCDataReader(CurveData.Segment_Formula3);
            value = reader.ReadCurveSegment();
            Assert.AreEqual(CurveData.Segment_ValFormula3, value, "Read Formula3");

            reader = new ICCDataReader(CurveData.Segment_Sampled1);
            value = reader.ReadCurveSegment();
            Assert.AreEqual(CurveData.Segment_ValSampled1, value, "Read Sampled1");

            reader = new ICCDataReader(CurveData.Segment_Sampled2);
            value = reader.ReadCurveSegment();
            Assert.AreEqual(CurveData.Segment_ValSampled2, value, "Read Sampled2");
        }

        [TestMethod]
        public void ReadFormulaCurveElement()
        {
            var reader = new ICCDataReader(CurveData.Formula_Var1);
            var value = reader.ReadFormulaCurveElement();
            Assert.AreEqual(CurveData.Formula_ValVar1, value, "Read Var1");

            reader = new ICCDataReader(CurveData.Formula_Var2);
            value = reader.ReadFormulaCurveElement();
            Assert.AreEqual(CurveData.Formula_ValVar2, value, "Read Var2");

            reader = new ICCDataReader(CurveData.Formula_Var3);
            value = reader.ReadFormulaCurveElement();
            Assert.AreEqual(CurveData.Formula_ValVar3, value, "Read Var3");
        }

        [TestMethod]
        public void ReadSampledCurveElement()
        {
            var reader = new ICCDataReader(CurveData.Sampled_Grad1);
            var value = reader.ReadSampledCurveElement();
            Assert.AreEqual(CurveData.Sampled_ValGrad1, value, "Read Grad1");

            reader = new ICCDataReader(CurveData.Sampled_Grad2);
            value = reader.ReadSampledCurveElement();
            Assert.AreEqual(CurveData.Sampled_ValGrad2, value, "Read Grad2");
        }

        #endregion
    }
}