using System;
using System.Linq;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ColorManager.ICC;
using ColorManager.Conversion;

namespace ColorManager
{
    public unsafe partial class ColorConverter : IDisposable
    {
        #region Static Content

        public static bool IsInitiated
        {
            get { return _IsInitiated; }
        }
        private static bool _IsInitiated = false;
        
        public static ConversionPath[] ConversionPaths
        {
            get { return _ConversionPaths.ToArray(); }
        }
        protected static List<ConversionPath> _ConversionPaths = new List<ConversionPath>();

        public static ChromaticAdaption[] ChromaticAdaptions
        {
            get { return _ChromaticAdaptions.ToArray(); }
        }
        protected static List<ChromaticAdaption> _ChromaticAdaptions = new List<ChromaticAdaption>();

        public static void Init()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type[] types;
            Type pathTp = typeof(ConversionPath);
            Type caTp = typeof(ChromaticAdaption);

            foreach (var assembly in assemblies)
            {
                types = assembly.GetTypes();

                var paths = from t in types
                            where t.IsSubclassOf(pathTp)
                            && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as ConversionPath;
                _ConversionPaths.AddRange(paths);

                var cas = from t in types
                          where t.IsSubclassOf(caTp)
                          && t.GetConstructor(Type.EmptyTypes) != null
                          select Activator.CreateInstance(t) as ChromaticAdaption;
                _ChromaticAdaptions.AddRange(cas);
            }
            _IsInitiated = true;
        }

        public static void AddConversionPath(ConversionPath path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (!_ConversionPaths.Contains(path)) _ConversionPaths.Add(path);
        }

        public static bool RemoveConversionPath(ConversionPath path)
        {
            if (path == null) throw new ArgumentNullException("path");
            return _ConversionPaths.Remove(path);
        }

        public static void AddChromaticAdaption(ChromaticAdaption ca)
        {
            if (ca == null) throw new ArgumentNullException("ca");
            if (!_ChromaticAdaptions.Contains(ca)) _ChromaticAdaptions.Add(ca);
        }

        public static bool RemoveChromaticAdaption(ChromaticAdaption ca)
        {
            if (ca == null) throw new ArgumentNullException("ca");
            return _ChromaticAdaptions.Remove(ca);
        }
        
        #endregion

        #region Variables

        protected ConversionDelegate ConversionMethod;
        protected ConversionData Data;
        protected bool IsDisposed;

        protected readonly Color InColor;
        protected readonly Color OutColor;
        protected readonly double* InValues;
        protected readonly double* OutValues;

        protected readonly GCHandle InValuesHandle;
        protected readonly GCHandle OutValuesHandle;

        #endregion


        public ColorConverter(Color inColor, Color outColor)
        {
            if (inColor == null || outColor == null) throw new ArgumentNullException();

            if (!IsInitiated) Init();

            this.InColor = inColor;
            this.OutColor = outColor;
            InValuesHandle = GCHandle.Alloc(InColor.Values, GCHandleType.Pinned);
            OutValuesHandle = GCHandle.Alloc(OutColor.Values, GCHandleType.Pinned);
            InValues = (double*)InValuesHandle.AddrOfPinnedObject();
            OutValues = (double*)OutValuesHandle.AddrOfPinnedObject();
            Data = new ConversionData(InColor, OutColor);

            var types = new Type[] { typeof(ColorConverter), typeof(double*), typeof(double*), typeof(ConversionData) };
            DynamicMethod convMethod = new DynamicMethod("ConversionMethod", null, types, typeof(ColorConverter));
            GetConversionMethod(convMethod.GetILGenerator());
            ConversionMethod = convMethod.CreateDelegate(typeof(ConversionDelegate), this) as ConversionDelegate;             
        }

        protected virtual void GetConversionMethod(ILGenerator CMIL)
        {
            if (InColor.Space is ColorspaceICC || OutColor.Space is ColorspaceICC)
            {
                var iccCreator = new ConversionCreator_ICC(CMIL, Data, InColor, OutColor);
                iccCreator.SetConversionMethod();
            }
            else
            {
                var colCreator = new ConversionCreator_Color(CMIL, Data, InColor, OutColor);
                colCreator.SetConversionMethod();
            }
        }


        ~ColorConverter()
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
                if (InValuesHandle.IsAllocated) InValuesHandle.Free();
                if (OutValuesHandle.IsAllocated) OutValuesHandle.Free();
                if (Data != null) Data.Dispose();

                if (managed)
                {
                    ConversionMethod = null;
                    Data = null;
                }
                IsDisposed = true;
            }
        }

        /// <summary>
        /// Converts the colors given in the constructor
        /// </summary>
        public void Convert()
        {
            ConversionMethod(InValues, OutValues, Data);
        }
    }
}
