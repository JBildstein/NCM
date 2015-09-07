using System;
using System.IO;
using System.Text;

namespace ColorManager.ICC
{
    public abstract class ICCProfileWriter
    {
        protected MemoryStream DataStream;
        
        private bool LittleEndian = BitConverter.IsLittleEndian;

        protected ICCProfileWriter()
        {
            DataStream = new MemoryStream(128);
        }


        public void WriteHeader()
        {
            DataStream.Position = 0;
            //Write header
        }


        #region Write Primitives

        /// <summary>
        /// Writes a byte
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected int WriteByte(byte value)
        {
            DataStream.WriteByte(value);
            return 1;
        }

        /// <summary>
        /// Writes an ushort
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteUInt16(ushort value)
        {
            return WriteBytes((byte*)&value, 2);
        }

        /// <summary>
        /// Writes a short
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteInt16(short value)
        {
            return WriteBytes((byte*)&value, 2);
        }

        /// <summary>
        /// Writes an uint
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteUInt32(uint value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes an int
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteInt32(int value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes an ulong
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteUInt64(ulong value)
        {
            return WriteBytes((byte*)&value, 8);
        }

        /// <summary>
        /// Writes a long
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteInt64(long value)
        {
            return WriteBytes((byte*)&value, 8);
        }

        /// <summary>
        /// Writes a float
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteSingle(float value)
        {
            return WriteBytes((byte*)&value, 4);
        }

        /// <summary>
        /// Writes a double
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected unsafe int WriteDouble(double value)
        {
            return WriteBytes((byte*)&value, 8);
        }


        /// <summary>
        /// Writes a signed 32bit number with 1 sign bit, 15 value bits and 16 fractional bits 
        /// </summary>
        /// <returns>the number of bytes written</returns>
        protected int WriteFix16(double value)
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
        protected int WriteUFix16(double value)
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
        protected int WriteU1Fix15(double value)
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
        protected int WriteUFix8(double value)
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
        /// <param name="length">number of bytes to read</param>
        /// <returns>the number of bytes written</returns>
        protected int WriteASCIIString(string value)
        {
            byte[] data = Encoding.ASCII.GetBytes(value);
            DataStream.Write(data, 0, data.Length);
            return data.Length;
        }

        /// <summary>
        /// Writes an UTF-16 big-endian encoded string
        /// </summary>
        /// <param name="length">number of bytes to read</param>
        /// <returns>the number of bytes written</returns>
        protected int WriteUnicodeString(string value)
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
                 + WriteBytes(2);
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
                 + WriteBytes(value.Data);
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
                 + WriteBytes(3)
                 + WriteBytes(value.VendorData);
        }

        /// <summary>
        /// Writes an XYZ number
        /// </summary>
        /// <returns>the number of bytes written</returns>
        private int WriteXYZnumber(XYZNumber value)
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
            return WriteASCIIString(SetRange(value.Name, 32))
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
                 + WriteMultiLocalizedUnicodeTagDataEntry(value.DeviceManufacturerInfo)
                 + WriteMultiLocalizedUnicodeTagDataEntry(value.DeviceModelInfo);
        }

        #endregion

        #region Write Tag Data Entries

        private int WriteMultiLocalizedUnicodeTagDataEntry(MultiLocalizedUnicodeTagDataEntry value)
        {
            return 0;
        }

        #endregion

        #region Helper

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

        private int WriteBytes(byte[] data)
        {
            DataStream.Write(data, 0, data.Length);
            return data.Length;
        }

        private int WriteBytes(int length)
        {
            for (int i = 0; i < length; i++)
            {
                DataStream.WriteByte(0);
            }
            return length;
        }

        private int SetRange(int value, int max, int min)
        {
            if (value > max) return max;
            else if (value < min) return min;
            else return value;
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

        #endregion
    }
}
