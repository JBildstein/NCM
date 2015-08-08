using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Stores data about a conversion from one color to another
    /// </summary>
    public abstract class ConversionPath
    {
        /// <summary>
        /// Type of color from which is converted
        /// </summary>
        public abstract Type From { get; }
        /// <summary>
        /// Type of color to which is converted
        /// </summary>
        public abstract Type To { get; }

        /// <summary>
        /// An array of commands that convert from one to another color
        /// </summary>
        public abstract IConversionCommand[] Commands { get; }

        /// <summary>
        /// Compares two conversion paths for their equality of <see cref="From"/> and <see cref="To"/> types
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(ConversionPath a, ConversionPath b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.From == b.From && a.To == b.To;
        }
        /// <summary>
        /// Compares two conversion paths for their inequality of <see cref="From"/> and <see cref="To"/> types
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(ConversionPath a, ConversionPath b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Compares this conversion path with another for their equality of <see cref="From"/> and <see cref="To"/> types
        /// </summary>
        /// <param name="obj">The color to compare to</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            ConversionPath c = obj as ConversionPath;
            if ((object)c == null) return false;
            return c.From == this.From && c.To == this.To;
        }
        /// <summary>
        /// Calculates a hash code of this conversion path
        /// </summary>
        /// <returns>The hash code of this color</returns>
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

    /// <summary>
    /// Stores data about a conversion from one color to another
    /// </summary>
    public abstract class ConversionPath<TFrom, TTo> : ConversionPath
        where TFrom : Color
        where TTo : Color
    {
        /// <summary>
        /// Type of color from which is converted
        /// </summary>
        public override Type From { get { return typeof(TFrom); } }
        /// <summary>
        /// Type of color to which is converted
        /// </summary>
        public override Type To { get { return typeof(TTo); } }
    }
}
