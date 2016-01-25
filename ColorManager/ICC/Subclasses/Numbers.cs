using System;

namespace ColorManager.ICC
{
    /// <summary>
    /// ICC Profile version number
    /// </summary>
    public struct VersionNumber
    {
        /// <summary>
        /// Major version
        /// </summary>
        public readonly int Major;
        /// <summary>
        /// Minor version
        /// </summary>
        public readonly int Minor;
        /// <summary>
        /// Bugfix version
        /// </summary>
        public readonly int Bugfix;

        /// <summary>
        /// Creates a new instance of the <see cref="VersionNumber"/> struct
        /// </summary>
        /// <param name="Major">Major version</param>
        /// <param name="Minor">Minor version</param>
        public VersionNumber(int Major, int Minor)
        {
            this.Major = Major;
            this.Minor = Minor;
            Bugfix = 0;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="VersionNumber"/> struct
        /// </summary>
        /// <param name="Major">Major version</param>
        /// <param name="Minor">Minor version</param>
        /// <param name="Bugfix">Bugfix version</param>
        public VersionNumber(int Major, int Minor, int Bugfix)
        {
            this.Major = Major;
            this.Minor = Minor;
            this.Bugfix = Bugfix;
        }

        /// <summary>
        /// Determines whether the specified <see cref="VersionNumber"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="VersionNumber"/></param>
        /// <param name="b">The second <see cref="VersionNumber"/></param>
        /// <returns>True if the <see cref="VersionNumber"/>s are equal; otherwise, false</returns>
        public static bool operator ==(VersionNumber a, VersionNumber b)
        {
            return a.Major == b.Major && a.Minor == b.Minor && a.Bugfix == b.Bugfix;
        }

        /// <summary>
        /// Determines whether the specified <see cref="VersionNumber"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="VersionNumber"/></param>
        /// <param name="b">The second <see cref="VersionNumber"/></param>
        /// <returns>True if the <see cref="VersionNumber"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(VersionNumber a, VersionNumber b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="VersionNumber"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="VersionNumber"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="VersionNumber"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is VersionNumber && this == (VersionNumber)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="VersionNumber"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="VersionNumber"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Major.GetHashCode();
                hash *= 16777619 ^ Minor.GetHashCode();
                hash *= 16777619 ^ Bugfix.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Major}.{ Minor}.{Bugfix}.0";
        }
    }

    /// <summary>
    /// XYZ number
    /// </summary>
    public struct XYZNumber
    {
        /// <summary>
        /// The X, Y and Z values in an array
        /// </summary>
        public double[] XYZ
        {
            get { return new double[] { X, Y, Z }; }
        }
        /// <summary>
        /// X-Value
        /// </summary>
        public readonly double X;
        /// <summary>
        /// Y-Value
        /// </summary>
        public readonly double Y;
        /// <summary>
        /// Z-Value
        /// </summary>
        public readonly double Z;

        /// <summary>
        /// Creates a new instance of the <see cref="XYZNumber"/> struct
        /// </summary>
        /// <param name="X">X-Value</param>
        /// <param name="Y">Y-Value</param>
        /// <param name="Z">Z-Value</param>
        public XYZNumber(double X, double Y, double Z)
        {
            if (double.IsNaN(X) || double.IsInfinity(X)) throw new ArgumentException($"{nameof(X)} is not a number");
            if (double.IsNaN(Y) || double.IsInfinity(Y)) throw new ArgumentException($"{nameof(Y)} is not a number");
            if (double.IsNaN(Z) || double.IsInfinity(Z)) throw new ArgumentException($"{nameof(Z)} is not a number");

            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }


        /// <summary>
        /// Determines whether the specified <see cref="XYZNumber"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="XYZNumber"/></param>
        /// <param name="b">The second <see cref="XYZNumber"/></param>
        /// <returns>True if the <see cref="XYZNumber"/>s are equal; otherwise, false</returns>
        public static bool operator ==(XYZNumber a, XYZNumber b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        /// <summary>
        /// Determines whether the specified <see cref="XYZNumber"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="XYZNumber"/></param>
        /// <param name="b">The second <see cref="XYZNumber"/></param>
        /// <returns>True if the <see cref="XYZNumber"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(XYZNumber a, XYZNumber b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="XYZNumber"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="XYZNumber"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="XYZNumber"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is XYZNumber && this == (XYZNumber)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="XYZNumber"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="XYZNumber"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ X.GetHashCode();
                hash *= 16777619 ^ Y.GetHashCode();
                hash *= 16777619 ^ Z.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{X}; {Y}; {Z}";
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent
        /// string representation, using the specified format.
        /// </summary>
        /// <param name="format">A numeric format string</param>
        /// <returns>he string representation of the value of this instance as specified by format</returns>
        public string ToString(string format)
        {
            return $"{X.ToString(format)}; {Y.ToString(format)}; {Z.ToString(format)}";
        }
    }

    /// <summary>
    /// ICC Profile ID
    /// </summary>
    public struct ProfileID
    {
        /// <summary>
        /// States if the ID is set or just consists of zeros
        /// </summary>
        public bool IsSet
        {
            get
            {
                return Values != null && Values[0] != 0
                    && Values[1] != 0 && Values[2] != 0
                    && Values[3] != 0;
            }
        }
        /// <summary>
        /// The values the ID consists of (always has a length of 4)
        /// </summary>
        public readonly uint[] Values;

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileID"/> struct
        /// </summary>
        /// <param name="Values">The ID values (length must be 4)</param>
        public ProfileID(uint[] Values)
        {
            if (Values == null) throw new ArgumentNullException(nameof(Values));
            if (Values.Length != 4) throw new ArgumentOutOfRangeException(nameof(Values), "Values must have a length of 4");

            this.Values = Values;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileID"/> struct
        /// </summary>
        /// <param name="p1">Part 1 of the ID</param>
        /// <param name="p2">Part 1 of the ID</param>
        /// <param name="p3">Part 1 of the ID</param>
        /// <param name="p4">Part 1 of the ID</param>
        public ProfileID(uint p1, uint p2, uint p3, uint p4)
            : this(new uint[] { p1, p2, p3, p4 })
        { }

        private static string ToHex(uint value)
        {
            return value.ToString("X").PadLeft(8, '0');
        }


        /// <summary>
        /// Determines whether the specified <see cref="ProfileID"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileID"/></param>
        /// <param name="b">The second <see cref="ProfileID"/></param>
        /// <returns>True if the <see cref="ProfileID"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileID a, ProfileID b)
        {
            return CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ProfileID"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileID"/></param>
        /// <param name="b">The second <see cref="ProfileID"/></param>
        /// <returns>True if the <see cref="ProfileID"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ProfileID a, ProfileID b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ProfileID"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ProfileID"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ProfileID"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ProfileID && this == (ProfileID)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProfileID"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ProfileID"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ CMP.GetHashCode(Values);
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            if (Values == null) return "00000000-00000000-00000000-00000000";
            else return $"{ToHex(Values[0])}-{ToHex(Values[1])}-{ToHex(Values[2])}-{ToHex(Values[3])}";
        }
    }

    /// <summary>
    /// Position of an object within an ICC profile
    /// </summary>
    public struct PositionNumber
    {
        /// <summary>
        /// Offset in bytes
        /// </summary>
        public readonly uint Offset;
        /// <summary>
        /// Size in bytes
        /// </summary>
        public readonly uint Size;

        /// <summary>
        /// Creates a new instance of the <see cref="PositionNumber"/> struct
        /// </summary>
        /// <param name="Offset">Offset in bytes</param>
        /// <param name="Size">Size in bytes</param>
        public PositionNumber(uint Offset, uint Size)
        {
            this.Offset = Offset;
            this.Size = Size;
        }


        /// <summary>
        /// Determines whether the specified <see cref="PositionNumber"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="PositionNumber"/></param>
        /// <param name="b">The second <see cref="PositionNumber"/></param>
        /// <returns>True if the <see cref="PositionNumber"/>s are equal; otherwise, false</returns>
        public static bool operator ==(PositionNumber a, PositionNumber b)
        {
            return a.Offset == b.Offset && a.Size == b.Size;
        }

        /// <summary>
        /// Determines whether the specified <see cref="PositionNumber"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="PositionNumber"/></param>
        /// <param name="b">The second <see cref="PositionNumber"/></param>
        /// <returns>True if the <see cref="PositionNumber"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(PositionNumber a, PositionNumber b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="PositionNumber"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="PositionNumber"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="PositionNumber"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is PositionNumber && this == (PositionNumber)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="PositionNumber"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="PositionNumber"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Offset.GetHashCode();
                hash *= 16777619 ^ Size.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Offset}; {Size}";
        }
    }

    /// <summary>
    /// Associates a normalized device code with a measurement value
    /// </summary>
    public struct ResponseNumber
    {
        /// <summary>
        /// Device Code
        /// </summary>
        public readonly ushort DeviceCode;
        /// <summary>
        /// Measurement Value
        /// </summary>
        public readonly double MeasurementValue;

        /// <summary>
        /// Creates a new instance of the <see cref="ResponseNumber"/> struct
        /// </summary>
        /// <param name="DeviceCode">Device Code</param>
        /// <param name="MeasurementValue">Measurement Value</param>
        public ResponseNumber(ushort DeviceCode, double MeasurementValue)
        {
            this.DeviceCode = DeviceCode;
            this.MeasurementValue = MeasurementValue;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ResponseNumber"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseNumber"/></param>
        /// <param name="b">The second <see cref="ResponseNumber"/></param>
        /// <returns>True if the <see cref="ResponseNumber"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ResponseNumber a, ResponseNumber b)
        {
            return a.DeviceCode == b.DeviceCode && a.MeasurementValue == b.MeasurementValue;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ResponseNumber"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseNumber"/></param>
        /// <param name="b">The second <see cref="ResponseNumber"/></param>
        /// <returns>True if the <see cref="ResponseNumber"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ResponseNumber a, ResponseNumber b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ResponseNumber"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ResponseNumber"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ResponseNumber"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ResponseNumber && this == (ResponseNumber)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ResponseNumber"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ResponseNumber"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ DeviceCode.GetHashCode();
                hash *= 16777619 ^ MeasurementValue.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"Code: {DeviceCode}; Value: {MeasurementValue}";
        }
    }
}
