using System;

namespace ColorManager.ICC
{
    public sealed class CLUT
    {
        public double[][] Values
        {
            get { return _Values; }
        }
        private double[][] _Values;

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


        private CLUT(int inChCount, int outChCount, byte[] gridPointCount)
        {
            if (gridPointCount == null) throw new ArgumentNullException(nameof(gridPointCount));

            _InputChannelCount = inChCount;
            _OutputChannelCount = outChCount;
            _GridPointCount = gridPointCount;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CLUT"/> class
        /// </summary>
        /// <param name="values">The CLUT values</param>
        /// <param name="inChCount">The input channel count</param>
        /// <param name="outChCount">The output channel count</param>
        /// <param name="gridPointCount">The gridpoint count</param>
        public CLUT(double[][] values, int inChCount, int outChCount, byte[] gridPointCount)
            :this(inChCount, outChCount, gridPointCount)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            _Values = values;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CLUT"/> class
        /// </summary>
        /// <param name="values">The CLUT values</param>
        /// <param name="inChCount">The input channel count</param>
        /// <param name="outChCount">The output channel count</param>
        /// <param name="gridPointCount">The gridpoint count</param>
        public CLUT(ushort[][] values, int inChCount, int outChCount, byte[] gridPointCount)
            : this(inChCount, outChCount, gridPointCount)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = ushort.MaxValue;

            _Values = new double[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                _Values[i] = new double[values[i].Length];
                for (int j = 0; j < values[i].Length; j++) _Values[i][j] = values[i][j] / max;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CLUT"/> class
        /// </summary>
        /// <param name="values">The CLUT values</param>
        /// <param name="inChCount">The input channel count</param>
        /// <param name="outChCount">The output channel count</param>
        /// <param name="gridPointCount">The gridpoint count</param>
        public CLUT(byte[][] values, int inChCount, int outChCount, byte[] gridPointCount)
            : this(inChCount, outChCount, gridPointCount)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = byte.MaxValue;

            _Values = new double[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                _Values[i] = new double[values[i].Length];
                for (int j = 0; j < values[i].Length; j++) _Values[i][j] = values[i][j] / max;
            }
        }


        /// <summary>
        /// Determines whether the specified <see cref="CLUT"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUT"/></param>
        /// <param name="b">The second <see cref="CLUT"/></param>
        /// <returns>True if the <see cref="CLUT"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CLUT a, CLUT b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.InputChannelCount == b.InputChannelCount && a.OutputChannelCount == b.OutputChannelCount
                && CMP.Compare(a.GridPointCount, b.GridPointCount) && CMP.Compare(a.Values, b.Values);
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
                hash *= CMP.GetHashCode(Values);
                return hash;
            }
        }
    }
    
    public sealed class LUT
    {
        public double[] Values
        {
            get { return _Values; }
        }
        private double[] _Values;

        /// <summary>
        /// Creates a new instance of the <see cref="LUT"/> class
        /// </summary>
        /// <param name="values">The LUT values</param>
        public LUT(double[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            _Values = values;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LUT"/> class
        /// </summary>
        /// <param name="values">The LUT values</param>
        public LUT(ushort[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = ushort.MaxValue;

            _Values = new double[values.Length];
            for (int i = 0; i < values.Length; i++) _Values[i] = values[i] / max;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LUT"/> class
        /// </summary>
        /// <param name="values">The LUT values</param>
        public LUT(byte[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = byte.MaxValue;

            _Values = new double[values.Length];
            for (int i = 0; i < values.Length; i++) _Values[i] = values[i] / max;
        }

        /// <summary>
        /// Determines whether the specified <see cref="LUT"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT"/></param>
        /// <param name="b">The second <see cref="LUT"/></param>
        /// <returns>True if the <see cref="LUT"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LUT a, LUT b)
        {
            return CMP.Compare(a.Values, b.Values);
        }

        /// <summary>
        /// Determines whether the specified <see cref="LUT"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT"/></param>
        /// <param name="b">The second <see cref="LUT"/></param>
        /// <returns>True if the <see cref="LUT"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(LUT a, LUT b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="LUT"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="LUT"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="LUT"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is LUT && this == (LUT)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="LUT"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="LUT"/></returns>
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
