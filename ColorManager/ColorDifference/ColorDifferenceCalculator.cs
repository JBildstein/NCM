using System;
using System.Runtime.InteropServices;

namespace ColorManager.ColorDifference
{
    public unsafe abstract class ColorDifferenceCalculator : IDisposable
    {
        #region Variables

        protected Color Color1;
        protected Color Color2;
        protected bool IsDisposed;

        protected readonly double* Col1Values;
        protected readonly double* Col2Values;
        protected readonly double* Vars;
        protected readonly int VarsLength = 32;

        private double[] VarsArray;
        private GCHandle VarsHandle;
        private GCHandle Col1ValuesHandle;
        private GCHandle Col2ValuesHandle;

        #endregion

        #region Init/Dispose

        protected ColorDifferenceCalculator(Color Color1, Color Color2)
        {
            this.Color1 = Color1;
            this.Color2 = Color2;

            Col1ValuesHandle = GCHandle.Alloc(Color1.Values, GCHandleType.Pinned);
            Col2ValuesHandle = GCHandle.Alloc(Color2.Values, GCHandleType.Pinned);
            Col1Values = (double*)Col1ValuesHandle.AddrOfPinnedObject();
            Col2Values = (double*)Col2ValuesHandle.AddrOfPinnedObject();

            VarsArray = new double[VarsLength];
            VarsHandle = GCHandle.Alloc(VarsArray, GCHandleType.Pinned);
            Vars = (double*)VarsHandle.AddrOfPinnedObject();
        }
        
        ~ColorDifferenceCalculator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool managed)
        {
            if (!IsDisposed)
            {
                if (managed) { }

                if (VarsHandle.IsAllocated) VarsHandle.Free();
                if (Col1ValuesHandle.IsAllocated) Col1ValuesHandle.Free();
                if (Col2ValuesHandle.IsAllocated) Col2ValuesHandle.Free();

                IsDisposed = true;
            }
        }

        #endregion

        #region Methods

        public abstract double DeltaE();

        public abstract double DeltaH();

        public abstract double DeltaC();

        #endregion
    }
}
