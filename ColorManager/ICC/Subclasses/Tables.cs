using System;

namespace ColorManager.ICC
{
    public sealed class CLUT
    {
        public readonly double[][] Values;
        public readonly CLUTDataType DataType;
        public readonly int InputChannelCount;
        public readonly int OutputChannelCount;
        public readonly byte[] GridPointCount;

        /// <summary>
        /// Creates a new instance of the <see cref="CLUT"/> class
        /// </summary>
        /// <param name="values">The CLUT values</param>
        /// <param name="inChCount">The input channel count</param>
        /// <param name="outChCount">The output channel count</param>
        /// <param name="gridPointCount">The gridpoint count</param>
        /// <param name="type">The data type of this CLUT</param>
        public CLUT(double[][] values, byte[] gridPointCount, CLUTDataType type)
            : this(gridPointCount?.Length ?? 1, values?[0]?.Length ?? 1, gridPointCount, type)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            Values = values;
            CheckValues();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CLUT"/> class
        /// </summary>
        /// <param name="values">The CLUT values</param>
        /// <param name="inChCount">The input channel count</param>
        /// <param name="outChCount">The output channel count</param>
        /// <param name="gridPointCount">The gridpoint count</param>
        public CLUT(ushort[][] values, byte[] gridPointCount)
            : this(gridPointCount?.Length ?? 1, values?[0]?.Length ?? 1, gridPointCount, CLUTDataType.UInt16)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = ushort.MaxValue;

            Values = new double[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                Values[i] = new double[values[i].Length];
                for (int j = 0; j < values[i].Length; j++) Values[i][j] = values[i][j] / max;
            }

            CheckValues();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CLUT"/> class
        /// </summary>
        /// <param name="values">The CLUT values</param>
        /// <param name="inChCount">The input channel count</param>
        /// <param name="outChCount">The output channel count</param>
        /// <param name="gridPointCount">The gridpoint count</param>
        public CLUT(byte[][] values, byte[] gridPointCount)
            : this(gridPointCount?.Length ?? 1, values?[0]?.Length ?? 1, gridPointCount, CLUTDataType.UInt8)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = byte.MaxValue;

            Values = new double[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                Values[i] = new double[values[i].Length];
                for (int j = 0; j < values[i].Length; j++) Values[i][j] = values[i][j] / max;
            }

            CheckValues();
        }

        private CLUT(int inChCount, int outChCount, byte[] gridPointCount, CLUTDataType type)
        {
            if (gridPointCount == null) throw new ArgumentNullException(nameof(gridPointCount));
            if (!Enum.IsDefined(typeof(CLUTDataType), type)) throw new ArgumentException($"{nameof(type)} value is not of a defined Enum value");
            if (inChCount < 1 || inChCount > 15) throw new ArgumentOutOfRangeException("Input channel count must be in the range of 1-15");
            if (outChCount < 1 || outChCount > 15) throw new ArgumentOutOfRangeException("Output channel count must be in the range of 1-15");

            DataType = type;
            InputChannelCount = inChCount;
            OutputChannelCount = outChCount;
            GridPointCount = gridPointCount;
        }

        private void CheckValues()
        {
            int length = 0;
            for (int i = 0; i < InputChannelCount; i++) { length += (int)Math.Pow(GridPointCount[i], InputChannelCount); }
            length /= InputChannelCount;

            if (Values.Length != length) throw new ArgumentException("Length of values array does not match");
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
        public readonly double[] Values;

        /// <summary>
        /// Creates a new instance of the <see cref="LUT"/> class
        /// </summary>
        /// <param name="values">The LUT values</param>
        public LUT(double[] Values)
        {
            if (Values == null) throw new ArgumentNullException(nameof(Values));
            this.Values = Values;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LUT"/> class
        /// </summary>
        /// <param name="values">The LUT values</param>
        public LUT(ushort[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = ushort.MaxValue;

            this.Values = new double[values.Length];
            for (int i = 0; i < values.Length; i++) this.Values[i] = values[i] / max;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LUT"/> class
        /// </summary>
        /// <param name="values">The LUT values</param>
        public LUT(byte[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            const double max = byte.MaxValue;

            this.Values = new double[values.Length];
            for (int i = 0; i < values.Length; i++) this.Values[i] = values[i] / max;
        }


        /// <summary>
        /// Determines whether the specified <see cref="LUT"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="LUT"/></param>
        /// <param name="b">The second <see cref="LUT"/></param>
        /// <returns>True if the <see cref="LUT"/>s are equal; otherwise, false</returns>
        public static bool operator ==(LUT a, LUT b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;

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
            LUT c = obj as LUT;
            if ((object)c == null) return false;
            return c == this;
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
