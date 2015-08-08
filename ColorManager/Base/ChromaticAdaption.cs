using System;
using ColorManager.Conversion;

namespace ColorManager
{
    /// <summary>
    /// Represents a chromatic adaption method (abstract class)
    /// </summary>
    public abstract class ChromaticAdaption
    {
        /// <summary>
        /// The type of color this chromatic adaption is performed on
        /// </summary>
        public abstract Type ColorType { get; }
        /// <summary>
        /// The method to execute to do the chromatic adaption
        /// </summary>
        public abstract ConversionDelegate Method { get; }

        /// <summary>
        /// Gets the conversion data necessary for the chromatic adaption
        /// </summary>
        /// <param name="data">The data that is used to perform the chromatic adaption</param>
        /// <returns>The conversion data necessary for the chromatic adaption</returns>
        public abstract CustomData GetCAData(ConversionData data);

        /// <summary>
        /// Compares two chromatic adaption methods for their equality of the color type
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(ChromaticAdaption a, ChromaticAdaption b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.ColorType == b.ColorType;
        }
        /// <summary>
        /// Compares two chromatic adaption methods for their inequality of the color type
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(ChromaticAdaption a, ChromaticAdaption b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Compares this chromatic adaption method with another for their equality of the color type
        /// </summary>
        /// <param name="obj">The color to compare to</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            ChromaticAdaption c = obj as ChromaticAdaption;
            if ((object)c == null) return false;
            return c == this;
        }
        /// <summary>
        /// Calculates a hash code of this chromatic adaption method
        /// </summary>
        /// <returns>The hash code of this color</returns>
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
