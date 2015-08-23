using System;
using System.Runtime.InteropServices;
using ColorManager.Conversion;

namespace ColorManager.ICC.Conversion
{
    public sealed unsafe class ICCData : CustomData
    {
        public override void* DataPointer
        {
            get { return DataPtr.ToPointer(); }
        }

        private IntPtr DataPtr;
        private GCHandle[] ArrHandle;
        
        public ICCData(double[][] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            DataPtr = Marshal.AllocHGlobal(data.Length * IntPtr.Size);
            ArrHandle = new GCHandle[data.Length];

            double** tmp = (double**)DataPtr;

            for (int i = 0; i < data.Length; i++)
            {
                ArrHandle[i] = GCHandle.Alloc(data[i], GCHandleType.Pinned);
                tmp[i] = (double*)ArrHandle[i].AddrOfPinnedObject();
            }
        }

        protected override void Dispose(bool managed)
        {
            if (ArrHandle != null)
            {
                foreach (var handle in ArrHandle) { if (handle.IsAllocated) handle.Free(); }
            }
            if (DataPtr != IntPtr.Zero) Marshal.FreeHGlobal(DataPtr);
            DataPtr = IntPtr.Zero;
        }
    }
}
