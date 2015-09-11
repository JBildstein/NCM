using System;
using System.Text;
using System.Globalization;

namespace ColorManager.ICC
{
    /// <summary>
    /// Provides methods to read ICC data types
    /// </summary>
    public sealed class ICCDataReader
    {
        /// <summary>
        /// The data that is read
        /// </summary>
        public readonly byte[] Data;
        /// <summary>
        /// The current reading position
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set
            {
                if (value < 0) _Index = 0;
                else if (value > Data.Length - 1) _Index = Data.Length - 1;
                else _Index = value;
            }
        }

        private int _Index;
        private static readonly bool LittleEndian = BitConverter.IsLittleEndian;

        /// <summary>
        /// Creates a new instance of the <see cref="ICCDataReader"/> class
        /// </summary>
        /// <param name="Data">The data to read</param>
        public ICCDataReader(byte[] Data)
        {
            this.Data = Data;
        }

        #region Read Primitives
        
        /// <summary>
        /// Reads an ushort
        /// </summary>
        /// <returns>the value</returns>
        public ushort ReadUInt16()
        {
            unchecked
            {
                if (LittleEndian) return (ushort)((Data[Index++] << 8) | Data[Index++]);
                else return (ushort)(Data[Index++] | (Data[Index++] << 8));
            }
        }

        /// <summary>
        /// Reads a short
        /// </summary>
        /// <returns>the value</returns>
        public short ReadInt16()
        {
            unchecked { return (short)(ReadUInt16() - ushort.MaxValue - 1); }
        }

        /// <summary>
        /// Reads an uint
        /// </summary>
        /// <returns>the value</returns>
        public uint ReadUInt32()
        {
            unchecked
            {
                if (LittleEndian) return (uint)((Data[Index++] << 24) | (Data[Index++] << 16) | (Data[Index++] << 8) | Data[Index++]);
                else return (uint)(Data[Index++] | (Data[Index++] << 8) | (Data[Index++] << 16) | (Data[Index++] << 24));
            }
        }

        /// <summary>
        /// Reads an int
        /// </summary>
        /// <returns>the value</returns>
        public int ReadInt32()
        {
            unchecked { return (int)(ReadUInt32() - uint.MaxValue - 1); }
        }

        /// <summary>
        /// Reads an ulong
        /// </summary>
        /// <returns>the value</returns>
        public ulong ReadUInt64()
        {
            unchecked
            {
                if (LittleEndian) return (ulong)ReadUInt32() << 32 | ReadUInt32();
                else return ReadUInt32() | (ulong)ReadUInt32() << 32;
            }
        }

        /// <summary>
        /// Reads a long
        /// </summary>
        /// <returns>the value</returns>
        public long ReadInt64()
        {
            unchecked { return (long)(ReadUInt64() - ulong.MaxValue - 1); }
        }

        /// <summary>
        /// Reads a float
        /// </summary>
        /// <returns>the value</returns>
        public unsafe float ReadSingle()
        {
            int val = ReadInt32();
            return *((float*)&val);
        }

        /// <summary>
        /// Reads a double
        /// </summary>
        /// <returns>the value</returns>
        public unsafe double ReadDouble()
        {
            long val = ReadInt64();
            return *((double*)&val);
        }


        /// <summary>
        /// Reads an ASCII encoded string
        /// </summary>
        /// <param name="length">number of bytes to read</param>
        /// <returns>The value as a string</returns>
        public string ReadASCIIString(int length)
        {
            return Encoding.ASCII.GetString(Data, AIndex(length), length);
        }

        /// <summary>
        /// Reads an UTF-16 big-endian encoded string
        /// </summary>
        /// <param name="length">number of bytes to read</param>
        /// <returns>The value as a string</returns>
        public string ReadUnicodeString(int length)
        {
            return Encoding.BigEndianUnicode.GetString(Data, AIndex(length), length);
        }


        /// <summary>
        /// Reads a signed 32bit number with 1 sign bit, 15 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>The number as double</returns>
        public double ReadFix16()
        {
            return ReadInt32() / 65536d;
        }

        /// <summary>
        /// Reads an unsigned 32bit number with 16 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>The number as double</returns>
        public double ReadUFix16()
        {
            return ReadUInt32() / 65536d;
        }

        /// <summary>
        /// Reads an unsigned 16bit number with 1 value bit and 15 fractional bits 
        /// </summary>
        /// <returns>The number as double</returns>
        public double ReadU1Fix15()
        {
            return ReadUInt16() / 32768d;
        }

        /// <summary>
        /// Reads an unsigned 16bit number with 8 value bits and 8 fractional bits
        /// </summary>
        /// <returns>The number as double</returns>
        public double ReadUFix8()
        {
            return ReadUInt16() / 256d;
        }

        #endregion

        #region Read Structs

        /// <summary>
        /// Reads a DateTime
        /// </summary>
        /// <returns>the value</returns>
        public DateTime ReadDateTime()
        {
            try
            {
                return new DateTime(ReadUInt16(), ReadUInt16(), ReadUInt16(),
                    ReadUInt16(), ReadUInt16(), ReadUInt16(), DateTimeKind.Utc);
            }
            catch (ArgumentOutOfRangeException) { throw new InvalidProfileException("Invalid DateTime format"); }
        }

