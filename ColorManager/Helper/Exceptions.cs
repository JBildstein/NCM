using System;

namespace ColorManager
{
    /// <summary>
    /// No path for conversion from one to another color was found
    /// </summary>
    public sealed class ConversionNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ConversionNotFoundException"/> class
        /// </summary>
        public ConversionNotFoundException()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionNotFoundException"/> class
        /// </summary>
        /// <param name="from">They color type from which was tried to convert</param>
        /// <param name="to">They color type to which was tried to convert</param>
        public ConversionNotFoundException(Type from, Type to)
            : base("From " + from.Name + " to " + to.Name)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionNotFoundException"/> class
        /// </summary>
        /// <param name="from">They color type from which was tried to convert</param>
        /// <param name="to">They color type to which was tried to convert</param>
        /// <param name="InnerException">The inner exception</param>
        public ConversionNotFoundException(Type from, Type to, Exception InnerException)
            : base("From " + from.Name + " to " + to.Name, InnerException)
        { }
    }
}
