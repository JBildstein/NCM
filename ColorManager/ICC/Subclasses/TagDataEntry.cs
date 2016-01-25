using System;
using System.Linq;
using System.Text;

namespace ColorManager.ICC
{
    /// <summary>
    /// The data of an entry
    /// </summary>
    public abstract class TagDataEntry
    {
        /// <summary>
        /// Type Signature
        /// </summary>
        public TypeSignature Signature
        {
            get { return _Signature; }
        }
        /// <summary>
        /// Tag Signature
        /// </summary>
        public TagSignature TagSignature
        {
            get { return _TagSignature; }
            set { _TagSignature = value; }
        }

        private TypeSignature _Signature = TypeSignature.Unknown;
        private TagSignature _TagSignature = TagSignature.Unknown;

        /// <summary>
        /// Creates a new instance of the <see cref="TagDataEntry"/> class
        /// TagSignature will be <see cref="TagSignature.Unknown"/>
        /// </summary>
        /// <param name="Signature">Type Signature</param>
        protected TagDataEntry(TypeSignature Signature)
            : this(Signature, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="TagDataEntry"/> class
        /// </summary>
        /// <param name="Signature">Type Signature</param>
        /// <param name="TagSignature">Tag Signature</param>
        protected TagDataEntry(TypeSignature Signature, TagSignature TagSignature)
        {
            _Signature = Signature;
            _TagSignature = TagSignature;
        }


        /// <summary>
        /// Determines whether the specified <see cref="TagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TagDataEntry"/></param>
        /// <param name="b">The second <see cref="TagDataEntry"/></param>
        /// <returns>True if the <see cref="TagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(TagDataEntry a, TagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature;
        }

