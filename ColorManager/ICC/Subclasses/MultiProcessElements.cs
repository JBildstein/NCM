using System;

namespace ColorManager.ICC
{
    /// <summary>
    /// An element to process data
    /// </summary>
    public abstract class MultiProcessElement
    {
        /// <summary>
        /// The signature of this element
        /// </summary>
        public readonly MultiProcessElementSignature Signature;
        /// <summary>
        /// Number of input channels
        /// </summary>
        public readonly int InputChannelCount;
        /// <summary>
        /// Number of output channels
        /// </summary>
        public readonly int OutputChannelCount;

        /// <summary>
        /// Creates a new instance of the <see cref="MultiProcessElement"/> class
        /// </summary>
        /// <param name="Signature">The signature of this element</param>
        /// <param name="inChCount">Number of input channels</param>
        /// <param name="outChCount">Number of output channels</param>
        protected MultiProcessElement(MultiProcessElementSignature Signature, int inChCount, int outChCount)
        {
            if (!Enum.IsDefined(typeof(MultiProcessElementSignature), Signature))
                throw new ArgumentException($"{nameof(Signature)} value is not of a defined Enum value");
            if (inChCount < 1 || inChCount > 15)
                throw new ArgumentOutOfRangeException("Input channel count must be in the range of 1-15");
            if (outChCount < 1 || outChCount > 15)
                throw new ArgumentOutOfRangeException("Output channel count must be in the range of 1-15");

            this.Signature = Signature;
            InputChannelCount = inChCount;
            OutputChannelCount = outChCount;
        }

        /// <summary>
        /// Determines whether the specified <see cref="MultiProcessElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MultiProcessElement"/></param>
        /// <param name="b">The second <see cref="MultiProcessElement"/></param>
        /// <returns>True if the <see cref="MultiProcessElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(MultiProcessElement a, MultiProcessElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount;
        }

        /// <summary>
        /// Determines whether the specified <see cref="MultiProcessElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MultiProcessElement"/></param>
        /// <param name="b">The second <see cref="MultiProcessElement"/></param>
        /// <returns>True if the <see cref="MultiProcessElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(MultiProcessElement a, MultiProcessElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="MultiProcessElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="MultiProcessElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="MultiProcessElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MultiProcessElement c = obj as MultiProcessElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="MultiProcessElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="MultiProcessElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// A set of curves to process data
    /// </summary>
    public sealed class CurveSetProcessElement : MultiProcessElement
    {
        /// <summary>
        /// An array with one dimensional curves
        /// </summary>
        public readonly OneDimensionalCurve[] Curves;

        /// <summary>
        /// Creates a new instance of the <see cref="CurveSetProcessElement"/> class
        /// </summary>
        /// <param name="Curves">An array with one dimensional curves</param>
        public CurveSetProcessElement(OneDimensionalCurve[] Curves)
            : base(MultiProcessElementSignature.CurveSet, Curves?.Length ?? 1, Curves?.Length ?? 1)
        {
            if (Curves == null) throw new ArgumentNullException(nameof(Curves));
            this.Curves = Curves;
        }

        /// <summary>
        /// Determines whether the specified <see cref="CurveSetProcessElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CurveSetProcessElement"/></param>
        /// <param name="b">The second <see cref="CurveSetProcessElement"/></param>
        /// <returns>True if the <see cref="CurveSetProcessElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CurveSetProcessElement a, CurveSetProcessElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount && CMP.Compare(a.Curves, b.Curves);
        }

