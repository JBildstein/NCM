using System;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace ColorManager.ICC
{
    /// <summary>
    /// A generic ICC exception. It is used for ICC related exceptions.
    /// </summary>
    [Serializable]
    public class ICCException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ICCException"/> class
        /// </summary>
        public ICCException()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ICCException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        public ICCException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ICCException"/> class
        /// </summary>
        /// <param name="message">An additional message</param>
        /// <param name="innerException">The inner exception</param>
        public ICCException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ICCException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ICCException(SerializationInfo info, StreamingContext context)
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
    /// An ICC profile reading error (e.g. data is not aligned correctly on a byte level)
    /// </summary>
    [Serializable]
    public sealed class CorruptProfileException : ICCException
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CorruptProfileException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private CorruptProfileException(SerializationInfo info, StreamingContext context)
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
    /// An ICC profile setup error (e.g. does not contain necessary tags)
    /// </summary>
    [Serializable]
    public sealed class InvalidProfileException : ICCException
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

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidProfileException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private InvalidProfileException(SerializationInfo info, StreamingContext context)
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
    /// An attempt to use an invalid ICC profile class was made
    /// </summary>
    [Serializable]
    public sealed class InvalidProfileClassException : ICCException
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

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidProfileClassException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/>  that contains contextual
        /// information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private InvalidProfileClassException(SerializationInfo info, StreamingContext context)
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
