using System;

namespace ColorManager.ICC
{
    public class ColorspaceICC : Colorspace
    {
        public ICCProfile Profile
        {
            get { return _Profile; }
        }
        private ICCProfile _Profile;

        public ColorspaceICC(ICCProfile profile)
            : base(new WhitepointD50())
        {
            if (profile == null) throw new ArgumentNullException("profile");
            this._Profile = profile;
        }

        public static bool operator ==(ColorspaceICC a, ColorspaceICC b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Profile == b.Profile;
        }
        public static bool operator !=(ColorspaceICC a, ColorspaceICC b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            ColorspaceICC c = obj as ColorspaceICC;
            if ((object)c == null) return base.Equals(obj);
            return c == this;
        }
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