        /// <summary>
        /// Determines whether the specified <see cref="CurveSetProcessElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CurveSetProcessElement"/></param>
        /// <param name="b">The second <see cref="CurveSetProcessElement"/></param>
        /// <returns>True if the <see cref="CurveSetProcessElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CurveSetProcessElement a, CurveSetProcessElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CurveSetProcessElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CurveSetProcessElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CurveSetProcessElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CurveSetProcessElement c = obj as CurveSetProcessElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CurveSetProcessElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CurveSetProcessElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= CMP.GetHashCode(Curves);
                return hash;
            }
        }
    }

    /// <summary>
    /// A matrix element to process data
    /// </summary>
    public sealed class MatrixProcessElement : MultiProcessElement
    {
        /// <summary>
        /// Two dimensional matrix with size of Input-Channels x Output-Channels
        /// </summary>
        public readonly double[,] MatrixIxO;
        /// <summary>
        /// One dimensional matrix with size of Output-Channels x 1
        /// </summary>
        public readonly double[] MatrixOx1;

        /// <summary>
        /// Creates a new instance of the <see cref="MatrixProcessElement"/> class
        /// </summary>
        /// <param name="MatrixIxO">Two dimensional matrix with size of Input-Channels x Output-Channels</param>
        /// <param name="MatrixOx1">One dimensional matrix with size of Output-Channels x 1</param>
        public MatrixProcessElement(double[,] MatrixIxO, double[] MatrixOx1)
            : base(MultiProcessElementSignature.Matrix, MatrixIxO?.GetLength(0) ?? 1, MatrixIxO?.GetLength(1) ?? 1)
        {
            if (MatrixIxO == null) throw new ArgumentNullException(nameof(MatrixIxO));
            if (MatrixOx1 == null) throw new ArgumentNullException(nameof(MatrixOx1));
            if (MatrixIxO.GetLength(1) != MatrixOx1.Length)
                throw new ArgumentException($"Second dimension if {MatrixIxO} must be the same length as {nameof(MatrixOx1)}");

            this.MatrixIxO = MatrixIxO;
            this.MatrixOx1 = MatrixOx1;
        }

        /// <summary>
        /// Determines whether the specified <see cref="MatrixProcessElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MatrixProcessElement"/></param>
        /// <param name="b">The second <see cref="MatrixProcessElement"/></param>
        /// <returns>True if the <see cref="MatrixProcessElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(MatrixProcessElement a, MatrixProcessElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount && CMP.Compare(a.MatrixIxO, b.MatrixIxO)
                && CMP.Compare(a.MatrixOx1, b.MatrixOx1);
        }

        /// <summary>
        /// Determines whether the specified <see cref="MatrixProcessElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="MatrixProcessElement"/></param>
        /// <param name="b">The second <see cref="MatrixProcessElement"/></param>
        /// <returns>True if the <see cref="MatrixProcessElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(MatrixProcessElement a, MatrixProcessElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="MatrixProcessElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="MatrixProcessElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="MatrixProcessElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MatrixProcessElement c = obj as MatrixProcessElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="MatrixProcessElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="MatrixProcessElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= 16777619 ^ MatrixIxO.GetHashCode();
                hash *= 16777619 ^ MatrixOx1.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// An CLUT (Color Look-Up-Table) element to process data
    /// </summary>
    public sealed class CLUTProcessElement : MultiProcessElement
    {
        /// <summary>
        /// The color lookup table of this element
        /// </summary>
        public readonly CLUT CLUTValue;

        /// <summary>
        /// Creates a new instance of the <see cref="CurveSetProcessElement"/> class
        /// </summary>
        /// <param name="CLUTValue">The color lookup table of this element</param>
        public CLUTProcessElement(CLUT CLUTValue)
            : base(MultiProcessElementSignature.CLUT, CLUTValue?.InputChannelCount ?? 1, CLUTValue?.OutputChannelCount ?? 1)
        {
            if (CLUTValue == null) throw new ArgumentNullException(nameof(CLUTValue));
            this.CLUTValue = CLUTValue;
        }

        /// <summary>
        /// Determines whether the specified <see cref="CLUTProcessElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUTProcessElement"/></param>
        /// <param name="b">The second <see cref="CLUTProcessElement"/></param>
        /// <returns>True if the <see cref="CLUTProcessElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CLUTProcessElement a, CLUTProcessElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount && a.CLUTValue == b.CLUTValue;
        }

        /// <summary>
        /// Determines whether the specified <see cref="CLUTProcessElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CLUTProcessElement"/></param>
        /// <param name="b">The second <see cref="CLUTProcessElement"/></param>
        /// <returns>True if the <see cref="CLUTProcessElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CLUTProcessElement a, CLUTProcessElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CLUTProcessElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CLUTProcessElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CLUTProcessElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CLUTProcessElement c = obj as CLUTProcessElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CLUTProcessElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CLUTProcessElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                hash *= 16777619 ^ CLUTValue.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// A placeholder <see cref="MultiProcessElement"/> (might be used for future ICC versions)
    /// </summary>
    public sealed class bACSProcessElement : MultiProcessElement
    {
        /// <summary>
        /// Creates a new instance of the <see cref="eACSProcessElement"/> class
        /// </summary>
        /// <param name="inChCount">Number of input channels</param>
        /// <param name="outChCount">Number of output channels</param>
        public bACSProcessElement(int inChCount, int outChCount)
            : base(MultiProcessElementSignature.bACS, inChCount, outChCount)
        { }

        /// <summary>
        /// Determines whether the specified <see cref="bACSProcessElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="bACSProcessElement"/></param>
        /// <param name="b">The second <see cref="bACSProcessElement"/></param>
        /// <returns>True if the <see cref="bACSProcessElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(bACSProcessElement a, bACSProcessElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount;
        }

        /// <summary>
        /// Determines whether the specified <see cref="bACSProcessElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="bACSProcessElement"/></param>
        /// <param name="b">The second <see cref="bACSProcessElement"/></param>
        /// <returns>True if the <see cref="bACSProcessElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(bACSProcessElement a, bACSProcessElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="bACSProcessElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="bACSProcessElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="bACSProcessElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            bACSProcessElement c = obj as bACSProcessElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="bACSProcessElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="bACSProcessElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// A placeholder <see cref="MultiProcessElement"/> (might be used for future ICC versions)
    /// </summary>
    public sealed class eACSProcessElement : MultiProcessElement
    {
        /// <summary>
        /// Creates a new instance of the <see cref="eACSProcessElement"/> class
        /// </summary>
        /// <param name="inChCount">Number of input channels</param>
        /// <param name="outChCount">Number of output channels</param>
        public eACSProcessElement(int inChCount, int outChCount)
            : base(MultiProcessElementSignature.eACS, inChCount, outChCount)
        { }

        /// <summary>
        /// Determines whether the specified <see cref="eACSProcessElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="eACSProcessElement"/></param>
        /// <param name="b">The second <see cref="eACSProcessElement"/></param>
        /// <returns>True if the <see cref="eACSProcessElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(eACSProcessElement a, eACSProcessElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.InputChannelCount == b.InputChannelCount
                && a.OutputChannelCount == b.OutputChannelCount;
        }

        /// <summary>
        /// Determines whether the specified <see cref="eACSProcessElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="eACSProcessElement"/></param>
        /// <param name="b">The second <see cref="eACSProcessElement"/></param>
        /// <returns>True if the <see cref="eACSProcessElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(eACSProcessElement a, eACSProcessElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="eACSProcessElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="eACSProcessElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="eACSProcessElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            eACSProcessElement c = obj as eACSProcessElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="eACSProcessElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="eACSProcessElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ InputChannelCount.GetHashCode();
                hash *= 16777619 ^ OutputChannelCount.GetHashCode();
                return hash;
            }
        }
    }
}
