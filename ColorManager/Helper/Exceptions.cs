using System;

namespace ColorManager
{
    public sealed class ConversionNotFoundException : Exception
    {
        public ConversionNotFoundException()
            : base()
        { }
        public ConversionNotFoundException(Type from, Type to)
            : base("From " + from.Name + " to " + to.Name)
        { }
        public ConversionNotFoundException(Type from, Type to, Exception InnerException)
            : base("From " + from.Name + " to " + to.Name, InnerException)
        { }
    }
}
