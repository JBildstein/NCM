using ColorManager.Conversion;

namespace ColorManager
{
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


        public Colorspace()
        {
            RefWhite = Whitepoint.Default;
        }

        public Colorspace(Whitepoint wp)
        {
            if (wp != null) RefWhite = wp;
            else RefWhite = Whitepoint.Default;
        }


        public virtual TransformToDelegate GetTransformation(bool IsInput)
        {
            return null;
        }

        public virtual CustomData GetData(bool IsInput)
        {
            return null;
        }


        public static bool operator ==(Colorspace a, Colorspace b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.RefWhite == b.RefWhite;
        }

        public static bool operator !=(Colorspace a, Colorspace b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            Colorspace c = obj as Colorspace;
            if ((object)c == null) return false;
            return c.RefWhite == this.RefWhite;
        }

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