        /// <summary>
        /// Determines whether the specified <see cref="TagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TagDataEntry"/></param>
        /// <param name="b">The second <see cref="TagDataEntry"/></param>
        /// <returns>True if the <see cref="TagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(TagDataEntry a, TagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="TagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="TagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="TagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            TagDataEntry c = obj as TagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="TagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="TagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// This tag stores data of an unknown tag data entry
    /// </summary>
    public sealed class UnknownTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The raw data of the entry
        /// </summary>
        public readonly byte[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="UnknownTagDataEntry"/> class
        /// </summary>
        /// <param name="Data"></param>
        public UnknownTagDataEntry(byte[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="UnknownTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The raw data of the entry</param>
        /// <param name="TagSignature">Tag Signature</param>
        public UnknownTagDataEntry(byte[] Data, TagSignature TagSignature)
            : base(TypeSignature.Unknown, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }

        /// <summary>
        /// Determines whether the specified <see cref="UnknownTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UnknownTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UnknownTagDataEntry"/></param>
        /// <returns>True if the <see cref="UnknownTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(UnknownTagDataEntry a, UnknownTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="UnknownTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UnknownTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UnknownTagDataEntry"/></param>
        /// <returns>True if the <see cref="UnknownTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(UnknownTagDataEntry a, UnknownTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="UnknownTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="UnknownTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="UnknownTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UnknownTagDataEntry c = obj as UnknownTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="UnknownTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="UnknownTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// The chromaticity tag type provides basic chromaticity data
    /// and type of phosphors or colorants of a monitor to applications and utilities.
    /// </summary>
    public sealed class ChromaticityTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of channels
        /// </summary>
        public readonly int ChannelCount;
        /// <summary>
        /// Colorant Type
        /// </summary>
        public readonly ColorantEncoding ColorantType;
        /// <summary>
        /// Values per channel
        /// </summary>
        public readonly double[][] ChannelValues;

        /// <summary>
        /// Creates a new instance of the <see cref="ChromaticityTagDataEntry"/> class
        /// </summary>
        /// <param name="ColorantType">Colorant Type</param>
        public ChromaticityTagDataEntry(ColorantEncoding ColorantType)
            : this(ColorantType, GetColorantArray(ColorantType), TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ChromaticityTagDataEntry"/> class
        /// </summary>
        /// <param name="ChannelValues">Values per channel</param>
        public ChromaticityTagDataEntry(double[][] ChannelValues)
            : this(ColorantEncoding.Unknown, ChannelValues, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ChromaticityTagDataEntry"/> class
        /// </summary>
        /// <param name="ColorantType">Colorant Type</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ChromaticityTagDataEntry(ColorantEncoding ColorantType, TagSignature TagSignature)
            : this(ColorantType, GetColorantArray(ColorantType), TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ChromaticityTagDataEntry"/> class
        /// </summary>
        /// <param name="ChannelValues">Values per channel</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ChromaticityTagDataEntry(double[][] ChannelValues, TagSignature TagSignature)
            : this(ColorantEncoding.Unknown, ChannelValues, TagSignature)
        { }


        private ChromaticityTagDataEntry(ColorantEncoding ColorantType, double[][] ChannelValues, TagSignature TagSignature)
            : base(TypeSignature.Chromaticity, TagSignature)
        {
            if (ChannelValues == null) throw new ArgumentNullException(nameof(ChannelValues));
            if (ChannelValues.Length < 1 || ChannelValues.Length > 15)
                throw new ArgumentOutOfRangeException("Channel count must be in the range of 1-15");

            ChannelCount = ChannelValues.Length;
            this.ColorantType = ColorantType;
            this.ChannelValues = ChannelValues;
        }

        private static double[][] GetColorantArray(ColorantEncoding ColorantType)
        {
            switch (ColorantType)
            {
                case ColorantEncoding.EBU_Tech_3213_E:
                    return new double[][]
                    {
                        new double[] { 0.640, 0.330 },
                        new double[] { 0.290, 0.600 },
                        new double[] { 0.150, 0.060 },
                    };
                case ColorantEncoding.ITU_R_BT_709_2:
                    return new double[][]
                    {
                        new double[] { 0.640, 0.330 },
                        new double[] { 0.300, 0.600 },
                        new double[] { 0.150, 0.060 },
                    };
                case ColorantEncoding.P22:
                    return new double[][]
                    {
                        new double[] { 0.625, 0.340 },
                        new double[] { 0.280, 0.605 },
                        new double[] { 0.155, 0.070 },
                    };
                case ColorantEncoding.SMPTE_RP145:
                    return new double[][]
                    {
                        new double[] { 0.630, 0.340 },
                        new double[] { 0.310, 0.595 },
                        new double[] { 0.155, 0.070 },
                    };
                default:
                    throw new ArgumentException("Unrecognized colorant encoding");
            }
        }


        /// <summary>
        /// Determines whether the specified <see cref="ChromaticityTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ChromaticityTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ChromaticityTagDataEntry"/></param>
        /// <returns>True if the <see cref="ChromaticityTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ChromaticityTagDataEntry a, ChromaticityTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.ChannelCount == b.ChannelCount
                && a.ColorantType == b.ColorantType && CMP.Compare(a.ChannelValues, b.ChannelValues);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ChromaticityTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ChromaticityTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ChromaticityTagDataEntry"/></param>
        /// <returns>True if the <see cref="ChromaticityTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ChromaticityTagDataEntry a, ChromaticityTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ChromaticityTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ChromaticityTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ChromaticityTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ChromaticityTagDataEntry c = obj as ChromaticityTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ChromaticityTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ChromaticityTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ ChannelCount.GetHashCode();
                hash *= 16777619 ^ ColorantType.GetHashCode();
                hash *= CMP.GetHashCode(ChannelValues);
                return hash;
            }
        }
    }

    /// <summary>
    /// This tag specifies the laydown order in which colorants
    /// will be printed on an n-colorant device.
    /// </summary>
    public sealed class ColorantOrderTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Colorant order numbers
        /// </summary>
        public readonly byte[] ColorantNumber;

        /// <summary>
        /// Creates a new instance of the <see cref="ColorantOrderTagDataEntry"/> class
        /// </summary>
        public ColorantOrderTagDataEntry(byte[] ColorantNumber)
            : this(ColorantNumber, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorantOrderTagDataEntry"/> class
        /// </summary>
        /// <param name="ColorantNumber">Colorant order numbers</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ColorantOrderTagDataEntry(byte[] ColorantNumber, TagSignature TagSignature)
            : base(TypeSignature.ColorantOrder, TagSignature)
        {
            if (ColorantNumber == null) throw new ArgumentNullException(nameof(ColorantNumber));
            if (ColorantNumber.Length < 1 || ColorantNumber.Length > 15)
                throw new ArgumentOutOfRangeException("Channel count must be in the range of 1-15");

            this.ColorantNumber = ColorantNumber;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ColorantOrderTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ColorantOrderTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ColorantOrderTagDataEntry"/></param>
        /// <returns>True if the <see cref="ColorantOrderTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ColorantOrderTagDataEntry a, ColorantOrderTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.ColorantNumber, b.ColorantNumber);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ColorantOrderTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ColorantOrderTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ColorantOrderTagDataEntry"/></param>
        /// <returns>True if the <see cref="ColorantOrderTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ColorantOrderTagDataEntry a, ColorantOrderTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ColorantOrderTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ColorantOrderTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ColorantOrderTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ColorantOrderTagDataEntry c = obj as ColorantOrderTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ColorantOrderTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ColorantOrderTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(ColorantNumber);
                return hash;
            }
        }
    }

    /// <summary>
    /// The purpose of this tag is to identify the colorants used in
    /// the profile by a unique name and set of PCSXYZ or PCSLAB values
    /// to give the colorant an unambiguous value.
    /// </summary>
    public sealed class ColorantTableTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Colorant Data
        /// </summary>
        public readonly ColorantTableEntry[] ColorantData;

        /// <summary>
        /// Creates a new instance of the <see cref="ColorantTableTagDataEntry"/> class
        /// </summary>
        public ColorantTableTagDataEntry(ColorantTableEntry[] ColorantData)
            : this(ColorantData, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorantTableTagDataEntry"/> class
        /// </summary>
        /// <param name="ColorantData">Colorant Data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ColorantTableTagDataEntry(ColorantTableEntry[] ColorantData, TagSignature TagSignature)
            : base(TypeSignature.ColorantTable, TagSignature)
        {
            if (ColorantData == null) throw new ArgumentNullException(nameof(ColorantData));
            if (ColorantData.Length < 1 || ColorantData.Length > 15)
                throw new ArgumentOutOfRangeException("Channel count must be in the range of 1-15");

            this.ColorantData = ColorantData;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ColorantTableTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ColorantTableTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ColorantTableTagDataEntry"/></param>
        /// <returns>True if the <see cref="ColorantTableTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ColorantTableTagDataEntry a, ColorantTableTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.ColorantData, b.ColorantData);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ColorantTableTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ColorantTableTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ColorantTableTagDataEntry"/></param>
        /// <returns>True if the <see cref="ColorantTableTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ColorantTableTagDataEntry a, ColorantTableTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ColorantTableTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ColorantTableTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ColorantTableTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ColorantTableTagDataEntry c = obj as ColorantTableTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ColorantTableTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ColorantTableTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(ColorantData);
                return hash;
            }
        }
    }

    /// <summary>
    /// The type contains a one-dimensional table of double values.
    /// </summary>
    public sealed class CurveTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Curve Data
        /// </summary>
        public readonly double[] CurveData;
        /// <summary>
        /// Gamma value.
        /// Only valid if <see cref="IsGamma"/> is true
        /// </summary>
        public double Gamma
        {
            get { return CurveData[0]; }
        }
        /// <summary>
        /// True if curve maps input directly to output
        /// </summary>
        public bool IsIdentityResponse
        {
            get { return CurveData.Length == 0; }
        }
        /// <summary>
        /// True if the curve is a gamma curve
        /// </summary>
        public bool IsGamma
        {
            get { return CurveData.Length == 1; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CurveTagDataEntry"/> class
        /// </summary>
        public CurveTagDataEntry()
            : this(new double[0], TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CurveTagDataEntry"/> class
        /// </summary>
        /// <param name="Gamma">Gamma value</param>
        public CurveTagDataEntry(double Gamma)
            : this(new double[] { Gamma }, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CurveTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveData">Curve Data</param>
        public CurveTagDataEntry(double[] CurveData)
            : this(CurveData, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CurveTagDataEntry"/> class
        /// </summary>
        /// <param name="TagSignature">Tag Signature</param>
        public CurveTagDataEntry(TagSignature TagSignature)
            : this(new double[0], TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CurveTagDataEntry"/> class
        /// </summary>
        /// <param name="Gamma">Gamma value</param>
        /// <param name="TagSignature">Tag Signature</param>
        public CurveTagDataEntry(double Gamma, TagSignature TagSignature)
            : this(new double[] { Gamma }, TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CurveTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveData">Curve Data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public CurveTagDataEntry(double[] CurveData, TagSignature TagSignature)
            : base(TypeSignature.Curve, TagSignature)
        {
            if (CurveData == null) throw new ArgumentNullException(nameof(CurveData));
            this.CurveData = CurveData;
        }


        /// <summary>
        /// Determines whether the specified <see cref="CurveTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CurveTagDataEntry"/></param>
        /// <param name="b">The second <see cref="CurveTagDataEntry"/></param>
        /// <returns>True if the <see cref="CurveTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CurveTagDataEntry a, CurveTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.CurveData, b.CurveData);
        }

        /// <summary>
        /// Determines whether the specified <see cref="CurveTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CurveTagDataEntry"/></param>
        /// <param name="b">The second <see cref="CurveTagDataEntry"/></param>
        /// <returns>True if the <see cref="CurveTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CurveTagDataEntry a, CurveTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CurveTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CurveTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CurveTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CurveTagDataEntry c = obj as CurveTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CurveTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CurveTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(CurveData);
                return hash;
            }
        }
    }

    /// <summary>
    /// The dataType is a simple data structure that contains
    /// either 7-bit ASCII or binary data, i.e. textType data or transparent bytes.
    /// </summary>
    public sealed class DataTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Raw Data
        /// </summary>
        public readonly byte[] Data;
        /// <summary>
        /// True if <see cref="Data"/> represents 7bit ASCII encoded text
        /// </summary>
        public readonly bool IsASCII;
        /// <summary>
        /// The <see cref="Data"/> decoded as 7bit ASCII.
        /// Only valid if <see cref="IsASCII"/> is true
        /// </summary>
        public string ASCIIString
        {
            get
            {
                if (IsASCII) return Encoding.ASCII.GetString(Data);
                else return string.Empty;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DataTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The raw data</param>
        public DataTagDataEntry(byte[] Data)
            : this(Data, false, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="DataTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The raw data</param>
        /// <param name="IsASCII">True if the given data is 7bit ASCII encoded text</param>
        public DataTagDataEntry(byte[] Data, bool IsASCII)
            : this(Data, IsASCII, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="DataTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The raw data</param>
        /// <param name="IsASCII">True if the given data is 7bit ASCII encoded text</param>
        /// <param name="TagSignature">Tag Signature</param>
        public DataTagDataEntry(byte[] Data, bool IsASCII, TagSignature TagSignature)
            : base(TypeSignature.Data, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));

            this.Data = Data;
            this.IsASCII = IsASCII;
        }


        /// <summary>
        /// Determines whether the specified <see cref="DataTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="DataTagDataEntry"/></param>
        /// <param name="b">The second <see cref="DataTagDataEntry"/></param>
        /// <returns>True if the <see cref="DataTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(DataTagDataEntry a, DataTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.IsASCII == b.IsASCII
                && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DataTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="DataTagDataEntry"/></param>
        /// <param name="b">The second <see cref="DataTagDataEntry"/></param>
        /// <returns>True if the <see cref="DataTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(DataTagDataEntry a, DataTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="DataTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="DataTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="DataTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            DataTagDataEntry c = obj as DataTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="DataTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="DataTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ IsASCII.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type is a representation of the time and date.
    /// </summary>
    public sealed class DateTimeTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The DateTime value
        /// </summary>
        public readonly DateTime Value;

        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeTagDataEntry"/> class
        /// </summary>
        /// <param name="Value">The DateTime value</param>
        public DateTimeTagDataEntry(DateTime Value)
            : this(Value, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeTagDataEntry"/> class
        /// </summary>
        /// <param name="Value">The DateTime value</param>
        /// <param name="TagSignature">Tag Signature</param>
        public DateTimeTagDataEntry(DateTime Value, TagSignature TagSignature)
            : base(TypeSignature.DateTime, TagSignature)
        {
            this.Value = Value;
        }


        /// <summary>
        /// Determines whether the specified <see cref="DateTimeTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="DateTimeTagDataEntry"/></param>
        /// <param name="b">The second <see cref="DateTimeTagDataEntry"/></param>
        /// <returns>True if the <see cref="DateTimeTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(DateTimeTagDataEntry a, DateTimeTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.Value == b.Value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="DateTimeTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="DateTimeTagDataEntry"/></param>
        /// <param name="b">The second <see cref="DateTimeTagDataEntry"/></param>
        /// <returns>True if the <see cref="DateTimeTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(DateTimeTagDataEntry a, DateTimeTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="DateTimeTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="DateTimeTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="DateTimeTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            DateTimeTagDataEntry c = obj as DateTimeTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="DateTimeTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="DateTimeTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ Value.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// This structure represents a color transform using tables
    /// with 16-bit precision.
    /// </summary>
    public sealed class Lut16TagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of input channels
        /// </summary>
        public readonly int InputChannelCount;
        /// <summary>
        /// Number of output channels
        /// </summary>
        public readonly int OutputChannelCount;
        /// <summary>
        /// Conversion matrix
        /// </summary>
        public readonly double[,] Matrix;
        /// <summary>
        /// Input LUT
        /// </summary>
        public LUT[] InputValues;
        /// <summary>
        /// CLUT
        /// </summary>
        public readonly CLUT CLUTValues;
        /// <summary>
        /// Output LUT
        /// </summary>
        public readonly LUT[] OutputValues;

        /// <summary>
        /// Creates a new instance of the <see cref="Lut16TagDataEntry"/> class
        /// </summary>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        public Lut16TagDataEntry(LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues)
            : this(null, InputValues, CLUTValues, OutputValues, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Lut16TagDataEntry"/> class
        /// </summary>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        /// <param name="TagSignature">Tag Signature</param>
        public Lut16TagDataEntry(LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues, TagSignature TagSignature)
            : this(null, InputValues, CLUTValues, OutputValues, TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Lut16TagDataEntry"/> class
        /// </summary>
        /// <param name="Matrix">Conversion matrix (must be 3x3)</param>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        public Lut16TagDataEntry(double[,] Matrix, LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues)
            : this(Matrix, InputValues, CLUTValues, OutputValues, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Lut16TagDataEntry"/> class
        /// </summary>
        /// <param name="Matrix">Conversion matrix (must be 3x3)</param>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        /// <param name="TagSignature">Tag Signature</param>
        public Lut16TagDataEntry(double[,] Matrix, LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues, TagSignature TagSignature)
            : base(TypeSignature.Lut16, TagSignature)
        {
            if (InputValues == null) throw new ArgumentNullException(nameof(InputValues));
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (OutputValues == null) throw new ArgumentNullException(nameof(OutputValues));

            InputChannelCount = InputValues.Length;
            OutputChannelCount = OutputValues.Length;

            if (Matrix != null)
            {
                InputChannelCount = 3;
                if (Matrix.GetLength(0) != 3 || Matrix.GetLength(1) != 3)
                    throw new ArgumentOutOfRangeException(nameof(Matrix), "Matrix must have a length of three by three");
            }

            if (InputChannelCount != CLUTValues.InputChannelCount) throw new ArgumentException("Input channel count does not match");
            if (OutputChannelCount != CLUTValues.OutputChannelCount) throw new ArgumentException("Input channel count does not match");

            this.Matrix = Matrix;
            this.InputValues = InputValues;
            this.CLUTValues = CLUTValues;
            this.OutputValues = OutputValues;
        }


        /// <summary>
        /// Determines whether the specified <see cref="Lut16TagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="Lut16TagDataEntry"/></param>
        /// <param name="b">The second <see cref="Lut16TagDataEntry"/></param>
        /// <returns>True if the <see cref="Lut16TagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(Lut16TagDataEntry a, Lut16TagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.CLUTValues == b.CLUTValues
                && CMP.Compare(a.Matrix, b.Matrix) && CMP.Compare(a.InputValues, b.InputValues)
                && CMP.Compare(a.OutputValues, b.OutputValues);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Lut16TagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="Lut16TagDataEntry"/></param>
        /// <param name="b">The second <see cref="Lut16TagDataEntry"/></param>
        /// <returns>True if the <see cref="Lut16TagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(Lut16TagDataEntry a, Lut16TagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Lut16TagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Lut16TagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="Lut16TagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Lut16TagDataEntry c = obj as Lut16TagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Lut16TagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Lut16TagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ CLUTValues.GetHashCode();
                hash *= CMP.GetHashCode(Matrix);
                hash *= CMP.GetHashCode(InputValues);
                hash *= CMP.GetHashCode(OutputValues);
                return hash;
            }
        }
    }

    /// <summary>
    /// This structure represents a color transform using tables
    /// with 8-bit precision.
    /// </summary>
    public sealed class Lut8TagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of input channels
        /// </summary>
        public readonly int InputChannelCount;
        /// <summary>
        /// Number of output channels
        /// </summary>
        public readonly int OutputChannelCount;
        /// <summary>
        /// Conversion matrix
        /// </summary>
        public readonly double[,] Matrix;
        /// <summary>
        /// Input LUT
        /// </summary>
        public LUT[] InputValues;
        /// <summary>
        /// CLUT
        /// </summary>
        public readonly CLUT CLUTValues;
        /// <summary>
        /// Output LUT
        /// </summary>
        public readonly LUT[] OutputValues;

        /// <summary>
        /// Creates a new instance of the <see cref="Lut8TagDataEntry"/> class
        /// </summary>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        public Lut8TagDataEntry(LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues)
            : this(null, InputValues, CLUTValues, OutputValues, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Lut8TagDataEntry"/> class
        /// </summary>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        /// <param name="TagSignature">Tag Signature</param>
        public Lut8TagDataEntry(LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues, TagSignature TagSignature)
            : this(null, InputValues, CLUTValues, OutputValues, TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Lut8TagDataEntry"/> class
        /// </summary>
        /// <param name="Matrix">Conversion matrix (must be 3x3)</param>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        public Lut8TagDataEntry(double[,] Matrix, LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues)
            : this(Matrix, InputValues, CLUTValues, OutputValues, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Lut8TagDataEntry"/> class
        /// </summary>
        /// <param name="Matrix">Conversion matrix (must be 3x3)</param>
        /// <param name="InputValues">Input LUT</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="OutputValues">Output LUT</param>
        /// <param name="TagSignature">Tag Signature</param>
        public Lut8TagDataEntry(double[,] Matrix, LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues, TagSignature TagSignature)
            : base(TypeSignature.Lut8, TagSignature)
        {
            if (InputValues == null) throw new ArgumentNullException(nameof(InputValues));
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (OutputValues == null) throw new ArgumentNullException(nameof(OutputValues));

            InputChannelCount = InputValues.Length;
            OutputChannelCount = OutputValues.Length;

            if (Matrix != null)
            {
                InputChannelCount = 3;
                if (Matrix.GetLength(0) != 3 || Matrix.GetLength(1) != 3)
                    throw new ArgumentOutOfRangeException(nameof(Matrix), "Matrix must have a length of three by three");
            }

            if (InputChannelCount != CLUTValues.InputChannelCount) throw new ArgumentException("Input channel count does not match");
            if (OutputChannelCount != CLUTValues.OutputChannelCount) throw new ArgumentException("Input channel count does not match");

            if (InputValues.Any(t => t.Values.Length != 256)) throw new ArgumentException("Lookup table has to have a length of 256");
            if (OutputValues.Any(t => t.Values.Length != 256)) throw new ArgumentException("Lookup table has to have a length of 256");

            this.Matrix = Matrix;
            this.InputValues = InputValues;
            this.CLUTValues = CLUTValues;
            this.OutputValues = OutputValues;
        }


        /// <summary>
        /// Determines whether the specified <see cref="Lut8TagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="Lut8TagDataEntry"/></param>
        /// <param name="b">The second <see cref="Lut8TagDataEntry"/></param>
        /// <returns>True if the <see cref="Lut8TagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(Lut8TagDataEntry a, Lut8TagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.CLUTValues == b.CLUTValues
                && CMP.Compare(a.Matrix, b.Matrix) && CMP.Compare(a.InputValues, b.InputValues)
                && CMP.Compare(a.OutputValues, b.OutputValues);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Lut8TagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="Lut8TagDataEntry"/></param>
        /// <param name="b">The second <see cref="Lut8TagDataEntry"/></param>
        /// <returns>True if the <see cref="Lut8TagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(Lut8TagDataEntry a, Lut8TagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Lut8TagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Lut8TagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="Lut8TagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Lut8TagDataEntry c = obj as Lut8TagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Lut8TagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Lut8TagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ CLUTValues.GetHashCode();
                hash *= CMP.GetHashCode(Matrix);
                hash *= CMP.GetHashCode(InputValues);
                hash *= CMP.GetHashCode(OutputValues);
                return hash;
            }
        }
    }

    /// <summary>
    /// This structure represents a color transform.
    /// </summary>
    public sealed class LutAToBTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of input channels
        /// </summary>
        public readonly int InputChannelCount;
        /// <summary>
        /// Number of output channels
        /// </summary>
        public readonly int OutputChannelCount;
        /// <summary>
        /// Two dimensional conversion matrix (3x3)
        /// </summary>
        public readonly double[,] Matrix3x3;
        /// <summary>
        /// One dimensional conversion matrix (3x1)
        /// </summary>
        public readonly double[] Matrix3x1;
        /// <summary>
        /// CLUT
        /// </summary>
        public readonly CLUT CLUTValues;
        /// <summary>
        /// B Curve
        /// </summary>
        public readonly TagDataEntry[] CurveB;
        /// <summary>
        /// M Curve
        /// </summary>
        public readonly TagDataEntry[] CurveM;
        /// <summary>
        /// A Curve
        /// </summary>
        public readonly TagDataEntry[] CurveA;

        #region B

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveB)
        { }

        #endregion

        #region M, Matrix, B

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveM, double[,] Matrix3x3,
            double[] Matrix3x1, TagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveM, Matrix3x3, Matrix3x1, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveM, double[,] Matrix3x3,
            double[] Matrix3x1, TagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveM, Matrix3x3, Matrix3x1, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveM, double[,] Matrix3x3,
            double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveM, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveM, double[,] Matrix3x3,
            double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveM, double[,] Matrix3x3,
            double[] Matrix3x1, CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        #endregion

        #region A, CLUT, B

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveA, CLUT CLUTValues, TagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveA, CLUT CLUTValues, TagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveB)
        { }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues,
            ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues,
            CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues,
            ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues,
            CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveB)
        { }

        #endregion

        #region A, CLUT, M, Matrix, B

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveA, CLUT CLUTValues, TagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, TagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(TagDataEntry[] CurveA, CLUT CLUTValues, TagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, TagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }


        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(ParametricCurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, CurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutAToBTagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutAToBTagDataEntry(CurveTagDataEntry[] CurveA, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveA, CLUTValues, CurveM, Matrix3x3, Matrix3x1, CurveB)
        { }

        #endregion

        #region Base

        private LutAToBTagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB)
            : this(TagSignature, CurveB, new TagDataEntry[0], null, null, null, null)
        {
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            CurveM = null;//This was just a helper to uniquely identify the correct constructor
            InputChannelCount = OutputChannelCount = CurveB.Length;
        }

        private LutAToBTagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, TagDataEntry[] CurveB)
            : this(TagSignature, CurveB, CurveM, null, Matrix3x3, Matrix3x1, null)
        {
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            if (Matrix3x3 == null) throw new ArgumentNullException(nameof(Matrix3x3));
            if (Matrix3x1 == null) throw new ArgumentNullException(nameof(Matrix3x1));
            if (CurveM == null) throw new ArgumentNullException(nameof(CurveM));

            if (CurveB.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveB), "Curve B must have a length of three");
            if (CurveM.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveM), "Curve M must have a length of three");

            InputChannelCount = OutputChannelCount = 3;
        }

        private LutAToBTagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveA,
            CLUT CLUTValues, TagDataEntry[] CurveB)
            : this(TagSignature, CurveB, null, CurveA, null, null, CLUTValues)
        {
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            if (CurveA == null) throw new ArgumentNullException(nameof(CurveA));
            if (CurveA.Length < 1 || CurveA.Length > 15)
                throw new ArgumentOutOfRangeException("Number of A curves must be in the range of 1-15");
            if (CurveB.Length < 1 || CurveB.Length > 15)
                throw new ArgumentOutOfRangeException("Number of B curves must be in the range of 1-15");

            InputChannelCount = CurveA.Length;
            OutputChannelCount = CurveB.Length;

            if (CLUTValues.InputChannelCount != InputChannelCount) throw new ArgumentException("Input channel count does not match");
            if (CLUTValues.OutputChannelCount != OutputChannelCount) throw new ArgumentException("Output channel count does not match");
        }

