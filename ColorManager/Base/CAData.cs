using System;

namespace ColorManager.Conversion
{
    public abstract unsafe class CustomData : IDisposable
    {
        public abstract void* DataPointer { get; }

        ~CustomData()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool managed);
    }
}
