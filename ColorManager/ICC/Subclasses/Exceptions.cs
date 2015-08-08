using System;

namespace ColorManager.ICC
{
    /// <summary>
    /// An ICC profile reading error (e.g. data is not aligned correctly on a byte level)
    /// </summary>
    public sealed class CorruptProfileException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CorruptProfileException"/> class
        /// </summary>
        public CorruptProfileException()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CorruptProfileException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        public CorruptProfileException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CorruptProfileException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        /// <param name="innerException">The inner exception</param>
        public CorruptProfileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// An ICC profile setup error (e.g. does not contain necessary tags)
    /// </summary>
    public sealed class InvalidProfileException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="InvalidProfileException"/> class
        /// </summary>
        public InvalidProfileException()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="InvalidProfileException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        public InvalidProfileException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="InvalidProfileException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        /// <param name="innerException">The inner exception</param>
        public InvalidProfileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// The type of a color does not match a certain condition
    /// </summary>
    public sealed class ColorTypeException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ColorTypeException"/> class
        /// </summary>
        public ColorTypeException()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorTypeException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        public ColorTypeException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorTypeException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        /// <param name="innerException">The inner exception</param>
        public ColorTypeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// An attempt to use an invalid ICC profile class was made
    /// </summary>
    public sealed class InvalidProfileClassException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="InvalidProfileClassException"/> class
        /// </summary>
        public InvalidProfileClassException()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="InvalidProfileClassException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        public InvalidProfileClassException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="InvalidProfileClassException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        /// <param name="innerException">The inner exception</param>
        public InvalidProfileClassException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// The necessary conditions for a conversion were not met
    /// </summary>
    public sealed class ConversionSetupException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ConversionSetupException"/> class
        /// </summary>
        public ConversionSetupException()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionSetupException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        public ConversionSetupException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionSetupException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        /// <param name="innerException">The inner exception</param>
        public ConversionSetupException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
