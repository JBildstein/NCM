using System;
using System.Linq;

namespace ColorManager.ICC
{
    //TODO: unify all of the bit-depth-specific classes to one double class

    public abstract class CLUT
    {
        public int InputChannelCount
        {
            get { return _InputChannelCount; }
        }
        public int OutputChannelCount
        {
            get { return _OutputChannelCount; }
        }
        public byte[] GridPointCount
        {
            get { return _GridPointCount; }
        }

        private int _InputChannelCount;
        private int _OutputChannelCount;
        private byte[] _GridPointCount;

        protected CLUT(int inChCount, int outChCount, byte[] gridPointCount)
        {
            if (gridPointCount == null) throw new ArgumentNullException("gridPointCount");

            this._InputChannelCount = inChCount;
            this._OutputChannelCount = outChCount;
            this._GridPointCount = gridPointCount;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="CLUT"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT"/></param>
        /// <param name="b">The second <see cref="CLUT"/></param>
        /// <returns>True if the <see cref="CLUT"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CLUT a, CLUT b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.InputChannelCount == b.InputChannelCount && a.OutputChannelCount == b.OutputChannelCount
                && CMP.Compare(a.GridPointCount, b.GridPointCount);
        }

        /// <summary>
        /// Determines whether the specified <see cref="CLUT"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT"/></param>
        /// <param name="b">The second <see cref="CLUT"/></param>
        /// <returns>True if the <see cref="CLUT"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CLUT a, CLUT b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CLUT"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CLUT"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CLUT"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CLUT c = obj as CLUT;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CLUT"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CLUT"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(GridPointCount);
                return hash;
            }
        }
    }

    public sealed class CLUTf32 : CLUT
    {
        public double[][] Values
        {
            get { return _Values; }
        }
        private double[][] _Values;

        public CLUTf32(double[][] Values, int inChCount, int outChCount, byte[] gridPointCount)
            : base(inChCount, outChCount, gridPointCount)
        {
            this._Values = Values;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="CLUTf32"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUTf32"/></param>
        /// <param name="b">The second <see cref="CLUTf32"/></param>
        /// <returns>True if the <see cref="CLUTf32"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CLUTf32 a, CLUTf32 b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.InputChannelCount == b.InputChannelCount && a.OutputChannelCount == b.OutputChannelCount
                && CMP.Compare(a.GridPointCount, b.GridPointCount) && CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="CLUTf32"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUTf32"/></param>
        /// <param name="b">The second <see cref="CLUTf32"/></param>
        /// <returns>True if the <see cref="CLUTf32"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CLUTf32 a, CLUTf32 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CLUTf32"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CLUTf32"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CLUTf32"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CLUTf32 c = obj as CLUTf32;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CLUTf32"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CLUTf32"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(GridPointCount);
                hash *= CMP.GetHashCode(Values);
                return hash;
            }
        }
    }
    
    public sealed class CLUT16 : CLUT
    {
        public ushort[][] Values
        {
            get { return _Values; }
        }
        private ushort[][] _Values;

        public CLUT16(ushort[][] Values, int inChCount, int outChCount, byte[] gridPointCount)
            : base(inChCount, outChCount, gridPointCount)
        {
            if (Values == null) throw new ArgumentNullException("Values");
            this._Values = Values;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="CLUT16"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT16"/></param>
        /// <param name="b">The second <see cref="CLUT16"/></param>
        /// <returns>True if the <see cref="CLUT16"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CLUT16 a, CLUT16 b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.InputChannelCount == b.InputChannelCount && a.OutputChannelCount == b.OutputChannelCount
                && CMP.Compare(a.GridPointCount, b.GridPointCount) && CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="CLUT16"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT16"/></param>
        /// <param name="b">The second <see cref="CLUT16"/></param>
        /// <returns>True if the <see cref="CLUT16"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CLUT16 a, CLUT16 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CLUT16"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CLUT16"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CLUT16"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CLUT16 c = obj as CLUT16;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CLUT16"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CLUT16"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(GridPointCount);
                hash *= CMP.GetHashCode(Values);
                return hash;
            }
        }
    }

    public sealed class CLUT8 : CLUT
    {
        public byte[][] Values
        {
            get { return _Values; }
        }
        private byte[][] _Values;

        public CLUT8(byte[][] Values, int inChCount, int outChCount, byte[] gridPointCount)
            : base(inChCount, outChCount, gridPointCount)
        {
            if (Values == null) throw new ArgumentNullException("Values");
            this._Values = Values;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="CLUT8"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT8"/></param>
        /// <param name="b">The second <see cref="CLUT8"/></param>
        /// <returns>True if the <see cref="CLUT8"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CLUT8 a, CLUT8 b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.InputChannelCount == b.InputChannelCount && a.OutputChannelCount == b.OutputChannelCount
                && CMP.Compare(a.GridPointCount, b.GridPointCount) && CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="CLUT8"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT8"/></param>
        /// <param name="b">The second <see cref="CLUT8"/></param>
        /// <returns>True if the <see cref="CLUT8"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CLUT8 a, CLUT8 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CLUT8"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CLUT8"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CLUT8"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CLUT8 c = obj as CLUT8;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CLUT8"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CLUT8"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(GridPointCount);
                hash *= CMP.GetHashCode(Values);
                return hash;
            }
        }
    }

    public struct LUT16
    {
        public ushort[] Values;

        public LUT16(ushort[] Values)
        {
            if (Values == null) throw new ArgumentNullException("Values");
            this.Values = Values;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="LUT16"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT16"/></param>
        /// <param name="b">The second <see cref="LUT16"/></param>
        /// <returns>True if the <see cref="LUT16"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LUT16 a, LUT16 b)
        {
            return CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="LUT16"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT16"/></param>
        /// <param name="b">The second <see cref="LUT16"/></param>
        /// <returns>True if the <see cref="LUT16"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(LUT16 a, LUT16 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="LUT16"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="LUT16"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="LUT16"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is LUT16 && this == (LUT16)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="LUT16"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="LUT16"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= CMP.GetHashCode(Values);
                return hash;
            }
        }
    }

    public struct LUT8
    {
        public byte[] Values;

        public LUT8(byte[] Values)
        {
            if (Values == null) throw new ArgumentNullException("Values");
            this.Values = Values;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="LUT8"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT8"/></param>
        /// <param name="b">The second <see cref="LUT8"/></param>
        /// <returns>True if the <see cref="LUT8"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LUT8 a, LUT8 b)
        {
            return CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="LUT8"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT8"/></param>
        /// <param name="b">The second <see cref="LUT8"/></param>
        /// <returns>True if the <see cref="LUT8"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(LUT8 a, LUT8 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="LUT8"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="LUT8"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="LUT8"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is LUT8 && this == (LUT8)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="LUT8"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="LUT8"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= CMP.GetHashCode(Values);
                return hash;
            }
        }
    }
}