        private LutAToBTagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveA, CLUT CLUTValues, TagDataEntry[] CurveM,
            double[,] Matrix3x3, double[] Matrix3x1, TagDataEntry[] CurveB)
            : this(TagSignature, CurveB, CurveM, CurveA, Matrix3x3, Matrix3x1, CLUTValues)
        {
            if (Matrix3x1 == null) throw new ArgumentNullException(nameof(Matrix3x1));
            if (Matrix3x3 == null) throw new ArgumentNullException(nameof(Matrix3x3));
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            if (CurveM == null) throw new ArgumentNullException(nameof(CurveM));
            if (CurveA == null) throw new ArgumentNullException(nameof(CurveA));
            if (CurveB.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveB), "Curve B must have a length of three");
            if (CurveM.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveM), "Curve M must have a length of three");
            if (CurveA.Length < 1 || CurveA.Length > 15)
                throw new ArgumentOutOfRangeException("Number of A curves must be in the range of 1-15");

            InputChannelCount = CurveA.Length;
            OutputChannelCount = 3;

            if (CLUTValues.InputChannelCount != InputChannelCount) throw new ArgumentException("Input channel count does not match");
            if (CLUTValues.OutputChannelCount != OutputChannelCount) throw new ArgumentException("Output channel count does not match");
        }


        private LutAToBTagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB, TagDataEntry[] CurveM,
            TagDataEntry[] CurveA, double[,] Matrix3x3, double[] Matrix3x1, CLUT CLUTValues)
            : base(TypeSignature.LutAToB, TagSignature)
        {
            if (Matrix3x1 != null && Matrix3x1.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(Matrix3x1), "Matrix must have a length of three");
            if (Matrix3x3 != null && (Matrix3x3.GetLength(0) != 3 || Matrix3x3.GetLength(1) != 3))
                throw new ArgumentOutOfRangeException(nameof(Matrix3x3), "Matrix must have a length of three by three");

            this.Matrix3x3 = Matrix3x3;
            this.Matrix3x1 = Matrix3x1;
            this.CLUTValues = CLUTValues;
            this.CurveB = CurveB;
            this.CurveM = CurveM;
            this.CurveA = CurveA;
        }

        #endregion


        /// <summary>
        /// Determines whether the specified <see cref="LutAToBTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LutAToBTagDataEntry"/></param>
        /// <param name="b">The second <see cref="LutAToBTagDataEntry"/></param>
        /// <returns>True if the <see cref="LutAToBTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LutAToBTagDataEntry a, LutAToBTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount && a.CLUTValues == b.CLUTValues
                && CMP.Compare(a.Matrix3x3, b.Matrix3x3) && CMP.Compare(a.Matrix3x1, b.Matrix3x1)
                && CMP.Compare(a.CurveB, b.CurveB) && CMP.Compare(a.CurveM, b.CurveM)
                && CMP.Compare(a.CurveA, b.CurveA);
        }

        /// <summary>
        /// Determines whether the specified <see cref="LutAToBTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LutAToBTagDataEntry"/></param>
        /// <param name="b">The second <see cref="LutAToBTagDataEntry"/></param>
        /// <returns>True if the <see cref="LutAToBTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(LutAToBTagDataEntry a, LutAToBTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="LutAToBTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="LutAToBTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="LutAToBTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            LutAToBTagDataEntry c = obj as LutAToBTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="LutAToBTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="LutAToBTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= 16777619 ^ CLUTValues.GetHashCode();
                hash *= CMP.GetHashCode(Matrix3x3);
                hash *= CMP.GetHashCode(Matrix3x1);
                hash *= CMP.GetHashCode(CurveB);
                hash *= CMP.GetHashCode(CurveM);
                hash *= CMP.GetHashCode(CurveA);
                return hash;
            }
        }
    }

    /// <summary>
    /// This structure represents a color transform.
    /// </summary>
    public sealed class LutBToATagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of input channels
        /// </summary>
        public readonly int InputChannelCount;
        /// <summary>
        /// Number of output channels
        /// </summary>
        public readonly int OutputChannelCount;
        /// <summary>
        /// Two dimensional conversion matrix (3x3)
        /// </summary>
        public readonly double[,] Matrix3x3;
        /// <summary>
        /// One dimensional conversion matrix (3x1)
        /// </summary>
        public readonly double[] Matrix3x1;
        /// <summary>
        /// CLUT
        /// </summary>
        public readonly CLUT CLUTValues;
        /// <summary>
        /// B Curve
        /// </summary>
        public readonly TagDataEntry[] CurveB;
        /// <summary>
        /// M Curve
        /// </summary>
        public readonly TagDataEntry[] CurveM;
        /// <summary>
        /// A Curve
        /// </summary>
        public readonly TagDataEntry[] CurveA;

        #region B

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveB)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB)
            : this(TagSignature.Unknown, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveB)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, TagSignature TagSignature)
            : this(TagSignature, CurveB)
        { }

        #endregion

        #region B, Matrix, M

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, double[,] Matrix3x3,
            double[] Matrix3x1, TagDataEntry[] CurveM)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, double[,] Matrix3x3,
            double[] Matrix3x1, TagDataEntry[] CurveM, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveM)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveM)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB,
            double[,] Matrix3x3, double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveM)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB,
            double[,] Matrix3x3, double[] Matrix3x1, CurveTagDataEntry[] CurveM)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }


        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3,
            double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveM, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3,
            double[] Matrix3x1, ParametricCurveTagDataEntry[] CurveM, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3,
            double[] Matrix3x1, CurveTagDataEntry[] CurveM, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM)
        { }

        #endregion

        #region B, CLUT, A

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, CLUT CLUTValues, TagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, CLUTValues, CurveA)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, CLUT CLUTValues, TagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, CLUTValues, CurveA)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) || !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) || !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }


        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, CLUT CLUTValues, CurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, CLUT CLUTValues, CurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, CLUTValues, CurveA)
        { }


        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, CLUT CLUTValues,
            ParametricCurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, CLUT CLUTValues,
            CurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, CLUT CLUTValues,
            ParametricCurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, CLUT CLUTValues,
            CurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, CLUTValues, CurveA)
        { }

        #endregion

        #region B, Matrix, M, CLUT, A

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            TagDataEntry[] CurveM, CLUT CLUTValues, TagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(TagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            TagDataEntry[] CurveM, CLUT CLUTValues, TagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        {
            if (CurveB.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveB)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveM.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveM)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
            if (CurveA.Any(t => !(t is ParametricCurveTagDataEntry) && !(t is CurveTagDataEntry)))
                throw new ArgumentException($"{nameof(CurveA)} must be of type {nameof(ParametricCurveTagDataEntry)} or {nameof(CurveTagDataEntry)}");
        }



        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA)
            : this(TagSignature.Unknown, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }


        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(ParametricCurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, ParametricCurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            CurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="LutBToATagDataEntry"/> class
        /// </summary>
        /// <param name="CurveA">A Curve</param>
        /// <param name="CLUTValues">CLUT</param>
        /// <param name="CurveM">M Curve</param>
        /// <param name="Matrix3x3">Two dimensional conversion matrix (3x3)</param>
        /// <param name="Matrix3x1">One dimensional conversion matrix (3x1)</param>
        /// <param name="CurveB">B Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public LutBToATagDataEntry(CurveTagDataEntry[] CurveB, double[,] Matrix3x3, double[] Matrix3x1,
            ParametricCurveTagDataEntry[] CurveM, CLUT CLUTValues, CurveTagDataEntry[] CurveA, TagSignature TagSignature)
            : this(TagSignature, CurveB, Matrix3x3, Matrix3x1, CurveM, CLUTValues, CurveA)
        { }

        #endregion

        #region Base

        private LutBToATagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB)
            : this(TagSignature, CurveB, new TagDataEntry[0], null, null, null, null)
        {
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            CurveM = null;//This was just a helper to uniquely identify the correct constructor
            InputChannelCount = OutputChannelCount = CurveB.Length;
        }

        private LutBToATagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB,
            double[,] Matrix3x3, double[] Matrix3x1, TagDataEntry[] CurveM)
            : this(TagSignature, CurveB, CurveM, null, Matrix3x3, Matrix3x1, null)
        {
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            if (Matrix3x3 == null) throw new ArgumentNullException(nameof(Matrix3x3));
            if (Matrix3x1 == null) throw new ArgumentNullException(nameof(Matrix3x1));
            if (CurveM == null) throw new ArgumentNullException(nameof(CurveM));

            if (CurveB.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveB), "Curve B must have a length of three");
            if (CurveM.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveM), "Curve M must have a length of three");

            InputChannelCount = OutputChannelCount = 3;
        }

        private LutBToATagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB,
            CLUT CLUTValues, TagDataEntry[] CurveA)
            : this(TagSignature, CurveB, null, CurveA, null, null, CLUTValues)
        {
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            if (CurveA == null) throw new ArgumentNullException(nameof(CurveA));
            if (CurveA.Length < 1 || CurveA.Length > 15)
                throw new ArgumentOutOfRangeException("Number of A curves must be in the range of 1-15");
            if (CurveB.Length < 1 || CurveB.Length > 15)
                throw new ArgumentOutOfRangeException("Number of B curves must be in the range of 1-15");

            InputChannelCount = CurveB.Length;
            OutputChannelCount = CurveA.Length;

            if (CLUTValues.InputChannelCount != InputChannelCount) throw new ArgumentException("Input channel count does not match");
            if (CLUTValues.OutputChannelCount != OutputChannelCount) throw new ArgumentException("Output channel count does not match");
        }

        private LutBToATagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB, double[,] Matrix3x3,
            double[] Matrix3x1, TagDataEntry[] CurveM, CLUT CLUTValues, TagDataEntry[] CurveA)
            : this(TagSignature, CurveB, CurveM, CurveA, Matrix3x3, Matrix3x1, CLUTValues)
        {
            if (Matrix3x1 == null) throw new ArgumentNullException(nameof(Matrix3x1));
            if (Matrix3x3 == null) throw new ArgumentNullException(nameof(Matrix3x3));
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (CurveB == null) throw new ArgumentNullException(nameof(CurveB));
            if (CurveM == null) throw new ArgumentNullException(nameof(CurveM));
            if (CurveA == null) throw new ArgumentNullException(nameof(CurveA));
            if (CurveB.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveB), "Curve B must have a length of three");
            if (CurveM.Length != 3) throw new ArgumentOutOfRangeException(nameof(CurveM), "Curve M must have a length of three");
            if (CurveA.Length < 1 || CurveA.Length > 15)
                throw new ArgumentOutOfRangeException("Number of A curves must be in the range of 1-15");

            InputChannelCount = 3;
            OutputChannelCount = CurveA.Length;

            if (CLUTValues.InputChannelCount != InputChannelCount) throw new ArgumentException("Input channel count does not match");
            if (CLUTValues.OutputChannelCount != OutputChannelCount) throw new ArgumentException("Output channel count does not match");
        }


        private LutBToATagDataEntry(TagSignature TagSignature, TagDataEntry[] CurveB, TagDataEntry[] CurveM,
            TagDataEntry[] CurveA, double[,] Matrix3x3, double[] Matrix3x1, CLUT CLUTValues)
            : base(TypeSignature.LutBToA, TagSignature)
        {
            if (Matrix3x1 != null && Matrix3x1.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(Matrix3x1), "Matrix must have a length of three");
            if (Matrix3x3 != null && (Matrix3x3.GetLength(0) != 3 || Matrix3x3.GetLength(1) != 3))
                throw new ArgumentOutOfRangeException(nameof(Matrix3x3), "Matrix must have a length of three by three");

            this.Matrix3x3 = Matrix3x3;
            this.Matrix3x1 = Matrix3x1;
            this.CLUTValues = CLUTValues;
            this.CurveB = CurveB;
            this.CurveM = CurveM;
            this.CurveA = CurveA;
        }

        #endregion


        /// <summary>
        /// Determines whether the specified <see cref="LutBToATagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LutBToATagDataEntry"/></param>
        /// <param name="b">The second <see cref="LutBToATagDataEntry"/></param>
        /// <returns>True if the <see cref="LutBToATagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LutBToATagDataEntry a, LutBToATagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount && a.CLUTValues == b.CLUTValues
                && CMP.Compare(a.Matrix3x3, b.Matrix3x3) && CMP.Compare(a.Matrix3x1, b.Matrix3x1)
                && CMP.Compare(a.CurveB, b.CurveB) && CMP.Compare(a.CurveM, b.CurveM)
                && CMP.Compare(a.CurveA, b.CurveA);
        }

        /// <summary>
        /// Determines whether the specified <see cref="LutBToATagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LutBToATagDataEntry"/></param>
        /// <param name="b">The second <see cref="LutBToATagDataEntry"/></param>
        /// <returns>True if the <see cref="LutBToATagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(LutBToATagDataEntry a, LutBToATagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="LutBToATagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="LutBToATagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="LutBToATagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            LutBToATagDataEntry c = obj as LutBToATagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="LutBToATagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="LutBToATagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= 16777619 ^ CLUTValues.GetHashCode();
                hash *= CMP.GetHashCode(Matrix3x3);
                hash *= CMP.GetHashCode(Matrix3x1);
                hash *= CMP.GetHashCode(CurveB);
                hash *= CMP.GetHashCode(CurveM);
                hash *= CMP.GetHashCode(CurveA);
                return hash;
            }
        }
    }

    /// <summary>
    /// The measurementType information refers only to the internal
    /// profile data and is meant to provide profile makers an alternative
    /// to the default measurement specifications.
    /// </summary>
    public sealed class MeasurementTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Observer
        /// </summary>
        public readonly StandardObserver Observer;
        /// <summary>
        /// XYZ Backing values
        /// </summary>
        public readonly XYZNumber XYZBacking;
        /// <summary>
        /// Geometry
        /// </summary>
        public readonly MeasurementGeometry Geometry;
        /// <summary>
        /// Flare
        /// </summary>
        public readonly double Flare;
        /// <summary>
        /// Illuminant
        /// </summary>
        public readonly StandardIlluminant Illuminant;

        /// <summary>
        /// Creates a new instance of the <see cref="MeasurementTagDataEntry"/> class
        /// </summary>
        /// <param name="Observer">Observer</param>
        /// <param name="XYZBacking">XYZ Backing values</param>
        /// <param name="Geometry">Geometry</param>
        /// <param name="Flare">Flare</param>
        /// <param name="Illuminant">Illuminant</param>
        public MeasurementTagDataEntry(StandardObserver Observer, XYZNumber XYZBacking,
            MeasurementGeometry Geometry, double Flare, StandardIlluminant Illuminant)
            : this(Observer, XYZBacking, Geometry, Flare, Illuminant, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="MeasurementTagDataEntry"/> class
        /// </summary>
        /// <param name="Observer">Observer</param>
        /// <param name="XYZBacking">XYZ Backing values</param>
        /// <param name="Geometry">Geometry</param>
        /// <param name="Flare">Flare</param>
        /// <param name="Illuminant">Illuminant</param>
        /// <param name="TagSignature">Tag Signature</param>
        public MeasurementTagDataEntry(StandardObserver Observer, XYZNumber XYZBacking,
            MeasurementGeometry Geometry, double Flare, StandardIlluminant Illuminant, TagSignature TagSignature)
            : base(TypeSignature.Measurement, TagSignature)
        {
            if (double.IsNaN(Flare) || double.IsInfinity(Flare)) throw new ArgumentException($"{nameof(Flare)} is not a number");
            if (!Enum.IsDefined(typeof(StandardObserver), Observer))
                throw new ArgumentException($"{nameof(Observer)} value is not of a defined Enum value");
            if (!Enum.IsDefined(typeof(MeasurementGeometry), Geometry))
                throw new ArgumentException($"{nameof(Geometry)} value is not of a defined Enum value");
            if (!Enum.IsDefined(typeof(StandardIlluminant), Illuminant))
                throw new ArgumentException($"{nameof(Illuminant)} value is not of a defined Enum value");

            this.Observer = Observer;
            this.XYZBacking = XYZBacking;
            this.Geometry = Geometry;
            this.Flare = Flare;
            this.Illuminant = Illuminant;
        }


        /// <summary>
        /// Determines whether the specified <see cref="MeasurementTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MeasurementTagDataEntry"/></param>
        /// <param name="b">The second <see cref="MeasurementTagDataEntry"/></param>
        /// <returns>True if the <see cref="MeasurementTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(MeasurementTagDataEntry a, MeasurementTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.Observer == b.Observer
                && a.XYZBacking == b.XYZBacking && a.Geometry == b.Geometry
                && a.Flare == b.Flare && a.Illuminant == b.Illuminant;
        }

        /// <summary>
        /// Determines whether the specified <see cref="MeasurementTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MeasurementTagDataEntry"/></param>
        /// <param name="b">The second <see cref="MeasurementTagDataEntry"/></param>
        /// <returns>True if the <see cref="MeasurementTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(MeasurementTagDataEntry a, MeasurementTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="MeasurementTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="MeasurementTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="MeasurementTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MeasurementTagDataEntry c = obj as MeasurementTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="MeasurementTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="MeasurementTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ Observer.GetHashCode();
                hash *= 16777619 ^ XYZBacking.GetHashCode();
                hash *= 16777619 ^ Geometry.GetHashCode();
                hash *= 16777619 ^ Flare.GetHashCode();
                hash *= 16777619 ^ Illuminant.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// This tag structure contains a set of records each referencing
    /// a multilingual string associated with a profile.
    /// </summary>
    public sealed class MultiLocalizedUnicodeTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Localized Text
        /// </summary>
        public readonly LocalizedString[] Text;

        /// <summary>
        /// Creates a new instance of the <see cref="MultiLocalizedUnicodeTagDataEntry"/> class
        /// </summary>
        /// <param name="Text">Localized Text</param>
        public MultiLocalizedUnicodeTagDataEntry(LocalizedString[] Text)
            : this(Text, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="MultiLocalizedUnicodeTagDataEntry"/> class
        /// </summary>
        /// <param name="Text">Localized Text</param>
        /// <param name="TagSignature">Tag Signature</param>
        public MultiLocalizedUnicodeTagDataEntry(LocalizedString[] Text, TagSignature TagSignature)
            : base(TypeSignature.MultiLocalizedUnicode, TagSignature)
        {
            if (Text == null) throw new ArgumentNullException(nameof(Text));
            this.Text = Text;
        }


        /// <summary>
        /// Determines whether the specified <see cref="MultiLocalizedUnicodeTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MultiLocalizedUnicodeTagDataEntry"/></param>
        /// <param name="b">The second <see cref="MultiLocalizedUnicodeTagDataEntry"/></param>
        /// <returns>True if the <see cref="MultiLocalizedUnicodeTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(MultiLocalizedUnicodeTagDataEntry a, MultiLocalizedUnicodeTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Text, b.Text);
        }

        /// <summary>
        /// Determines whether the specified <see cref="MultiLocalizedUnicodeTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MultiLocalizedUnicodeTagDataEntry"/></param>
        /// <param name="b">The second <see cref="MultiLocalizedUnicodeTagDataEntry"/></param>
        /// <returns>True if the <see cref="MultiLocalizedUnicodeTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(MultiLocalizedUnicodeTagDataEntry a, MultiLocalizedUnicodeTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="MultiLocalizedUnicodeTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="MultiLocalizedUnicodeTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="MultiLocalizedUnicodeTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MultiLocalizedUnicodeTagDataEntry c = obj as MultiLocalizedUnicodeTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="MultiLocalizedUnicodeTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="MultiLocalizedUnicodeTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Text);
                return hash;
            }
        }
    }

    /// <summary>
    /// This structure represents a color transform, containing
    /// a sequence of processing elements.
    /// </summary>
    public sealed class MultiProcessElementsTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of input channels
        /// </summary>
        public readonly int InputChannelCount;
        /// <summary>
        /// Number of output channels
        /// </summary>
        public readonly int OutputChannelCount;
        /// <summary>
        /// Processing elements
        /// </summary>
        public readonly MultiProcessElement[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="MultiProcessElementsTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">Processing elements</param>
        public MultiProcessElementsTagDataEntry(MultiProcessElement[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="MultiProcessElementsTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">Processing elements</param>
        /// <param name="TagSignature">Tag Signature</param>
        public MultiProcessElementsTagDataEntry(MultiProcessElement[] Data, TagSignature TagSignature)
            : base(TypeSignature.MultiProcessElements, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            if (Data.Length < 1) throw new ArgumentException($"{nameof(Data)} must have at least one element");

            InputChannelCount = Data[0].InputChannelCount;
            OutputChannelCount = Data[0].OutputChannelCount;

            if (Data.Any(t => t.InputChannelCount != InputChannelCount))
                throw new ArgumentException("Number of input channels do not match throughout the whole dataset");
            if (Data.Any(t => t.OutputChannelCount != OutputChannelCount))
                throw new ArgumentException("Number of output channels do not match throughout the whole dataset");

            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="MultiProcessElementsTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MultiProcessElementsTagDataEntry"/></param>
        /// <param name="b">The second <see cref="MultiProcessElementsTagDataEntry"/></param>
        /// <returns>True if the <see cref="MultiProcessElementsTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(MultiProcessElementsTagDataEntry a, MultiProcessElementsTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="MultiProcessElementsTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MultiProcessElementsTagDataEntry"/></param>
        /// <param name="b">The second <see cref="MultiProcessElementsTagDataEntry"/></param>
        /// <returns>True if the <see cref="MultiProcessElementsTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(MultiProcessElementsTagDataEntry a, MultiProcessElementsTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="MultiProcessElementsTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="MultiProcessElementsTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="MultiProcessElementsTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MultiProcessElementsTagDataEntry c = obj as MultiProcessElementsTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="MultiProcessElementsTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="MultiProcessElementsTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// The namedColor2Type is a count value and array of structures
    /// that provide color coordinates for color names.
    /// </summary>
    public sealed class NamedColor2TagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of coordinates
        /// </summary>
        public readonly int CoordCount;
        /// <summary>
        /// Prefix
        /// </summary>
        public readonly string Prefix;
        /// <summary>
        /// Suffix
        /// </summary>
        public readonly string Suffix;
        /// <summary>
        /// Vendor specific flags
        /// </summary>
        public readonly byte[] VendorFlags;
        /// <summary>
        /// The named colors
        /// </summary>
        public readonly NamedColor[] Colors;

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor2TagDataEntry"/> class
        /// </summary>
        /// <param name="CoordCount">Number of coordinates</param>
        /// <param name="Colors">The named colors</param>
        public NamedColor2TagDataEntry(int CoordCount, NamedColor[] Colors)
            : this(new byte[4], null, null, CoordCount, Colors, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor2TagDataEntry"/> class
        /// </summary>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Suffix">Suffix</param>
        /// <param name="CoordCount">Number of coordinates</param>
        /// <param name="Colors">The named colors</param>
        public NamedColor2TagDataEntry(string Prefix, string Suffix, int CoordCount, NamedColor[] Colors)
            : this(new byte[4], Prefix, Suffix, CoordCount, Colors, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor2TagDataEntry"/> class
        /// </summary>
        /// <param name="VendorFlags">Vendor specific flags</param>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Suffix">Suffix</param>
        /// <param name="CoordCount">Number of coordinates</param>
        /// <param name="Colors">The named colors</param>
        public NamedColor2TagDataEntry(byte[] VendorFlags, string Prefix, string Suffix, int CoordCount, NamedColor[] Colors)
            : this(VendorFlags, Prefix, Suffix, CoordCount, Colors, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor2TagDataEntry"/> class
        /// </summary>
        /// <param name="CoordCount">Number of coordinates</param>
        /// <param name="Colors">The named colors</param>
        /// <param name="TagSignature">Tag Signature</param>
        public NamedColor2TagDataEntry(int CoordCount, NamedColor[] Colors, TagSignature TagSignature)
            : this(new byte[4], null, null, CoordCount, Colors, TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor2TagDataEntry"/> class
        /// </summary>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Suffix">Suffix</param>
        /// <param name="CoordCount">Number of coordinates</param>
        /// <param name="Colors">The named colors</param>
        /// <param name="TagSignature">Tag Signature</param>
        public NamedColor2TagDataEntry(string Prefix, string Suffix, int CoordCount, NamedColor[] Colors, TagSignature TagSignature)
            : this(new byte[4], Prefix, Suffix, CoordCount, Colors, TagSignature)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColor2TagDataEntry"/> class
        /// </summary>
        /// <param name="VendorFlags">Vendor specific flags</param>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Suffix">Suffix</param>
        /// <param name="CoordCount">Number of coordinates</param>
        /// <param name="Colors">The named colors</param>
        /// <param name="TagSignature">Tag Signature</param>
        public NamedColor2TagDataEntry(byte[] VendorFlags, string Prefix, string Suffix, int CoordCount, NamedColor[] Colors, TagSignature TagSignature)
            : base(TypeSignature.NamedColor2, TagSignature)
        {
            if (Colors == null) throw new ArgumentNullException(nameof(Colors));
            if (VendorFlags == null) throw new ArgumentNullException(nameof(VendorFlags));
            if (VendorFlags.Length != 4) throw new ArgumentException($"{nameof(VendorFlags)} must have a length of four");
            if (Colors.Any(t => t.DeviceCoordinates?.Length != CoordCount))
                throw new ArgumentException($"Number of device coordinates of {nameof(Colors)} don't match with {nameof(CoordCount)}");

            this.VendorFlags = VendorFlags;
            this.CoordCount = CoordCount;
            this.Prefix = Prefix;
            this.Suffix = Suffix;
            this.Colors = Colors;
        }


        /// <summary>
        /// Determines whether the specified <see cref="NamedColor2TagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="NamedColor2TagDataEntry"/></param>
        /// <param name="b">The second <see cref="NamedColor2TagDataEntry"/></param>
        /// <returns>True if the <see cref="NamedColor2TagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(NamedColor2TagDataEntry a, NamedColor2TagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.CoordCount == b.CoordCount
                && a.Prefix == b.Prefix && a.Suffix == b.Suffix && CMP.Compare(a.VendorFlags, b.VendorFlags)
                && CMP.Compare(a.Colors, b.Colors);
        }

        /// <summary>
        /// Determines whether the specified <see cref="NamedColor2TagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="NamedColor2TagDataEntry"/></param>
        /// <param name="b">The second <see cref="NamedColor2TagDataEntry"/></param>
        /// <returns>True if the <see cref="NamedColor2TagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(NamedColor2TagDataEntry a, NamedColor2TagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="NamedColor2TagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="NamedColor2TagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="NamedColor2TagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            NamedColor2TagDataEntry c = obj as NamedColor2TagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="NamedColor2TagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="NamedColor2TagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ CoordCount.GetHashCode();
                hash *= 16777619 ^ Prefix.GetHashCode();
                hash *= 16777619 ^ Suffix.GetHashCode();
                hash *= CMP.GetHashCode(VendorFlags);
                hash *= CMP.GetHashCode(Colors);
                return hash;
            }
        }
    }

    /// <summary>
    /// The parametricCurveType describes a one-dimensional curve by
    /// specifying one of a predefined set of functions using the parameters.
    /// </summary>
    public sealed class ParametricCurveTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The Curve
        /// </summary>
        public readonly ParametricCurve Curve;

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurveTagDataEntry"/> class
        /// </summary>
        /// <param name="Curve">The Curve</param>
        public ParametricCurveTagDataEntry(ParametricCurve Curve)
            : this(Curve, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurveTagDataEntry"/> class
        /// </summary>
        /// <param name="Curve">The Curve</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ParametricCurveTagDataEntry(ParametricCurve Curve, TagSignature TagSignature)
            : base(TypeSignature.ParametricCurve, TagSignature)
        {
            this.Curve = Curve;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ParametricCurveTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ParametricCurveTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ParametricCurveTagDataEntry"/></param>
        /// <returns>True if the <see cref="ParametricCurveTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ParametricCurveTagDataEntry a, ParametricCurveTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.Curve == b.Curve;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ParametricCurveTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ParametricCurveTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ParametricCurveTagDataEntry"/></param>
        /// <returns>True if the <see cref="ParametricCurveTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ParametricCurveTagDataEntry a, ParametricCurveTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ParametricCurveTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ParametricCurveTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ParametricCurveTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ParametricCurveTagDataEntry c = obj as ParametricCurveTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ParametricCurveTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ParametricCurveTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ Curve.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// This type is an array of structures, each of which contains information
    /// from the header fields and tags from the original profiles which were
    /// combined to create the final profile.
    /// </summary>
    public sealed class ProfileSequenceDescTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Profile Descriptions
        /// </summary>
        public readonly ProfileDescription[] Descriptions;

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileSequenceDescTagDataEntry"/> class
        /// </summary>
        /// <param name="Descriptions">Profile Descriptions</param>
        public ProfileSequenceDescTagDataEntry(ProfileDescription[] Descriptions)
            : this(Descriptions, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileSequenceDescTagDataEntry"/> class
        /// </summary>
        /// <param name="Descriptions">Profile Descriptions</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ProfileSequenceDescTagDataEntry(ProfileDescription[] Descriptions, TagSignature TagSignature)
            : base(TypeSignature.ProfileSequenceDesc, TagSignature)
        {
            if (Descriptions == null) throw new ArgumentNullException(nameof(Descriptions));
            this.Descriptions = Descriptions;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ProfileSequenceDescTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileSequenceDescTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ProfileSequenceDescTagDataEntry"/></param>
        /// <returns>True if the <see cref="ProfileSequenceDescTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileSequenceDescTagDataEntry a, ProfileSequenceDescTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Descriptions, b.Descriptions);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileSequenceDescTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileSequenceDescTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ProfileSequenceDescTagDataEntry"/></param>
        /// <returns>True if the <see cref="ProfileSequenceDescTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ProfileSequenceDescTagDataEntry a, ProfileSequenceDescTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ProfileSequenceDescTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ProfileSequenceDescTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ProfileSequenceDescTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ProfileSequenceDescTagDataEntry c = obj as ProfileSequenceDescTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProfileSequenceDescTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ProfileSequenceDescTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Descriptions);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type is an array of structures, each of which contains information
    /// for identification of a profile used in a sequence.
    /// </summary>
    public sealed class ProfileSequenceIdentifierTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Profile Identifiers
        /// </summary>
        public readonly ProfileSequenceIdentifier[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileSequenceIdentifierTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">Profile Identifiers</param>
        public ProfileSequenceIdentifierTagDataEntry(ProfileSequenceIdentifier[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileSequenceIdentifierTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">Profile Identifiers</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ProfileSequenceIdentifierTagDataEntry(ProfileSequenceIdentifier[] Data, TagSignature TagSignature)
            : base(TypeSignature.ProfileSequenceIdentifier, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ProfileSequenceIdentifierTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileSequenceIdentifierTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ProfileSequenceIdentifierTagDataEntry"/></param>
        /// <returns>True if the <see cref="ProfileSequenceIdentifierTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileSequenceIdentifierTagDataEntry a, ProfileSequenceIdentifierTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileSequenceIdentifierTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileSequenceIdentifierTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ProfileSequenceIdentifierTagDataEntry"/></param>
        /// <returns>True if the <see cref="ProfileSequenceIdentifierTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ProfileSequenceIdentifierTagDataEntry a, ProfileSequenceIdentifierTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ProfileSequenceIdentifierTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ProfileSequenceIdentifierTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ProfileSequenceIdentifierTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ProfileSequenceIdentifierTagDataEntry c = obj as ProfileSequenceIdentifierTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProfileSequenceIdentifierTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ProfileSequenceIdentifierTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// The purpose of this tag type is to provide a mechanism to relate physical
    /// colorant amounts with the normalized device codes produced by lut8Type, lut16Type,
    /// lutAToBType, lutBToAType or multiProcessElementsType tags so that corrections can
    /// be made for variation in the device without having to produce a new profile.
    /// </summary>
    public sealed class ResponseCurveSet16TagDataEntry : TagDataEntry
    {
        /// <summary>
        /// Number of channels
        /// </summary>
        public readonly ushort ChannelCount;
        /// <summary>
        /// The Curves
        /// </summary>
        public readonly ResponseCurve[] Curves;

        /// <summary>
        /// Creates a new instance of the <see cref="ResponseCurveSet16TagDataEntry"/> class
        /// </summary>
        /// <param name="Curves">The Curves</param>
        public ResponseCurveSet16TagDataEntry(ResponseCurve[] Curves)
            : this(Curves, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ResponseCurveSet16TagDataEntry"/> class
        /// </summary>
        /// <param name="Curves">The Curves</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ResponseCurveSet16TagDataEntry(ResponseCurve[] Curves, TagSignature TagSignature)
            : base(TypeSignature.ResponseCurveSet16, TagSignature)
        {
            if (Curves == null) throw new ArgumentNullException(nameof(Curves));
            if (Curves.Length < 1) throw new ArgumentException($"{nameof(Curves)} needs at least one element");

            this.Curves = Curves;
            ChannelCount = (ushort)Curves[0].ResponseArrays.Length;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ResponseCurveSet16TagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseCurveSet16TagDataEntry"/></param>
        /// <param name="b">The second <see cref="ResponseCurveSet16TagDataEntry"/></param>
        /// <returns>True if the <see cref="ResponseCurveSet16TagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ResponseCurveSet16TagDataEntry a, ResponseCurveSet16TagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.ChannelCount == b.ChannelCount
                && CMP.Compare(a.Curves, b.Curves);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ResponseCurveSet16TagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseCurveSet16TagDataEntry"/></param>
        /// <param name="b">The second <see cref="ResponseCurveSet16TagDataEntry"/></param>
        /// <returns>True if the <see cref="ResponseCurveSet16TagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ResponseCurveSet16TagDataEntry a, ResponseCurveSet16TagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ResponseCurveSet16TagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ResponseCurveSet16TagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ResponseCurveSet16TagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ResponseCurveSet16TagDataEntry c = obj as ResponseCurveSet16TagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ResponseCurveSet16TagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ResponseCurveSet16TagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ ChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(Curves);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents an array of doubles (from 32bit fixed point values).
    /// </summary>
    public sealed class Fix16ArrayTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The array data
        /// </summary>
        public readonly double[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="Fix16ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        public Fix16ArrayTagDataEntry(double[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="Fix16ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public Fix16ArrayTagDataEntry(double[] Data, TagSignature TagSignature)
            : base(TypeSignature.S15Fixed16Array, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="Fix16ArrayTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="Fix16ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="Fix16ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="Fix16ArrayTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(Fix16ArrayTagDataEntry a, Fix16ArrayTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Fix16ArrayTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="Fix16ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="Fix16ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="Fix16ArrayTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(Fix16ArrayTagDataEntry a, Fix16ArrayTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Fix16ArrayTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Fix16ArrayTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="Fix16ArrayTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Fix16ArrayTagDataEntry c = obj as Fix16ArrayTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Fix16ArrayTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Fix16ArrayTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// Typically this type is used for registered tags that can
    /// be displayed on many development systems as a sequence of four characters.
    /// </summary>
    public sealed class SignatureTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The Signature
        /// </summary>
        public readonly string SignatureData;

        /// <summary>
        /// Creates a new instance of the <see cref="SignatureTagDataEntry"/> class
        /// </summary>
        /// <param name="SignatureData">The Signature</param>
        public SignatureTagDataEntry(string SignatureData)
            : this(SignatureData, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="SignatureTagDataEntry"/> class
        /// </summary>
        /// <param name="SignatureData">The Signature</param>
        /// <param name="TagSignature">Tag Signature</param>
        public SignatureTagDataEntry(string SignatureData, TagSignature TagSignature)
            : base(TypeSignature.Signature, TagSignature)
        {
            if (SignatureData == null) throw new ArgumentNullException(nameof(SignatureData));
            this.SignatureData = SignatureData;
        }


        /// <summary>
        /// Determines whether the specified <see cref="SignatureTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="SignatureTagDataEntry"/></param>
        /// <param name="b">The second <see cref="SignatureTagDataEntry"/></param>
        /// <returns>True if the <see cref="SignatureTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(SignatureTagDataEntry a, SignatureTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.SignatureData == b.SignatureData;
        }

        /// <summary>
        /// Determines whether the specified <see cref="SignatureTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="SignatureTagDataEntry"/></param>
        /// <param name="b">The second <see cref="SignatureTagDataEntry"/></param>
        /// <returns>True if the <see cref="SignatureTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(SignatureTagDataEntry a, SignatureTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="SignatureTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="SignatureTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="SignatureTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            SignatureTagDataEntry c = obj as SignatureTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="SignatureTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="SignatureTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ SignatureData.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// This is a simple text structure that contains a text string.
    /// </summary>
    public sealed class TextTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The Text
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Creates a new instance of the <see cref="TextTagDataEntry"/> class
        /// </summary>
        /// <param name="Text">The Text</param>
        public TextTagDataEntry(string Text)
            : this(Text, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="TextTagDataEntry"/> class
        /// </summary>
        /// <param name="Text">The Text</param>
        /// <param name="TagSignature">Tag Signature</param>
        public TextTagDataEntry(string Text, TagSignature TagSignature)
            : base(TypeSignature.Text, TagSignature)
        {
            if (Text == null) throw new ArgumentNullException(nameof(Text));
            this.Text = Text;
        }


        /// <summary>
        /// Determines whether the specified <see cref="TextTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TextTagDataEntry"/></param>
        /// <param name="b">The second <see cref="TextTagDataEntry"/></param>
        /// <returns>True if the <see cref="TextTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(TextTagDataEntry a, TextTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.Text == b.Text;
        }

        /// <summary>
        /// Determines whether the specified <see cref="TextTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TextTagDataEntry"/></param>
        /// <param name="b">The second <see cref="TextTagDataEntry"/></param>
        /// <returns>True if the <see cref="TextTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(TextTagDataEntry a, TextTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="TextTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="TextTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="TextTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            TextTagDataEntry c = obj as TextTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="TextTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="TextTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ Text.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents an array of doubles (from 32bit values).
    /// </summary>
    public sealed class UFix16ArrayTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The array data
        /// </summary>
        public readonly double[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="UFix16ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        public UFix16ArrayTagDataEntry(double[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="UFix16ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public UFix16ArrayTagDataEntry(double[] Data, TagSignature TagSignature)
            : base(TypeSignature.U16Fixed16Array, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="UFix16ArrayTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UFix16ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UFix16ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UFix16ArrayTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(UFix16ArrayTagDataEntry a, UFix16ArrayTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="UFix16ArrayTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UFix16ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UFix16ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UFix16ArrayTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(UFix16ArrayTagDataEntry a, UFix16ArrayTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="UFix16ArrayTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="UFix16ArrayTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="UFix16ArrayTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UFix16ArrayTagDataEntry c = obj as UFix16ArrayTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="UFix16ArrayTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="UFix16ArrayTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents an array of unsigned shorts.
    /// </summary>
    public sealed class UInt16ArrayTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The array data
        /// </summary>
        public readonly ushort[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="UInt16ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        public UInt16ArrayTagDataEntry(ushort[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="UInt16ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public UInt16ArrayTagDataEntry(ushort[] Data, TagSignature TagSignature)
            : base(TypeSignature.UInt16Array, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="UInt16ArrayTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt16ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt16ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt16ArrayTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(UInt16ArrayTagDataEntry a, UInt16ArrayTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="UInt16ArrayTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt16ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt16ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt16ArrayTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(UInt16ArrayTagDataEntry a, UInt16ArrayTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="UInt16ArrayTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="UInt16ArrayTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="UInt16ArrayTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UInt16ArrayTagDataEntry c = obj as UInt16ArrayTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="UInt16ArrayTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="UInt16ArrayTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents an array of unsigned 32bit integers.
    /// </summary>
    public sealed class UInt32ArrayTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The array data
        /// </summary>
        public readonly uint[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="UInt32ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        public UInt32ArrayTagDataEntry(uint[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="UInt32ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public UInt32ArrayTagDataEntry(uint[] Data, TagSignature TagSignature)
            : base(TypeSignature.UInt32Array, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="UInt32ArrayTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt32ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt32ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt32ArrayTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(UInt32ArrayTagDataEntry a, UInt32ArrayTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="UInt32ArrayTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt32ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt32ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt32ArrayTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(UInt32ArrayTagDataEntry a, UInt32ArrayTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="UInt32ArrayTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="UInt32ArrayTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="UInt32ArrayTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UInt32ArrayTagDataEntry c = obj as UInt32ArrayTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="UInt32ArrayTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="UInt32ArrayTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents an array of unsigned 64bit integers.
    /// </summary>
    public sealed class UInt64ArrayTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The array data
        /// </summary>
        public readonly ulong[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="UInt64ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        public UInt64ArrayTagDataEntry(ulong[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="UInt64ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public UInt64ArrayTagDataEntry(ulong[] Data, TagSignature TagSignature)
            : base(TypeSignature.UInt64Array, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="UInt64ArrayTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt64ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt64ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt64ArrayTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(UInt64ArrayTagDataEntry a, UInt64ArrayTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="UInt64ArrayTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt64ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt64ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt64ArrayTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(UInt64ArrayTagDataEntry a, UInt64ArrayTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="UInt64ArrayTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="UInt64ArrayTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="UInt64ArrayTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UInt64ArrayTagDataEntry c = obj as UInt64ArrayTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="UInt64ArrayTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="UInt64ArrayTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents an array of bytes.
    /// </summary>
    public sealed class UInt8ArrayTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The array data
        /// </summary>
        public readonly byte[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="UInt8ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        public UInt8ArrayTagDataEntry(byte[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="UInt8ArrayTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The array data</param>
        /// <param name="TagSignature">Tag Signature</param>
        public UInt8ArrayTagDataEntry(byte[] Data, TagSignature TagSignature)
            : base(TypeSignature.UInt8Array, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="UInt8ArrayTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt8ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt8ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt8ArrayTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(UInt8ArrayTagDataEntry a, UInt8ArrayTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="UInt8ArrayTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="UInt8ArrayTagDataEntry"/></param>
        /// <param name="b">The second <see cref="UInt8ArrayTagDataEntry"/></param>
        /// <returns>True if the <see cref="UInt8ArrayTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(UInt8ArrayTagDataEntry a, UInt8ArrayTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="UInt8ArrayTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="UInt8ArrayTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="UInt8ArrayTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UInt8ArrayTagDataEntry c = obj as UInt8ArrayTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="UInt8ArrayTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="UInt8ArrayTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// This type represents a set of viewing condition parameters.
    /// </summary>
    public sealed class ViewingConditionsTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// XYZ values of Illuminant
        /// </summary>
        public readonly XYZNumber IlluminantXYZ;
        /// <summary>
        /// XYZ values of Surrounding
        /// </summary>
        public readonly XYZNumber SurroundXYZ;
        /// <summary>
        /// Illuminant
        /// </summary>
        public readonly StandardIlluminant Illuminant;

        /// <summary>
        /// Creates a new instance of the <see cref="ViewingConditionsTagDataEntry"/> class
        /// </summary>
        /// <param name="IlluminantXYZ">XYZ values of Illuminant</param>
        /// <param name="SurroundXYZ">XYZ values of Surrounding</param>
        /// <param name="Illuminant">Illuminant</param>
        public ViewingConditionsTagDataEntry(XYZNumber IlluminantXYZ, XYZNumber SurroundXYZ, StandardIlluminant Illuminant)
            : this(IlluminantXYZ, SurroundXYZ, Illuminant, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ViewingConditionsTagDataEntry"/> class
        /// </summary>
        /// <param name="IlluminantXYZ">XYZ values of Illuminant</param>
        /// <param name="SurroundXYZ">XYZ values of Surrounding</param>
        /// <param name="Illuminant">Illuminant</param>
        /// <param name="TagSignature">Tag Signature</param>
        public ViewingConditionsTagDataEntry(XYZNumber IlluminantXYZ, XYZNumber SurroundXYZ,
            StandardIlluminant Illuminant, TagSignature TagSignature)
            : base(TypeSignature.ViewingConditions, TagSignature)
        {
            if (!Enum.IsDefined(typeof(StandardIlluminant), Illuminant))
                throw new ArgumentException($"{nameof(Illuminant)} value is not of a defined Enum value");

            this.IlluminantXYZ = IlluminantXYZ;
            this.SurroundXYZ = SurroundXYZ;
            this.Illuminant = Illuminant;
        }


        /// <summary>
        /// Determines whether the specified <see cref="ViewingConditionsTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ViewingConditionsTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ViewingConditionsTagDataEntry"/></param>
        /// <returns>True if the <see cref="ViewingConditionsTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ViewingConditionsTagDataEntry a, ViewingConditionsTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.IlluminantXYZ == b.IlluminantXYZ
                && a.SurroundXYZ == b.SurroundXYZ && a.Illuminant == b.Illuminant;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ViewingConditionsTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ViewingConditionsTagDataEntry"/></param>
        /// <param name="b">The second <see cref="ViewingConditionsTagDataEntry"/></param>
        /// <returns>True if the <see cref="ViewingConditionsTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ViewingConditionsTagDataEntry a, ViewingConditionsTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ViewingConditionsTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ViewingConditionsTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ViewingConditionsTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ViewingConditionsTagDataEntry c = obj as ViewingConditionsTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ViewingConditionsTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ViewingConditionsTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ IlluminantXYZ.GetHashCode();
                hash *= 16777619 ^ SurroundXYZ.GetHashCode();
                hash *= 16777619 ^ Illuminant.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// The XYZType contains an array of XYZ values.
    /// </summary>
    public sealed class XYZTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// The XYZ numbers
        /// </summary>
        public readonly XYZNumber[] Data;

        /// <summary>
        /// Creates a new instance of the <see cref="XYZTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The XYZ numbers</param>
        public XYZTagDataEntry(XYZNumber[] Data)
            : this(Data, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="XYZTagDataEntry"/> class
        /// </summary>
        /// <param name="Data">The XYZ numbers</param>
        /// <param name="TagSignature">Tag Signature</param>
        public XYZTagDataEntry(XYZNumber[] Data, TagSignature TagSignature)
            : base(TypeSignature.XYZ, TagSignature)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            this.Data = Data;
        }


        /// <summary>
        /// Determines whether the specified <see cref="XYZTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="XYZTagDataEntry"/></param>
        /// <param name="b">The second <see cref="XYZTagDataEntry"/></param>
        /// <returns>True if the <see cref="XYZTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(XYZTagDataEntry a, XYZTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.Data, b.Data);
        }

        /// <summary>
        /// Determines whether the specified <see cref="XYZTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="XYZTagDataEntry"/></param>
        /// <param name="b">The second <see cref="XYZTagDataEntry"/></param>
        /// <returns>True if the <see cref="XYZTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(XYZTagDataEntry a, XYZTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="XYZTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="XYZTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="XYZTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            XYZTagDataEntry c = obj as XYZTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="XYZTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="XYZTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(Data);
                return hash;
            }
        }
    }

    /// <summary>
    /// The TextDescriptionType contains three types of text description.
    /// </summary>
    public sealed class TextDescriptionTagDataEntry : TagDataEntry
    {
        /// <summary>
        /// ASCII text
        /// </summary>
        public readonly string ASCII;
        /// <summary>
        /// Unicode text
        /// </summary>
        public readonly string Unicode;
        /// <summary>
        /// ScriptCode text
        /// </summary>
        public readonly string ScriptCode;

        /// <summary>
        /// Unicode Language-Code
        /// </summary>
        public readonly uint UnicodeLanguageCode;
        /// <summary>
        /// ScriptCode Code
        /// </summary>
        public readonly ushort ScriptCodeCode;

        /// <summary>
        /// Creates a new instance of the <see cref="TextDescriptionTagDataEntry"/> class
        /// </summary>
        /// <param name="ASCII">ASCII text</param>
        /// <param name="Unicode">Unicode text</param>
        /// <param name="ScriptCode">ScriptCode text</param>
        /// <param name="UnicodeLanguageCode">Unicode Language-Code</param>
        /// <param name="ScriptCodeCode">ScriptCode Code</param>
        public TextDescriptionTagDataEntry(string ASCII, string Unicode,
            string ScriptCode, uint UnicodeLanguageCode, ushort ScriptCodeCode)
            : this(ASCII, Unicode, ScriptCode, UnicodeLanguageCode, ScriptCodeCode, TagSignature.Unknown)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="TextDescriptionTagDataEntry"/> class
        /// </summary>
        /// <param name="ASCII">ASCII text</param>
        /// <param name="Unicode">Unicode text</param>
        /// <param name="ScriptCode">ScriptCode text</param>
        /// <param name="UnicodeLanguageCode">Unicode Language-Code</param>
        /// <param name="ScriptCodeCode">ScriptCode Code</param>
        /// <param name="TagSignature">Tag Signature</param>
        public TextDescriptionTagDataEntry(string ASCII, string Unicode, string ScriptCode,
            uint UnicodeLanguageCode, ushort ScriptCodeCode, TagSignature TagSignature)
            : base(TypeSignature.TextDescription, TagSignature)
        {
            this.ASCII = ASCII;
            this.Unicode = Unicode;
            this.ScriptCode = ScriptCode;
            this.UnicodeLanguageCode = UnicodeLanguageCode;
            this.ScriptCodeCode = ScriptCodeCode;
        }


        /// <summary>
        /// Determines whether the specified <see cref="TextDescriptionTagDataEntry"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TextDescriptionTagDataEntry"/></param>
        /// <param name="b">The second <see cref="TextDescriptionTagDataEntry"/></param>
        /// <returns>True if the <see cref="TextDescriptionTagDataEntry"/>s are equal; otherwise, false</returns>
        public static bool operator ==(TextDescriptionTagDataEntry a, TextDescriptionTagDataEntry b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.ASCII == b.ASCII
                && a.Unicode == b.Unicode && a.ScriptCode == b.ScriptCode
                && a.UnicodeLanguageCode == b.UnicodeLanguageCode
                && a.ScriptCodeCode == b.ScriptCodeCode;
        }

        /// <summary>
        /// Determines whether the specified <see cref="TextDescriptionTagDataEntry"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="TextDescriptionTagDataEntry"/></param>
        /// <param name="b">The second <see cref="TextDescriptionTagDataEntry"/></param>
        /// <returns>True if the <see cref="TextDescriptionTagDataEntry"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(TextDescriptionTagDataEntry a, TextDescriptionTagDataEntry b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="TextDescriptionTagDataEntry"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="TextDescriptionTagDataEntry"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="TextDescriptionTagDataEntry"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            TextDescriptionTagDataEntry c = obj as TextDescriptionTagDataEntry;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="TextDescriptionTagDataEntry"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="TextDescriptionTagDataEntry"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ ASCII.GetHashCode();
                hash *= 16777619 ^ Unicode.GetHashCode();
                hash *= 16777619 ^ ScriptCode.GetHashCode();
                hash *= 16777619 ^ UnicodeLanguageCode.GetHashCode();
                hash *= 16777619 ^ ScriptCodeCode.GetHashCode();
                return hash;
            }
        }
    }
}
