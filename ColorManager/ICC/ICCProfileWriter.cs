using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ColorManager.ICC
{
    /// <summary>
    /// Provides methods to write ICC profiles
    /// </summary>
    public sealed class ICCProfileWriter
    {
        /// <summary>
        /// Writes an <see cref="ICCProfile"/> into a byte array
        /// </summary>
        /// <param name="profile">the ICC profile to write</param>
        /// <returns>the ICC data</returns>
        public byte[] WriteProfile(ICCProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            using (var stream = new MemoryStream(128))
            {
                var writer = new ICCDataWriter(stream);

                TagTableEntry[] table = WriteTagData(writer, profile);
                WriteTagTable(writer, table);

                profile.Size = (uint)stream.Length;
                WriteHeader(writer, profile);
                profile.ID = ICCProfile.CalculateHash(stream.ToArray());
                writer.WriteProfileID(profile.ID);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Writes an <see cref="ICCProfile"/> into a file
        /// </summary>
        /// <param name="profile">the ICC profile to write</param>
        /// <param name="path">the path to the new ICC file</param>
        public void WriteProfile(ICCProfile profile, string path)
        {
            var data = WriteProfile(profile);
            File.WriteAllBytes(path, data);
        }

        /// <summary>
        /// Writes an <see cref="ICCProfile"/> into a <see cref="Stream"/>
        /// </summary>
        /// <param name="profile">the ICC profile to write</param>
        /// <param name="stream">the stream to which the ICC data will be written to</param>
        public void WriteProfile(ICCProfile profile, Stream stream)
        {
            var data = WriteProfile(profile);
            stream.Write(data, 0, data.Length);
        }
        

        private void WriteHeader(ICCDataWriter writer, ICCProfile profile)
        {
            writer.DataStream.Position = 0;

            writer.WriteUInt32(profile.Size);
            writer.WriteASCIIString(profile.CMMType, 4);
            writer.WriteVersionNumber(profile.Version);
            writer.WriteUInt32((uint)profile.Class);
            writer.WriteUInt32((uint)profile.DataColorspace);
            writer.WriteUInt32((uint)profile.PCS);
            writer.WriteDateTime(profile.CreationDate);
            writer.WriteASCIIString(profile.FileSignature, 4);
            writer.WriteUInt32((uint)profile.PrimaryPlatformSignature);
            writer.WriteProfileFlag(profile.Flags);
            writer.WriteUInt32(profile.DeviceManufacturer);
            writer.WriteUInt32(profile.DeviceModel);
            writer.WriteDeviceAttribute(profile.DeviceAttributes);
            writer.WriteUInt32((uint)profile.RenderingIntent);
            writer.WriteXYZNumber(profile.PCSIlluminant);
            writer.WriteASCIIString(profile.CreatorSignature, 4);
        }

        private void WriteTagTable(ICCDataWriter writer, TagTableEntry[] table)
        {
            //128 = size of ICC header
            writer.DataStream.Position = 128;

            writer.WriteUInt32((uint)table.Length);
            foreach (var entry in table)
            {
                writer.WriteUInt32((uint)entry.Signature);
                writer.WriteUInt32(entry.Offset);
                writer.WriteUInt32(entry.DataSize);
            }
        }

        private TagTableEntry[] WriteTagData(ICCDataWriter writer, ICCProfile profile)
        {
            List<TagDataEntry> InData = new List<TagDataEntry>(profile.Data);
            List<TagDataEntry[]> DupData = new List<TagDataEntry[]>();

            while(InData.Count > 0)
            {
                var items = InData.Where(t => InData[0].Equals(t)).ToArray();
                DupData.Add(items);
                foreach (var item in items) { InData.Remove(item); }
            }
            
            List<TagTableEntry> Table = new List<TagTableEntry>();
            //(Header size) + (entry count) + (nr of entries) * (size of table entry)
            writer.DataStream.Position = 128 + 4 + profile.Data.Count * 12; ;

            foreach (var entry in DupData)
            {
                TagTableEntry tentry;
                writer.WriteTagDataEntry(entry[0], out tentry);
                foreach (var item in entry)
                {
                    Table.Add(new TagTableEntry(item.TagSignature, tentry.Offset, tentry.DataSize));
                }
            }
            return Table.ToArray();
        }
    }
}
