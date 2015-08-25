using ColorManager.Conversion;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of a colorspace
    /// </summary>
    public class Colorspace
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public virtual string Name
        {
            get { return string.Concat("Colorspace ", RefWhite.Name); }
        }
        /// <summary>
        /// Reference white for this colorspace
        /// </summary>
        public virtual Whitepoint ReferenceWhite
        {
            get { return RefWhite; }
        }
        /// <summary>
        /// Field for the <see cref="ReferenceWhite"/> property
        /// </summary>
        protected Whitepoint RefWhite;
        
        /// <summary>
        /// The default colorspace
        /// </summary>
        public static Colorspace Default
        {
            get { return _Default; }
            set { if (value != null) _Default = value; }
        }
        private static Colorspace _Default = new Colorspace(Whitepoint.Default);

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace"/> class with the default reference white
        /// </summary>
        public Colorspace()
        {
            RefWhite = Whitepoint.Default;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Colorspace"/> class
        /// </summary>
        /// <param name="wp">The reference white</param>
        public Colorspace(Whitepoint wp)
        {
            if (wp != null) RefWhite = wp;
            else RefWhite = Whitepoint.Default;
        }

        /// <summary>
        /// If overridden, returns a custom delegate to transform a color or null otherwise
        /// <para>An example would be a gamma adjustment.</para>
        /// </summary>
        /// <param name="IsInput">True if used for the input color, false otherwise</param>
        /// <returns>A custom delegate to transform a color</returns>
        public virtual TransformToDelegate GetTransformation(bool IsInput)
        {
            return null;
        }

        /// <summary>
        /// If overridden, returns some custom data to transform a color or null otherwise
        /// <para>An example would be a conversion matrix.</para>
        /// </summary>
        /// <param name="IsInput">True if used for the input color, false otherwise</param>
        /// <returns>Custom data to transform a color</returns>
        public virtual CustomData GetData(bool IsInput)
        {
            return null;
        }


        /// <summary>
        /// Compares two colorspaces for their equality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(Colorspace a, Colorspace b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.RefWhite == b.RefWhite;
        }
        /// <summary>
        /// Compares two colorspaces for their inequality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(Colorspace a, Colorspace b)
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
            Colorspace c = obj as Colorspace;
            if ((object)c == null) return false;
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
                return hash;
            }
        }
    }
}
