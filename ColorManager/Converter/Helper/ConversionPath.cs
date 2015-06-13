using System;

namespace ColorManager.Conversion
{
    public abstract class ConversionPath
    {
        public abstract Type From { get; }
        public abstract Type To { get; }

        public abstract IConversionCommand[] Commands { get; }
        
        public static bool operator ==(ConversionPath a, ConversionPath b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.From == b.From && a.To == b.To;
        }
        public static bool operator !=(ConversionPath a, ConversionPath b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            ConversionPath c = obj as ConversionPath;
            if ((object)c == null) return false;
            return c.From == this.From && c.To == this.To;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ From.GetHashCode();
                hash *= 16777619 ^ To.GetHashCode();
                return hash;
            }
        }
    }

    public abstract class ConversionPath<TFrom, TTo> : ConversionPath
        where TFrom : Color
        where TTo : Color
    {
        public override Type From { get { return typeof(TFrom); } }
        public override Type To { get { return typeof(TTo); } }
    }
}
