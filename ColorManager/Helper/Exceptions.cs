using System;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace ColorManager
{
    /// <summary>
    /// No path for conversion from one to another color was found
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private ConversionNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo"/>
        /// with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/>
        /// that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/>
        /// that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">The info parameter is a null reference</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// The type of a color does not match a certain condition
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTypeException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private ColorTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo"/>
        /// with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/>
        /// that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/>
        /// that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">The info parameter is a null reference</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }
    }
    
    /// <summary>
    /// The necessary conditions for a conversion were not met
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionSetupException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private ConversionSetupException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo"/>
        /// with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/>
        /// that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/>
        /// that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">The info parameter is a null reference</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }
    }
}
