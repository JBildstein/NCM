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
        public MultiProcessElementSignature Signature
        {
            get { return _Signature; }
        }
        /// <summary>
        /// Number of input channels
        /// </summary>
        public int InputChannelCount
        {
            get { return _InputChannelCount; }
        }
        /// <summary>
        /// Number of output channels
        /// </summary>
        public int OutputChannelCount
        {
            get { return _OutputChannelCount; }
        }

        private MultiProcessElementSignature _Signature;
        private int _InputChannelCount;
        private int _OutputChannelCount;

        protected MultiProcessElement(MultiProcessElementSignature Signature, int inChCount, int outChCount)
        {
            this._Signature = Signature;
            this._InputChannelCount = inChCount;
            this._OutputChannelCount = outChCount;
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
        public OneDimensionalCurve[] Curves
        {
            get { return _Curves; }
        }
        private OneDimensionalCurve[] _Curves;

        public CurveSetProcessElement(int inChCount, int outChCount, OneDimensionalCurve[] curves)
            : base(MultiProcessElementSignature.CurveSet, inChCount, outChCount)
        {
            if (curves == null) throw new ArgumentNullException("curves");
            this._Curves = curves;
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
        public double[,] MatrixIxO
        {
            get { return _MatrixIxO; }
        }
        public double[] MatrixOx1
        {
            get { return _MatrixOx1; }
        }

        private double[,] _MatrixIxO;
        private double[] _MatrixOx1;

        public MatrixProcessElement(int inChCount, int outChCount, double[,] MatrixIxO, double[] MatrixOx1)
            : base(MultiProcessElementSignature.Matrix, inChCount, outChCount)
        {
            if (MatrixIxO == null) throw new ArgumentNullException("MatrixIxO");
            if (MatrixOx1 == null) throw new ArgumentNullException("MatrixOx1");

            this._MatrixIxO = MatrixIxO;
            this._MatrixOx1 = MatrixOx1;
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
        public CLUT CLUTValue
        {
            get { return _CLUTValue; }
        }
        private CLUT _CLUTValue;

        public CLUTProcessElement(int inChCount, int outChCount, CLUT CLUTValue)
            : base(MultiProcessElementSignature.CLUT, inChCount, outChCount)
        {
            if (CLUTValue == null) throw new ArgumentNullException("CLUTValue");
            this._CLUTValue = CLUTValue;
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
}