        /// <summary>
        /// Reads an ICC profile version number
        /// </summary>
        /// <returns>the version number</returns>
        public VersionNumber ReadVersionNumber()
        {
            int Major = Data[AIndex(1)];
            byte MB = Data[AIndex(1)];
            int Minor = (MB >> 4) & 0x0F;
            int BugFix = MB & 0x0F;
            AIndex(2);//2 bytes reserved
            return new VersionNumber(Major, Minor, BugFix);
        }

        /// <summary>
        /// Reads an ICC profile flag
        /// </summary>
        /// <returns>the profile flag</returns>
        public unsafe ProfileFlag ReadProfileFlag()
        {
            var flags = new bool[16];
            ushort flagVal = (ushort)((Data[AIndex(1)] << 8) | Data[AIndex(1)]);
            for (int i = 0; i < 16; i++) flags[15 - i] = GetBit(flagVal, i);
            AIndex(2);//ICC reserved data

            return new ProfileFlag(flags);
        }

        /// <summary>
        /// Reads ICC device attributes
        /// </summary>
        /// <returns>the device attributes</returns>
        public DeviceAttribute ReadDeviceAttribute()
        {
            //Read first byte and the next 3 bytes are unused:
            byte b = Data[AIndex(4)];
            var vdata = new byte[4];
            Buffer.BlockCopy(Data, AIndex(4), vdata, 0, 4);
            return new DeviceAttribute(GetBit(b, 0), GetBit(b, 1), GetBit(b, 2), GetBit(b, 3), vdata);
        }

        /// <summary>
        /// Reads an XYZ number
        /// </summary>
        /// <returns>the XYZ number</returns>
        public XYZNumber ReadXYZNumber()
        {
            return new XYZNumber(ReadFix16(), ReadFix16(), ReadFix16());
        }

        /// <summary>
        /// Reads a profile ID
        /// </summary>
        /// <returns>the profile ID</returns>
        public ProfileID ReadProfileID()
        {
            return new ProfileID(ReadUInt32(), ReadUInt32(), ReadUInt32(), ReadUInt32());
        }

        /// <summary>
        /// Reads a position number
        /// </summary>
        /// <returns>the position number</returns>
        public PositionNumber ReadPositionNumber()
        {
            return new PositionNumber(ReadUInt32(), ReadUInt32());
        }

        /// <summary>
        /// Reads a response number
        /// </summary>
        /// <returns>the response number</returns>
        public ResponseNumber ReadResponseNumber()
        {
            return new ResponseNumber(ReadUInt16(), ReadFix16());
        }

        /// <summary>
        /// Reads a named color
        /// </summary>
        /// <returns>the named color</returns>
        public NamedColor ReadNamedColor(int deviceCoordCount)
        {
            var name = ReadASCIIString(32);
            var pcsCoord = new ushort[3] { ReadUInt16(), ReadUInt16(), ReadUInt16() };
            var deviceCoord = new ushort[deviceCoordCount];
            for (int i = 0; i < deviceCoordCount; i++) deviceCoord[i] = ReadUInt16();

            return new NamedColor(name, pcsCoord, deviceCoord);
        }

        /// <summary>
        /// Reads a profile description
        /// </summary>
        /// <returns>the profile description</returns>
        public ProfileDescription ReadProfileDescription()
        {
            var manufacturer = ReadUInt32();
            var model = ReadUInt32();
            var attributes = ReadDeviceAttribute();
            var technologyInfo = (TagSignature)ReadUInt32();
            ReadTagDataEntryHeader(TypeSignature.MultiLocalizedUnicode);
            var manufacturerInfo = ReadMultiLocalizedUnicodeTagDataEntry();
            ReadTagDataEntryHeader(TypeSignature.MultiLocalizedUnicode);
            var modelInfo = ReadMultiLocalizedUnicodeTagDataEntry();

            return new ProfileDescription(manufacturer, model, attributes, technologyInfo, manufacturerInfo.Text, modelInfo.Text);
        }

        #endregion

        //LTODO: Lut16TagDataEntry can have legacy Lab values. See page 51/52

        #region Read Tag Data Entries

