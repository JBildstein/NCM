namespace ColorManager.ICC
{
    public struct VersionNumber
    {
        public int Major;
        public int Minor;
        public int BugFix;

        public VersionNumber(int Major, int Minor, int Bugfix)
        {
            this.Major = Major;
            this.Minor = Minor;
            BugFix = Bugfix;
        }

        /// <summary>
        /// Determines whether the specified <see cref="VersionNumber"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="VersionNumber"/></param>
        /// <param name="b">The second <see cref="VersionNumber"/></param>
        /// <returns>True if the <see cref="VersionNumber"/>s are equal; otherwise, false</returns>
        public static bool operator ==(VersionNumber a, VersionNumber b)
        {
            return a.Major == b.Major && a.Minor == b.Minor && a.BugFix == b.BugFix;
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
                hash *= 16777619 ^ BugFix.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return $"{Major}.{ Minor}.{BugFix}.0";
        }
    }

    public struct XYZNumber
    {
        public double[] XYZ
        {
            get { return new double[] { X, Y, Z }; }
        }
        public double X;
        public double Y;
        public double Z;

        public XYZNumber(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            
            //TODO: not sure why this X/Y/Z > 2 check was here
            //if (X > 2) this.X /= 256d;
            //if (Y > 2) this.Y /= 256d;
            //if (Z > 2) this.Z /= 256d;
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
            return string.Concat(X, "; ", Y, "; ", Z);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent
        /// string representation, using the specified format.
        /// </summary>
        /// <param name="format">A numeric format string</param>
        /// <returns>he string representation of the value of this instance as specified by format</returns>
        public string ToString(string format)
        {
            return string.Concat(X.ToString(format), "; ", Y.ToString(format), "; ", Z.ToString(format));
        }
    }

    public struct ProfileID
    {
        public bool IsSet
        {
            get
            {
                return NumericValue != null && NumericValue.Length == 4
                    && NumericValue[0] != 0 && NumericValue[1] != 0
                    && NumericValue[2] != 0 && NumericValue[3] != 0;
            }
        }

        public string StringValue;
        public uint[] NumericValue;

        public ProfileID(uint p1, uint p2, uint p3, uint p4)
        {
            NumericValue = new uint[] { p1, p2, p3, p4 };
            StringValue = string.Concat(p1.ToString("X").PadRight(8, '0'), "-",
                p2.ToString("X").PadRight(8, '0'), "-",
                p3.ToString("X").PadRight(8, '0'), "-",
                p4.ToString("X").PadRight(8, '0'));
        }


        /// <summary>
        /// Determines whether the specified <see cref="ProfileID"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ProfileID"/></param>
        /// <param name="b">The second <see cref="ProfileID"/></param>
        /// <returns>True if the <see cref="ProfileID"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ProfileID a, ProfileID b)
        {
            return a.StringValue == b.StringValue;
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
                hash *= 16777619 ^ StringValue.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return StringValue;
        }
    }

    public struct PositionNumber
    {
        public uint Offset;
        public uint Size;

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
            return string.Concat(Offset, ";", Size);
        }
    }

    public struct ResponseNumber
    {
        public ushort DeviceCode;
        public double MeasurmentValue;

        public ResponseNumber(ushort DeviceCode, double MeasurmentValue)
        {
            this.DeviceCode = DeviceCode;
            this.MeasurmentValue = MeasurmentValue;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ResponseNumber"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseNumber"/></param>
        /// <param name="b">The second <see cref="ResponseNumber"/></param>
        /// <returns>True if the <see cref="ResponseNumber"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ResponseNumber a, ResponseNumber b)
        {
            return a.DeviceCode == b.DeviceCode && a.MeasurmentValue == b.MeasurmentValue;
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
                hash *= 16777619 ^ MeasurmentValue.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return string.Concat(DeviceCode, ";", MeasurmentValue);
        }
    }
}
