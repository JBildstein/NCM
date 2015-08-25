using System;
using ColorManager.Conversion;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of a Gray colorspace
    /// </summary>
    public unsafe class ColorspaceGray : Colorspace
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public override string Name
        {
            get { return string.Concat("Gray ", RefWhite.Name); }
        }
        /// <summary>
        /// The gamma value
        /// </summary>
        public double Gamma
        {
            get { return _Gamma; }
        }
        /// <summary>
        /// Field for the <see cref="Gamma"/> property
        /// </summary>
        protected double _Gamma;
        /// <summary>
        /// Precalculated inverse gamma value
        /// </summary>
        protected double _Gamma1;

        /// <summary>
        /// The default colorspace
        /// </summary>
        public static new ColorspaceGray Default
        {
            get { return _Default; }
            set { if (value != null) _Default = value; }
        }
        private static ColorspaceGray _Default = new ColorspaceGray(Whitepoint.Default);

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceGray"/> class
        /// with a gamma value of 1.0 and the <see cref="Whitepoint.Default"/> reference white
        /// </summary>
        public ColorspaceGray()
            : this(Whitepoint.Default, 1.0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceGray"/> class
        /// with the <see cref="Whitepoint.Default"/> reference white
        /// </summary>
        /// <param name="gamma">The gamma value of this colorspace</param>
        public ColorspaceGray(double gamma)
            : this(Whitepoint.Default, gamma)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceGray"/> class
        /// with a gamma value of 1.0
        /// </summary>
        /// <param name="wp">The reference white of this colorspace</param>
        public ColorspaceGray(Whitepoint wp)
            : this(wp, 1.0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceGray"/> class
        /// </summary>
        /// <param name="wp">The reference white of this colorspace</param>
        /// <param name="gamma">The gamma value of this colorspace</param>
        public ColorspaceGray(Whitepoint wp, double gamma)
            : base(wp)
        {
            _Gamma = gamma;
            _Gamma1 = 1d / gamma;
        }

        /// <summary>
        /// Returns a custom delegate to transform a color
        /// </summary>
        /// <param name="IsInput">True if used for the input color, false otherwise</param>
        /// <returns>A custom delegate to transform a color</returns>
        public override TransformToDelegate GetTransformation(bool IsInput)
        {
            if (IsInput) return ToLinear;
            else return ToNonLinear;
        }

        /// <summary>
        /// Linearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to non-Linear input values</param>
        /// <param name="outVal">Pointer to linear output values</param>
        protected virtual void ToLinear(double* inVal, double* outVal)
        {
            *outVal = Math.Pow(*inVal, _Gamma);
        }

        /// <summary>
        /// Delinearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to linear input values</param>
        /// <param name="outVal">Pointer to non-Linear output values</param>
        protected virtual void ToNonLinear(double* inVal, double* outVal)
        {
            *outVal = Math.Pow(*inVal, _Gamma1);
        }


        /// <summary>
        /// Compares two colorspaces for their equality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(ColorspaceGray a, ColorspaceGray b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.RefWhite == b.RefWhite && a.Gamma == b.Gamma;
        }
        /// <summary>
        /// Compares two colorspaces for their inequality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(ColorspaceGray a, ColorspaceGray b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Compares this colorspace with another for their equality of values
        /// </summary>
        /// <param name="obj">The colorspace to compare to</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            ColorspaceGray c = obj as ColorspaceGray;
            if ((object)c == null) return base.Equals(obj);
            return this == c;
        }
        /// <summary>
        /// Calculates a hash code of this colorspace
        /// </summary>
        /// <returns>The hash code of this colorspace</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ RefWhite.GetHashCode();
                hash *= 16777619 ^ Gamma.GetHashCode();
                return hash;
            }
        }
    }
}