        /// <summary>
        /// Reads a tag data entry
        /// </summary>
        /// <param name="info">The table entry with reading information</param>
        /// <returns>the tag data entry</returns>
        public TagDataEntry ReadTagDataEntry(TagTableEntry info)
        {
            Index = (int)info.Offset;
            TypeSignature t = ReadTagDataEntryHeader();

            switch (t)
            {
                case TypeSignature.Chromaticity:
                    return ReadChromaticityTagDataEntry();
                case TypeSignature.ColorantOrder:
                    return ReadColorantOrderTagDataEntry();
                case TypeSignature.ColorantTable:
                    return ReadColorantTableTagDataEntry();
                case TypeSignature.Curve:
                    return ReadCurveTagDataEntry();
                case TypeSignature.Data:
                    return ReadDataTagDataEntry(info.DataSize);
                case TypeSignature.DateTime:
                    return ReadDateTimeTagDataEntry();
                case TypeSignature.Lut16:
                    return ReadLut16TagDataEntry();
                case TypeSignature.Lut8:
                    return ReadLut8TagDataEntry();
                case TypeSignature.LutAToB:
                    return ReadLutAToBTagDataEntry();
                case TypeSignature.LutBToA:
                    return ReadLutBToATagDataEntry();
                case TypeSignature.Measurement:
                    return ReadMeasurementTagDataEntry();
                case TypeSignature.MultiLocalizedUnicode:
                    return ReadMultiLocalizedUnicodeTagDataEntry();
                case TypeSignature.MultiProcessElements:
                    return ReadMultiProcessElementsTagDataEntry();
                case TypeSignature.NamedColor2:
                    return ReadNamedColor2TagDataEntry();
                case TypeSignature.ParametricCurve:
                    return ReadParametricCurveTagDataEntry();
                case TypeSignature.ProfileSequenceDesc:
                    return ReadProfileSequenceDescTagDataEntry();
                case TypeSignature.ProfileSequenceIdentifier:
                    return ReadProfileSequenceIdentifierTagDataEntry();
                case TypeSignature.ResponseCurveSet16:
                    return ReadResponseCurveSet16TagDataEntry();
                case TypeSignature.S15Fixed16Array:
                    return ReadFix16ArrayTagDataEntry(info.DataSize);
                case TypeSignature.Signature:
                    return ReadSignatureTagDataEntry();
                case TypeSignature.Text:
                    return ReadTextTagDataEntry(info.DataSize);
                case TypeSignature.U16Fixed16Array:
                    return ReadUFix16ArrayTagDataEntry(info.DataSize);
                case TypeSignature.UInt16Array:
                    return ReadUInt16ArrayTagDataEntry(info.DataSize);
                case TypeSignature.UInt32Array:
                    return ReadUInt32ArrayTagDataEntry(info.DataSize);
                case TypeSignature.UInt64Array:
                    return ReadUInt64ArrayTagDataEntry(info.DataSize);
                case TypeSignature.UInt8Array:
                    return ReadUInt8ArrayTagDataEntry(info.DataSize);
                case TypeSignature.ViewingConditions:
                    return ReadViewingConditionsTagDataEntry(info.DataSize);
                case TypeSignature.XYZ:
                    return ReadXYZTagDataEntry(info.DataSize);

                case TypeSignature.Unknown:
                default:
                    return ReadUnknownTagDataEntry(info.DataSize);
            }
        }

        /// <summary>
        /// Reads the header of a <see cref="TagDataEntry"/>
        /// </summary>
        /// <param name="expected">Optional expected value to check against</param>
        /// <returns></returns>
        public TypeSignature ReadTagDataEntryHeader(TypeSignature expected = (TypeSignature)uint.MaxValue)
        {
            TypeSignature t = (TypeSignature)ReadUInt32();
            AIndex(4);//4 bytes are not used
            if (expected != (TypeSignature)uint.MaxValue && t != expected) throw new CorruptProfileException($"Signature {t} is not expected {expected}");
            return t;
        }


        public UnknownTagDataEntry ReadUnknownTagDataEntry(uint size)
        {
            var count = (int)size - 8;
            var adata = new byte[count];
            Buffer.BlockCopy(Data, AIndex(count), adata, 0, count);

            return new UnknownTagDataEntry(adata);
        }

        public ChromaticityTagDataEntry ReadChromaticityTagDataEntry()
        {
            ushort channelCount = ReadUInt16();
            ColorantEncoding colorant = (ColorantEncoding)ReadUInt16();

            if (colorant != ColorantEncoding.Unknown && channelCount != 3) { throw new CorruptProfileException("ChromaticityTagDataEntry"); }
            else if (colorant != ColorantEncoding.Unknown && Enum.IsDefined(typeof(ColorantEncoding), colorant)) { return new ChromaticityTagDataEntry(channelCount, colorant); }
            else
            {
                double[][] values = new double[channelCount][];
                for (int i = 0; i < channelCount; i++)
                {
                    values[i] = new double[2];
                    values[i][0] = ReadUFix16();
                    values[i][1] = ReadUFix16();
                }
                return new ChromaticityTagDataEntry(channelCount, colorant, values);
            }
        }

        public ColorantOrderTagDataEntry ReadColorantOrderTagDataEntry()
        {
            var colorantCount = ReadUInt32();
            var number = new byte[colorantCount];
            Buffer.BlockCopy(Data, AIndex((int)colorantCount), number, 0, (int)colorantCount);
            return new ColorantOrderTagDataEntry(colorantCount, number);
        }

        public ColorantTableTagDataEntry ReadColorantTableTagDataEntry()
        {
            var colorantCount = ReadUInt32();
            var cdata = new ColorantTableEntry[colorantCount];
            for (int i = 0; i < colorantCount; i++)
            {
                string name = ReadASCIIString(32);
                ushort pcs1 = ReadUInt16();
                ushort pcs2 = ReadUInt16();
                ushort pcs3 = ReadUInt16();
                cdata[i] = new ColorantTableEntry(name, pcs1, pcs2, pcs3);
            }
            return new ColorantTableTagDataEntry(colorantCount, cdata);
        }

        public CurveTagDataEntry ReadCurveTagDataEntry()
        {
            var pointCount = ReadUInt32();

            if (pointCount == 0) { return new CurveTagDataEntry(); }
            else if (pointCount == 1) { return new CurveTagDataEntry(ReadUFix8()); }
            else
            {
                var cdata = new double[pointCount];
                for (int i = 0; i < pointCount; i++) { cdata[i] = ReadUInt16() / 65535d; }
                return new CurveTagDataEntry(cdata);
            }

            //TODO: Page 48: If the input is PCSXYZ, 1+(32 767/32 768) shall be mapped to the value 1,0. If the output is PCSXYZ, the value 1,0 shall be mapped to 1+(32 767/32 768).
        }

