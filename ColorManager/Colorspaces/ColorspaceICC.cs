using System;

namespace ColorManager.ICC
{
    /// <summary>
    /// Stores information and values of an ICC colorspace
    /// </summary>
    public class ColorspaceICC : Colorspace
    {
        /// <summary>
        /// The ICC profile of this colorspace
        /// </summary>
        public ICCProfile Profile
        {
            get { return _Profile; }
        }
        private ICCProfile _Profile;

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceICC"/> class
        /// </summary>
        /// <param name="profile">The ICC profile of this colorspace</param>
        public ColorspaceICC(ICCProfile profile)
            : base(new WhitepointD50())//TODO: whitepoint is not always D50 for ICC profile
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            _Profile = profile;
        }

        /// <summary>
        /// Compares two colorspaces for their equality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(ColorspaceICC a, ColorspaceICC b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Profile == b.Profile;
        }
        /// <summary>
        /// Compares two colorspaces for their inequality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(ColorspaceICC a, ColorspaceICC b)
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
            ColorspaceICC c = obj as ColorspaceICC;
            if ((object)c == null) return base.Equals(obj);
            return c == this;
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
                hash *= 16777619 ^ Profile.GetHashCode();
                return hash;
            }
        }
    }
}
