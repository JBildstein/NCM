using System;

namespace ColorManager.ICC
{
    /// <summary>
    /// An ICC profile reading error (e.g. data is not aligned correctly on a byte level)
    /// </summary>
    public sealed class CorruptProfileException : Exception
    {
        public CorruptProfileException()
        { }

        public CorruptProfileException(string message)
            : base(message)
        { }

        public CorruptProfileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    /// <summary>
    /// An ICC profile setup error (e.g. does not contain necessary tags)
    /// </summary>
    public sealed class InvalidProfileException : Exception
    {
        public InvalidProfileException()
        { }

        public InvalidProfileException(string message)
            : base(message)
        { }

        public InvalidProfileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public sealed class ColorTypeException : Exception
    {
        public ColorTypeException()
        { }

        public ColorTypeException(string message)
            : base(message)
        { }

        public ColorTypeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public sealed class InvalidProfileClassException : Exception
    {
        public InvalidProfileClassException()
        { }

        public InvalidProfileClassException(string message)
            : base(message)
        { }

        public InvalidProfileClassException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public sealed class ConversionSetupException : Exception
    {
        public ConversionSetupException()
        { }

        public ConversionSetupException(string message)
            : base(message)
        { }

        public ConversionSetupException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
