using System;
using System.IO;
using System.Text;

namespace ColorManager.ICC
{
    /// <summary>
    /// Provides methods to write ICC data types
    /// </summary>
    public sealed class ICCDataWriter
    {
        #region Variables

        /// <summary>
        /// The underlying stream where the data is written to
        /// </summary>
        public readonly Stream DataStream;
        private static readonly bool LittleEndian = BitConverter.IsLittleEndian;
        private static readonly double[,] IdentityMatrix = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ICCDataWriter"/> class
        /// </summary>
        /// <param name="stream">The stream to write the data into</param>
        public ICCDataWriter(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanWrite) throw new ArgumentException("Stream must be writable");

            DataStream = stream;
        }

        #region Write Primitives

        /// <summary>
        /// Writes a byte
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteByte(byte value)
        {
            DataStream.WriteByte(value);
            return 1;
        }

        /// <summary>
        /// Writes an ushort
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteUInt16(ushort value)
        {
            return WriteBytes((byte*)&value, 2);
        }

        /// <summary>
        /// Writes a short
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteInt16(short value)
        {
            return WriteBytes((byte*)&value, 2);
        }

        /// <summary>
        /// Writes an uint
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteUInt32(uint value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes an int
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteInt32(int value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes an ulong
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteUInt64(ulong value)
        {
            return WriteBytes((byte*)&value, 8);
        }

        /// <summary>
        /// Writes a long
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteInt64(long value)
        {
            return WriteBytes((byte*)&value, 8);
        }

        /// <summary>
        /// Writes a float
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteSingle(float value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes a double
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public unsafe int WriteDouble(double value)
        {
            return WriteBytes((byte*)&value, 8);
        }


        /// <summary>
        /// Writes a signed 32bit number with 1 sign bit, 15 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteFix16(double value)
        {
            const double max = short.MaxValue + (65535d / 65536d);
            const double min = short.MinValue;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 65536d;

            return WriteInt32((int)Math.Round(value, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// Writes an unsigned 32bit number with 16 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteUFix16(double value)
        {
            const double max = ushort.MaxValue + (65535d / 65536d);
            const double min = ushort.MinValue;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 65536d;

            return WriteUInt32((uint)Math.Round(value, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// Writes an unsigned 16bit number with 1 value bit and 15 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteU1Fix15(double value)
        {
            const double max = 1 + (32767d / 32768d);
            const double min = 0;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 32768d;

            return WriteUInt16((ushort)Math.Round(value, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// Writes an unsigned 16bit number with 8 value bits and 8 fractional bits
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteUFix8(double value)
        {
            const double max = byte.MaxValue + (255d / 256d);
            const double min = byte.MinValue;

            if (value > max) value = max;
            else if (value < min) value = min;

            value *= 256d;

            return WriteUInt16((ushort)Math.Round(value, MidpointRounding.AwayFromZero));
        }


        /// <summary>
        /// Writes an ASCII encoded string
        /// </summary>
        /// <param name="value">the string to write</param>
        /// <returns>the number of bytes written</returns>
        public int WriteASCIIString(string value)
        {
            return WriteASCIIString(value, value.Length);
        }

        /// <summary>
        /// Writes an ASCII encoded string
        /// </summary>
        /// <param name="value">the string to write</param>
        /// <param name="length">the desired length of the string</param>
        /// <returns>the number of bytes written</returns>
        public int WriteASCIIString(string value, int length)
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
        public int WriteUnicodeString(string value)
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
        public int WriteDateTime(DateTime value)
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
        public int WriteVersionNumber(VersionNumber value)
        {
            byte major = SetRangeUInt8(value.Major);
            byte minor = (byte)SetRange(value.Minor, 0, 15);
            byte bugfix = (byte)SetRange(value.Bugfix, 0, 15);
            byte mb = (byte)((minor << 4) | bugfix);

            return WriteByte(major)
                 + WriteByte(mb)
                 + WriteEmpty(2);
        }

        /// <summary>
        /// Writes an ICC profile flag
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteProfileFlag(ProfileFlag value)
        {
            int flags = 0;
            for (int i = value.Flags.Length - 1; i >= 0; i--)
            {
                if (value.Flags[i]) flags |= 1 << i;
            }

            return WriteUInt16((ushort)flags)
                 + WriteEmpty(2);
        }

        /// <summary>
        /// Writes ICC device attributes
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteDeviceAttribute(DeviceAttribute value)
        {
            int flags = 0;
            flags |= (int)value.Opacity << 7;
            flags |= (int)value.Reflectivity << 6;
            flags |= (int)value.Polarity << 5;
            flags |= (int)value.Chroma << 4;

            return WriteByte((byte)flags)
                 + WriteEmpty(3)
                 + WriteArray(value.VendorData);
        }

        /// <summary>
        /// Writes an XYZ number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteXYZNumber(XYZNumber value)
        {
            return WriteFix16(value.X)
                 + WriteFix16(value.Y)
                 + WriteFix16(value.Z);
        }

        /// <summary>
        /// Writes a profile ID
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteProfileID(ProfileID value)
        {
            return WriteArray(value.Values);
        }

        /// <summary>
        /// Writes a position number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WritePositionNumber(PositionNumber value)
        {
            return WriteUInt32(value.Offset)
                 + WriteUInt32(value.Size);
        }

        /// <summary>
        /// Writes a response number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteResponseNumber(ResponseNumber value)
        {
            return WriteUInt16(value.DeviceCode)
                 + WriteFix16(value.MeasurementValue);
        }

        /// <summary>
        /// Writes a named color
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteNamedColor(NamedColor value)
        {
            return WriteASCIIString(value.Name, 31)
                 + WriteEmpty(1)    //Null terminator
                 + WriteArray(value.PCScoordinates)
                 + WriteArray(value.DeviceCoordinates);
        }

        /// <summary>
        /// Writes a profile description
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public int WriteProfileDescription(ProfileDescription value)
        {
            return WriteUInt32(value.DeviceManufacturer)
                 + WriteUInt32(value.DeviceModel)
                 + WriteDeviceAttribute(value.DeviceAttributes)
                 + WriteUInt32((uint)value.TechnologyInformation)
                 + WriteTagDataEntryHeader(TypeSignature.MultiLocalizedUnicode)
                 + WriteMultiLocalizedUnicodeTagDataEntry(new MultiLocalizedUnicodeTagDataEntry(value.DeviceManufacturerInfo))
                 + WriteTagDataEntryHeader(TypeSignature.MultiLocalizedUnicode)
                 + WriteMultiLocalizedUnicodeTagDataEntry(new MultiLocalizedUnicodeTagDataEntry(value.DeviceModelInfo));
        }

        #endregion

        #region Write Tag Data Entries

        /// <summary>
        /// Writes a tag data entry
        /// </summary>
        /// <param name="data">The entry to write</param>
        /// <param name="table">The table entry for the written data entry</param>
        /// <returns>The number of bytes written (excluding padding)</returns>
        public int WriteTagDataEntry(TagDataEntry data, out TagTableEntry table)
        {
            uint offset = (uint)DataStream.Position;
            int c = WriteTagDataEntry(data);
            WritePadding();
            table = new TagTableEntry(data.TagSignature, offset, (uint)c);
            return c;
        }

        /// <summary>
        /// Writes a tag data entry (without padding)
        /// </summary>
        /// <param name="entry">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteTagDataEntry(TagDataEntry entry)
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

                //V2 Type:
                case TypeSignature.TextDescription:
                    c += WriteTextDescriptionTagDataEntry(entry as TextDescriptionTagDataEntry);
                    break;

                case TypeSignature.Unknown:
                default:
                    c += WriteUnknownTagDataEntry(entry as UnknownTagDataEntry);
                    break;
            }
            return c;
        }

        /// <summary>
        /// Writes the header of a <see cref="TagDataEntry"/>
        /// </summary>
        /// <param name="signature">The signature of the entry</param>
        /// <returns>The number of bytes written</returns>
        public int WriteTagDataEntryHeader(TypeSignature signature)
        {
            return WriteUInt32((uint)signature)
                 + WriteEmpty(4);
        }


        /// <summary>
        /// Writes a <see cref="UnknownTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteUnknownTagDataEntry(UnknownTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        /// <summary>
        /// Writes a <see cref="ChromaticityTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteChromaticityTagDataEntry(ChromaticityTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt16((ushort)value.ChannelCount);
            c += WriteUInt16((ushort)value.ColorantType);

            for (int i = 0; i < value.ChannelCount; i++)
            {
                c += WriteUFix16(value.ChannelValues[i][0]);
                c += WriteUFix16(value.ChannelValues[i][1]);
            }
            return c;
        }

        /// <summary>
        /// Writes a <see cref="ColorantOrderTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteColorantOrderTagDataEntry(ColorantOrderTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteUInt32((uint)value.ColorantNumber.Length)
                 + WriteArray(value.ColorantNumber);
        }

        /// <summary>
        /// Writes a <see cref="ColorantTableTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteColorantTableTagDataEntry(ColorantTableTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt32((uint)value.ColorantData.Length);
            foreach (var colorant in value.ColorantData)
            {
                c += WriteASCIIString(colorant.Name, 32);
                c += WriteUInt16(colorant.PCS1);
                c += WriteUInt16(colorant.PCS2);
                c += WriteUInt16(colorant.PCS3);
            }

            return c;
        }

        /// <summary>
        /// Writes a <see cref="CurveTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCurveTagDataEntry(CurveTagDataEntry value)
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

        /// <summary>
        /// Writes a <see cref="DataTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteDataTagDataEntry(DataTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteEmpty(3)
                 + WriteByte((byte)(value.IsASCII ? 0x01 : 0x00))
                 + WriteArray(value.Data);
        }

        /// <summary>
        /// Writes a <see cref="DateTimeTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteDateTimeTagDataEntry(DateTimeTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteDateTime(value.Value);
        }

        /// <summary>
        /// Writes a <see cref="Lut16TagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteLut16TagDataEntry(Lut16TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteByte((byte)value.InputValues.Length);
            c += WriteByte((byte)value.OutputValues.Length);
            c += WriteByte(value.CLUTValues.GridPointCount[0]);
            c += WriteEmpty(1);

            c += WriteMatrix(value.Matrix ?? IdentityMatrix, false);

            c += WriteUInt16((ushort)value.InputValues[0].Values.Length);
            c += WriteUInt16((ushort)value.OutputValues[0].Values.Length);

            foreach (var lut in value.InputValues) { c += WriteLUT16(lut); }

            c += WriteCLUT16(value.CLUTValues);

            foreach (var lut in value.OutputValues) { c += WriteLUT16(lut); }

            return c;
        }

        /// <summary>
        /// Writes a <see cref="Lut8TagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteLut8TagDataEntry(Lut8TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteByte((byte)value.InputChannelCount);
            c += WriteByte((byte)value.OutputChannelCount);
            c += WriteByte((byte)value.CLUTValues.Values[0].Length);
            c += WriteEmpty(1);

            c += WriteMatrix(value.Matrix ?? IdentityMatrix, false);

            foreach (var lut in value.InputValues) { c += WriteLUT8(lut); }

            c += WriteCLUT8(value.CLUTValues);

            foreach (var lut in value.OutputValues) { c += WriteLUT8(lut); }

            return c;
        }

        /// <summary>
        /// Writes a <see cref="LutAToBTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteLutAToBTagDataEntry(LutAToBTagDataEntry value)
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
                c += WriteCurves(value.CurveB);
                c += WritePadding();
            }

            if (value.Matrix3x1 != null && value.Matrix3x3 != null)
            {
                matrixOffset = DataStream.Position;
                c += WriteMatrix(value.Matrix3x3, false);
                c += WriteMatrix(value.Matrix3x1, false);
                c += WritePadding();
            }

            if (value.CurveM != null)
            {
                mCurveOffset = DataStream.Position;
                c += WriteCurves(value.CurveM);
                c += WritePadding();
            }

            if (value.CLUTValues != null)
            {
                CLUTOffset = DataStream.Position;
                c += WriteCLUT(value.CLUTValues);
                c += WritePadding();
            }

            if (value.CurveA != null)
            {
                aCurveOffset = DataStream.Position;
                c += WriteCurves(value.CurveA);
                c += WritePadding();
            }

            //Set offset values
            long lpos = DataStream.Position;
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

            DataStream.Position = lpos;
            return c;
        }

        /// <summary>
        /// Writes a <see cref="LutBToATagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteLutBToATagDataEntry(LutBToATagDataEntry value)
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
                c += WriteCurves(value.CurveB);
                c += WritePadding();
            }

