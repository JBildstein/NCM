using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ColorManager.ICC
{
    /// <summary>
    /// ICC Profile Flag
    /// </summary>
    public struct ProfileFlag
    {
        /// <summary>
        /// True if the profile is embedded in a file; false otherwise
        /// </summary>
        public bool IsEmbedded
        {
            get { return Flags?[0] ?? false; }
            set { if (Flags != null) Flags[0] = value; }
        }
        /// <summary>
        /// True if the profile is independent; false otherwise
        /// </summary>
        public bool IsIndependent
        {
            get { return Flags?[1] ?? false; }
            set { if (Flags != null) Flags[1] = value; }
        }
        /// <summary>
        /// All profile flags
        /// </summary>
        public readonly bool[] Flags;

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileFlag"/> struct
        /// </summary>
        /// <param name="IsEmbedded">True if the profile is embedded in a file; false otherwise</param>
        /// <param name="IsIndependent">True if the profile is independent; false otherwise</param>
        public ProfileFlag(bool IsEmbedded, bool IsIndependent)
        {
            Flags = new bool[16];
            Flags[0] = IsEmbedded;
            Flags[1] = IsIndependent;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileFlag"/> struct
        /// </summary>
        /// <param name="Flags">All profile flags (length must be 16)</param>
        public ProfileFlag(bool[] Flags)
        {
            if (Flags == null) throw new ArgumentNullException(nameof(Flags));
            if (Flags.Length != 16) throw new ArgumentException("Flags must have a length of 16");

            this.Flags = Flags;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileFlag"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileFlag"/></param>
        /// <param name="b">The second <see cref="ProfileFlag"/></param>
        /// <returns>True if the <see cref="ProfileFlag"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileFlag a, ProfileFlag b)
        {
            return CMP.Compare(a.Flags, b.Flags);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileFlag"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileFlag"/></param>
        /// <param name="b">The second <see cref="ProfileFlag"/></param>
        /// <returns>True if the <see cref="ProfileFlag"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ProfileFlag a, ProfileFlag b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ProfileFlag"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ProfileFlag"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ProfileFlag"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ProfileFlag && this == (ProfileFlag)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProfileFlag"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ProfileFlag"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= CMP.GetHashCode(Flags);
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"Embedded: {IsEmbedded}; Independent: {IsIndependent}";
        }
    }

    /// <summary>
    /// Device Attributes
    /// </summary>
    public struct DeviceAttribute
    {
        /// <summary>
        /// Opacity
        /// </summary>
        public readonly OpacityAttribute Opacity;
        /// <summary>
        /// Reflectivity
        /// </summary>
        public readonly ReflectivityAttribute Reflectivity;
        /// <summary>
        /// Polarity
        /// </summary>
        public readonly PolarityAttribute Polarity;
        /// <summary>
        /// Chroma
        /// </summary>
        public readonly ChromaAttribute Chroma;
        /// <summary>
        /// Additional vendor specific data
        /// </summary>
        public readonly byte[] VendorData;

        /// <summary>
        /// Creates a new instance of the <see cref="DeviceAttribute"/> struct
        /// </summary>
        /// <param name="Opacity">Opacity</param>
        /// <param name="Reflectivity">Reflectivity</param>
        /// <param name="Polarity">Polarity</param>
        /// <param name="Chroma">Chroma</param>
        public DeviceAttribute(OpacityAttribute Opacity, ReflectivityAttribute Reflectivity,
            PolarityAttribute Polarity, ChromaAttribute Chroma)
            : this(Opacity, Reflectivity, Polarity, Chroma, new byte[4])
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="DeviceAttribute"/> struct
        /// </summary>
        /// <param name="Opacity">Opacity</param>
        /// <param name="Reflectivity">Reflectivity</param>
        /// <param name="Polarity">Polarity</param>
        /// <param name="Chroma">Chroma</param>
        /// <param name="VendorData">Additional vendor specific data</param>
        public DeviceAttribute(OpacityAttribute Opacity, ReflectivityAttribute Reflectivity,
            PolarityAttribute Polarity, ChromaAttribute Chroma, byte[] VendorData)
        {
            if (VendorData == null) throw new ArgumentNullException(nameof(VendorData));
            if (VendorData.Length != 4) throw new ArgumentException($"{nameof(VendorData)} must have a length of 4");

            if (!Enum.IsDefined(typeof(OpacityAttribute), Opacity)) throw new ArgumentException($"{nameof(Opacity)} value is not of a defined Enum value");
            if (!Enum.IsDefined(typeof(ReflectivityAttribute), Reflectivity)) throw new ArgumentException($"{nameof(Reflectivity)} value is not of a defined Enum value");
            if (!Enum.IsDefined(typeof(PolarityAttribute), Polarity)) throw new ArgumentException($"{nameof(Polarity)} value is not of a defined Enum value");
            if (!Enum.IsDefined(typeof(ChromaAttribute), Chroma)) throw new ArgumentException($"{nameof(Chroma)} value is not of a defined Enum value");

            this.Opacity = Opacity;
            this.Reflectivity = Reflectivity;
            this.Polarity = Polarity;
            this.Chroma = Chroma;
            this.VendorData = VendorData;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DeviceAttribute"/> struct
        /// </summary>
        /// <param name="Opacity">Opacity</param>
        /// <param name="Reflectivity">Reflectivity</param>
        /// <param name="Polarity">Polarity</param>
        /// <param name="Chroma">Chroma</param>
        /// <param name="VendorData">Additional vendor specific data</param>
        public DeviceAttribute(bool Opacity, bool Reflectivity, bool Polarity, bool Chroma, byte[] VendorData)
        {
            if (VendorData == null) throw new ArgumentNullException(nameof(VendorData));
            if (VendorData.Length != 4) throw new ArgumentException("VendorData must have a length of 4");

            if (Opacity) this.Opacity = OpacityAttribute.Transparency;
            else this.Opacity = OpacityAttribute.Reflective;

            if (Reflectivity) this.Reflectivity = ReflectivityAttribute.Matte;
            else this.Reflectivity = ReflectivityAttribute.Glossy;

            if (Polarity) this.Polarity = PolarityAttribute.Negative;
            else this.Polarity = PolarityAttribute.Positive;

            if (Chroma) this.Chroma = ChromaAttribute.BlackWhite;
            else this.Chroma = ChromaAttribute.Color;

            this.VendorData = VendorData;
        }
        

        /// <summary>
        /// Determines whether the specified <see cref="DeviceAttribute"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="DeviceAttribute"/></param>
        /// <param name="b">The second <see cref="DeviceAttribute"/></param>
        /// <returns>True if the <see cref="DeviceAttribute"/>s are equal; otherwise, false</returns>
        public static bool operator ==(DeviceAttribute a, DeviceAttribute b)
        {
            return a.Opacity == b.Opacity && a.Reflectivity == b.Reflectivity
                && a.Polarity == b.Polarity && a.Chroma == b.Chroma
                && CMP.Compare(a.VendorData, b.VendorData);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DeviceAttribute"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="DeviceAttribute"/></param>
        /// <param name="b">The second <see cref="DeviceAttribute"/></param>
        /// <returns>True if the <see cref="DeviceAttribute"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(DeviceAttribute a, DeviceAttribute b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="DeviceAttribute"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="DeviceAttribute"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="DeviceAttribute"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is DeviceAttribute && this == (DeviceAttribute)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="DeviceAttribute"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="DeviceAttribute"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Opacity.GetHashCode();
                hash *= 16777619 ^ Reflectivity.GetHashCode();
                hash *= 16777619 ^ Polarity.GetHashCode();
                hash *= 16777619 ^ Chroma.GetHashCode();
                hash *= CMP.GetHashCode(VendorData);
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Opacity}; {Reflectivity}; {Polarity}; {Chroma}";
        }
    }

    /// <summary>
    /// Entry of ICC tag table
    /// </summary>
    public struct TagTableEntry
    {
        /// <summary>
        /// Signature of the tag
        /// </summary>
        public readonly TagSignature Signature;
        /// <summary>
        /// Offset of entry in bytes
        /// </summary>
        public readonly uint Offset;
        /// <summary>
        /// Size of entry in bytes
        /// </summary>
        public readonly uint DataSize;

        /// <summary>
        /// Creates a new instance of the <see cref="TagTableEntry"/> struct
        /// </summary>
        /// <param name="Signature">Signature of the tag</param>
        /// <param name="Offset">Offset of entry in bytes</param>
        /// <param name="DataSize">Size of entry in bytes</param>
        public TagTableEntry(TagSignature Signature, uint Offset, uint DataSize)
        {
            this.Signature = Signature;
            this.Offset = Offset;
            this.DataSize = DataSize;
        }

        /// <summary>
        /// Determines whether the specified <see cref="TagTableEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TagTableEntry"/></param>
        /// <param name="b">The second <see cref="TagTableEntry"/></param>
        /// <returns>True if the <see cref="TagTableEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(TagTableEntry a, TagTableEntry b)
        {
            return a.Signature == b.Signature && a.Offset == b.Offset && a.DataSize == b.DataSize;
        }

        /// <summary>
        /// Determines whether the specified <see cref="TagTableEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TagTableEntry"/></param>
        /// <param name="b">The second <see cref="TagTableEntry"/></param>
        /// <returns>True if the <see cref="TagTableEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(TagTableEntry a, TagTableEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="TagTableEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="TagTableEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="TagTableEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is TagTableEntry && this == (TagTableEntry)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="TagTableEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="TagTableEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ Offset.GetHashCode();
                hash *= 16777619 ^ DataSize.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Signature} (Offset: {Offset}; Size: {DataSize})";
        }
    }

    /// <summary>
    /// Entry of ICC colorant table
    /// </summary>
    public struct ColorantTableEntry
    {
        /// <summary>
        /// Colorant name
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// First PCS value
        /// </summary>
        public readonly ushort PCS1;
        /// <summary>
        /// Second PCS value
        /// </summary>
        public readonly ushort PCS2;
        /// <summary>
        /// Third PCS value
        /// </summary>
        public readonly ushort PCS3;

        /// <summary>
        /// Creates a new instance of the <see cref="ColorantTableEntry"/> struct
        /// </summary>
        /// <param name="Name">Name of the colorant</param>
        public ColorantTableEntry(string Name)
            : this(Name, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorantTableEntry"/> struct
        /// </summary>
        /// <param name="Name">Name of the colorant</param>
        /// <param name="PCS1">First PCS value</param>
        /// <param name="PCS2">Second PCS value</param>
        /// <param name="PCS3">Third PCS value</param>
        public ColorantTableEntry(string Name, ushort PCS1, ushort PCS2, ushort PCS3)
        {
            if (Name == null) throw new ArgumentNullException(nameof(Name));

            this.Name = Name;
            this.PCS1 = PCS1;
            this.PCS2 = PCS2;
            this.PCS3 = PCS3;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ColorantTableEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ColorantTableEntry"/></param>
        /// <param name="b">The second <see cref="ColorantTableEntry"/></param>
        /// <returns>True if the <see cref="ColorantTableEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ColorantTableEntry a, ColorantTableEntry b)
        {
            return a.Name == b.Name && a.PCS1 == b.PCS1 && a.PCS2 == b.PCS2 && a.PCS3 == b.PCS3;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ColorantTableEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ColorantTableEntry"/></param>
        /// <param name="b">The second <see cref="ColorantTableEntry"/></param>
        /// <returns>True if the <see cref="ColorantTableEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ColorantTableEntry a, ColorantTableEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ColorantTableEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ColorantTableEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ColorantTableEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ColorantTableEntry && this == (ColorantTableEntry)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ColorantTableEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ColorantTableEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Name.GetHashCode();
                hash *= 16777619 ^ PCS1.GetHashCode();
                hash *= 16777619 ^ PCS2.GetHashCode();
                hash *= 16777619 ^ PCS3.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Name}: {PCS1}; {PCS2}; {PCS3}";
        }
    }

    /// <summary>
    /// A string with a specific locale
    /// </summary>
    public struct LocalizedString
    {
        /// <summary>
        /// The culture of this string.
        /// This should consist of language (ISO 639-1) and country (ISO 3166-1) codes
        /// </summary>
        public readonly string Culture;
        /// <summary>
        /// The actual text value
        /// </summary>
        public readonly string Text;
        /// <summary>
        /// The <see cref="CultureInfo"/> from the <see cref="Culture"/> property.
        /// This does not work when the <see cref="Culture"/> property is not a valid culture
        /// </summary>
        public CultureInfo Locale
        {
            get { return CultureInfo.GetCultureInfo(Culture); }
        }

        private static Regex CultureRegex = new Regex(@"[a-z]{2}-[A-Z]{2}");

        /// <summary>
        /// Creates a new instance of the <see cref="LocalizedString"/> struct
        /// The culture will be <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="Text">The text value of this string</param>
        public LocalizedString(string Text)
            : this(CultureInfo.CurrentCulture.Name, Text)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LocalizedString"/> struct
        /// The culture will be <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="Culture">The culture of this string defined by language (ISO 639-1) and country (ISO 3166-1) codes</param>
        /// <param name="Text">The text value of this string</param>
        public LocalizedString(string Culture, string Text)
        {
            if (Culture == null) throw new ArgumentNullException(nameof(Locale));
            if (Text == null) throw new ArgumentNullException(nameof(Text));            
            if (!CultureRegex.IsMatch(Culture)) throw new FormatException("Culture must have the format [a-z]{2}-[A-Z]{2} e.g.\"en-US\"");

            this.Culture = Culture;
            this.Text = Text;
        }


        /// <summary>
        /// Determines whether the specified <see cref="LocalizedString"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LocalizedString"/></param>
        /// <param name="b">The second <see cref="LocalizedString"/></param>
        /// <returns>True if the <see cref="LocalizedString"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LocalizedString a, LocalizedString b)
        {
            return a.Text == b.Text && a.Culture == b.Culture;
        }

        /// <summary>
        /// Determines whether the specified <see cref="LocalizedString"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LocalizedString"/></param>
        /// <param name="b">The second <see cref="LocalizedString"/></param>
        /// <returns>True if the <see cref="LocalizedString"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(LocalizedString a, LocalizedString b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="LocalizedString"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="LocalizedString"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is LocalizedString && this == (LocalizedString)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="LocalizedString"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="LocalizedString"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Text.GetHashCode();
                hash *= 16777619 ^ Culture.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Culture}: {Text}";
        }
    }

    /// <summary>
    /// A specific color with a name
    /// </summary>
    public struct NamedColor
    {
        /// <summary>
        /// Name of the color
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Coordinates of the color in the profiles PCS
        /// </summary>
        public readonly ushort[] PCScoordinates;
        /// <summary>
        /// Coordinates of the color in the profiles Device-Space
        /// </summary>
        public readonly ushort[] DeviceCoordinates;

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor"/> struct
        /// </summary>
        /// <param name="Name">Name of the color</param>
        /// <param name="PCScoordinates">Coordinates of the color in the profiles PCS</param>
        /// <param name="DeviceCoordinates">Coordinates of the color in the profiles Device-Space</param>
        public NamedColor(string Name, ushort[] PCScoordinates, ushort[] DeviceCoordinates)
        {
            if (Name == null) throw new ArgumentNullException(nameof(Name));
            if (PCScoordinates == null) throw new ArgumentNullException(nameof(PCScoordinates));
            if (DeviceCoordinates != null)
            {
                if (PCScoordinates.Length < 1 || PCScoordinates.Length > 15)
                    throw new ArgumentOutOfRangeException(nameof(PCScoordinates), "Allowed range: 1-15");
            }
            if (DeviceCoordinates.Length < 1 || DeviceCoordinates.Length > 15)
                throw new ArgumentOutOfRangeException(nameof(DeviceCoordinates), "Allowed range: 1-15");

            this.Name = Name;
            this.PCScoordinates = PCScoordinates;
            this.DeviceCoordinates = DeviceCoordinates;
        }


        /// <summary>
        /// Determines whether the specified <see cref="NamedColor"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="NamedColor"/></param>
        /// <param name="b">The second <see cref="NamedColor"/></param>
        /// <returns>True if the <see cref="NamedColor"/>s are equal; otherwise, false</returns>
        public static bool operator ==(NamedColor a, NamedColor b)
        {
            return a.Name == b.Name && CMP.Compare(a.PCScoordinates, b.PCScoordinates)
                && CMP.Compare(a.DeviceCoordinates, b.DeviceCoordinates);
        }

        /// <summary>
        /// Determines whether the specified <see cref="NamedColor"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="NamedColor"/></param>
        /// <param name="b">The second <see cref="NamedColor"/></param>
        /// <returns>True if the <see cref="NamedColor"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(NamedColor a, NamedColor b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="NamedColor"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="NamedColor"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="NamedColor"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is NamedColor && this == (NamedColor)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="NamedColor"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="NamedColor"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Name.GetHashCode();
                hash *= CMP.GetHashCode(PCScoordinates);
                hash *= CMP.GetHashCode(DeviceCoordinates);
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// ICC Profile description
    /// </summary>
    public struct ProfileDescription
    {
        /// <summary>
        /// Device Manufacturer
        /// </summary>
        public readonly uint DeviceManufacturer;
        /// <summary>
        /// Device Model
        /// </summary>
        public readonly uint DeviceModel;
        /// <summary>
        /// Device Attributes
        /// </summary>
        public readonly DeviceAttribute DeviceAttributes;
        /// <summary>
        /// Technology Information
        /// </summary>
        public readonly TagSignature TechnologyInformation;
        /// <summary>
        /// Device Manufacturer Info
        /// </summary>
        public readonly LocalizedString[] DeviceManufacturerInfo;
        /// <summary>
        /// Device Model Info
        /// </summary>
        public readonly LocalizedString[] DeviceModelInfo;

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileDescription"/> struct
        /// </summary>
        /// <param name="DeviceManufacturer">Device Manufacturer</param>
        /// <param name="DeviceModel">Device Model</param>
        /// <param name="DeviceAttributes">Device Attributes</param>
        /// <param name="TechnologyInformation">Technology Information</param>
        /// <param name="DeviceManufacturerInfo">Device Manufacturer Info</param>
        /// <param name="DeviceModelInfo">Device Model Info</param>
        public ProfileDescription(uint DeviceManufacturer, uint DeviceModel, DeviceAttribute DeviceAttributes,
            TagSignature TechnologyInformation, LocalizedString[] DeviceManufacturerInfo, LocalizedString[] DeviceModelInfo)
        {
            if (DeviceManufacturerInfo == null) throw new ArgumentNullException(nameof(DeviceManufacturerInfo));
            if (DeviceModelInfo == null) throw new ArgumentNullException(nameof(DeviceModelInfo));

            this.DeviceManufacturer = DeviceManufacturer;
            this.DeviceModel = DeviceModel;
            this.DeviceAttributes = DeviceAttributes;
            this.TechnologyInformation = TechnologyInformation;
            this.DeviceManufacturerInfo = DeviceManufacturerInfo;
            this.DeviceModelInfo = DeviceModelInfo;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ProfileDescription"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileDescription"/></param>
        /// <param name="b">The second <see cref="ProfileDescription"/></param>
        /// <returns>True if the <see cref="ProfileDescription"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileDescription a, ProfileDescription b)
        {
            return a.DeviceManufacturer == b.DeviceManufacturer && a.DeviceModel == b.DeviceModel &&
                a.DeviceAttributes == b.DeviceAttributes && a.TechnologyInformation == b.TechnologyInformation &&
                CMP.Compare(a.DeviceManufacturerInfo, b.DeviceManufacturerInfo) && CMP.Compare(a.DeviceModelInfo, b.DeviceModelInfo);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileDescription"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileDescription"/></param>
        /// <param name="b">The second <see cref="ProfileDescription"/></param>
        /// <returns>True if the <see cref="ProfileDescription"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ProfileDescription a, ProfileDescription b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ProfileDescription"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ProfileDescription"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ProfileDescription"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ProfileDescription && this == (ProfileDescription)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProfileDescription"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ProfileDescription"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ DeviceManufacturer.GetHashCode();
                hash *= 16777619 ^ DeviceModel.GetHashCode();
                hash *= 16777619 ^ DeviceAttributes.GetHashCode();
                hash *= 16777619 ^ TechnologyInformation.GetHashCode();
                hash *= 16777619 ^ CMP.GetHashCode(DeviceManufacturerInfo);
                hash *= 16777619 ^ CMP.GetHashCode(DeviceModelInfo);
                return hash;
            }
        }
    }

    /// <summary>
    /// Description of a profile within a sequence
    /// </summary>
    public struct ProfileSequenceIdentifier
    {
        /// <summary>
        /// ID of the profile
        /// </summary>
        public readonly ProfileID ID;
        /// <summary>
        /// Description of the profile
        /// </summary>
        public readonly LocalizedString[] Description;

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileSequenceIdentifier"/> struct
        /// </summary>
        /// <param name="ID">ID of the profile</param>
        /// <param name="Description">Description of the profile</param>
        public ProfileSequenceIdentifier(ProfileID ID, LocalizedString[] Description)
        {
            if (Description == null) throw new ArgumentNullException(nameof(Description));

            this.ID = ID;
            this.Description = Description;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ProfileSequenceIdentifier"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileSequenceIdentifier"/></param>
        /// <param name="b">The second <see cref="ProfileSequenceIdentifier"/></param>
        /// <returns>True if the <see cref="ProfileSequenceIdentifier"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileSequenceIdentifier a, ProfileSequenceIdentifier b)
        {
            return a.ID == b.ID && CMP.Compare(a.Description, b.Description);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileDescription"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileDescription"/></param>
        /// <param name="b">The second <see cref="ProfileDescription"/></param>
        /// <returns>True if the <see cref="ProfileDescription"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ProfileSequenceIdentifier a, ProfileSequenceIdentifier b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ProfileSequenceIdentifier"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ProfileSequenceIdentifier"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ProfileSequenceIdentifier"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ProfileSequenceIdentifier && this == (ProfileSequenceIdentifier)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProfileSequenceIdentifier"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ProfileSequenceIdentifier"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ ID.GetHashCode();
                hash *= 16777619 ^ CMP.GetHashCode(Description);
                return hash;
            }
        }
    }
}
