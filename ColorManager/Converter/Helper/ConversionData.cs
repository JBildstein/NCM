using System;
using System.Runtime.InteropServices;
using ColorManager.ICC.Conversion;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Holds data that is needed for converting colors
    /// </summary>
    public unsafe class ConversionData : IDisposable
    {
        #region Public/Internal Variables

        /// <summary>
        /// First array of color values for temporary conversion storage.
        /// </summary>
        public double* ColVars1;
        /// <summary>
        /// Second array of color values for temporary conversion storage
        /// </summary>
        public double* ColVars2;
        /// <summary>
        /// Array for temporary calculation storage. Length is <see cref="MaxVars"/>
        /// <para>It can be used instead of allocating variables for each call</para>
        /// </summary>
        public double* Vars;
        /// <summary>
        /// The XYZ values of the input whitepoint
        /// </summary>
        public double* InWP;
        /// <summary>
        /// The XYZ values of the output whitepoint
        /// </summary>
        public double* OutWP;
        /// <summary>
        /// The Cx and Cy values of the input whitepoint
        /// </summary>
        public double* InWPCr;
        /// <summary>
        /// The Cx and Cy values of the output whitepoint
        /// </summary>
        public double* OutWPCr;
        /// <summary>
        /// The ICC data of the input profile
        /// </summary>
        public double** InICCData;
        /// <summary>
        /// The ICC data of the output profile
        /// </summary>
        public double** OutICCData;
        /// <summary>
        /// The data for the chromatic adaption
        /// <para>This can be anything and depends on the used chromatic adaption</para>
        /// </summary>
        public void* CAData;
        /// <summary>
        /// The data for the input colorspace
        /// <para>This can be anything and depends on the used colorspace</para>
        /// </summary>
        public void* InSpaceData;
        /// <summary>
        /// The data for the output colorspace
        /// <para>This can be anything and depends on the used colorspace</para>
        /// </summary>
        public void* OutSpaceData;
        /// <summary>
        /// The transformation method of the input colorspace
        /// <para>If not defined by the colorspace, this is null</para>
        /// </summary>
        public TransformToDelegate InTransform;
        /// <summary>
        /// The transformation method of the output colorspace
        /// <para>If not defined by the colorspace, this is null</para>
        /// </summary>
        public TransformToDelegate OutTransform;
        /// <summary>
        /// States if the two colors have a different whitepoint
        /// </summary>
        public bool IsDifferentWP;
        /// <summary>
        /// States if the conversion needs chromatic adaption
        /// </summary>
        public bool IsCARequired;

        /// <summary>
        /// Maximum number of temporary variables
        /// <para>This is the lenght of the <see cref="Vars"/> array</para>
        /// </summary>
        public const int MaxVars = 32;
        /// <summary>
        /// States if this data has been disposed
        /// </summary>
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        #endregion

        #region Private Variables

        private bool _IsDisposed;

        private double[] ColVars1Array = new double[Color.MaxChannels];
        private double[] ColVars2Array = new double[Color.MaxChannels];
        private double[] VarsArray = new double[MaxVars];

        private CustomData _CAData;
        private CustomData _InSpaceData;
        private CustomData _OutSpaceData;
        private ICCData _InICCData;
        private ICCData _OutICCData;
        private GCHandle ColVars1Handle;
        private GCHandle ColVars2Handle;
        private GCHandle VarsHandle;
        private GCHandle InWPHandle;
        private GCHandle OutWPHandle;
        private GCHandle InWPCrHandle;
        private GCHandle OutWPCrHandle;
        private GCHandle CAMatrixHandle;

        #endregion
        

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionData"/> class
        /// </summary>
        /// <param name="inColor"></param>
        /// <param name="outColor"></param>
        protected internal ConversionData(Color inColor, Color outColor)
        {
            if (inColor == null || outColor == null) throw new ArgumentNullException();

            Type inColorType = inColor.GetType();
            Type outColorType = inColor.GetType();

            Colorspace inSpace = inColor.Space;
            Colorspace outSpace = outColor.Space;

            Type inSpaceType = inSpace.GetType();
            Type outSpaceType = outSpace.GetType();

            IsCARequired = !inSpace.Equals(outSpace);
            IsDifferentWP = inSpace.ReferenceWhite != outSpace.ReferenceWhite;
            InTransform = inSpace.GetTransformation(true);
            OutTransform = outSpace.GetTransformation(false);

            _InSpaceData = inSpace.GetData(true);
            _OutSpaceData = outSpace.GetData(false);
            if (_InSpaceData != null) InSpaceData = _InSpaceData.DataPointer;
            if (_OutSpaceData != null) OutSpaceData = _OutSpaceData.DataPointer;

            ColVars1Handle = GCHandle.Alloc(ColVars1Array, GCHandleType.Pinned);
            ColVars2Handle = GCHandle.Alloc(ColVars2Array, GCHandleType.Pinned);
            VarsHandle = GCHandle.Alloc(VarsArray, GCHandleType.Pinned);
            InWPHandle = GCHandle.Alloc(inSpace.ReferenceWhite.XYZ, GCHandleType.Pinned);
            OutWPHandle = GCHandle.Alloc(outSpace.ReferenceWhite.XYZ, GCHandleType.Pinned);
            InWPCrHandle = GCHandle.Alloc(inSpace.ReferenceWhite.Cxy, GCHandleType.Pinned);
            OutWPCrHandle = GCHandle.Alloc(outSpace.ReferenceWhite.Cxy, GCHandleType.Pinned);

            ColVars1 = (double*)ColVars1Handle.AddrOfPinnedObject();
            ColVars2 = (double*)ColVars2Handle.AddrOfPinnedObject();
            Vars = (double*)VarsHandle.AddrOfPinnedObject();
            InWP = (double*)InWPHandle.AddrOfPinnedObject();
            OutWP = (double*)OutWPHandle.AddrOfPinnedObject();
        }

        /// <summary>
        /// Sets the chromatic adaption data
        /// </summary>
        /// <param name="ca">The chromatic adaption data</param>
        protected internal void SetCAData(ChromaticAdaption ca)
        {
            if (ca == null) throw new ArgumentNullException();
            var data = ca.GetCAData(this);
            _CAData = data;
            CAData = data.DataPointer;
        }

        /// <summary>
        /// Sets the ICC data (can be null if not used)
        /// </summary>
        /// <param name="inProfileData">Input profile data</param>
        /// <param name="outProfileData">Output profile data</param>
        protected internal void SetICCData(ICCData inProfileData, ICCData outProfileData)
        {
            if (inProfileData != null)
            {
                _InICCData = inProfileData;
                InICCData = (double**)inProfileData.DataPointer;
            }

            if (outProfileData != null)
            {
                _OutICCData = outProfileData;
                OutICCData = (double**)outProfileData.DataPointer;
            }
        }

        #region Dispose

        ~ConversionData()
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
                if (managed)
                {
                    InTransform = null;
                    OutTransform = null;
                    VarsArray = null;
                }

                if (_CAData != null) _CAData.Dispose();
                if (_InSpaceData != null) _InSpaceData.Dispose();
                if (_OutSpaceData != null) _OutSpaceData.Dispose();
                if (_InICCData != null) _InICCData.Dispose();
                if (_OutICCData != null) _OutICCData.Dispose();

                if (ColVars1Handle.IsAllocated) ColVars1Handle.Free();
                if (ColVars2Handle.IsAllocated) ColVars2Handle.Free();
                if (VarsHandle.IsAllocated) VarsHandle.Free();
                if (InWPHandle.IsAllocated) InWPHandle.Free();
                if (OutWPHandle.IsAllocated) OutWPHandle.Free();
                if (InWPCrHandle.IsAllocated) InWPCrHandle.Free();
                if (OutWPCrHandle.IsAllocated) OutWPCrHandle.Free();
                if (CAMatrixHandle.IsAllocated) CAMatrixHandle.Free();

                _IsDisposed = true;
            }
        }

        #endregion
    }
}
