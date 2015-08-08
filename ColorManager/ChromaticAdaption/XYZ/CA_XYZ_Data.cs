using System;
using System.Runtime.InteropServices;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Container for XZY chromatic adaption conversion data
    /// </summary>
    public sealed unsafe class CA_XYZ_Data : CustomData
    {
        /// <summary>
        /// Pointer to the data
        /// </summary>
        public override void* DataPointer
        {
            get { return DataHandle.AddrOfPinnedObject().ToPointer(); }
        }
        private GCHandle DataHandle;

        /// <summary>
        /// Creates a new instance of the <see cref="CA_XYZ_Data"/> class
        /// </summary>
        /// <param name="matrix">The matrix for the chromatic adaption</param>
        public CA_XYZ_Data(double[] matrix)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (matrix.Length != 9) throw new ArgumentException("Matrix has to have a length of nine");

            DataHandle = GCHandle.Alloc(matrix, GCHandleType.Pinned);
        }

        /// <summary>
        /// Releases all allocated resources
        /// </summary>
        /// <param name="managed">True if called by user, false if called by finalizer</param>
        protected override void Dispose(bool managed)
        {
            if (DataHandle.IsAllocated) DataHandle.Free();
        }
    }
}