        public DataTagDataEntry ReadDataTagDataEntry(uint size)
        {
            AIndex(3);//first 3 bytes are zero
            byte b = Data[AIndex(1)];
            //last bit of 4th byte is either 0 = ASCII or 1 = binary
            bool ascii = GetBit(b, 7);
            int length = (int)size - 12;
            var cdata = new byte[length];
            Buffer.BlockCopy(Data, AIndex(length), cdata, 0, length);

            return new DataTagDataEntry(cdata, ascii);
        }

        public DateTimeTagDataEntry ReadDateTimeTagDataEntry()
        {
            return new DateTimeTagDataEntry(ReadDateTime());
        }

        public Lut16TagDataEntry ReadLut16TagDataEntry()
        {
            var inChCount = Data[AIndex(1)];
            var outChCount = Data[AIndex(1)];
            var CLUTPointCount = Data[AIndex(1)];
            AIndex(1);//1 byte reserved

            var matrix = ReadMatrix(3, 3, false);

            var inTableCount = ReadUInt16();
            var outTableCount = ReadUInt16();

            //Input LUT
            var inValues = new LUT[inChCount];
            for (int i = 0; i < inChCount; i++) { inValues[i] = ReadLUT16(inTableCount); }

            //CLUT
            byte[] gridPointCount = new byte[16];
            for (int i = 0; i < 16; i++) { gridPointCount[i] = CLUTPointCount; }
            var clut = ReadCLUT16(inChCount, outChCount, gridPointCount);

            //Output LUT
            var outValues = new LUT[outChCount];
            for (int i = 0; i < outChCount; i++) { outValues[i] = ReadLUT16(outTableCount); }

            return new Lut16TagDataEntry(matrix, inValues, clut, outValues);
        }

        public Lut8TagDataEntry ReadLut8TagDataEntry()
        {
            var inChCount = Data[AIndex(1)];
            var outChCount = Data[AIndex(1)];
            var CLUTPointCount = Data[AIndex(1)];
            AIndex(1);//1 byte reserved

            var matrix = ReadMatrix(3, 3, false);

            //Input LUT
            var inValues = new LUT[inChCount];
            for (int i = 0; i < inChCount; i++) { inValues[i] = ReadLUT8(); }

            //CLUT
            byte[] gridPointCount = new byte[16];
            for (int i = 0; i < 16; i++) { gridPointCount[i] = CLUTPointCount; }
            var clut = ReadCLUT8(inChCount, outChCount, gridPointCount);

            //Output LUT
            var outValues = new LUT[outChCount];
            for (int i = 0; i < outChCount; i++) { outValues[i] = ReadLUT8(); }

            return new Lut8TagDataEntry(matrix, inValues, clut, outValues);
        }

        public LutAToBTagDataEntry ReadLutAToBTagDataEntry()
        {
            //Start of tag = Index minus signature(4 bytes) and reserved (4 bytes)
            int start = Index - 8;

            var inChCount = Data[AIndex(1)];
            var outChCount = Data[AIndex(1)];
            AIndex(2);//2 bytes reserved

            var bCurveOffset = ReadUInt32();
            var matrixOffset = ReadUInt32();
            var mCurveOffset = ReadUInt32();
            var CLUTOffset = ReadUInt32();
            var aCurveOffset = ReadUInt32();

            TagDataEntry[] bCurve = null;
            TagDataEntry[] mCurve = null;
            TagDataEntry[] aCurve = null;
            CLUT clut = null;
            double[,] Matrix3x3 = null;
            double[] Matrix3x1 = null;

            if (bCurveOffset != 0)
            {
                Index = (int)bCurveOffset + start;
                bCurve = ReadCurves(outChCount);
            }
            if (mCurveOffset != 0)
            {
                Index = (int)mCurveOffset + start;
                mCurve = ReadCurves(outChCount);
            }
            if (aCurveOffset != 0)
            {
                Index = (int)aCurveOffset + start;
                aCurve = ReadCurves(inChCount);
            }

            if (CLUTOffset != 0)
            {
                Index = (int)CLUTOffset + start;
                clut = ReadCLUT(inChCount, outChCount, false);
            }

            if (matrixOffset != 0)
            {
                Index = (int)matrixOffset + start;
                Matrix3x3 = ReadMatrix(3, 3, false);
                Matrix3x1 = ReadMatrix(3, false);
            }

            return new LutAToBTagDataEntry(inChCount, outChCount, Matrix3x3, Matrix3x1, clut, bCurve, mCurve, aCurve);
        }

