using System;
using System.Text;

namespace ColorManager.ICC
{
    /// <summary>
    /// The data of an entry
    /// </summary>
    public abstract class TagDataEntry
    {
        public TypeSignature Signature
        {
            get { return _Signature; }
        }
        public TagSignature TagSignature
        {
            get { return _TagSignature; }
        }

        private TypeSignature _Signature = TypeSignature.Unknown;
        internal protected TagSignature _TagSignature = TagSignature.Unknown;

        protected TagDataEntry(TypeSignature signature)
        {
            _Signature = signature;
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
        public byte[] Data
        {
            get { return _Data; }
        }
        private byte[] _Data;

        public UnknownTagDataEntry(byte[] Data)
            : base(TypeSignature.Unknown)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public int ChannelCount
        {
            get { return _ChannelCount; }
        }
        public ColorantEncoding ColorantType
        {
            get { return _ColorantType; }
        }
        public double[][] ChannelValues
        {
            get { return _ChannelValues; }
        }

        private int _ChannelCount;
        private ColorantEncoding _ColorantType;
        private double[][] _ChannelValues;

        public ChromaticityTagDataEntry(int ChannelCount, ColorantEncoding ColorantType)
            : base(TypeSignature.Chromaticity)
        {
            if (ChannelValues == null) throw new ArgumentNullException(nameof(ChannelValues));

            _ChannelCount = ChannelCount;
            _ColorantType = ColorantType;
            _ChannelValues = GetColorantArray();
        }

        public ChromaticityTagDataEntry(int ChannelCount, ColorantEncoding ColorantType, double[][] ChannelValues)
            : base(TypeSignature.Chromaticity)
        {
            if (ChannelValues == null) throw new ArgumentNullException(nameof(ChannelValues));

            _ChannelCount = ChannelCount;
            _ColorantType = ColorantType;
            _ChannelValues = ChannelValues;
        }

        private double[][] GetColorantArray()
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
        public uint ColorantCount
        {
            get { return _ColorantCount; }
        }
        public byte[] ColorantNumber
        {
            get { return _ColorantNumber; }
        }

        private uint _ColorantCount;
        private byte[] _ColorantNumber;

        public ColorantOrderTagDataEntry(uint ColorantCount, byte[] ColorantNumber)
            : base(TypeSignature.ColorantOrder)
        {
            if (ColorantNumber == null) throw new ArgumentNullException(nameof(ColorantNumber));
            _ColorantCount = ColorantCount;
            _ColorantNumber = ColorantNumber;
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
            return a.Signature == b.Signature && a.ColorantCount == b.ColorantCount
                && CMP.Compare(a.ColorantNumber, b.ColorantNumber);
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
                hash *= 16777619 ^ ColorantCount.GetHashCode();
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
        public uint ColorantCount
        {
            get { return _ColorantCount; }
        }
        public ColorantTableEntry[] ColorantData
        {
            get { return _ColorantData; }
        }

        private uint _ColorantCount;
        private ColorantTableEntry[] _ColorantData;

        public ColorantTableTagDataEntry(uint ColorantCount, ColorantTableEntry[] ColorantData)
            : base(TypeSignature.ColorantTable)
        {
            if (ColorantData == null) throw new ArgumentNullException(nameof(ColorantData));
            _ColorantCount = ColorantCount;
            _ColorantData = ColorantData;
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
            return a.Signature == b.Signature && a.ColorantCount == b.ColorantCount
                && CMP.Compare(a.ColorantData, b.ColorantData);
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
                hash *= 16777619 ^ ColorantCount.GetHashCode();
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
        public double[] CurveData
        {
            get { return _CurveData; }
        }
        public bool IsIdentityResponse
        {
            get { return _IsIdentityResponse; }
        }
        public bool IsGamma
        {
            get { return _IsGamma; }
        }
        public double Gamma
        {
            get { return _Gamma; }
        }

        private double[] _CurveData;
        private bool _IsIdentityResponse;
        private bool _IsGamma;
        private double _Gamma;

        public CurveTagDataEntry()
            : base(TypeSignature.Curve)
        {
            _IsIdentityResponse = true;
        }

        public CurveTagDataEntry(double[] CurveData)
            : base(TypeSignature.Curve)
        {
            if (CurveData == null) throw new ArgumentNullException(nameof(CurveData));
            _CurveData = CurveData;
        }

        public CurveTagDataEntry(double Gamma)
            : base(TypeSignature.Curve)
        {
            _IsGamma = true;
            _Gamma = Gamma;
            _CurveData = new double[] { Gamma };
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
        public byte[] Data
        {
            get { return _Data; }
        }
        public bool IsASCII
        {
            get { return _IsASCII; }
        }
        public string ASCIIString
        {
            get
            {
                if (IsASCII) return Encoding.ASCII.GetString(Data);
                else return string.Empty;
            }
        }

        private byte[] _Data;
        private bool _IsASCII;

        public DataTagDataEntry(byte[] Data, bool IsASCII)
            : base(TypeSignature.Data)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
            _IsASCII = IsASCII;
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
        public DateTime Value
        {
            get { return _Value; }
        }
        private DateTime _Value;

        public DateTimeTagDataEntry(DateTime value)
            : base(TypeSignature.DateTime)
        {
            _Value = value;
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
        public double[,] Matrix
        {
            get { return _Matrix; }
        }
        public LUT[] InputValues
        {
            get { return _InputValues; }
        }
        public CLUT CLUTValues
        {
            get { return _CLUTValues; }
        }
        public LUT[] OutputValues
        {
            get { return _OutputValues; }
        }

        private double[,] _Matrix;
        private LUT[] _InputValues;
        private CLUT _CLUTValues;
        private LUT[] _OutputValues;

        public Lut16TagDataEntry(double[,] Matrix, LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues)
            : base(TypeSignature.Lut16)
        {
            if (Matrix == null) throw new ArgumentNullException(nameof(Matrix));
            if (InputValues == null) throw new ArgumentNullException(nameof(InputValues));
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (OutputValues == null) throw new ArgumentNullException(nameof(OutputValues));

            _Matrix = Matrix;
            _InputValues = InputValues;
            _CLUTValues = CLUTValues;
            _OutputValues = OutputValues;
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
        public double[,] Matrix
        {
            get { return _Matrix; }
        }
        public LUT[] InputValues
        {
            get { return _InputValues; }
        }
        public CLUT CLUTValues
        {
            get { return _CLUTValues; }
        }
        public LUT[] OutputValues
        {
            get { return _OutputValues; }
        }

        private double[,] _Matrix;
        private LUT[] _InputValues;
        private CLUT _CLUTValues;
        private LUT[] _OutputValues;

        public Lut8TagDataEntry(double[,] Matrix, LUT[] InputValues, CLUT CLUTValues, LUT[] OutputValues)
            : base(TypeSignature.Lut8)
        {
            if (Matrix == null) throw new ArgumentNullException(nameof(Matrix));
            if (InputValues == null) throw new ArgumentNullException(nameof(InputValues));
            if (CLUTValues == null) throw new ArgumentNullException(nameof(CLUTValues));
            if (OutputValues == null) throw new ArgumentNullException(nameof(OutputValues));

            _Matrix = Matrix;
            _InputValues = InputValues;
            _CLUTValues = CLUTValues;
            _OutputValues = OutputValues;
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
        public int InputChannelCount
        {
            get { return _InputChannelCount; }
        }
        public int OutputChannelCount
        {
            get { return _OutputChannelCount; }
        }
        public double[,] Matrix3x3
        {
            get { return _Matrix3x3; }
        }
        public double[] Matrix3x1
        {
            get { return _Matrix3x1; }
        }
        public CLUT CLUTValues
        {
            get { return _CLUTValues; }
        }
        public TagDataEntry[] CurveB
        {
            get { return _CurveB; }
        }
        public TagDataEntry[] CurveM
        {
            get { return _CurveM; }
        }
        public TagDataEntry[] CurveA
        {
            get { return _CurveA; }
        }

        private int _InputChannelCount;
        private int _OutputChannelCount;
        private double[,] _Matrix3x3;
        private double[] _Matrix3x1;
        private CLUT _CLUTValues;
        private TagDataEntry[] _CurveB;
        private TagDataEntry[] _CurveM;
        private TagDataEntry[] _CurveA;

        public LutAToBTagDataEntry(int inChCount, int outChCount, double[,] Matrix3x3, double[] Matrix3x1,
            CLUT CLUTValues, TagDataEntry[] CurveB, TagDataEntry[] CurveM, TagDataEntry[] CurveA)
            : base(TypeSignature.LutAToB)
        {
            _InputChannelCount = inChCount;
            _OutputChannelCount = outChCount;
            _Matrix3x3 = Matrix3x3;
            _Matrix3x1 = Matrix3x1;
            _CLUTValues = CLUTValues;
            _CurveB = CurveB;
            _CurveM = CurveM;
            _CurveA = CurveA;
        }

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
        public int InputChannelCount
        {
            get { return _InputChannelCount; }
        }
        public int OutputChannelCount
        {
            get { return _OutputChannelCount; }
        }
        public double[,] Matrix3x3
        {
            get { return _Matrix3x3; }
        }
        public double[] Matrix3x1
        {
            get { return _Matrix3x1; }
        }
        public CLUT CLUTValues
        {
            get { return _CLUTValues; }
        }
        public TagDataEntry[] CurveB
        {
            get { return _CurveB; }
        }
        public TagDataEntry[] CurveM
        {
            get { return _CurveM; }
        }
        public TagDataEntry[] CurveA
        {
            get { return _CurveA; }
        }

        private int _InputChannelCount;
        private int _OutputChannelCount;
        private double[,] _Matrix3x3;
        private double[] _Matrix3x1;
        private CLUT _CLUTValues;
        private TagDataEntry[] _CurveB;
        private TagDataEntry[] _CurveM;
        private TagDataEntry[] _CurveA;

        public LutBToATagDataEntry(int inChCount, int outChCount, double[,] Matrix3x3, double[] Matrix3x1,
            CLUT CLUTValues, TagDataEntry[] CurveB, TagDataEntry[] CurveM, TagDataEntry[] CurveA)
            : base(TypeSignature.LutBToA)
        {
            _InputChannelCount = inChCount;
            _OutputChannelCount = outChCount;
            _Matrix3x3 = Matrix3x3;
            _Matrix3x1 = Matrix3x1;
            _CLUTValues = CLUTValues;
            _CurveB = CurveB;
            _CurveM = CurveM;
            _CurveA = CurveA;
        }

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
        public StandardObserver Observer
        {
            get { return _Observer; }
        }
        public XYZNumber XYZBacking
        {
            get { return _XYZBacking; }
        }
        public MeasurementGeometry Geometry
        {
            get { return _Geometry; }
        }
        public double Flare
        {
            get { return _Flare; }
        }
        public StandardIlluminant Illuminant
        {
            get { return _Illuminant; }
        }

        private StandardObserver _Observer;
        private XYZNumber _XYZBacking;
        private MeasurementGeometry _Geometry;
        private double _Flare;
        private StandardIlluminant _Illuminant;

        public MeasurementTagDataEntry(StandardObserver Observer, XYZNumber XYZBacking,
            MeasurementGeometry Geometry, double Flare, StandardIlluminant Illuminant)
            : base(TypeSignature.Data)
        {
            _Observer = Observer;
            _XYZBacking = XYZBacking;
            _Geometry = Geometry;
            _Flare = Flare;
            _Illuminant = Illuminant;
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
        public LocalizedString[] Text
        {
            get { return _Text; }
        }
        private LocalizedString[] _Text;

        public MultiLocalizedUnicodeTagDataEntry(LocalizedString[] Text)
            : base(TypeSignature.MultiLocalizedUnicode)
        {
            if (Text == null) throw new ArgumentNullException(nameof(Text));
            _Text = Text;
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
        public int InputChannelCount
        {
            get { return _InputChannelCount; }
        }
        public int OutputChannelCount
        {
            get { return _OutputChannelCount; }
        }
        public MultiProcessElement[] Data
        {
            get { return _Data; }
        }

        private int _InputChannelCount;
        private int _OutputChannelCount;
        private MultiProcessElement[] _Data;

        public MultiProcessElementsTagDataEntry(int inChCount, int outChCount, MultiProcessElement[] Data)
            : base(TypeSignature.MultiProcessElements)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _InputChannelCount = inChCount;
            _OutputChannelCount = outChCount;
            _Data = Data;
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
        public byte[] VendorFlag
        {
            get { return _VendorFlag; }
        }
        public int CoordCount
        {
            get { return _CoordCount; }
        }
        public string Prefix
        {
            get { return _Prefix; }
        }
        public string Suffix
        {
            get { return _Suffix; }
        }
        public NamedColor[] Colors
        {
            get { return _Colors; }
        }

        private byte[] _VendorFlag;
        private int _CoordCount;
        private string _Prefix;
        private string _Suffix;
        private NamedColor[] _Colors;

        public NamedColor2TagDataEntry(byte[] VendorFlag, int CoordCount, string Prefix, string Suffix, NamedColor[] Colors)
            : base(TypeSignature.NamedColor2)
        {
            if (VendorFlag == null) throw new ArgumentNullException(nameof(VendorFlag));
            if (Colors == null) throw new ArgumentNullException(nameof(Colors));

            _VendorFlag = VendorFlag;
            _CoordCount = CoordCount;
            _Prefix = Prefix;
            _Suffix = Suffix;
            _Colors = Colors;
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
                && a.Prefix == b.Prefix && a.Suffix == b.Suffix && CMP.Compare(a.VendorFlag, b.VendorFlag)
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
                hash *= CMP.GetHashCode(VendorFlag);
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
        public ParametricCurve Curve
        {
            get { return _Curve; }
        }
        private ParametricCurve _Curve;

        public ParametricCurveTagDataEntry(ParametricCurve Curve)
            : base(TypeSignature.ParametricCurve)
        {
            _Curve = Curve;
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
        public ProfileDescription[] Descriptions
        {
            get { return _Descriptions; }
        }
        private ProfileDescription[] _Descriptions;

        public ProfileSequenceDescTagDataEntry(ProfileDescription[] Descriptions)
            : base(TypeSignature.ProfileSequenceDesc)
        {
            if (Descriptions == null) throw new ArgumentNullException(nameof(Descriptions));
            _Descriptions = Descriptions;
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
        public ProfileSequenceIdentifier[] Data
        {
            get { return _Data; }
        }
        private ProfileSequenceIdentifier[] _Data;

        public ProfileSequenceIdentifierTagDataEntry(ProfileSequenceIdentifier[] Data)
            : base(TypeSignature.ProfileSequenceIdentifier)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));

            _Data = Data;
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
        public int ChannelCount
        {
            get { return _ChannelCount; }
        }
        public ResponseCurve[] Curves
        {
            get { return _Curves; }
        }

        private int _ChannelCount;
        private ResponseCurve[] _Curves;

        public ResponseCurveSet16TagDataEntry(int ChannelCount, ResponseCurve[] Curves)
            : base(TypeSignature.ResponseCurveSet16)
        {
            if (Curves == null) throw new ArgumentNullException(nameof(Curves));
            _ChannelCount = ChannelCount;
            _Curves = Curves;
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
        public double[] Data
        {
            get { return _Data; }
        }
        private double[] _Data;

        public Fix16ArrayTagDataEntry(double[] Data)
            : base(TypeSignature.S15Fixed16Array)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public string SignatureData
        {
            get { return _SignatureData; }
        }
        private string _SignatureData;

        public SignatureTagDataEntry(string SignatureData)
            : base(TypeSignature.Signature)
        {
            _SignatureData = SignatureData;
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
        public string Text
        {
            get { return _Text; }
        }
        private string _Text;

        public TextTagDataEntry(string Text)
            : base(TypeSignature.Text)
        {
            _Text = Text;
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
        public double[] Data
        {
            get { return _Data; }
        }
        private double[] _Data;

        public UFix16ArrayTagDataEntry(double[] Data)
            : base(TypeSignature.U16Fixed16Array)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public ushort[] Data
        {
            get { return _Data; }
        }
        private ushort[] _Data;

        public UInt16ArrayTagDataEntry(ushort[] Data)
            : base(TypeSignature.UInt16Array)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public uint[] Data
        {
            get { return _Data; }
        }
        private uint[] _Data;

        public UInt32ArrayTagDataEntry(uint[] Data)
            : base(TypeSignature.UInt32Array)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public ulong[] Data
        {
            get { return _Data; }
        }
        private ulong[] _Data;

        public UInt64ArrayTagDataEntry(ulong[] Data)
            : base(TypeSignature.UInt64Array)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public byte[] Data
        {
            get { return _Data; }
        }
        private byte[] _Data;

        public UInt8ArrayTagDataEntry(byte[] Data)
            : base(TypeSignature.UInt8Array)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
        public XYZNumber IlluminantXYZ
        {
            get { return _IlluminantXYZ; }
        }
        public XYZNumber SurroundXYZ
        {
            get { return _SurroundXYZ; }
        }
        public StandardIlluminant Illuminant
        {
            get { return _Illuminant; }
        }

        private XYZNumber _IlluminantXYZ;
        private XYZNumber _SurroundXYZ;
        private StandardIlluminant _Illuminant;

        public ViewingConditionsTagDataEntry(XYZNumber IlluminantXYZ, XYZNumber SurroundXYZ, StandardIlluminant Illuminant)
            : base(TypeSignature.ViewingConditions)
        {
            _IlluminantXYZ = IlluminantXYZ;
            _SurroundXYZ = SurroundXYZ;
            _Illuminant = Illuminant;
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
        public XYZNumber[] Data
        {
            get { return _Data; }
        }
        private XYZNumber[] _Data;

        public XYZTagDataEntry(XYZNumber[] Data)
            : base(TypeSignature.XYZ)
        {
            if (Data == null) throw new ArgumentNullException(nameof(Data));
            _Data = Data;
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
}