            if (value.Matrix3x1 != null && value.Matrix3x3 != null)
            {
                matrixOffset = DataStream.Position;
                c += WriteMatrix(value.Matrix3x3, false);
                c += WriteMatrix(value.Matrix3x1, false);
                c += WritePadding();
            }

            if (value.CurveM != null)
            {
                mCurveOffset = DataStream.Position;
                c += WriteCurves(value.CurveM);
                c += WritePadding();
            }

            if (value.CLUTValues != null)
            {
                CLUTOffset = DataStream.Position;
                c += WriteCLUT(value.CLUTValues);
                c += WritePadding();
            }

            if (value.CurveA != null)
            {
                aCurveOffset = DataStream.Position;
                c += WriteCurves(value.CurveA);
                c += WritePadding();
            }

            //Set offset values
            long lpos = DataStream.Position;
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

            DataStream.Position = lpos;
            return c;
        }

        /// <summary>
        /// Writes a <see cref="MeasurementTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMeasurementTagDataEntry(MeasurementTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteUInt32((uint)value.Observer)
                 + WriteXYZNumber(value.XYZBacking)
                 + WriteUInt32((uint)value.Geometry)
                 + WriteUFix16(value.Flare)
                 + WriteUInt32((uint)value.Illuminant);
        }

        /// <summary>
        /// Writes a <see cref="MultiLocalizedUnicodeTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMultiLocalizedUnicodeTagDataEntry(MultiLocalizedUnicodeTagDataEntry value)
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
            long lpos = DataStream.Position;
            DataStream.Position = tpos;
            for (int i = 0; i < count; i++)
            {
                string[] code = value.Text[i].Culture.Split('-');
                if (code.Length != 2) throw new Exception();
                c += WriteASCIIString(code[0], 2);
                c += WriteASCIIString(code[1], 2);
                c += WriteUInt32((uint)length[i]);
                c += WriteUInt32(offset[i]);
            }

            DataStream.Position = lpos;
            return c;
        }

        /// <summary>
        /// Writes a <see cref="MultiProcessElementsTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMultiProcessElementsTagDataEntry(MultiProcessElementsTagDataEntry value)
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
                c += WritePadding();
                posTable[i] = new PositionNumber(offset, (uint)size);
                c += size;
            }

            //Write position table
            long lpos = DataStream.Position;
            DataStream.Position = tpos;
            foreach (var pos in posTable) { c += WritePositionNumber(pos); }

            DataStream.Position = lpos;
            return c;
        }

        /// <summary>
        /// Writes a <see cref="NamedColor2TagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteNamedColor2TagDataEntry(NamedColor2TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteArray(value.VendorFlags)
                  + WriteEmpty(4 - value.VendorFlags.Length)
                  + WriteUInt32((uint)value.Colors.Length)
                  + WriteUInt32((uint)value.CoordCount)
                  + WriteASCIIString(value.Prefix, 31)
                  + WriteEmpty(1)//Null terminator
                  + WriteASCIIString(value.Suffix, 31)
                  + WriteEmpty(1);//Null terminator

            foreach (var color in value.Colors) { c += WriteNamedColor(color); }

            return c;
        }

        /// <summary>
        /// Writes a <see cref="ParametricCurveTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteParametricCurveTagDataEntry(ParametricCurveTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteParametricCurve(value.Curve);
        }

        /// <summary>
        /// Writes a <see cref="ProfileSequenceDescTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteProfileSequenceDescTagDataEntry(ProfileSequenceDescTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt32((uint)value.Descriptions.Length);
            foreach (var desc in value.Descriptions) { c += WriteProfileDescription(desc); }
            return c;
        }

        /// <summary>
        /// Writes a <see cref="ProfileSequenceIdentifierTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteProfileSequenceIdentifierTagDataEntry(ProfileSequenceIdentifierTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;
            int count = value.Data.Length;

            int c = WriteUInt32((uint)count);

            //Jump over position table
            long tpos = DataStream.Position;
            DataStream.Position += count * 8;
            var table = new PositionNumber[count];

            for (int i = 0; i < count; i++)
            {
                uint offset = (uint)(DataStream.Position - start);
                int size = WriteProfileID(value.Data[i].ID);
                size += WriteTagDataEntry(new MultiLocalizedUnicodeTagDataEntry(value.Data[i].Description));
                size += WritePadding();
                table[i] = new PositionNumber(offset, (uint)size);
                c += size;
            }

            //Write position table
            long lpos = DataStream.Position;
            DataStream.Position = tpos;
            foreach (var pos in table) { c += WritePositionNumber(pos); }

            DataStream.Position = lpos;
            return c;
        }

        /// <summary>
        /// Writes a <see cref="ResponseCurveSet16TagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteResponseCurveSet16TagDataEntry(ResponseCurveSet16TagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            long start = DataStream.Position - 8;

            int c = WriteUInt16(value.ChannelCount);
            c += WriteUInt16((ushort)value.Curves.Length);

            //Jump over position table
            long tpos = DataStream.Position;
            DataStream.Position += value.Curves.Length * 4;

            var offset = new uint[value.Curves.Length];

            for (int i = 0; i < value.Curves.Length; i++)
            {
                offset[i] = (uint)(DataStream.Position - start);
                c += WriteResponseCurve(value.Curves[i]);
                c += WritePadding();
            }

            //Write position table
            long lpos = DataStream.Position;
            DataStream.Position = tpos;
            c += WriteArray(offset);

            DataStream.Position = lpos;
            return c;
        }

        /// <summary>
        /// Writes a <see cref="Fix16ArrayTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteFix16ArrayTagDataEntry(Fix16ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Data.Length; i++) { c += WriteFix16(value.Data[i] * 256d); }
            return c;
        }

        /// <summary>
        /// Writes a <see cref="SignatureTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteSignatureTagDataEntry(SignatureTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteASCIIString(value.SignatureData, 4);
        }

        /// <summary>
        /// Writes a <see cref="TextTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteTextTagDataEntry(TextTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteASCIIString(value.Text);
        }

        /// <summary>
        /// Writes a <see cref="UFix16ArrayTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteUFix16ArrayTagDataEntry(UFix16ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Data.Length; i++) { c += WriteUFix16(value.Data[i]); }
            return c;
        }

        /// <summary>
        /// Writes a <see cref="UInt16ArrayTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteUInt16ArrayTagDataEntry(UInt16ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        /// <summary>
        /// Writes a <see cref="UInt32ArrayTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteUInt32ArrayTagDataEntry(UInt32ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        /// <summary>
        /// Writes a <see cref="UInt64ArrayTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteUInt64ArrayTagDataEntry(UInt64ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        /// <summary>
        /// Writes a <see cref="UInt8ArrayTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteUInt8ArrayTagDataEntry(UInt8ArrayTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteArray(value.Data);
        }

        /// <summary>
        /// Writes a <see cref="ViewingConditionsTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteViewingConditionsTagDataEntry(ViewingConditionsTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteXYZNumber(value.IlluminantXYZ)
                 + WriteXYZNumber(value.SurroundXYZ)
                 + WriteUInt32((uint)value.Illuminant);
        }

        /// <summary>
        /// Writes a <see cref="XYZTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteXYZTagDataEntry(XYZTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Data.Length; i++) { c += WriteXYZNumber(value.Data[i]); }
            return c;
        }

        /// <summary>
        /// Writes a <see cref="TextDescriptionTagDataEntry"/>
        /// </summary>
        /// <param name="value">The entry to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteTextDescriptionTagDataEntry(TextDescriptionTagDataEntry value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int size, c = 0;

            if (value.ASCII == null) c += WriteUInt32(0);
            else
            {
                DataStream.Position += 4;
                c += size = WriteASCIIString(value.ASCII + '\0');
                DataStream.Position -= size + 4;
                c += WriteUInt32((uint)size);
                DataStream.Position += size;
            }

            if (value.Unicode == null)
            {
                c += WriteUInt32(0);
                c += WriteUInt32(0);
            }
            else
            {
                DataStream.Position += 8;
                c += size = WriteUnicodeString(value.Unicode + '\0');
                DataStream.Position -= size + 8;
                c += WriteUInt32(value.UnicodeLanguageCode);
                c += WriteUInt32((uint)value.Unicode.Length + 1);
                DataStream.Position += size;
            }

            if (value.ScriptCode == null)
            {
                c += WriteUInt16(0);
                c += WriteByte(0);
                c += WriteEmpty(67);
            }
            else
            {
                DataStream.Position += 3;
                c += size = WriteASCIIString(SetRange(value.ScriptCode, 66) + '\0');
                DataStream.Position -= size + 3;
                c += WriteUInt16(value.ScriptCodeCode);
                c += WriteByte((byte)(value.ScriptCode.Length > 66 ? 67 : value.ScriptCode.Length + 1));
                DataStream.Position += size;
            }

            return c;
        }

        #endregion

        #region Write Matrix

        /// <summary>
        /// Writes a two dimensional matrix
        /// </summary>        
        /// <param name="value">The matrix to write</param>
        /// <param name="isSingle">True if the values are encoded as Single; false if encoded as Fix16</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMatrix(double[,] value, bool isSingle)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int y = 0; y < value.GetLength(1); y++)
            {
                for (int x = 0; x < value.GetLength(0); x++)
                {
                    if (isSingle) { c += WriteSingle((float)value[x, y]); }
                    else { c += WriteFix16(value[x, y]); }
                }
            }
            return c;
        }

        /// <summary>
        /// Writes a one dimensional matrix
        /// </summary>        
        /// <param name="value">The matrix to write</param>
        /// <param name="isSingle">True if the values are encoded as Single; false if encoded as Fix16</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMatrix(double[] value, bool isSingle)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (isSingle) { c += WriteSingle((float)value[i]); }
                else { c += WriteFix16(value[i]); }
            }
            return c;
        }

        #endregion

        #region Write (C)LUT
        
        /// <summary>
        /// Writes an 8bit lookup table
        /// </summary>
        /// <param name="value">The LUT to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteLUT8(LUT value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            foreach (var item in value.Values) { c += WriteByte(SetRangeUInt8(item)); }
            return c;
        }

        /// <summary>
        /// Writes an 16bit lookup table
        /// </summary>
        /// <param name="value">The LUT to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteLUT16(LUT value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = 0;
            foreach (var item in value.Values) { c += WriteUInt16(SetRangeUInt16(item)); }
            return c;
        }

        /// <summary>
        /// Writes an color lookup table
        /// </summary>
        /// <param name="value">The CLUT to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCLUT(CLUT value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteArray(value.GridPointCount);
            c += WriteEmpty(16 - value.GridPointCount.Length);

            switch (value.DataType)
            {
                case CLUTDataType.Float:
                    return c + WriteCLUTf32(value);
                case CLUTDataType.UInt8:
                    c += WriteByte(1);
                    c += WriteEmpty(3);
                    return c + WriteCLUT8(value);
                case CLUTDataType.UInt16:
                    c += WriteByte(2);
                    c += WriteEmpty(3);
                    return c + WriteCLUT16(value);

                default:
                    throw new CorruptProfileException("CLUT");
            }
        }

        /// <summary>
        /// Writes a 8bit color lookup table
        /// </summary>
        /// <param name="value">The CLUT to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCLUT8(CLUT value)
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

        /// <summary>
        /// Writes a 16bit color lookup table
        /// </summary>
        /// <param name="value">The CLUT to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCLUT16(CLUT value)
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

        /// <summary>
        /// Writes a 32bit float color lookup table
        /// </summary>
        /// <param name="value">The CLUT to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCLUTf32(CLUT value)
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

        /// <summary>
        /// Writes a <see cref="MultiProcessElement"/>
        /// </summary>
        /// <param name="value">The element to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMultiProcessElement(MultiProcessElement value)
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

                default:
                    throw new CorruptProfileException("MultiProcessElement");
            }
        }

        /// <summary>
        /// Writes a CurveSet <see cref="MultiProcessElement"/>
        /// </summary>
        /// <param name="value">The element to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCurveSetProcessElement(CurveSetProcessElement value)
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

        /// <summary>
        /// Writes a Matrix <see cref="MultiProcessElement"/>
        /// </summary>
        /// <param name="value">The element to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteMatrixProcessElement(MatrixProcessElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteMatrix(value.MatrixIxO, true)
                 + WriteMatrix(value.MatrixOx1, true);
        }

        /// <summary>
        /// Writes a CLUT <see cref="MultiProcessElement"/>
        /// </summary>
        /// <param name="value">The element to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCLUTProcessElement(CLUTProcessElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return WriteCLUT(value.CLUTValue);
        }

        #endregion

        #region Write Curves

        /// <summary>
        /// Writes a <see cref="OneDimensionalCurve"/>
        /// </summary>
        /// <param name="value">The curve to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteOneDimensionalCurve(OneDimensionalCurve value)
        {
            int c = WriteUInt16((ushort)value.Segments.Length);
            c += WriteEmpty(2);

            foreach (var point in value.BreakPoints) { c += WriteSingle((float)point); }

            foreach (var segment in value.Segments) { c += WriteCurveSegment(segment); }

            return c;
        }

        /// <summary>
        /// Writes a <see cref="ResponseCurve"/>
        /// </summary>
        /// <param name="value">The curve to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteResponseCurve(ResponseCurve value)
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

        /// <summary>
        /// Writes a <see cref="ParametricCurve"/>
        /// </summary>
        /// <param name="value">The curve to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteParametricCurve(ParametricCurve value)
        {
            int c = WriteUInt16(value.type);
            c += WriteEmpty(2);

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

        /// <summary>
        /// Writes a <see cref="CurveSegment"/>
        /// </summary>
        /// <param name="value">The curve to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteCurveSegment(CurveSegment value)
        {
            int c = WriteUInt32((uint)value.Signature);
            c += WriteEmpty(4);

            switch (value.Signature)
            {
                case CurveSegmentSignature.FormulaCurve:
                    return c + WriteFormulaCurveElement(value as FormulaCurveElement);
                case CurveSegmentSignature.SampledCurve:
                    return c + WriteSampledCurveElement(value as SampledCurveElement);
                default:
                    throw new CorruptProfileException("CurveSegment");
            }
        }

        /// <summary>
        /// Writes a <see cref="FormulaCurveElement"/>
        /// </summary>
        /// <param name="value">The curve to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteFormulaCurveElement(FormulaCurveElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt16(value.type);
            c += WriteEmpty(2);

            if (value.type == 0 || value.type == 1) c += WriteSingle((float)value.gamma);
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

        /// <summary>
        /// Writes a <see cref="SampledCurveElement"/>
        /// </summary>
        /// <param name="value">The curve to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteSampledCurveElement(SampledCurveElement value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            int c = WriteUInt32((uint)value.CurveEntries.Length);
            foreach (var entry in value.CurveEntries) { c += WriteSingle((float)entry); }

            return c;
        }

        #endregion

        #region Write Array

        /// <summary>
        /// Writes a byte array
        /// </summary>
        /// <param name="data">The array to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteArray(byte[] data)
        {
            DataStream.Write(data, 0, data.Length);
            return data.Length;
        }

        /// <summary>
        /// Writes a ushort array
        /// </summary>
        /// <param name="data">The array to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteArray(ushort[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteUInt16(data[i]); }
            return c;
        }

        /// <summary>
        /// Writes a short array
        /// </summary>
        /// <param name="data">The array to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteArray(short[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteInt16(data[i]); }
            return c;
        }

        /// <summary>
        /// Writes a uint array
        /// </summary>
        /// <param name="data">The array to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteArray(uint[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteUInt32(data[i]); }
            return c;
        }

        /// <summary>
        /// Writes an int array
        /// </summary>
        /// <param name="data">The array to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteArray(int[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteInt32(data[i]); }
            return c;
        }

        /// <summary>
        /// Writes a ulong array
        /// </summary>
        /// <param name="data">The array to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteArray(ulong[] data)
        {
            int c = 0;
            for (int i = 0; i < data.Length; i++) { c += WriteUInt64(data[i]); }
            return c;
        }

        #endregion

        #region Write Misc

        /// <summary>
        /// Write a number of empty bytes
        /// </summary>
        /// <param name="length">The number of bytes to write</param>
        /// <returns>The number of bytes written</returns>
        public int WriteEmpty(int length)
        {
            for (int i = 0; i < length; i++)
            {
                DataStream.WriteByte(0);
            }
            return length;
        }

        /// <summary>
        /// Writes empty bytes to a 4-byte margin
        /// </summary>
        /// <returns>The number of bytes written</returns>
        public int WritePadding()
        {
            int p = 4 - (int)DataStream.Position % 4;
            return WriteEmpty(p >= 4 ? 0 : p);
        }

        /// <summary>
        /// Writes given bytes from pointer
        /// </summary>
        /// <param name="data">Pointer to the bytes to write</param>
        /// <param name="length">The number of bytes to write</param>
        /// <returns>The number of bytes written</returns>
        private unsafe int WriteBytes(byte* data, int length)
        {
            if (LittleEndian)
            {
                for (int i = length - 1; i >= 0; i--)
                    DataStream.WriteByte(data[i]);
            }
            else
            {
                for (int i = 0; i < length; i++)
                    DataStream.WriteByte(data[i]);
            }

            return length;
        }

        /// <summary>
        /// Writes curve data
        /// </summary>
        /// <param name="curves">The curves to write</param>
        /// <returns>The number of bytes written</returns>
        private int WriteCurves(TagDataEntry[] curves)
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
                c += WritePadding();
            }
            return c;
        }

        #endregion

        #region Subroutines

        /// <summary>
        /// Changes the value to fit into the given bounds (if out of bounds)
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <returns>The given value within the bounds</returns>
        private int SetRange(int value, int min, int max)
        {
            if (value > max) return max;
            else if (value < min) return min;
            else return value;
        }

        /// <summary>
        /// Changes the value to fit into the given bounds (if out of bounds)
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        /// <returns>The given value within the bounds</returns>
        private double SetRange(double value, double min, double max)
        {
            if (value > max) return max;
            else if (value < min) return min;
            else return value;
        }

        /// <summary>
        /// Shortens a string to a given maximum length
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <param name="length">The maximum number of characters</param>
        /// <returns>The given string within the bounds</returns>
        private string SetRange(string value, int length)
        {
            if (value.Length < length) return value.PadRight(length);
            else if (value.Length > length) return value.Substring(0, length);
            else return value;
        }

        /// <summary>
        /// Checks a double to fit into the range of 0-1 and scales it up to the range of an unsigned 16bit integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The ushort representation of the given value</returns>
        private ushort SetRangeUInt16(double value)
        {
            if (value > 1) value = 1;
            else if (value < 0) value = 0;
            return (ushort)(value * ushort.MaxValue + 0.5);
        }

        /// <summary>
        /// Checks a double to fit into the range of 0-1 and scales it up to the range of an unsigned 8bit integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The ushort representation of the given value</returns>
        private byte SetRangeUInt8(double value)
        {
            if (value > 1) value = 1;
            else if (value < 0) value = 0;
            return (byte)(value * byte.MaxValue + 0.5);
        }

        /// <summary>
        /// Fits an integer into the byte range and converts it to a byte
        /// </summary>
        /// <param name="value">The integer to convert</param>
        /// <returns>The byte representation of the given value</returns>
        private byte SetRangeUInt8(int value)
        {
            if (value > byte.MaxValue) return byte.MaxValue;
            else if (value < byte.MinValue) return byte.MinValue;
            else return (byte)value;
        }

        #endregion
    }
}
