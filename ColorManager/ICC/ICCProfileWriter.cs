using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ColorManager.ICC
{
    public sealed class ICCProfileWriter
    {
        private readonly bool LittleEndian = BitConverter.IsLittleEndian;

        private MemoryStream DataStream;
        private List<TagEntry> TagTable;

        private int TagTableSize
        {
            //4 = tag count; 12 = size of each table entry
            get { return 4 + TagTable.Count * 12; }
        }
        private int HeaderSize
        {
            get { return 128; }
        }

        //TODO: some TagDataEntries might need padding bytes (see documentation)
        //TODO: some TagDataEntries might reuse information by setting the same offset value

        public ICCProfileWriter()
        {
            DataStream = new MemoryStream(128);
            TagTable = new List<TagEntry>();
        }
        
        public void WriteProfile()
        {
            WriteHeader();
            //Write Data before Table to set offset and size
            WriteTagData();
            WriteTagTable();
        }

        private void WriteHeader()
        {
            DataStream.Position = 0;
            //Write header
        }

        private void WriteTagTable()
        {
            DataStream.Position = HeaderSize;
            WriteUInt32((uint)TagTable.Count);
            foreach (var entry in TagTable)
            {
                WriteUInt32((uint)entry.Table.Signature);
                WriteUInt32(entry.Table.Offset);
                WriteUInt32(entry.Table.DataSize);
            }
        }

        private void WriteTagData()
        {
            DataStream.Position = HeaderSize + TagTableSize;
            foreach (var entry in TagTable)
            {
                WriteTagDataEntry(entry);
            }
        }

        private sealed class TagEntry
        {
            public TagTableEntry Table;
            public TagDataEntry Data;

            public TagEntry(TagTableEntry Table, TagDataEntry Data)
            {
                if (Table == null) throw new ArgumentNullException(nameof(Table));
                if (Data == null) throw new ArgumentNullException(nameof(Data));

                this.Table = Table;
                this.Data = Data;
            }
        }

        #region Write Primitives

        /// <summary>
        /// Writes a byte
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteByte(byte value)
        {
            DataStream.WriteByte(value);
            return 1;
        }

        /// <summary>
        /// Writes an ushort
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteUInt16(ushort value)
        {
            return WriteBytes((byte*)&value, 2);
        }

        /// <summary>
        /// Writes a short
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteInt16(short value)
        {
            return WriteBytes((byte*)&value, 2);
        }

        /// <summary>
        /// Writes an uint
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteUInt32(uint value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes an int
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteInt32(int value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes an ulong
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteUInt64(ulong value)
        {
            return WriteBytes((byte*)&value, 8);
        }

        /// <summary>
        /// Writes a long
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteInt64(long value)
        {
            return WriteBytes((byte*)&value, 8);
        }

        /// <summary>
        /// Writes a float
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteSingle(float value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes a double
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private unsafe int WriteDouble(double value)
        {
            return WriteBytes((byte*)&value, 8);
        }


        /// <summary>
        /// Writes a signed 32bit number with 1 sign bit, 15 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteFix16(double value)
        {
            const double max = short.MaxValue + (65535d / 65536d);
            const double min = short.MinValue;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 65536d;

            return WriteInt32((int)value);
        }

        /// <summary>
        /// Writes an unsigned 32bit number with 16 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteUFix16(double value)
        {
            const double max = ushort.MaxValue + (65535d / 65536d);
            const double min = ushort.MinValue;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 65536d;

            return WriteUInt32((uint)value);
        }

        /// <summary>
        /// Writes an unsigned 16bit number with 1 value bit and 15 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteU1Fix15(double value)
        {
            const double max = 1 + (32767d / 32768d);
            const double min = 0;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 32768d;

            return WriteUInt16((ushort)value);
        }

        /// <summary>
        /// Writes an unsigned 16bit number with 8 value bits and 8 fractional bits
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteUFix8(double value)
        {
            const double max = byte.MaxValue + (255d / 256d);
            const double min = byte.MinValue;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 256d;

            return WriteUInt16((ushort)value);
        }


        /// <summary>
        /// Writes an ASCII encoded string
        /// </summary>
        /// <param name="value">the string to write</param>
        /// <returns>the number of bytes written</returns>
        private int WriteASCIIString(string value)
        {
            return WriteASCIIString(value, value.Length);
        }

        /// <summary>
        /// Writes an ASCII encoded string
        /// </summary>
        /// <param name="value">the string to write</param>
        /// <param name="length">the desired length of the string</param>
        /// <returns>the number of bytes written</returns>
        private int WriteASCIIString(string value, int length)
        {
            byte[] data = Encoding.ASCII.GetBytes(SetRange(value, length));
            DataStream.Write(data, 0, data.Length);
            return data.Length;
        }

        /// <summary>
        /// Writes an UTF-16 big-endian encoded string
        /// </summary>
        /// <param name="value">the string to write</param>
        /// <returns>the number of bytes written</returns>
        private int WriteUnicodeString(string value)
        {
            byte[] data = Encoding.BigEndianUnicode.GetBytes(value);
            DataStream.Write(data, 0, data.Length);
            return data.Length;
        }

        #endregion

        #region Write Structs

        /// <summary>
        /// Writes a DateTime
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteDateTime(DateTime value)
        {
            return WriteUInt16((ushort)value.Year)
                 + WriteUInt16((ushort)value.Month)
                 + WriteUInt16((ushort)value.Day)
                 + WriteUInt16((ushort)value.Hour)
                 + WriteUInt16((ushort)value.Minute)
                 + WriteUInt16((ushort)value.Second);
        }

        /// <summary>
        /// Writes an ICC profile version number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteVersionNumber(VersionNumber value)
        {
            byte major = SetRange(value.Major);
            byte minor = SetRange(value.Minor);
            byte bugfix = SetRange(value.Minor);
            byte mb = (byte)((minor << 4) | bugfix);

            return WriteByte(major)
                 + WriteByte(mb)
                 + WriteEmpty(2);
        }

        /// <summary>
        /// Writes an ICC profile flag
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteProfileFlag(ProfileFlag value)
        {
            int flags = 0;
            for (int i = value.Flags.Length - 1; i >= 0; i--)
            {
                if (value.Flags[i]) flags |= 1 << i;
            }

            return WriteUInt16((ushort)flags)
                 + WriteArray(value.Data);
        }

        /// <summary>
        /// Writes ICC device attributes
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteDeviceAttribute(DeviceAttribute value)
        {
            int flags = 0;
            flags |= (int)value.Opacity;
            flags |= (int)value.Reflectivity << 1;
            flags |= (int)value.Polarity << 2;
            flags |= (int)value.Chroma << 3;

            return WriteByte((byte)flags)
                 + WriteEmpty(3)
                 + WriteArray(value.VendorData);
        }

        /// <summary>
        /// Writes an XYZ number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteXYZNumber(XYZNumber value)
        {
            return WriteFix16(value.X)
                 + WriteFix16(value.Y)
                 + WriteFix16(value.Z);
        }

        /// <summary>
        /// Writes a profile ID
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteProfileID(ProfileID value)
        {
            return WriteArray(value.NumericValue);
        }

        /// <summary>
        /// Writes a position number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WritePositionNumber(PositionNumber value)
        {
            return WriteUInt32(value.Offset)
                 + WriteUInt32(value.Size);
        }

        /// <summary>
        /// Writes a response number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteResponseNumber(ResponseNumber value)
        {
            return WriteUInt16(value.DeviceCode)
                 + WriteFix16(value.MeasurmentValue);
        }

        /// <summary>
        /// Writes a named color
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteNamedColor(NamedColor value)
        {
            return WriteASCIIString(value.Name, 32)
                 + WriteArray(value.PCScoordinates)
                 + WriteArray(value.DeviceCoordinates);
        }

        /// <summary>
        /// Writes a profile description
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteProfileDescription(ProfileDescription value)
        {
            return WriteUInt32(value.DeviceManufacturer)
                 + WriteUInt32(value.DeviceModel)
                 + WriteDeviceAttribute(value.DeviceAttributes)
                 + WriteUInt32((uint)value.TechnologyInformation)
                 + WriteMultiLocalizedUnicodeTagDataEntry(new MultiLocalizedUnicodeTagDataEntry(value.DeviceManufacturerInfo))
                 + WriteMultiLocalizedUnicodeTagDataEntry(new MultiLocalizedUnicodeTagDataEntry(value.DeviceModelInfo));
        }

        #endregion

        #region Write Tag Data Entries

        private int WriteUnknownTagDataEntry(UnknownTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        private int WriteChromaticityTagDataEntry(ChromaticityTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt16((ushort)value.ChannelCount);
            c += WriteUInt16((ushort)value.ChannelCount);

            for (int i = 0; i < value.ChannelCount; i++)
            {
                c += WriteUFix16(value.ChannelValues[i][0]);
                c += WriteUFix16(value.ChannelValues[i][1]);
            }
            return c;
        }

        private int WriteColorantOrderTagDataEntry(ColorantOrderTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteUInt32(value.ColorantCount)
                 + WriteArray(value.ColorantNumber);
        }

        private int WriteColorantTableTagDataEntry(ColorantTableTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt32(value.ColorantCount);
            foreach (var colorant in value.ColorantData)
            {
                c += WriteASCIIString(colorant.Name, 32);
                c += WriteUInt16(colorant.PCS1);
                c += WriteUInt16(colorant.PCS2);
                c += WriteUInt16(colorant.PCS3);
            }

            return c;
        }

        private int WriteCurveTagDataEntry(CurveTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;

            if (value.IsIdentityResponse)
            {
                c += WriteUInt32(0);
            }
            else if (value.IsGamma)
            {
                c += WriteUInt32(1);
                c += WriteUFix8(value.Gamma);
            }
            else
            {
                c += WriteUInt32((uint)value.CurveData.Length);
                for (int i = 0; i < value.CurveData.Length; i++)
                {
                    c += WriteUInt16(SetRangeUInt16(value.CurveData[i]));
                }
            }

            return c;

            //TODO: Page 48: If the input is PCSXYZ, 1+(32 767/32 768) shall be mapped to the value 1,0. If the output is PCSXYZ, the value 1,0 shall be mapped to 1+(32 767/32 768).
        }

        private int WriteDataTagDataEntry(DataTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteEmpty(3)
                 + WriteByte((byte)(value.IsASCII ? 0x80 : 0x00))
                 + WriteArray(value.Data);
        }

        private int WriteDateTimeTagDataEntry(DateTimeTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteDateTime(value.Value);
        }

        private int WriteLut16TagDataEntry(Lut16TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteByte((byte)value.InputValues.Length);
            c += WriteByte((byte)value.OutputValues.Length);
            c += WriteByte(value.CLUTValues.GridPointCount[0]);
            c += WriteEmpty(1);

            c += WriteMatrix(value.Matrix, false);

            foreach (var lut in value.InputValues) { c += WriteLUT16(lut); }

            c += WriteCLUT16(value.CLUTValues);

            foreach (var lut in value.OutputValues) { c += WriteLUT16(lut); }

            return c;
        }

        private int WriteLut8TagDataEntry(Lut8TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteByte((byte)value.InputValues.Length);
            c += WriteByte((byte)value.OutputValues.Length);
            c += WriteByte(value.CLUTValues.GridPointCount[0]);
            c += WriteEmpty(1);

            c += WriteMatrix(value.Matrix, false);

            foreach (var lut in value.InputValues) { c += WriteLUT8(lut); }

            c += WriteCLUT8(value.CLUTValues);

            foreach (var lut in value.OutputValues) { c += WriteLUT8(lut); }

            return c;
        }

        private int WriteLutAToBTagDataEntry(LutAToBTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            long start = DataStream.Position - 8;

            int c = WriteByte((byte)value.InputChannelCount);
            c += WriteByte((byte)value.OutputChannelCount);
            c += WriteEmpty(2);

            long bCurveOffset = 0;
            long matrixOffset = 0;
            long mCurveOffset = 0;
            long CLUTOffset = 0;
            long aCurveOffset = 0;

            //Jump over offset values
            long offsetpos = DataStream.Position;
            DataStream.Position += 5 * 4;

            if (value.CurveB != null)
            {
                bCurveOffset = DataStream.Position;
                c += WriteCurve(value.CurveB);
            }
            if (value.CurveM != null)
            {
                mCurveOffset = DataStream.Position;
                c += WriteCurve(value.CurveM);
            }
            if (value.CurveA != null)
            {
                aCurveOffset = DataStream.Position;
                c += WriteCurve(value.CurveA);
            }

            if (value.CLUTValues != null)
            {
                CLUTOffset = DataStream.Position;
                c += WriteCLUT(value.CLUTValues);
            }

            if (value.Matrix3x1 != null && value.Matrix3x3 != null)
            {
                matrixOffset = DataStream.Position;
                c += WriteMatrix(value.Matrix3x3, false);
                c += WriteMatrix(value.Matrix3x1, false);
            }

            //Set offset values
            DataStream.Position = offsetpos;

            if (bCurveOffset != 0) bCurveOffset -= start;
            if (matrixOffset != 0) matrixOffset -= start;
            if (mCurveOffset != 0) mCurveOffset -= start;
            if (CLUTOffset != 0) CLUTOffset -= start;
            if (aCurveOffset != 0) aCurveOffset -= start;
            
            c += WriteUInt32((uint)bCurveOffset);
            c += WriteUInt32((uint)matrixOffset);
            c += WriteUInt32((uint)mCurveOffset);
            c += WriteUInt32((uint)CLUTOffset);
            c += WriteUInt32((uint)aCurveOffset);

            return c;
        }

        private int WriteLutBToATagDataEntry(LutBToATagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;

            int c = WriteByte((byte)value.InputChannelCount);
            c += WriteByte((byte)value.OutputChannelCount);
            c += WriteEmpty(2);

            long bCurveOffset = 0;
            long matrixOffset = 0;
            long mCurveOffset = 0;
            long CLUTOffset = 0;
            long aCurveOffset = 0;

            //Jump over offset values
            long offsetpos = DataStream.Position;
            DataStream.Position += 5 * 4;

            if (value.CurveB != null)
            {
                bCurveOffset = DataStream.Position;
                c += WriteCurve(value.CurveB);
            }
            if (value.CurveM != null)
            {
                mCurveOffset = DataStream.Position;
                c += WriteCurve(value.CurveM);
            }
            if (value.CurveA != null)
            {
                aCurveOffset = DataStream.Position;
                c += WriteCurve(value.CurveA);
            }

            if (value.CLUTValues != null)
            {
                CLUTOffset = DataStream.Position;
                c += WriteCLUT(value.CLUTValues);
            }

            if (value.Matrix3x1 != null && value.Matrix3x3 != null)
            {
                matrixOffset = DataStream.Position;
                c += WriteMatrix(value.Matrix3x3, false);
                c += WriteMatrix(value.Matrix3x1, false);
            }

            //Set offset values
            DataStream.Position = offsetpos;

            if (bCurveOffset != 0) bCurveOffset -= start;
            if (matrixOffset != 0) matrixOffset -= start;
            if (mCurveOffset != 0) mCurveOffset -= start;
            if (CLUTOffset != 0) CLUTOffset -= start;
            if (aCurveOffset != 0) aCurveOffset -= start;

            c += WriteUInt32((uint)bCurveOffset);
            c += WriteUInt32((uint)matrixOffset);
            c += WriteUInt32((uint)mCurveOffset);
            c += WriteUInt32((uint)CLUTOffset);
            c += WriteUInt32((uint)aCurveOffset);

            return c;
        }

        private int WriteMeasurementTagDataEntry(MeasurementTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteUInt32((uint)value.Observer)
                 + WriteXYZNumber(value.XYZBacking)
                 + WriteUInt32((uint)value.Geometry)
                 + WriteUFix8(value.Flare)
                 + WriteUInt32((uint)value.Illuminant);
        }

        private int WriteMultiLocalizedUnicodeTagDataEntry(MultiLocalizedUnicodeTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;

            int count = value.Text.Length;

            int c = WriteUInt32((uint)count);
            c += WriteUInt32(12);//One record has always 12 bytes size
            
            //Jump over position table
            long tpos = DataStream.Position;
            DataStream.Position += count * 12;

            var offset = new uint[count];
            var length = new int[count];

            for (int i = 0; i < count; i++)
            {
                offset[i] = (uint)(DataStream.Position - start);
                c += length[i] = WriteUnicodeString(value.Text[i].Text);
            }

            //Write position table
            DataStream.Position = tpos;
            for (int i = 0; i < count; i++)
            {
                string[] code = value.Text[i].Locale.Name.Split('-');
                if (code.Length != 2) throw new Exception();
                c += WriteASCIIString(code[0], 2);
                c += WriteASCIIString(code[1], 2);
                c += WriteUInt32((uint)length[i]);
                c += WriteUInt32(offset[i]);
            }

            return c;
        }

        private int WriteMultiProcessElementsTagDataEntry(MultiProcessElementsTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;

            int c = WriteUInt16((ushort)value.InputChannelCount);
            c += WriteUInt16((ushort)value.OutputChannelCount);
            c += WriteUInt32((uint)value.Data.Length);

            //Jump over position table
            long tpos = DataStream.Position;
            DataStream.Position += value.Data.Length * 8;

            var posTable = new PositionNumber[value.Data.Length];
            for (int i = 0; i < value.Data.Length; i++)
            {
                uint offset = (uint)(DataStream.Position - start);
                int size = WriteMultiProcessElement(value.Data[i]);
                size += WritePadding();
                posTable[i] = new PositionNumber(offset, (uint)size);
                c += size;
            }

            //Write position table
            DataStream.Position = tpos;
            foreach (var pos in posTable) { c += WritePositionNumber(pos); }

            return c;
        }

        private int WriteNamedColor2TagDataEntry(NamedColor2TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteArray(value.VendorFlag)
                  + WriteUInt32((uint)value.Colors.Length)
                  + WriteUInt32((uint)value.CoordCount)
                  + WriteASCIIString(value.Prefix, 32)
                  + WriteASCIIString(value.Suffix, 32);

            foreach (var color in value.Colors) { c += WriteNamedColor(color); }

            return c;
        }

        private int WriteParametricCurveTagDataEntry(ParametricCurveTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteParametricCurve(value.Curve);
        }

        private int WriteProfileSequenceDescTagDataEntry(ProfileSequenceDescTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt32((uint)value.Descriptions.Length);
            foreach (var desc in value.Descriptions)
            {
                c += WriteProfileDescription(desc);
            }
            return c;
        }

        private int WriteProfileSequenceIdentifierTagDataEntry(ProfileSequenceIdentifierTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;
            int count = value.Data.Length;

            int c = WriteUInt16((ushort)count);

            //Jump over position table
            long tpos = DataStream.Position;
            DataStream.Position += count * 8;
            var table = new PositionNumber[count];

            for (int i = 0; i < count; i++)
            {
                uint offset = (uint)(DataStream.Position - start);
                int size = WriteProfileID(value.Data[i].ID);
                size += WriteTagDataEntry(new MultiLocalizedUnicodeTagDataEntry(value.Data[i].Description));
                table[i] = new PositionNumber(offset, (uint)size);
                c += size;
            }

            //Write position table
            DataStream.Position = tpos;
            foreach (var pos in table) { c += WritePositionNumber(pos); }

            return c;
        }

        private int WriteResponseCurveSet16TagDataEntry(ResponseCurveSet16TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;

            int c = WriteUInt16((ushort)value.ChannelCount);
            c += WriteUInt16((ushort)value.Curves.Length);

            //Jump over position table
            long tpos = DataStream.Position;
            DataStream.Position += value.Curves.Length * 4;

            var offset = new uint[value.Curves.Length];

            for (int i = 0; i < value.Curves.Length; i++)
            {
                offset[i] = (uint)DataStream.Position;
                c += WriteResponseCurve(value.Curves[i]);
                c += WritePadding();
            }

            //Write position table
            DataStream.Position = tpos;
            c += WriteArray(offset);

            return c;
        }

        private int WriteFix16ArrayTagDataEntry(Fix16ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Data.Length; i++)
            {
                c += WriteFix16(value.Data[i] * 256d);
            }

            return c;
        }

        private int WriteSignatureTagDataEntry(SignatureTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteASCIIString(value.SignatureData, 4);
        }

        private int WriteTextTagDataEntry(TextTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteASCIIString(value.Text);
        }

        private int WriteUFix16ArrayTagDataEntry(UFix16ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Data.Length; i++)
            {
                c += WriteUFix16(value.Data[i]);
            }

            return c;
        }

        private int WriteUInt16ArrayTagDataEntry(UInt16ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        private int WriteUInt32ArrayTagDataEntry(UInt32ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        private int WriteUInt64ArrayTagDataEntry(UInt64ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        private int WriteUInt8ArrayTagDataEntry(UInt8ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        private int WriteViewingConditionsTagDataEntry(ViewingConditionsTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteXYZNumber(value.IlluminantXYZ)
                 + WriteXYZNumber(value.SurroundXYZ)
                 + WriteUInt32((uint)value.Illuminant);
        }

        private int WriteXYZTagDataEntry(XYZTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Data.Length; i++)
            {
                c += WriteXYZNumber(value.Data[i]);
            }
            return c;
        }

        #endregion

        #region Write Matrix

        private int WriteMatrix(double[,] value, bool isFloat)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int y = 0; y < value.GetLength(1); y++)
            {
                for (int x = 0; x < value.GetLength(0); x++)
                {
                    if (isFloat) { c += WriteSingle((float)value[x, y]); }
                    else { c += WriteFix16(value[x, y]); }
                }
            }
            return c;
        }

        private int WriteMatrix(double[] value, bool isFloat)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (isFloat) { c += WriteSingle((float)value[i]); }
                else { c += WriteFix16(value[i]); }
            }
            return c;
        }

        #endregion

        #region Write (C)LUT

        private int WriteLUT16(LUT value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            foreach (var item in value.Values) { c += WriteUInt16(SetRangeUInt16(item)); }
            return c;
        }

        private int WriteLUT8(LUT value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            foreach (var item in value.Values) { c += WriteByte(SetRangeUInt8(item)); }
            return c;
        }

        private int WriteCLUT(CLUT value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteArray(value.GridPointCount);

            switch (value.DataType)
            {
                case CLUTDataType.Float:
                    return c + WriteCLUTf32(value);
                case CLUTDataType.UInt8:
                    WriteByte(1);
                    WriteEmpty(3);
                    return c + WriteCLUT8(value);
                case CLUTDataType.UInt16:
                    WriteByte(2);
                    WriteEmpty(3);
                    return c + WriteCLUT16(value);

                default:
                    throw new CorruptProfileException("CLUT");
            }
        }

        private int WriteCLUT8(CLUT value)
        {
            int c = 0;
            foreach (var inArr in value.Values)
            {
                foreach (var item in inArr)
                {
                    c += WriteByte(SetRangeUInt8(item));
                }
            }
            return c;
        }

        private int WriteCLUT16(CLUT value)
        {
            int c = 0;
            foreach (var inArr in value.Values)
            {
                foreach (var item in inArr)
                {
                    c += WriteUInt16(SetRangeUInt16(item));
                }
            }
            return c;
        }

        private int WriteCLUTf32(CLUT value)
        {
            int c = 0;
            foreach (var inArr in value.Values)
            {
                foreach (var item in inArr)
                {
                    c += WriteSingle((float)item);
                }
            }
            return c;
        }

        #endregion

        #region Write MultiProcessElement

        private int WriteMultiProcessElement(MultiProcessElement value)
        {
            int c = WriteUInt32((uint)value.Signature);
            c += WriteUInt16((ushort)value.InputChannelCount);
            c += WriteUInt16((ushort)value.OutputChannelCount);

            switch (value.Signature)
            {
                case MultiProcessElementSignature.CurveSet:
                    return c + WriteCurveSetProcessElement(value as CurveSetProcessElement);
                case MultiProcessElementSignature.Matrix:
                    return c + WriteMatrixProcessElement(value as MatrixProcessElement);
                case MultiProcessElementSignature.CLUT:
                    return c + WriteCLUTProcessElement(value as CLUTProcessElement);

                case MultiProcessElementSignature.bACS:
                case MultiProcessElementSignature.eACS:
                    return c + WriteEmpty(8);

                case MultiProcessElementSignature.Unknown:
                default:
                    throw new CorruptProfileException("MultiProcessElement");
            }
        }

        private int WriteCurveSetProcessElement(CurveSetProcessElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            foreach (var curve in value.Curves)
            {
                c += WriteOneDimensionalCurve(curve);
                c += WritePadding();
            }
            return c;
        }

        private int WriteMatrixProcessElement(MatrixProcessElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteMatrix(value.MatrixIxO, true)
                 + WriteMatrix(value.MatrixOx1, true);
        }

        private int WriteCLUTProcessElement(CLUTProcessElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteCLUT(value.CLUTValue);
        }

        #endregion

        #region Write Curves

        private int WriteCurve(TagDataEntry[] curves)
        {
            int c = 0;
            foreach (var curve in curves)
            {
                if (curve.Signature != TypeSignature.Curve && curve.Signature != TypeSignature.ParametricCurve)
                {
                    string msg = "Curve has to be either of type \"Curve\" or \"ParametricCurve\" for LutAToB- and LutBToA-TagDataEntries";
                    throw new CorruptProfileException(msg);
                }
                c += WriteTagDataEntry(curve);
            }
            return c;
        }

        private int WriteOneDimensionalCurve(OneDimensionalCurve value)
        {
            int c = WriteUInt16((ushort)value.Segments.Length);
            c += WriteEmpty(2);

            foreach (var point in value.BreakPoints) { c += WriteSingle((float)point); }

            foreach (var segment in value.Segments) { c += WriteCurveSegment(segment); }

            return c;
        }

        private int WriteResponseCurve(ResponseCurve value)
        {
            int c = WriteUInt32((uint)value.CurveType);
            int channels = value.XYZvalues.Length;

            foreach (var responseArr in value.ResponseArrays) { c += WriteUInt32((uint)responseArr.Length); }
            foreach (var xyz in value.XYZvalues) { c += WriteXYZNumber(xyz); }
            foreach (var responseArr in value.ResponseArrays)
            {
                foreach (var response in responseArr)
                {
                    c += WriteResponseNumber(response);
                }
            }

            return c;
        }

        private int WriteParametricCurve(ParametricCurve value)
        {
            int c = WriteUInt16(value.type);
            WriteEmpty(2);

            if (value.type >= 0 && value.type <= 4) c += WriteFix16(value.g);
            if (value.type > 0 && value.type <= 4)
            {
                c += WriteFix16(value.a);
                c += WriteFix16(value.b);
            }
            if (value.type > 1 && value.type <= 4) c += WriteFix16(value.c);
            if (value.type > 2 && value.type <= 4) c += WriteFix16(value.d);
            if (value.type == 4)
            {
                c += WriteFix16(value.e);
                c += WriteFix16(value.f);
            }

            return c;
        }

        private int WriteCurveSegment(CurveSegment value)
        {
            int c = WriteUInt32((uint)value.Signature);
            c += WriteEmpty(4);

            switch (value.Signature)
            {
                case CurveSegmentSignature.FormulaCurve:
                    return WriteFormulaCurveElement(value as FormulaCurveElement);
                case CurveSegmentSignature.SampledCurve:
                    return WriteSampledCurveElement(value as SampledCurveElement);
                default:
                    throw new CorruptProfileException("CurveSegment");
            }
        }

        private int WriteFormulaCurveElement(FormulaCurveElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt16(value.type);
            WriteEmpty(2);

            if (value.type == 0 || value.type == 1) c += WriteFix16(value.gamma);
            if (value.type >= 0 && value.type <= 2)
            {
                c += WriteSingle((float)value.a);
                c += WriteSingle((float)value.b);
                c += WriteSingle((float)value.c);
            }
            if (value.type == 1 || value.type == 2) c += WriteSingle((float)value.d);
            if (value.type == 2) c += WriteSingle((float)value.e);

            return c;
        }

        private int WriteSampledCurveElement(SampledCurveElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt32((uint)value.CurveEntries.Length);
            foreach (var entry in value.CurveEntries) { c += WriteSingle((float)entry); }

            return c;
        }

        #endregion

        #region Write Array

        private int WriteArray(byte[] data)
        {
            DataStream.Write(data, 0, data.Length);
            return data.Length;
        }

        private int WriteArray(ushort[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteUInt16(data[i]); }
            return c;
        }

        private int WriteArray(short[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteInt16(data[i]); }
            return c;
        }

        private int WriteArray(uint[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteUInt32(data[i]); }
            return c;
        }

        private int WriteArray(int[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteInt32(data[i]); }
            return c;
        }

        private int WriteArray(ulong[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteUInt64(data[i]); }
            return c;
        }

        #endregion

        #region Write Misc

        private unsafe int WriteBytes(byte* data, int length)
        {
            if (LittleEndian)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    DataStream.WriteByte(data[i]);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    DataStream.WriteByte(data[i]);
                }
            }
            return length;
        }

        private int WriteEmpty(int length)
        {
            for (int i = 0; i < length; i++)
            {
                DataStream.WriteByte(0);
            }
            return length;
        }

        private int WriteTagDataEntryHeader(TypeSignature signature)
        {
            return WriteUInt32((uint)signature)
                 + WriteEmpty(4);
        }

        private int WritePadding()
        {
            return WriteEmpty((int)DataStream.Position % 4);
        }

        #endregion

        #region Helper

        private int SetRange(int value, int min, int max)
        {
            if (value > max) return max;
            else if (value < min) return min;
            else return value;
        }

        private double SetRange(double value, double min, double max)
        {
            if (value > max) return max;
            else if (value < min) return min;
            else return value;
        }

        private ushort SetRangeUInt16(double value)
        {
            if (value > 1) value = 1;
            else if (value < 0) value = 0;
            return (ushort)(value * ushort.MaxValue);
        }

        private byte SetRangeUInt8(double value)
        {
            if (value > 1) value = 1;
            else if (value < 0) value = 0;
            return (byte)(value * byte.MaxValue);
        }

        private byte SetRange(int value)
        {
            if (value > byte.MaxValue) return byte.MaxValue;
            else if (value < byte.MinValue) return byte.MinValue;
            else return (byte)value;
        }

        private string SetRange(string value, int length)
        {
            if (value.Length < length) return value.PadRight(length);
            else if (value.Length > length) return value.Substring(0, length);
            else return value;
        }

        /// <summary>
        /// Writes a tag data entry
        /// </summary>
        /// <param name="info">The table entry with writing information</param>
        /// <returns>the number of bytes written</returns>
        private int WriteTagDataEntry(TagEntry info)
        {
            int c = WriteTagDataEntry(info.Data);
            info.Table = new TagTableEntry(info.Table.Signature, (uint)DataStream.Position, (uint)c);
            return c;
        }

        private int WriteTagDataEntry(TagDataEntry entry)
        {
            TypeSignature t = entry.Signature;
            int c = WriteTagDataEntryHeader(entry.Signature);

            switch (t)
            {
                case TypeSignature.Chromaticity:
                    c += WriteChromaticityTagDataEntry(entry as ChromaticityTagDataEntry);
                    break;
                case TypeSignature.ColorantOrder:
                    c += WriteColorantOrderTagDataEntry(entry as ColorantOrderTagDataEntry);
                    break;
                case TypeSignature.ColorantTable:
                    c += WriteColorantTableTagDataEntry(entry as ColorantTableTagDataEntry);
                    break;
                case TypeSignature.Curve:
                    c += WriteCurveTagDataEntry(entry as CurveTagDataEntry);
                    break;
                case TypeSignature.Data:
                    c += WriteDataTagDataEntry(entry as DataTagDataEntry);
                    break;
                case TypeSignature.DateTime:
                    c += WriteDateTimeTagDataEntry(entry as DateTimeTagDataEntry);
                    break;
                case TypeSignature.Lut16:
                    c += WriteLut16TagDataEntry(entry as Lut16TagDataEntry);
                    break;
                case TypeSignature.Lut8:
                    c += WriteLut8TagDataEntry(entry as Lut8TagDataEntry);
                    break;
                case TypeSignature.LutAToB:
                    c += WriteLutAToBTagDataEntry(entry as LutAToBTagDataEntry);
                    break;
                case TypeSignature.LutBToA:
                    c += WriteLutBToATagDataEntry(entry as LutBToATagDataEntry);
                    break;
                case TypeSignature.Measurement:
                    c += WriteMeasurementTagDataEntry(entry as MeasurementTagDataEntry);
                    break;
                case TypeSignature.MultiLocalizedUnicode:
                    c += WriteMultiLocalizedUnicodeTagDataEntry(entry as MultiLocalizedUnicodeTagDataEntry);
                    break;
                case TypeSignature.MultiProcessElements:
                    c += WriteMultiProcessElementsTagDataEntry(entry as MultiProcessElementsTagDataEntry);
                    break;
                case TypeSignature.NamedColor2:
                    c += WriteNamedColor2TagDataEntry(entry as NamedColor2TagDataEntry);
                    break;
                case TypeSignature.ParametricCurve:
                    c += WriteParametricCurveTagDataEntry(entry as ParametricCurveTagDataEntry);
                    break;
                case TypeSignature.ProfileSequenceDesc:
                    c += WriteProfileSequenceDescTagDataEntry(entry as ProfileSequenceDescTagDataEntry);
                    break;
                case TypeSignature.ProfileSequenceIdentifier:
                    c += WriteProfileSequenceIdentifierTagDataEntry(entry as ProfileSequenceIdentifierTagDataEntry);
                    break;
                case TypeSignature.ResponseCurveSet16:
                    c += WriteResponseCurveSet16TagDataEntry(entry as ResponseCurveSet16TagDataEntry);
                    break;
                case TypeSignature.S15Fixed16Array:
                    c += WriteFix16ArrayTagDataEntry(entry as Fix16ArrayTagDataEntry);
                    break;
                case TypeSignature.Signature:
                    c += WriteSignatureTagDataEntry(entry as SignatureTagDataEntry);
                    break;
                case TypeSignature.Text:
                    c += WriteTextTagDataEntry(entry as TextTagDataEntry);
                    break;
                case TypeSignature.U16Fixed16Array:
                    c += WriteUFix16ArrayTagDataEntry(entry as UFix16ArrayTagDataEntry);
                    break;
                case TypeSignature.UInt16Array:
                    c += WriteUInt16ArrayTagDataEntry(entry as UInt16ArrayTagDataEntry);
                    break;
                case TypeSignature.UInt32Array:
                    c += WriteUInt32ArrayTagDataEntry(entry as UInt32ArrayTagDataEntry);
                    break;
                case TypeSignature.UInt64Array:
                    c += WriteUInt64ArrayTagDataEntry(entry as UInt64ArrayTagDataEntry);
                    break;
                case TypeSignature.UInt8Array:
                    c += WriteUInt8ArrayTagDataEntry(entry as UInt8ArrayTagDataEntry);
                    break;
                case TypeSignature.ViewingConditions:
                    c += WriteViewingConditionsTagDataEntry(entry as ViewingConditionsTagDataEntry);
                    break;
                case TypeSignature.XYZ:
                    c += WriteXYZTagDataEntry(entry as XYZTagDataEntry);
                    break;

                case TypeSignature.Unknown:
                default:
                    c += WriteUnknownTagDataEntry(entry as UnknownTagDataEntry);
                    break;
            }

            return c + WritePadding();
        }

        #endregion
    }
}
