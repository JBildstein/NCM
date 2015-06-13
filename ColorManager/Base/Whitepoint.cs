using System;

namespace ColorManager
{
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


        public static bool operator ==(Whitepoint a, Whitepoint b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return Math.Round(a.X, CA) == Math.Round(b.X, CA)
                && Math.Round(a.Y, CA) == Math.Round(b.Y, CA)
                && Math.Round(a.Z, CA) == Math.Round(b.Z, CA)
                && Math.Round(a.Cx, CA) == Math.Round(b.Cx, CA)
                && Math.Round(a.Cy, CA) == Math.Round(b.Cy, CA);
        }

        public static bool operator !=(Whitepoint a, Whitepoint b)
        {
            return !(a == b);
        }

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
    }
}
