using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ColorManager.ICC
{
    /// <summary>
    /// Represents an ICC profile
    /// </summary>
    public class ICCProfile
    {
        #region Variables

        /// <summary>
        /// Size of profile in bytes
        /// </summary>
        public uint Size { get; set; }
        /// <summary>
        /// Preferred CMM (Color Management Module) type
        /// </summary>
        public string CMMType { get; set; }
        /// <summary>
        /// Version number of profile
        /// </summary>
        public VersionNumber Version { get; set; }
        /// <summary>
        /// Type of profile
        /// </summary>
        public ProfileClassName Class { get; set; }
        /// <summary>
        /// Colorspace of data
        /// </summary>
        public Type DataColorspace { get; set; }
        /// <summary>
        /// Type name of colorspace of data
        /// </summary>
        public ColorSpaceType DataColorspaceType { get; set; }
        /// <summary>
        /// Profile Connection Space
        /// </summary>
        public Type PCS { get; set; }
        /// <summary>
        /// Type name of Profile Connection Space
        /// </summary>
        public ColorSpaceType PCSType { get; set; }
        /// <summary>
        /// Date and time this profile as first created
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Has to be "acsp"
        /// </summary>
        public string FileSignature { get; set; }
        /// <summary>
        /// Primary platform this profile as created for
        /// </summary>
        public PrimaryPlatformType PrimaryPlatformSignature { get; set; }
        /// <summary>
        /// Profile flags to indicate various options for the CMM such as distributed processing and caching options
        /// </summary>
        public ProfileFlag Flags { get; set; }
        /// <summary>
        /// Device manufacturer of the device for hich this profile is created
        /// </summary>
        public uint DeviceManufacturer { get; set; }
        /// <summary>
        /// Device model of the device for hich this profile is created
        /// </summary>
        public uint DeviceModel { get; set; }
        /// <summary>
        /// Device attributes unique to the particular device setup such as media type
        /// </summary>
        public DeviceAttribute DeviceAttributes { get; set; }
        /// <summary>
        /// Rendering Intent
        /// </summary>
        public RenderingIntent RenderingIntent { get; set; }
        /// <summary>
        /// The normalized XYZ values of the illuminant of the PCS
        /// </summary>
        public XYZNumber PCSIlluminant { get; set; }
        /// <summary>
        /// Profile creator signature
        /// </summary>
        public string CreatorSignature { get; set; }
        /// <summary>
        /// Profile ID
        /// </summary>
        public ProfileID ID { get; set; }

        /// <summary>
        /// The actual profile data
        /// </summary>
        public List<TagDataEntry> Data { get; set; }
        
        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ICCProfile"/> class
        /// </summary>
        public ICCProfile()
        {
            Data = new List<TagDataEntry>();
        }

        #region Tags

        /// <summary>
        /// Checks if this profile contains a specific tag
        /// </summary>
        /// <param name="sig">The tag signature to look for</param>
        /// <returns>True if the tag is contained, false otherwise</returns>
        public bool HasTag(TagSignature sig)
        {
            return Data.Any(t => t.TagSignature == sig);
        }

        /// <summary>
        /// Gets the <see cref="TagDataEntry"/> of given tag signature
        /// </summary>
        /// <param name="sig">the tag signature of the wanted <see cref="TagDataEntry"/></param>
        /// <returns>The <see cref="TagDataEntry"/> or null if not found</returns>
        public TagDataEntry GetTag(TagSignature sig)
        {
            return Data.FirstOrDefault(t => t.TagSignature == sig);
        }

        #endregion

        #region Get Color

        /// <summary>
        /// Creates an instance of the <see cref="DataColorspaceType"/>
        /// </summary>
        /// <param name="useICC">Use this ICC profile as colorspace.
        /// <para>This is mostly true, regarding of what is set.</para>
        /// <para>Only a few color types in combination with the right profile
        /// type can not have an ICC colorspace</para>
        /// <para>Default value is set to true.</para>
        /// </param>
        /// <returns>An instance of the <see cref="DataColorspaceType"/></returns>
        public Color GetDataColor(bool useICC = true)
        {
            return GetColor(useICC, DataColorspaceType);
        }

        /// <summary>
        /// Creates an instance of the <see cref="PCSType"/>
        /// </summary>
        /// <param name="useICC">Use this ICC profile as colorspace.
        /// <para>This is mostly true, regarding of what is set.</para>
        /// <para>Only a few color types in combination with the right profile
        /// type can not have an ICC colorspace</para>
        /// <para>Default value is set to false.</para>
        /// </param>
        /// <returns>An instance of the <see cref="PCSType"/></returns>
        public Color GetPCSColor(bool useICC = false)
        {
            //LTODO: GetPCSColor should actually only return either Lab or XYZ
            return GetColor(useICC, PCSType);
        }

        private Color GetColor(bool useICC, ColorSpaceType type)
        {
            //TODO: Whitepoint might be different for different kind of profiles (Abstract, DeviceLink)
            var wp = new WhitepointD50();

            switch (type)
            {
                case ColorSpaceType.CIEXYZ:
                    if (useICC) return new ColorXYZ(this);
                    else return new ColorXYZ(wp);
                case ColorSpaceType.CIELAB:
                    if (useICC) return new ColorLab(this);
                    else return new ColorLab(wp);
                case ColorSpaceType.CIELUV:
                    if (useICC) return new ColorLuv(this);
                    else return new ColorLuv(wp);
                case ColorSpaceType.YCbCr:
                    return new ColorYCbCr(this);
                case ColorSpaceType.CIEYxy:
                    if (useICC) return new ColorYxy(this);
                    else return new ColorYxy(wp);
                case ColorSpaceType.RGB:
                    return new ColorRGB(this);
                case ColorSpaceType.Gray:
                    return new ColorGray(this);
                case ColorSpaceType.HSV:
                    return new ColorHSV(this);
                case ColorSpaceType.HLS:
                    return new ColorHSL(this);
                case ColorSpaceType.CMYK:
                    return new ColorCMYK(this);
                case ColorSpaceType.CMY:
                    return new ColorCMY(this);
                case ColorSpaceType.Color2:
                    return new ColorX(this, 2);
                case ColorSpaceType.Color3:
                    return new ColorX(this, 3);
                case ColorSpaceType.Color4:
                    return new ColorX(this, 4);
                case ColorSpaceType.Color5:
                    return new ColorX(this, 5);
                case ColorSpaceType.Color6:
                    return new ColorX(this, 6);
                case ColorSpaceType.Color7:
                    return new ColorX(this, 7);
                case ColorSpaceType.Color8:
                    return new ColorX(this, 8);
                case ColorSpaceType.Color9:
                    return new ColorX(this, 9);
                case ColorSpaceType.Color10:
                    return new ColorX(this, 10);
                case ColorSpaceType.Color11:
                    return new ColorX(this, 11);
                case ColorSpaceType.Color12:
                    return new ColorX(this, 12);
                case ColorSpaceType.Color13:
                    return new ColorX(this, 13);
                case ColorSpaceType.Color14:
                    return new ColorX(this, 14);
                case ColorSpaceType.Color15:
                    return new ColorX(this, 15);

                default:
                    throw new ArgumentException("Invalid color type. Could not create instance.");
            }
        }

        #endregion

        #region Profile Conversion Tag

        public TagDataEntry[] GetConversionTag(bool toPCS)
        {
            var pcm = GetConversionMethod();

            switch (pcm)
            {
                case ProfileConversionMethod.D0:
                    if (toPCS) return GetTagDataEntry(TagSignature.DToB0);
                    else return GetTagDataEntry(TagSignature.BToD0);

                case ProfileConversionMethod.D1:
                    if (toPCS) return GetTagDataEntry(TagSignature.DToB1);
                    else return GetTagDataEntry(TagSignature.BToD1);

                case ProfileConversionMethod.D2:
                    if (toPCS) return GetTagDataEntry(TagSignature.DToB2);
                    else return GetTagDataEntry(TagSignature.BToD2);

                case ProfileConversionMethod.D3:
                    if (toPCS) return GetTagDataEntry(TagSignature.DToB3);
                    else return GetTagDataEntry(TagSignature.BToD3);

                case ProfileConversionMethod.A0:
                    if (toPCS) return GetTagDataEntry(TagSignature.AToB0);
                    else return GetTagDataEntry(TagSignature.BToA0);

                case ProfileConversionMethod.A1:
                    if (toPCS) return GetTagDataEntry(TagSignature.AToB1);
                    else return GetTagDataEntry(TagSignature.BToA1);

                case ProfileConversionMethod.A2:
                    if (toPCS) return GetTagDataEntry(TagSignature.AToB2);
                    else return GetTagDataEntry(TagSignature.BToA2);

                case ProfileConversionMethod.ColorTRC:
                    var data = new TagDataEntry[]
                    {
                        GetTag(TagSignature.RedTRC),
                        GetTag(TagSignature.GreenTRC),
                        GetTag(TagSignature.BlueTRC),
                        GetTag(TagSignature.RedMatrixColumn),
                        GetTag(TagSignature.GreenMatrixColumn),
                        GetTag(TagSignature.BlueMatrixColumn),
                    };
                    if (data.All(t => t != null)) return data;
                    else return null;

                case ProfileConversionMethod.GrayTRC:
                    return new TagDataEntry[] { GetTag(TagSignature.GrayTRC) };

                case ProfileConversionMethod.Invalid:
                default:
                    return null;
            }
        }

        private TagDataEntry[] GetTagDataEntry(TagSignature sig1)
        {
            TagDataEntry entry = GetTag(sig1);
            if (entry != null) return new TagDataEntry[] { entry };
            else return null;
        }

        #endregion

        #region Profile Conversion Method

        public ProfileConversionMethod GetConversionMethod()
        {
            switch (Class)
            {
                case ProfileClassName.InputDevice:
                case ProfileClassName.DisplayDevice:
                case ProfileClassName.OutputDevice:
                case ProfileClassName.ColorSpace:
                    return CheckIntent1();

                case ProfileClassName.DeviceLink:
                case ProfileClassName.Abstract:
                    return CheckIntent2();

                default:
                    return ProfileConversionMethod.Invalid;
            }
        }

        private ProfileConversionMethod CheckIntent1()
        {
            var ri = RenderingIntent;
            var ci = ProfileConversionMethod.Invalid;

            ci = CheckIntent_D();
            if (ci != ProfileConversionMethod.Invalid) return ci;

            ci = CheckIntent_A();
            if (ci != ProfileConversionMethod.Invalid) return ci;

            ci = CheckIntent_A0();
            if (ci != ProfileConversionMethod.Invalid) return ci;

            ci = CheckIntent_TRC();
            if (ci != ProfileConversionMethod.Invalid) return ci;

            return ci;
        }

        private ProfileConversionMethod CheckIntent_D()
        {
            if ((HasTag(TagSignature.DToB0) || HasTag(TagSignature.BToD0))
                && RenderingIntent == RenderingIntent.Perceptual)
                return ProfileConversionMethod.D0;

            if ((HasTag(TagSignature.DToB1) || HasTag(TagSignature.BToD1))
                && RenderingIntent == RenderingIntent.MediaRelativeColorimetric)
                return ProfileConversionMethod.D1;

            if ((HasTag(TagSignature.DToB2) || HasTag(TagSignature.BToD2))
                && RenderingIntent == RenderingIntent.Saturation)
                return ProfileConversionMethod.D2;

            if ((HasTag(TagSignature.DToB3) || HasTag(TagSignature.BToD3))
                && RenderingIntent == RenderingIntent.AbsoluteColorimetric)
                return ProfileConversionMethod.D3;

            return ProfileConversionMethod.Invalid;
        }

        private ProfileConversionMethod CheckIntent_A()
        {
            if ((HasTag(TagSignature.AToB0) || HasTag(TagSignature.BToA0))
                && RenderingIntent == RenderingIntent.Perceptual)
                return ProfileConversionMethod.A0;

            if ((HasTag(TagSignature.AToB1) || HasTag(TagSignature.BToA1))
                && RenderingIntent == RenderingIntent.MediaRelativeColorimetric)
                return ProfileConversionMethod.A1;

            if ((HasTag(TagSignature.AToB2) || HasTag(TagSignature.BToA2))
                && RenderingIntent == RenderingIntent.Saturation)
                return ProfileConversionMethod.A2;

            return ProfileConversionMethod.Invalid;
        }

        private ProfileConversionMethod CheckIntent_A0()
        {
            if (HasTag(TagSignature.AToB0) || HasTag(TagSignature.BToA0)) return ProfileConversionMethod.A0;
            else return ProfileConversionMethod.Invalid;
        }

        private ProfileConversionMethod CheckIntent_TRC()
        {
            if (HasTag(TagSignature.RedMatrixColumn)
                && HasTag(TagSignature.GreenMatrixColumn)
                && HasTag(TagSignature.BlueMatrixColumn)
                && HasTag(TagSignature.RedTRC)
                && HasTag(TagSignature.GreenTRC)
                && HasTag(TagSignature.BlueTRC))
                return ProfileConversionMethod.ColorTRC;

            if (HasTag(TagSignature.GrayTRC)) return ProfileConversionMethod.GrayTRC;

            return ProfileConversionMethod.Invalid;
        }

        private ProfileConversionMethod CheckIntent2()
        {
            if (HasTag(TagSignature.DToB0) || HasTag(TagSignature.BToD0)) return ProfileConversionMethod.D0;
            if (HasTag(TagSignature.AToB0) || HasTag(TagSignature.AToB0)) return ProfileConversionMethod.A0;

            return ProfileConversionMethod.Invalid;
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Determines whether the specified <see cref="ICCProfile"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ICCProfile"/></param>
        /// <param name="b">The second <see cref="ICCProfile"/></param>
        /// <returns>True if the <see cref="ICCProfile"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ICCProfile a, ICCProfile b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.ID == b.ID;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ICCProfile"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ICCProfile"/></param>
        /// <param name="b">The second <see cref="ICCProfile"/></param>
        /// <returns>True if the <see cref="ICCProfile"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ICCProfile a, ICCProfile b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ICCProfile"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ICCProfile"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ICCProfile"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ICCProfile c = obj as ICCProfile;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ICCProfile"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ICCProfile"/></returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Calculates the MD5 hash value of the data array
        /// </summary>
        /// <returns>The calculated hash</returns>
        public static ProfileID CalculateHash(byte[] data)
        {
            byte[] ndata = new byte[data.Length];
            Buffer.BlockCopy(data, 0, ndata, 0, data.Length);

            var md5 = new MD5CryptoServiceProvider();

            //Profile flags
            SetZero(ndata, 44, 4);
            //Rendering Intent
            SetZero(ndata, 64, 4);
            //Profile ID
            SetZero(ndata, 84, 16);

            var hash = md5.ComputeHash(ndata);

            uint p1 = ReadUInt32(hash, 0);
            uint p2 = ReadUInt32(hash, 4);
            uint p3 = ReadUInt32(hash, 8);
            uint p4 = ReadUInt32(hash, 12);

            return new ProfileID(p1, p2, p3, p4);
        }

        /// <summary>
        /// Sets a range of values of a byte array to zero
        /// </summary>
        /// <param name="start">start index</param>
        /// <param name="length">number of values to set zero</param>
        private static void SetZero(byte[] data, int start, int length)
        {
            for (int i = 0; i < length; i++) data[start + i] = 0;
        }

        /// <summary>
        /// Reads an uint from given data
        /// </summary>
        /// <param name="data">The byte array from which the value will be read</param>
        /// <param name="start">The start index from the number to read</param>
        /// <returns>the value</returns>
        private static uint ReadUInt32(byte[] data, int start)
        {
            unchecked
            {
                if (BitConverter.IsLittleEndian) return (uint)((data[start++] << 24) | (data[start++] << 16) | (data[start++] << 8) | data[start++]);
                else return (uint)(data[start++] | (data[start++] << 8) | (data[start++] << 16) | (data[start++] << 24));
            }
        }

        #endregion
    }
}