        public LutBToATagDataEntry ReadLutBToATagDataEntry()
        {
            //Start of tag = Index minus signature(4 bytes) and reserved (4 bytes)
            int start = Index - 8;

            var inChCount = Data[AIndex(1)];
            var outChCount = Data[AIndex(1)];
            AIndex(2);//2 bytes reserved

            uint bCurveOffset = ReadUInt32();
            uint matrixOffset = ReadUInt32();
            uint mCurveOffset = ReadUInt32();
            uint CLUTOffset = ReadUInt32();
            uint aCurveOffset = ReadUInt32();

            TagDataEntry[] bCurve = null;
            TagDataEntry[] mCurve = null;
            TagDataEntry[] aCurve = null;
            CLUT clut = null;
            double[,] Matrix3x3 = null;
            double[] Matrix3x1 = null;

            if (bCurveOffset != 0)
            {
                Index = (int)bCurveOffset + start;
                bCurve = ReadCurves(inChCount);
            }
            if (mCurveOffset != 0)
            {
                Index = (int)mCurveOffset + start;
                mCurve = ReadCurves(inChCount);
            }
            if (aCurveOffset != 0)
            {
                Index = (int)aCurveOffset + start;
                aCurve = ReadCurves(outChCount);
            }

            if (CLUTOffset != 0)
            {
                Index = (int)CLUTOffset + start;
                clut = ReadCLUT(inChCount, outChCount, false);
            }

            if (matrixOffset != 0)
            {
                Index = (int)matrixOffset + start;
                Matrix3x3 = ReadMatrix(3, 3, false);
                Matrix3x1 = ReadMatrix(3, false);
            }

            return new LutBToATagDataEntry(inChCount, outChCount, Matrix3x3, Matrix3x1, clut, bCurve, mCurve, aCurve);
        }

        public MeasurementTagDataEntry ReadMeasurementTagDataEntry()
        {
            return new MeasurementTagDataEntry((StandardObserver)ReadUInt32(), ReadXYZNumber(),
                (MeasurementGeometry)ReadUInt32(), ReadUFix16(), (StandardIlluminant)ReadUInt32());
        }

        public MultiLocalizedUnicodeTagDataEntry ReadMultiLocalizedUnicodeTagDataEntry()
        {
            //Start of tag = Index minus signature(4 bytes) and reserved (4 bytes)
            var start = Index - 8;
            var RecordCount = ReadUInt32();
            var RecordSize = ReadUInt32();
            var Text = new LocalizedString[RecordCount];

            var culture = new CultureInfo[RecordCount];
            var length = new uint[RecordCount];
            var offset = new uint[RecordCount];

            for (int i = 0; i < RecordCount; i++)
            {
                culture[i] = new CultureInfo($"{ReadASCIIString(2)}-{ReadASCIIString(2)}");
                length[i] = ReadUInt32();
                offset[i] = ReadUInt32();
            }

            for (int i = 0; i < RecordCount; i++)
            {
                Index = (int)(start + offset[i]);
                Text[i] = new LocalizedString(culture[i], ReadUnicodeString((int)length[i]));
            }

            return new MultiLocalizedUnicodeTagDataEntry(Text);
        }

        public MultiProcessElementsTagDataEntry ReadMultiProcessElementsTagDataEntry()
        {
            int start = Index - 8;

            var inChCount = ReadUInt16();
            var outChCount = ReadUInt16();
            var elementCount = ReadUInt32();

            var positionTable = new PositionNumber[elementCount];
            for (int i = 0; i < elementCount; i++) positionTable[i] = ReadPositionNumber();

            var mdata = new MultiProcessElement[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                Index = (int)positionTable[i].Offset + start;
                mdata[i] = ReadMultiProcessElement();
            }

            return new MultiProcessElementsTagDataEntry(inChCount, outChCount, mdata);
        }

        public NamedColor2TagDataEntry ReadNamedColor2TagDataEntry()
        {
            var vendorFlag = new byte[4];
            Buffer.BlockCopy(Data, AIndex(4), vendorFlag, 0, 4);
            var colorCount = ReadUInt32();
            var coordCount = (int)ReadUInt32();
            var prefix = ReadASCIIString(32);
            var suffix = ReadASCIIString(32);

            var colors = new NamedColor[colorCount];
            for (int i = 0; i < colorCount; i++) colors[i] = ReadNamedColor(coordCount);

            return new NamedColor2TagDataEntry(vendorFlag, coordCount, prefix, suffix, colors);
        }

        public ParametricCurveTagDataEntry ReadParametricCurveTagDataEntry()
        {
            return new ParametricCurveTagDataEntry(ReadParametricCurve());
        }

        public ProfileSequenceDescTagDataEntry ReadProfileSequenceDescTagDataEntry()
        {
            var count = ReadUInt32();
            var description = new ProfileDescription[count];
            for (int i = 0; i < count; i++) description[i] = ReadProfileDescription();

            return new ProfileSequenceDescTagDataEntry(description);
        }

        public ProfileSequenceIdentifierTagDataEntry ReadProfileSequenceIdentifierTagDataEntry()
        {
            var count = ReadUInt32();
            var table = new PositionNumber[count];
            for (int i = 0; i < count; i++) table[i] = ReadPositionNumber();

            var entries = new ProfileSequenceIdentifier[count];
            for (int i = 0; i < count; i++)
            {
                var id = ReadProfileID();
                ReadTagDataEntryHeader(TypeSignature.MultiLocalizedUnicode);
                var description = ReadMultiLocalizedUnicodeTagDataEntry();
                entries[i] = new ProfileSequenceIdentifier(id, description.Text);
            }

            return new ProfileSequenceIdentifierTagDataEntry(entries);
        }

        public ResponseCurveSet16TagDataEntry ReadResponseCurveSet16TagDataEntry()
        {
            //Start of tag = Index minus signature(4 bytes) and reserved (4 bytes)
            var start = Index - 8;
            var channelCount = ReadUInt16();
            var measurmentCount = ReadUInt16();

            var offset = new uint[measurmentCount];
            for (int i = 0; i < measurmentCount; i++) offset[i] = ReadUInt32();

            var curves = new ResponseCurve[measurmentCount];
            for (int i = 0; i < measurmentCount; i++)
            {
                Index = (int)(start + offset[i]);
                curves[i] = ReadResponseCurve(channelCount);
            }

            return new ResponseCurveSet16TagDataEntry(channelCount, curves);
        }

