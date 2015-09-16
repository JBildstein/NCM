using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ColorManager.ICC
{
    public struct ProfileFlag
    {
        public bool IsEmbedded
        {
            get { return Flags?[0] ?? false; }
            set { if (Flags != null) Flags[0] = value; }
        }
        public bool IsIndependent
        {
            get { return Flags?[1] ?? false; }
            set { if (Flags != null) Flags[1] = value; }
        }
        public readonly bool[] Flags;

        public ProfileFlag(bool IsEmbedded, bool IsIndependent)
        {
            Flags = new bool[16];
            Flags[0] = IsEmbedded;
            Flags[1] = IsIndependent;
        }

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

    public struct DeviceAttribute
    {
        public readonly OpacityAttribute Opacity;
        public readonly ReflectivityAttribute Reflectivity;
        public readonly PolarityAttribute Polarity;
        public readonly ChromaAttribute Chroma;
        public readonly byte[] VendorData;

        public DeviceAttribute(OpacityAttribute Opacity, ReflectivityAttribute Reflectivity,
            PolarityAttribute Polarity, ChromaAttribute Chroma)
            : this(Opacity, Reflectivity, Polarity, Chroma, new byte[4])
        { }

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

    public struct TagTableEntry
    {
        public readonly TagSignature Signature;
        public readonly uint Offset;
        public readonly uint DataSize;

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

    public struct ColorantTableEntry
    {
        public readonly string Name;
        public readonly ushort PCS1;
        public readonly ushort PCS2;
        public readonly ushort PCS3;

        public ColorantTableEntry(string Name)
            : this(Name, 0, 0, 0)
        { }

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

    public struct LocalizedString
    {
        public readonly string Culture;
        public readonly string Text;
        public CultureInfo Locale
        {
            get { return CultureInfo.GetCultureInfo(Culture); }
        }

        private static Regex CultureRegex = new Regex(@"[a-z]{2}-[A-Z]{2}");

        public LocalizedString(string Text)
            : this(CultureInfo.CurrentCulture.Name, Text)
        { }

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

    public struct NamedColor
    {
        public readonly string Name;
        public readonly ushort[] PCScoordinates;
        public readonly ushort[] DeviceCoordinates;


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

    public struct ProfileDescription
    {
        public readonly uint DeviceManufacturer;
        public readonly uint DeviceModel;
        public readonly DeviceAttribute DeviceAttributes;
        public readonly TagSignature TechnologyInformation;
        public readonly LocalizedString[] DeviceManufacturerInfo;
        public readonly LocalizedString[] DeviceModelInfo;

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

    public struct ProfileSequenceIdentifier
    {
        public readonly ProfileID ID;
        public readonly LocalizedString[] Description;

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
