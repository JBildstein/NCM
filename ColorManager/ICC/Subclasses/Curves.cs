using System;

namespace ColorManager.ICC
{
    /// <summary>
    /// A one dimensional curve
    /// </summary>
    public struct OneDimensionalCurve
    {
        /// <summary>
        /// Breakpoints separate two curve segments
        /// </summary>
        public double[] BreakPoints;
        /// <summary>
        /// An array of curve segments
        /// </summary>
        public CurveSegment[] Segments;

        public OneDimensionalCurve(double[] BreakPoints, CurveSegment[] Segments)
        {
            if (BreakPoints == null) throw new ArgumentNullException(nameof(BreakPoints));
            if (Segments == null) throw new ArgumentNullException(nameof(Segments));
            
            this.BreakPoints = BreakPoints;
            this.Segments = Segments;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="OneDimensionalCurve"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="OneDimensionalCurve"/></param>
        /// <param name="b">The second <see cref="OneDimensionalCurve"/></param>
        /// <returns>True if the <see cref="OneDimensionalCurve"/>s are equal; otherwise, false</returns>
        public static bool operator ==(OneDimensionalCurve a, OneDimensionalCurve b)
        {
            return CMP.Compare(a.BreakPoints, b.BreakPoints)
                && CMP.Compare(a.Segments, b.Segments);
        }

        /// <summary>
        /// Determines whether the specified <see cref="OneDimensionalCurve"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="OneDimensionalCurve"/></param>
        /// <param name="b">The second <see cref="OneDimensionalCurve"/></param>
        /// <returns>True if the <see cref="OneDimensionalCurve"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(OneDimensionalCurve a, OneDimensionalCurve b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="OneDimensionalCurve"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="OneDimensionalCurve"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="OneDimensionalCurve"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is OneDimensionalCurve && this == (OneDimensionalCurve)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="OneDimensionalCurve"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="OneDimensionalCurve"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= CMP.GetHashCode(BreakPoints);
                hash *= CMP.GetHashCode(Segments);
                return hash;
            }
        }
    }

    /// <summary>
    /// A response curve
    /// </summary>
    public struct ResponseCurve
    {
        public CurveMeasurementEncodings CurveType;
        public XYZNumber[] XYZvalues;
        public ResponseNumber[][] ResponseArrays;

        public ResponseCurve(CurveMeasurementEncodings CurveType, XYZNumber[] XYZvalues, ResponseNumber[][] ResponseArrays)
        {
            if (XYZvalues == null) throw new ArgumentNullException(nameof(XYZvalues));
            if (ResponseArrays == null) throw new ArgumentNullException(nameof(ResponseArrays));
            if (XYZvalues.Length != ResponseArrays.Length) throw new ArgumentException("Arrays must have same length");

            this.CurveType = CurveType;
            this.XYZvalues = XYZvalues;
            this.ResponseArrays = ResponseArrays;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ResponseCurve"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseCurve"/></param>
        /// <param name="b">The second <see cref="ResponseCurve"/></param>
        /// <returns>True if the <see cref="ResponseCurve"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ResponseCurve a, ResponseCurve b)
        {
            return a.CurveType == b.CurveType && CMP.Compare(a.XYZvalues, b.XYZvalues)
                && CMP.Compare(a.ResponseArrays, b.ResponseArrays);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ResponseCurve"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ResponseCurve"/></param>
        /// <param name="b">The second <see cref="ResponseCurve"/></param>
        /// <returns>True if the <see cref="ResponseCurve"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ResponseCurve a, ResponseCurve b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ResponseCurve"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ResponseCurve"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ResponseCurve"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ResponseCurve && this == (ResponseCurve)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ResponseCurve"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ResponseCurve"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ CurveType.GetHashCode();
                hash *= CMP.GetHashCode(XYZvalues);
                hash *= CMP.GetHashCode(ResponseArrays);
                return hash;
            }
        }
    }

    /// <summary>
    /// A parametric curve
    /// </summary>
    public struct ParametricCurve
    {
        public ushort type;
        public double g;
        public double a;
        public double b;
        public double c;
        public double d;
        public double e;
        public double f;

        public ParametricCurve(ushort type, double g, double a, double b, double c, double d, double e, double f)
        {
            this.type = type;
            this.g = g;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }

        public ParametricCurve(double g, double a, double b, double c, double d, double e, double f)
            : this(4, g, a, b, c, d, e, f)
        { }

        public ParametricCurve(double g)
            : this(0, g, 0, 0, 0, 0, 0, 0)
        { }

        public ParametricCurve(double g, double a, double b)
            : this(1, g, a, b, 0, 0, 0, 0)
        { }

        public ParametricCurve(double g, double a, double b, double c)
            : this(2, g, a, b, c, 0, 0, 0)
        { }

        public ParametricCurve(double g, double a, double b, double c, double d)
            : this(3, g, a, b, c, d, 0, 0)
        { }
        
        /// <summary>
        /// Determines whether the specified <see cref="ParametricCurve"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ParametricCurve"/></param>
        /// <param name="b">The second <see cref="ParametricCurve"/></param>
        /// <returns>True if the <see cref="ParametricCurve"/>s are equal; otherwise, false</returns>
        public static bool operator ==(ParametricCurve a, ParametricCurve b)
        {
            return a.type == b.type && a.g == b.g && a.a == b.a
                && a.b == b.b && a.c == b.c && a.d == b.d && a.e == b.e && a.f == b.f;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ParametricCurve"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="ParametricCurve"/></param>
        /// <param name="b">The second <see cref="ParametricCurve"/></param>
        /// <returns>True if the <see cref="ParametricCurve"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(ParametricCurve a, ParametricCurve b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ParametricCurve"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ParametricCurve"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="ParametricCurve"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ParametricCurve && this == (ParametricCurve)obj;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ParametricCurve"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ParametricCurve"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ type.GetHashCode();
                hash *= 16777619 ^ g.GetHashCode();
                hash *= 16777619 ^ a.GetHashCode();
                hash *= 16777619 ^ b.GetHashCode();
                hash *= 16777619 ^ c.GetHashCode();
                hash *= 16777619 ^ d.GetHashCode();
                hash *= 16777619 ^ e.GetHashCode();
                hash *= 16777619 ^ f.GetHashCode();
                return hash;
            }
        }
    }


    /// <summary>
    /// A segment of a curve
    /// </summary>
    public abstract class CurveSegment
    {
        /// <summary>
        /// The signature of this segment
        /// </summary>
        public CurveSegmentSignature Signature
        {
            get { return _Signature; }
        }
        private CurveSegmentSignature _Signature;

        protected CurveSegment(CurveSegmentSignature Signature)
        {
            _Signature = Signature;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="CurveSegment"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CurveSegment"/></param>
        /// <param name="b">The second <see cref="CurveSegment"/></param>
        /// <returns>True if the <see cref="CurveSegment"/>s are equal; otherwise, false</returns>
        public static bool operator ==(CurveSegment a, CurveSegment b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature;
        }

        /// <summary>
        /// Determines whether the specified <see cref="CurveSegment"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="CurveSegment"/></param>
        /// <param name="b">The second <see cref="CurveSegment"/></param>
        /// <returns>True if the <see cref="CurveSegment"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(CurveSegment a, CurveSegment b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="CurveSegment"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="CurveSegment"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="CurveSegment"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            CurveSegment c = obj as CurveSegment;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="CurveSegment"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="CurveSegment"/></returns>
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
    /// A formula based curve segment
    /// </summary>
    public sealed class FormulaCurveElement : CurveSegment
    {
        public readonly ushort type;
        public readonly double gamma;
        public readonly double a;
        public readonly double b;
        public readonly double c;
        public readonly double d;
        public readonly double e;

        public FormulaCurveElement(ushort type, double gamma, double a, double b, double c, double d, double e)
            : base(CurveSegmentSignature.FormulaCurve)
        {
            this.type = type;
            this.gamma = gamma;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="FormulaCurveElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="FormulaCurveElement"/></param>
        /// <param name="b">The second <see cref="FormulaCurveElement"/></param>
        /// <returns>True if the <see cref="FormulaCurveElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(FormulaCurveElement a, FormulaCurveElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && a.type == b.type && a.gamma == b.gamma && a.a == b.a
                && a.b == b.b && a.c == b.c && a.d == b.d && a.e == b.e;
        }

        /// <summary>
        /// Determines whether the specified <see cref="FormulaCurveElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="FormulaCurveElement"/></param>
        /// <param name="b">The second <see cref="FormulaCurveElement"/></param>
        /// <returns>True if the <see cref="FormulaCurveElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(FormulaCurveElement a, FormulaCurveElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="FormulaCurveElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="FormulaCurveElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="FormulaCurveElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            FormulaCurveElement c = obj as FormulaCurveElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="FormulaCurveElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="FormulaCurveElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= 16777619 ^ type.GetHashCode();
                hash *= 16777619 ^ gamma.GetHashCode();
                hash *= 16777619 ^ a.GetHashCode();
                hash *= 16777619 ^ b.GetHashCode();
                hash *= 16777619 ^ c.GetHashCode();
                hash *= 16777619 ^ d.GetHashCode();
                hash *= 16777619 ^ e.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// A sampled curve segment
    /// </summary>
    public sealed class SampledCurveElement : CurveSegment
    {
        /// <summary>
        /// The curve entries
        /// </summary>
        public double[] CurveEntries
        {
            get { return _CurveEntries; }
        }
        private double[] _CurveEntries;

        public SampledCurveElement(double[] CurveEntries)
            : base(CurveSegmentSignature.SampledCurve)
        {
            if (CurveEntries == null) throw new ArgumentNullException(nameof(CurveEntries));
            _CurveEntries = CurveEntries;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="SampledCurveElement"/>s are equal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="SampledCurveElement"/></param>
        /// <param name="b">The second <see cref="SampledCurveElement"/></param>
        /// <returns>True if the <see cref="SampledCurveElement"/>s are equal; otherwise, false</returns>
        public static bool operator ==(SampledCurveElement a, SampledCurveElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Signature == b.Signature && CMP.Compare(a.CurveEntries, b.CurveEntries);
        }

        /// <summary>
        /// Determines whether the specified <see cref="SampledCurveElement"/>s are unequal to each other.
        /// </summary>
        /// <param name="a">The first <see cref="SampledCurveElement"/></param>
        /// <param name="b">The second <see cref="SampledCurveElement"/></param>
        /// <returns>True if the <see cref="SampledCurveElement"/>s are unequal; otherwise, false</returns>
        public static bool operator !=(SampledCurveElement a, SampledCurveElement b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="SampledCurveElement"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="SampledCurveElement"/></param>
        /// <returns>true if the specified <see cref="object"/> is equal to the current <see cref="SampledCurveElement"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            SampledCurveElement c = obj as SampledCurveElement;
            if ((object)c == null) return false;
            return c == this;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="SampledCurveElement"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="SampledCurveElement"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Signature.GetHashCode();
                hash *= CMP.GetHashCode(CurveEntries);
                return hash;
            }
        }
    }
}
