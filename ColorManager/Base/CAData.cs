using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Container for custom conversion data (abstract class)
    /// </summary>
    public abstract unsafe class CustomData : IDisposable
    {
        /// <summary>
        /// Pointer to the data
        /// </summary>
        public abstract void* DataPointer { get; }

        /// <summary>
        /// Finalizer of the <see cref="CustomData"/> class
        /// </summary>
        ~CustomData()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all allocated resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all allocated resources
        /// </summary>
        /// <param name="managed">True if called by user, false if called by finalizer</param>
        protected abstract void Dispose(bool managed);
    }
}