        public Fix16ArrayTagDataEntry ReadFix16ArrayTagDataEntry(uint size)
        {
            var count = (size - 8) / 4;
            var adata = new double[count];
            for (int i = 0; i < count; i++) adata[i] = ReadFix16() / 256d;

            return new Fix16ArrayTagDataEntry(adata);
        }

        public SignatureTagDataEntry ReadSignatureTagDataEntry()
        {
            return new SignatureTagDataEntry(ReadASCIIString(4));
        }

        public TextTagDataEntry ReadTextTagDataEntry(uint size)
        {
            return new TextTagDataEntry(ReadASCIIString((int)size - 8));
        }

        public UFix16ArrayTagDataEntry ReadUFix16ArrayTagDataEntry(uint size)
        {
            var count = (size - 8) / 4;
            var adata = new double[count];
            for (int i = 0; i < count; i++) adata[i] = ReadUFix16();

            return new UFix16ArrayTagDataEntry(adata);
        }

        public UInt16ArrayTagDataEntry ReadUInt16ArrayTagDataEntry(uint size)
        {
            var count = (size - 8) / 2;
            var adata = new ushort[count];
            for (int i = 0; i < count; i++) adata[i] = ReadUInt16();

            return new UInt16ArrayTagDataEntry(adata);
        }

        public UInt32ArrayTagDataEntry ReadUInt32ArrayTagDataEntry(uint size)
        {
            var count = (size - 8) / 4;
            var adata = new uint[count];
            for (int i = 0; i < count; i++) adata[i] = ReadUInt32();

            return new UInt32ArrayTagDataEntry(adata);
        }

        public UInt64ArrayTagDataEntry ReadUInt64ArrayTagDataEntry(uint size)
        {
            var count = (size - 8) / 8;
            var adata = new ulong[count];
            for (int i = 0; i < count; i++) adata[i] = ReadUInt64();

            return new UInt64ArrayTagDataEntry(adata);
        }

        public UInt8ArrayTagDataEntry ReadUInt8ArrayTagDataEntry(uint size)
        {
            var count = (int)size - 8;
            var adata = new byte[count];
            Buffer.BlockCopy(Data, AIndex(count), adata, 0, count);

            return new UInt8ArrayTagDataEntry(adata);
        }

        public ViewingConditionsTagDataEntry ReadViewingConditionsTagDataEntry(uint size)
        {
            return new ViewingConditionsTagDataEntry(ReadXYZNumber(), ReadXYZNumber(), (StandardIlluminant)ReadUInt32());
        }

        public XYZTagDataEntry ReadXYZTagDataEntry(uint size)
        {
            var count = (size - 8) / 12;
            var adata = new XYZNumber[count];
            for (int i = 0; i < count; i++) adata[i] = ReadXYZNumber();

            return new XYZTagDataEntry(adata);
        }

        #endregion

        #region Read Matrix

