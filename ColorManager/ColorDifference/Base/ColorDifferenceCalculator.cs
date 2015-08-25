using System;
using System.Runtime.InteropServices;

namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Provides methods to calculate the difference between two colors (abstract class)
    /// </summary>
    public unsafe abstract class ColorDifferenceCalculator : IDisposable
    {
        #region Variables

        /// <summary>
        /// First color to compare
        /// </summary>
        protected Color Color1;
        /// <summary>
        /// Second color to compare
        /// </summary>
        protected Color Color2;
        /// <summary>
        /// States if this class has been disposed or not
        /// </summary>
        protected bool IsDisposed;

        /// <summary>
        /// Pointer to the color values of the first color
        /// </summary>
        protected readonly double* Col1Values;
        /// <summary>
        /// Pointer to the color values of the second color
        /// </summary>
        protected readonly double* Col2Values;
        /// <summary>
        /// Pointer to array for temporary variables
        /// </summary>
        protected readonly double* Vars;
        /// <summary>
        /// Length of the array for temporary variables
        /// </summary>
        protected readonly int VarsLength = 32;

        private double[] VarsArray;
        private GCHandle VarsHandle;
        private GCHandle Col1ValuesHandle;
        private GCHandle Col2ValuesHandle;

        #endregion

        #region Init/Dispose

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifferenceCalculator"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
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

        /// <summary>
        /// Finalizer of the <see cref="ColorDifferenceCalculator"/> class
        /// </summary>
        ~ColorDifferenceCalculator()
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
        protected virtual void Dispose(bool managed)
        {
            if (!IsDisposed)
            {
                if (VarsHandle.IsAllocated) VarsHandle.Free();
                if (Col1ValuesHandle.IsAllocated) Col1ValuesHandle.Free();
                if (Col2ValuesHandle.IsAllocated) Col2ValuesHandle.Free();

                IsDisposed = true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public abstract double DeltaE();

        /// <summary>
        /// Calculate the hue difference between two colors
        /// </summary>
        /// <returns>The hue difference between Col1Values and Col2Values</returns>
        public abstract double DeltaH();

        /// <summary>
        /// Calculate the chroma difference between two colors
        /// </summary>
        /// <returns>The chroma difference between Color1 and Color2</returns>
        public abstract double DeltaC();

        #endregion
    }
}
