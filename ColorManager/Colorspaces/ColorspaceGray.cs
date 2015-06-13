using System;
using ColorManager.Conversion;

namespace ColorManager
{
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


        public ColorspaceGray()
            : this(Whitepoint.Default, 1.0)
        { }

        public ColorspaceGray(Whitepoint wp)
            : this(wp, 1.0)
        { }

        public ColorspaceGray(Whitepoint wp, double gamma)
            : base(wp)
        {
            this._Gamma = gamma;
            this._Gamma1 = 1d / gamma;
        }


        public override TransformToDelegate GetTransformation(bool IsInput)
        {
            if (IsInput) return ToLinear;
            else return ToNonLinear;
        }

        protected virtual void ToLinear(double* inValues, double* outValues)
        {
            *outValues = Math.Pow(*inValues, _Gamma);
        }

        protected virtual void ToNonLinear(double* inValues, double* outValues)
        {
            *outValues = Math.Pow(*inValues, _Gamma1);
        }


        public static bool operator ==(ColorspaceGray a, ColorspaceGray b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.RefWhite == b.RefWhite && a.Gamma == b.Gamma;
        }
        public static bool operator !=(ColorspaceGray a, ColorspaceGray b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            ColorspaceGray c = obj as ColorspaceGray;
            if ((object)c == null) return base.Equals(obj);
            return c.RefWhite == this.RefWhite && c.Gamma == this.Gamma;
        }
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
