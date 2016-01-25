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
        public readonly double[] BreakPoints;
        /// <summary>
        /// An array of curve segments
        /// </summary>
        public readonly CurveSegment[] Segments;

        /// <summary>
        /// Creates a new instance of the <see cref="OneDimensionalCurve"/> struct
        /// </summary>
        /// <param name="BreakPoints">The break points of this curve</param>
        /// <param name="Segments">The segments of this curve</param>
        public OneDimensionalCurve(double[] BreakPoints, CurveSegment[] Segments)
        {
            if (BreakPoints == null) throw new ArgumentNullException(nameof(BreakPoints));
            if (Segments == null) throw new ArgumentNullException(nameof(Segments));
            if (BreakPoints.Length != Segments.Length - 1) throw new ArgumentException("Number of BreakPoints must be one less than number of Segments");

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
        /// <summary>
        /// The type of this curve
        /// </summary>
        public readonly CurveMeasurementEncodings CurveType;
        /// <summary>
        /// 
        /// </summary>
        public readonly XYZNumber[] XYZvalues;
        /// <summary>
        /// 
        /// </summary>
        public readonly ResponseNumber[][] ResponseArrays;

        /// <summary>
        /// Creates a new instance of the <see cref="ResponseCurve"/> struct
        /// </summary>
        /// <param name="CurveType">The type of this curve</param>
        /// <param name="XYZvalues"></param>
        /// <param name="ResponseArrays"></param>
        public ResponseCurve(CurveMeasurementEncodings CurveType, XYZNumber[] XYZvalues, ResponseNumber[][] ResponseArrays)
        {
            if (XYZvalues == null) throw new ArgumentNullException(nameof(XYZvalues));
            if (ResponseArrays == null) throw new ArgumentNullException(nameof(ResponseArrays));
            if (XYZvalues.Length != ResponseArrays.Length) throw new ArgumentException("Arrays must have same length");
            if (XYZvalues.Length < 1 || XYZvalues.Length > 15) throw new ArgumentException("Arrays length must be in the range of 1-15");

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
        /// <summary>
        /// The type of this curve
        /// </summary>
        public readonly ushort type;
        /// <summary>
        /// G curve parameter
        /// </summary>
        public readonly double g;
        /// <summary>
        /// A curve parameter
        /// </summary>
        public readonly double a;
        /// <summary>
        /// B curve parameter
        /// </summary>
        public readonly double b;
        /// <summary>
        /// C curve parameter
        /// </summary>
        public readonly double c;
        /// <summary>
        /// D curve parameter
        /// </summary>
        public readonly double d;
        /// <summary>
        /// E curve parameter
        /// </summary>
        public readonly double e;
        /// <summary>
        /// F curve parameter
        /// </summary>
        public readonly double f;

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurve"/> struct with curve type 0
        /// </summary>
        /// <param name="g">G curve parameter</param>
        public ParametricCurve(double g)
            : this(0, g, 0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurve"/> struct with curve type 1
        /// </summary>
        /// <param name="g">G curve parameter</param>
        /// <param name="a">A curve parameter</param>
        /// <param name="b">B curve parameter</param>
        public ParametricCurve(double g, double a, double b)
            : this(1, g, a, b, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurve"/> struct with curve type 2
        /// </summary>
        /// <param name="g">G curve parameter</param>
        /// <param name="a">A curve parameter</param>
        /// <param name="b">B curve parameter</param>
        /// <param name="c">C curve parameter</param>
        public ParametricCurve(double g, double a, double b, double c)
            : this(2, g, a, b, c, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurve"/> struct with curve type 3
        /// </summary>
        /// <param name="g">G curve parameter</param>
        /// <param name="a">A curve parameter</param>
        /// <param name="b">B curve parameter</param>
        /// <param name="c">C curve parameter</param>
        /// <param name="d">D curve parameter</param>
        public ParametricCurve(double g, double a, double b, double c, double d)
            : this(3, g, a, b, c, d, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurve"/> struct with curve type 4
        /// </summary>
        /// <param name="g">G curve parameter</param>
        /// <param name="a">A curve parameter</param>
        /// <param name="b">B curve parameter</param>
        /// <param name="c">C curve parameter</param>
        /// <param name="d">D curve parameter</param>
        /// <param name="e">E curve parameter</param>
        /// <param name="f">F curve parameter</param>
        public ParametricCurve(double g, double a, double b, double c, double d, double e, double f)
            : this(4, g, a, b, c, d, e, f)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ParametricCurve"/> struct
        /// </summary>
        /// <param name="type">The type of this curve (0-4)</param>
        /// <param name="g">G curve parameter</param>
        /// <param name="a">A curve parameter</param>
        /// <param name="b">B curve parameter</param>
        /// <param name="c">C curve parameter</param>
        /// <param name="d">D curve parameter</param>
        /// <param name="e">E curve parameter</param>
        /// <param name="f">F curve parameter</param>
        private ParametricCurve(ushort type, double g, double a, double b, double c, double d, double e, double f)
        {
            if (type > 4) throw new ArgumentOutOfRangeException(nameof(type), "Type must be between 0 and 4");

            this.type = type;
            this.g = g;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }


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
        public readonly CurveSegmentSignature Signature;

        /// <summary>
        /// Creates a new instance of the <see cref="CurveSegment"/> class
        /// </summary>
        /// <param name="Signature">The signature of this segment</param>
        protected CurveSegment(CurveSegmentSignature Signature)
        {
            if (!Enum.IsDefined(typeof(CurveSegmentSignature), Signature))
                throw new ArgumentException($"{nameof(Signature)} value is not of a defined Enum value");

            this.Signature = Signature;
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
        /// <summary>
        /// The type of this curve
        /// </summary>
        public readonly ushort type;
        /// <summary>
        /// Gamma curve parameter
        /// </summary>
        public readonly double gamma;
        /// <summary>
        /// A curve parameter
        /// </summary>
        public readonly double a;
        /// <summary>
        /// B curve parameter
        /// </summary>
        public readonly double b;
        /// <summary>
        /// C curve parameter
        /// </summary>
        public readonly double c;
        /// <summary>
        /// D curve parameter
        /// </summary>
        public readonly double d;
        /// <summary>
        /// E curve parameter
        /// </summary>
        public readonly double e;

        /// <summary>
        /// Creates a new instance of the <see cref="FormulaCurveElement"/> class
        /// </summary>
        /// <param name="type">The type of this segment (0-2)</param>
        /// <param name="gamma">Gamma segment parameter</param>
        /// <param name="a">A segment parameter</param>
        /// <param name="b">B segment parameter</param>
        /// <param name="c">C segment parameter</param>
        /// <param name="d">D segment parameter</param>
        /// <param name="e">E segment parameter</param>
        public FormulaCurveElement(ushort type, double gamma, double a, double b, double c, double d, double e)
            : base(CurveSegmentSignature.FormulaCurve)
        {
            if (type > 2) throw new ArgumentOutOfRangeException(nameof(type), "Type must be between 0 and 2");

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
        /// The curve values of this segment
        /// </summary>
        public readonly double[] CurveEntries;

        /// <summary>
        /// Creates a new instance of the <see cref="SampledCurveElement"/> class
        /// </summary>
        /// <param name="CurveEntries">The curve values of this segment</param>
        public SampledCurveElement(double[] CurveEntries)
            : base(CurveSegmentSignature.SampledCurve)
        {
            if (CurveEntries == null) throw new ArgumentNullException(nameof(CurveEntries));
            if (CurveEntries.Length < 1) throw new ArgumentOutOfRangeException(nameof(CurveEntries), "There must be at least one value"); ;

            this.CurveEntries = CurveEntries;
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
