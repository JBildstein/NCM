using System;
using System.IO;

namespace ColorManager.ICC
{
    /// <summary>
    /// Provides methods to read ICC profiles
    /// </summary>
    public sealed class ICCProfileReader
    {
        /// <summary>
        /// Reads an <see cref="ICCProfile"/> from a given byte array
        /// </summary>
        /// <param name="data">the ICC data</param>
        /// <returns>the read ICC profile</returns>
        public ICCProfile Read(byte[] data)
        {
            var reader = new ICCDataReader(data);
            return ReadAll(reader);
        }

        /// <summary>
        /// Reads an <see cref="ICCProfile"/> from a file
        /// </summary>
        /// <param name="path">the path to the ICC file</param>
        /// <returns>the read ICC profile</returns>
        public ICCProfile Read(string path)
        {
            var data = File.ReadAllBytes(path);
            return Read(data);
        }

        /// <summary>
        /// Reads an <see cref="ICCProfile"/> from a stream
        /// </summary>
        /// <param name="dataStream">stream of the ICC file</param>
        /// <returns>the read ICC profile</returns>
        public ICCProfile Read(Stream dataStream)
        {
            byte[] size = new byte[4];
            dataStream.Read(size, (int)dataStream.Position, 4);
            dataStream.Position -= 4;

            var reader = new ICCDataReader(size);
            uint profileLength = reader.ReadUInt32();

            var data = new byte[profileLength];
            dataStream.Read(data, 0, (int)profileLength);
            reader = new ICCDataReader(data);
            return ReadAll(reader);
        }


        private ICCProfile ReadAll(ICCDataReader reader)
        {
            var profile = new ICCProfile();
            ReadHeader(reader, profile);
            var table = ReadTagTable(reader);
            ReadTagData(reader, table, profile);

            var calcHash = ICCProfile.CalculateHash(reader.Data);
            if (!profile.ID.IsSet) profile.ID = calcHash;
            else if (profile.ID != calcHash) throw new CorruptProfileException("Hash stored in profile does not match");

            return profile;
        }

        private void ReadHeader(ICCDataReader reader, ICCProfile profile)
        {
            reader.Index = 0;
            profile.Size = reader.ReadUInt32();
            profile.CMMType = reader.ReadASCIIString(4);
            profile.Version = reader.ReadVersionNumber();
            profile.Class = (ProfileClassName)reader.ReadUInt32();
            profile.DataColorspaceType = (ColorSpaceType)reader.ReadUInt32();
            profile.DataColorspace = GetColorType(profile.DataColorspaceType);
            profile.PCSType = (ColorSpaceType)reader.ReadUInt32();
            profile.PCS = GetColorType(profile.PCSType);
            profile.CreationDate = reader.ReadDateTime();
            profile.FileSignature = reader.ReadASCIIString(4);
            profile.PrimaryPlatformSignature = (PrimaryPlatformType)reader.ReadUInt32();
            profile.Flags = reader.ReadProfileFlag();
            profile.DeviceManufacturer = reader.ReadUInt32();
            profile.DeviceModel = reader.ReadUInt32();
            profile.DeviceAttributes = reader.ReadDeviceAttribute();
            profile.RenderingIntent = (RenderingIntent)reader.ReadUInt32();
            profile.PCSIlluminant = reader.ReadXYZNumber();
            profile.CreatorSignature = reader.ReadASCIIString(4);
            profile.ID = reader.ReadProfileID();
        }

        private TagTableEntry[] ReadTagTable(ICCDataReader reader)
        {
            reader.Index = 128;
            var tagCount = reader.ReadUInt32();
            var table = new TagTableEntry[tagCount];

            for (int i = 0; i < tagCount; i++)
            {
                uint TagSignature = reader.ReadUInt32();
                uint TagOffset = reader.ReadUInt32();
                uint TagSize = reader.ReadUInt32();
                table[i] = new TagTableEntry((TagSignature)TagSignature, TagOffset, TagSize);
            }
            return table;
        }

        private void ReadTagData(ICCDataReader reader, TagTableEntry[] table, ICCProfile profile)
        {
            for (int i = 0; i < table.Length; i++)
            {
                TagDataEntry entry = reader.ReadTagDataEntry(table[i]);
                entry.TagSignature = table[i].Signature;
                profile.Data.Add(entry);
            }
        }

        /// <summary>
        /// Gets the type of a color from a given <see cref="ColorSpaceType"/>
        /// </summary>
        /// <param name="tp">The ColorSpaceType</param>
        /// <returns>The type of the color</returns>
        private Type GetColorType(ColorSpaceType tp)
        {
            switch (tp)
            {
                case ColorSpaceType.CIEXYZ: return typeof(ColorXYZ);
                case ColorSpaceType.CIELAB: return typeof(ColorLab);
                case ColorSpaceType.CIELUV: return typeof(ColorLuv);
                case ColorSpaceType.YCbCr: return typeof(ColorYCbCr);
                case ColorSpaceType.CIEYxy: return typeof(ColorYxy);
                case ColorSpaceType.RGB: return typeof(ColorRGB);
                case ColorSpaceType.Gray: return typeof(ColorGray);
                case ColorSpaceType.HSV: return typeof(ColorHSV);
                case ColorSpaceType.HLS: return typeof(ColorHSL);
                case ColorSpaceType.CMYK: return typeof(ColorCMYK);
                case ColorSpaceType.CMY: return typeof(ColorCMY);
                case ColorSpaceType.Color2:
                case ColorSpaceType.Color3:
                case ColorSpaceType.Color4:
                case ColorSpaceType.Color5:
                case ColorSpaceType.Color6:
                case ColorSpaceType.Color7:
                case ColorSpaceType.Color8:
                case ColorSpaceType.Color9:
                case ColorSpaceType.Color10:
                case ColorSpaceType.Color11:
                case ColorSpaceType.Color12:
                case ColorSpaceType.Color13:
                case ColorSpaceType.Color14:
                case ColorSpaceType.Color15: return typeof(ColorX);

                default:
                    throw new CorruptProfileException("Unsupported color type: " + tp.ToString());
            }
        }
    }
}