        public double[,] ReadMatrix(int xCount, int yCount, bool isSingle)
        {
            double[,] Matrix = new double[xCount, yCount];
            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    if (isSingle) { Matrix[x, y] = ReadSingle(); }
                    else { Matrix[x, y] = ReadFix16(); }
                }
            }
            return Matrix;
        }

        public double[] ReadMatrix(int yCount, bool isSingle)
        {
            double[] Matrix = new double[yCount];
            for (int i = 0; i < yCount; i++)
            {
                if (isSingle) { Matrix[i] = ReadSingle(); }
                else { Matrix[i] = ReadFix16(); }
            }
            return Matrix;
        }

        #endregion

        #region Read (C)LUT

        /// <summary>
        /// Reads an 8bit lookup table
        /// </summary>
        /// <returns>the LUT</returns>
        public LUT ReadLUT8()
        {
            var values = new byte[256];
            Buffer.BlockCopy(Data, AIndex(256), values, 0, 256);
            return new LUT(values);
        }

        /// <summary>
        /// Reads a 16bit lookup table
        /// </summary>
        /// <param name="count">The number of entries</param>
        /// <returns>the LUT</returns>
        public LUT ReadLUT16(int count)
        {
            var values = new ushort[count];
            for (int i = 0; i < count; i++) { values[i] = ReadUInt16(); }
            return new LUT(values);
        }

        /// <summary>
        /// Reads a CLUT depending on type
        /// </summary>
        /// <param name="inChCount">Input channel count</param>
        /// <param name="outChCount">Output channel count</param>
        /// <param name="IsFloat">If true, it's read as CLUTf32,
        /// else read as either CLUT8 or CLUT16 depending on embedded information</param>
        /// <returns>A CLUT depending on type</returns>
        public CLUT ReadCLUT(int inChCount, int outChCount, bool IsFloat)
        {
            //Grid-points are always 16 bytes long but only 0-inChCount are used
            byte[] gridPointCount = new byte[inChCount];
            Buffer.BlockCopy(Data, AIndex(16), gridPointCount, 0, inChCount);

            if (!IsFloat)
            {
                byte p = Data[AIndex(4)];//First byte is info, last 3 bytes are reserved
                if (p == 1) { return ReadCLUT8(inChCount, outChCount, gridPointCount); }
                else if (p == 2) { return ReadCLUT16(inChCount, outChCount, gridPointCount); }
                else { throw new CorruptProfileException("CLUT"); }
            }
            else { return ReadCLUTf32(inChCount, outChCount, gridPointCount); }
        }

        /// <summary>
        /// Reads an 8 bit CLUT
        /// </summary>
        /// <param name="inChCount">Input channel count</param>
        /// <param name="outChCount">Output channel count</param>
        /// <param name="gridPointCount">Grid point count for each CLUT channel</param>
        /// <returns>A CLUT8</returns>
        public CLUT ReadCLUT8(int inChCount, int outChCount, byte[] gridPointCount)
        {
            int start = Index;
            int length = 0;
            for (int i = 0; i < inChCount; i++) { length += (int)Math.Pow(gridPointCount[i], inChCount); }
            length /= inChCount;

            const double max = byte.MaxValue;
            
            var values = new double[length][];
            for (int i = 0; i < length; i++)
            {
                values[i] = new double[outChCount];
                for (int j = 0; j < outChCount; j++) { values[i][j] = Data[Index++] / max; }
            }

            Index = start + length * outChCount;
            return new CLUT(values, inChCount, outChCount, gridPointCount, CLUTDataType.UInt8);
        }

        /// <summary>
        /// Reads a 16 bit CLUT
        /// </summary>
        /// <param name="inChCount">Input channel count</param>
        /// <param name="outChCount">Output channel count</param>
        /// <param name="gridPointCount">Grid point count for each CLUT channel</param>
        /// <returns>A CLUT16</returns>
        public CLUT ReadCLUT16(int inChCount, int outChCount, byte[] gridPointCount)
        {
            int start = Index;
            int length = 0;
            for (int i = 0; i < inChCount; i++) { length += (int)Math.Pow(gridPointCount[i], inChCount); }
            length /= inChCount;

            const double max = ushort.MaxValue;

            var values = new double[length][];
            for (int i = 0; i < length; i++)
            {
                values[i] = new double[outChCount];
                for (int j = 0; j < outChCount; j++) { values[i][j] = ReadUInt16() / max; }
            }

            Index = start + length * outChCount * 2;
            return new CLUT(values, inChCount, outChCount, gridPointCount, CLUTDataType.UInt16);
        }

        /// <summary>
        /// Reads a 32bit floating point CLUT
        /// </summary>
        /// <param name="inChCount">Input channel count</param>
        /// <param name="outChCount">Output channel count</param>
        /// <param name="gridPointCount">Grid point count for each CLUT channel</param>
        /// <returns>A CLUTf32</returns>
        public CLUT ReadCLUTf32(int inChCount, int outChCount, byte[] gridPointCount)
        {
            int start = Index;
            int length = 0;
            for (int i = 0; i < inChCount; i++) { length += (int)Math.Pow(gridPointCount[i], inChCount); }
            length /= inChCount;

            var values = new double[length][];
            for (int i = 0; i < length; i++)
            {
                values[i] = new double[outChCount];
                for (int j = 0; j < outChCount; j++) { values[i][j] = ReadSingle(); }
            }

            Index = start + length * outChCount * 4;
            return new CLUT(values, inChCount, outChCount, gridPointCount, CLUTDataType.Float);
        }

        #endregion

        #region Read MultiProcessElement

        public MultiProcessElement ReadMultiProcessElement()
        {
            var sig = (MultiProcessElementSignature)ReadUInt32();
            var inChCount = ReadUInt16();
            var outChCount = ReadUInt16();

            switch (sig)
            {
                case MultiProcessElementSignature.CurveSet:
                    return ReadCurveSetProcessElement(inChCount, outChCount);
                case MultiProcessElementSignature.Matrix:
                    return ReadMatrixProcessElement(inChCount, outChCount);
                case MultiProcessElementSignature.CLUT:
                    return ReadCLUTProcessElement(inChCount, outChCount);

                //Currently just placeholder for future ICC expansion
                case MultiProcessElementSignature.bACS:
                    AIndex(8);
                    return new bACSProcessElement(inChCount, outChCount);
                case MultiProcessElementSignature.eACS:
                    AIndex(8);
                    return new eACSProcessElement(inChCount, outChCount);

                default:
                    throw new CorruptProfileException("MultiProcessElement");
            }
        }

        public CurveSetProcessElement ReadCurveSetProcessElement(int inChCount, int outChCount)
        {
            var curves = new OneDimensionalCurve[inChCount];
            for (int i = 0; i < inChCount; i++)
            {
                curves[i] = ReadOneDimensionalCurve();
                APadding();
            }
            return new CurveSetProcessElement(inChCount, outChCount, curves);
        }

        public MatrixProcessElement ReadMatrixProcessElement(int inChCount, int outChCount)
        {
            return new MatrixProcessElement(inChCount, outChCount, ReadMatrix(inChCount, outChCount, true), ReadMatrix(outChCount, true));
        }

        public CLUTProcessElement ReadCLUTProcessElement(int inChCount, int outChCount)
        {
            return new CLUTProcessElement(inChCount, outChCount, ReadCLUT(inChCount, outChCount, true));
        }

        #endregion

        #region Read Curves

        /// <summary>
        /// Reads curve data
        /// </summary>
        /// <param name="count">Input channel count</param>
        /// <returns>the curve data</returns>
        private TagDataEntry[] ReadCurves(int count)
        {
            var tdata = new TagDataEntry[count];
            for (int i = 0; i < count; i++)
            {
                TypeSignature t = ReadTagDataEntryHeader();
                if (t != TypeSignature.Curve && t != TypeSignature.ParametricCurve) { throw new CorruptProfileException("Curve has to be either of type \"Curve\" or \"ParametricCurve\" for LutAToB- and LutBToA-TagDataEntries"); }

                if (t == TypeSignature.Curve) tdata[i] = ReadCurveTagDataEntry();
                else if (t == TypeSignature.ParametricCurve) tdata[i] = ReadParametricCurveTagDataEntry();
                APadding();
            }
            return tdata;
        }

        public OneDimensionalCurve ReadOneDimensionalCurve()
        {
            var segmentCount = ReadUInt16();
            AIndex(2);//2 bytes reserved
            var breakPoints = new double[segmentCount - 1];
            for (int i = 0; i < breakPoints.Length; i++) { breakPoints[i] = ReadSingle(); }

            var segments = new CurveSegment[segmentCount];
            for (int i = 0; i < segmentCount; i++) { segments[i] = ReadCurveSegment(); }

            return new OneDimensionalCurve(breakPoints, segments);
        }

        public ResponseCurve ReadResponseCurve(int channelCount)
        {
            var type = (CurveMeasurementEncodings)ReadUInt32();
            var measurment = new uint[channelCount];
            for (int i = 0; i < channelCount; i++) measurment[i] = ReadUInt32();

            var xyzValues = new XYZNumber[channelCount];
            for (int i = 0; i < channelCount; i++) xyzValues[i] = ReadXYZNumber();

            var response = new ResponseNumber[channelCount][];
            for (int i = 0; i < channelCount; i++)
            {
                response[i] = new ResponseNumber[measurment[i]];
                for (uint j = 0; j < measurment[i]; j++)
                {
                    response[i][j] = ReadResponseNumber();
                }
            }

            return new ResponseCurve(type, xyzValues, response);
        }

        public ParametricCurve ReadParametricCurve()
        {
            ushort type = ReadUInt16();
            AIndex(2);//2 bytes reserved
            double gamma, a, b, c, d, e, f;
            gamma = a = b = c = d = e = f = 0;

            if (type >= 0 && type <= 4) gamma = ReadFix16();
            if (type > 0 && type <= 4)
            {
                a = ReadFix16();
                b = ReadFix16();
            }
            if (type > 1 && type <= 4) c = ReadFix16();
            if (type > 2 && type <= 4) d = ReadFix16();
            if (type == 4)
            {
                e = ReadFix16();
                f = ReadFix16();
            }

            return new ParametricCurve(type, gamma, a, b, c, d, e, f);
        }

        public CurveSegment ReadCurveSegment()
        {
            var signature = (CurveSegmentSignature)ReadUInt32();
            AIndex(4);//4 bytes reserved

            switch (signature)
            {
                case CurveSegmentSignature.FormulaCurve:
                    return ReadFormulaCurveElement();
                case CurveSegmentSignature.SampledCurve:
                    return ReadSampledCurveElement();
                default:
                    throw new CorruptProfileException("CurveSegment");
            }
        }

        public FormulaCurveElement ReadFormulaCurveElement()
        {
            var type = ReadUInt16();
            AIndex(2);//2 bytes reserved
            double gamma, a, b, c, d, e;
            gamma = a = b = c = d = e = 0;

            if (type == 0 || type == 1) gamma = ReadFix16();
            if (type >= 0 && type <= 2)
            {
                a = ReadSingle();
                b = ReadSingle();
                c = ReadSingle();
            }
            if (type == 1 || type == 2) d = ReadSingle();
            if (type == 2) e = ReadSingle();

            return new FormulaCurveElement(type, gamma, a, b, c, d, e);
        }

        public SampledCurveElement ReadSampledCurveElement()
        {
            var count = ReadUInt32();
            var entries = new double[count];
            for (int i = 0; i < count; i++) entries[i] = ReadSingle();

            return new SampledCurveElement(entries);
        }

        #endregion

        #region Subroutines

        /// <summary>
        /// Gives the current <see cref="Index"/> without increment and adds the given increment
        /// </summary>
        /// <param name="increment">The value to increment <see cref="Index"/></param>
        /// <returns>The current <see cref="Index"/> without the increment</returns>
        private int AIndex(int increment)
        {
            var tmp = Index;
            Index += increment;
            return tmp;
        }

        /// <summary>
        /// Calculates the 4 byte padding and adds it to the <see cref="Index"/> variable
        /// </summary>
        private void APadding()
        {
            Index += Index % 4;
        }

        /// <summary>
        /// Gets the bit value at a specified position
        /// </summary>
        /// <param name="value">The value from where the bit will be extracted</param>
        /// <param name="position">Position of the bit. Zero based index from left to right.</param>
        /// <returns>The bit value at specified position</returns>
        private bool GetBit(byte value, int position)
        {
            return ((value >> (7 - position)) & 1) == 1;
        }

        /// <summary>
        /// Gets the bit value at a specified position
        /// </summary>
        /// <param name="value">The value from where the bit will be extracted</param>
        /// <param name="position">Position of the bit. Zero based index from left to right.</param>
        /// <returns>The bit value at specified position</returns>
        private bool GetBit(ushort value, int position)
        {
            return ((value >> (15 - position)) & 1) == 1;
        }

        #endregion
    }
}
