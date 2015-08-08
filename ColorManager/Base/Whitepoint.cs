using System;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of a whitepoint
    /// </summary>
    public abstract class Whitepoint
    {
        /// <summary>
        /// The default Whitepoint
        /// </summary>
        public static Whitepoint Default
        {
            get { return _Default; }
            set { if (value == null) _Default = value; }
        }
        private static Whitepoint _Default = new WhitepointD65();

        /// <summary>
        /// The number of decimals to which the values will be compared with ==, !=, Equals and GetHasCode
        /// </summary>
        public static int ComparisonAccuracy
        {
            get { return CA; }
            set
            {
                if (CA > 15) CA = 15;
                else if (CA < 0) CA = 0;
                else CA = value;
            }
        }
        private static int CA = 6;

        /// <summary>
        /// The name of this whitepoint
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// X value
        /// </summary>
        public abstract double X { get; }
        /// <summary>
        /// Y value
        /// </summary>
        public abstract double Y { get; }
        /// <summary>
        /// Z value
        /// </summary>
        public abstract double Z { get; }
        /// <summary>
        /// XYZ values array
        /// </summary>
        public double[] XYZ { get { return new double[] { X, Y, Z }; } }

        /// <summary>
        /// x chromaticity value
        /// </summary>
        public abstract double Cx { get; }
        /// <summary>
        /// y chromaticity value
        /// </summary>
        public abstract double Cy { get; }
        /// <summary>
        /// Chromaticity array
        /// </summary>
        public double[] Cxy { get { return new double[] { Cx, Cy }; } }


        /// <summary>
        /// Compares two whitepoints for their equality of values
        /// </summary>
        /// <param name="a">First whitepoint</param>
        /// <param name="b">Second whitepoint</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(Whitepoint a, Whitepoint b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return Math.Round(a.X, CA) == Math.Round(b.X, CA)
                && Math.Round(a.Y, CA) == Math.Round(b.Y, CA)
                && Math.Round(a.Z, CA) == Math.Round(b.Z, CA)
                && Math.Round(a.Cx, CA) == Math.Round(b.Cx, CA)
                && Math.Round(a.Cy, CA) == Math.Round(b.Cy, CA);
        }
        /// <summary>
        /// Compares two whitepoints for their inequality of values
        /// </summary>
        /// <param name="a">First whitepoint</param>
        /// <param name="b">Second whitepoint</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(Whitepoint a, Whitepoint b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Compares this whitepoint with another for their equality of values
        /// </summary>
        /// <param name="obj">The whitepoint to compare to</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            Whitepoint c = obj as Whitepoint;
            if ((object)c == null) return false;
            return Math.Round(c.X, CA) == Math.Round(this.X, CA)
                && Math.Round(c.Y, CA) == Math.Round(this.Y, CA)
                && Math.Round(c.Z, CA) == Math.Round(this.Z, CA)
                && Math.Round(c.Cx, CA) == Math.Round(this.Cx, CA)
                && Math.Round(c.Cy, CA) == Math.Round(this.Cy, CA);
        }
        /// <summary>
        /// Calculates a hash code of this whitepoint
        /// </summary>
        /// <returns>The hash code of this color</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Math.Round(X, CA).GetHashCode();
                hash *= 16777619 ^ Math.Round(Y, CA).GetHashCode();
                hash *= 16777619 ^ Math.Round(Z, CA).GetHashCode();
                hash *= 16777619 ^ Math.Round(Cx, CA).GetHashCode();
                hash *= 16777619 ^ Math.Round(Cy, CA).GetHashCode();
                return hash;
            }
        }

        #region Static List of Whitepoints

        /// <summary>
        /// A readonly field that represents a whitepoint A
        /// </summary>
        public static readonly WhitepointA A = new WhitepointA();
        /// <summary>
        /// A readonly field that represents a whitepoint B
        /// </summary>
        public static readonly WhitepointB B = new WhitepointB();
        /// <summary>
        /// A readonly field that represents a whitepoint C
        /// </summary>
        public static readonly WhitepointC C = new WhitepointC();
        /// <summary>
        /// A readonly field that represents a whitepoint D50
        /// </summary>
        public static readonly WhitepointD50 D50 = new WhitepointD50();
        /// <summary>
        /// A readonly field that represents a whitepoint D55
        /// </summary>
        public static readonly WhitepointD55 D55 = new WhitepointD55();
        /// <summary>
        /// A readonly field that represents a whitepoint D65
        /// </summary>
        public static readonly WhitepointD65 D65 = new WhitepointD65();
        /// <summary>
        /// A readonly field that represents a whitepoint D75
        /// </summary>
        public static readonly WhitepointD75 D75 = new WhitepointD75();
        /// <summary>
        /// A readonly field that represents a whitepoint E
        /// </summary>
        public static readonly WhitepointE E = new WhitepointE();
        /// <summary>
        /// A readonly field that represents a whitepoint F2
        /// </summary>
        public static readonly WhitepointF2 F2 = new WhitepointF2();
        /// <summary>
        /// A readonly field that represents a whitepoint F7
        /// </summary>
        public static readonly WhitepointF7 F7 = new WhitepointF7();
        /// <summary>
        /// A readonly field that represents a whitepoint F11
        /// </summary>
        public static readonly WhitepointF11 F11 = new WhitepointF11();

        #endregion
    }
}
