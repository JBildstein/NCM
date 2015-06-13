using System;
using System.Runtime.InteropServices;

namespace ColorManager.Conversion
{
    public sealed unsafe class CA_XYZ_Data : CustomData
    {
        public override void* DataPointer
        {
            get { return DataHandle.AddrOfPinnedObject().ToPointer(); }
        }
        private GCHandle DataHandle;

        public CA_XYZ_Data(double[] matrix)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (matrix.Length != 9) throw new ArgumentException("Matrix has to have a length of nine");

            DataHandle = GCHandle.Alloc(matrix, GCHandleType.Pinned);
        }

        protected override void Dispose(bool managed)
        {
            if (DataHandle.IsAllocated) DataHandle.Free();
        }
    }
}
