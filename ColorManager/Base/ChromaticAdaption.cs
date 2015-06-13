using System;
using ColorManager.Conversion;

namespace ColorManager
{
    public abstract class ChromaticAdaption
    {
        public abstract Type ColorType { get; }
        public abstract ConversionDelegate Method { get; }

        public abstract CustomData GetCAData(ConversionData data);

        public static bool operator ==(ChromaticAdaption a, ChromaticAdaption b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.ColorType == b.ColorType;
        }

        public static bool operator !=(ChromaticAdaption a, ChromaticAdaption b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            ChromaticAdaption c = obj as ChromaticAdaption;
            if ((object)c == null) return false;
            return c == this;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ ColorType.GetHashCode();
                return hash;
            }
        }
    }
}
