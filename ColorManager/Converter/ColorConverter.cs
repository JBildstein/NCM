using System;
using System.Linq;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ColorManager.ICC;
using ColorManager.Conversion;
using ColorManager.ICC.Conversion;

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

        private Color TempColor1;
        private Color TempColor2;

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
            var iccIn = InColor.Space as ColorspaceICC;
            var iccOut = OutColor.Space as ColorspaceICC;

            if (iccIn != null && iccOut != null)
            {
                if (iccIn.Profile == iccOut.Profile) SingleICCProfile(CMIL, iccIn.Profile);
                else DuaICCProfile(CMIL, iccIn.Profile, iccOut.Profile);
            }
            else if (iccIn != null) SingleICCProfile(CMIL, iccIn.Profile);
            else if (iccOut != null) SingleICCProfile(CMIL, iccOut.Profile);
            else
            {
                var colCreator = new ConversionCreator_Color(CMIL, Data, InColor, OutColor);
                colCreator.SetConversionMethod();
            }
        }

        #region Find conversion type
        
        private void DuaICCProfile(ILGenerator CMIL, ICCProfile profile1, ICCProfile profile2)
        {
            var inType = InColor.GetType();
            var outType = OutColor.GetType();

            if (inType == profile1.DataColorspace)
            {
                if (outType == profile2.DataColorspace) DuaICCProfile_Data_Data(CMIL, profile1, profile2);
                else if (outType == profile2.PCS) DuaICCProfile_Data_PCS(CMIL, profile1, profile2);
                else throw new ConversionSetupException(); //Should never happen because of Color checks
            }
            else if (inType == profile1.PCS)
            {
                if (outType == profile2.DataColorspace) DuaICCProfile_PCS_Data(CMIL, profile1, profile2);
                else if (outType == profile2.PCS) DuaICCProfile_PCS_PCS(CMIL, profile1, profile2);
                else throw new ConversionSetupException(); //Should never happen because of Color checks
            }
            else throw new ConversionSetupException(); //Should never happen because of Color checks
        }
        
        private void DuaICCProfile_PCS_PCS(ILGenerator CMIL, ICCProfile profile1, ICCProfile profile2)
        {
            //PCS1 == PCS2 will result in an assign
            //PCS1 != PCS2 will result in a conversion
            var c = new ConversionCreator_Color(CMIL, Data, InColor, OutColor);
            c.SetConversionMethod();
        }

        private void DuaICCProfile_PCS_Data(ILGenerator CMIL, ICCProfile profile1, ICCProfile profile2)
        {
            if (profile1.PCS == profile2.PCS)
            {
                var c = new ConversionCreator_ICC(CMIL, Data, InColor, OutColor);
                c.SetConversionMethod();
            }
            else
            {
                TempColor1 = profile2.GetPCSColor(true);
                var c1 = new ConversionCreator_Color(CMIL, Data, InColor, TempColor1, false);
                c1.SetConversionMethod();
                var c2 = new ConversionCreator_ICC(c1, TempColor1, OutColor, true);
                c2.SetConversionMethod();
            }
        }

        private void DuaICCProfile_Data_PCS(ILGenerator CMIL, ICCProfile profile1, ICCProfile profile2)
        {
            if (profile1.PCS == profile2.PCS)
            {
                var c = new ConversionCreator_ICC(CMIL, Data, InColor, OutColor);
                c.SetConversionMethod();
            }
            else
            {
                TempColor1 = profile1.GetPCSColor(true);
                var c1 = new ConversionCreator_ICC(CMIL, Data, InColor, TempColor1, false);
                c1.SetConversionMethod();
                var c2 = new ConversionCreator_Color(c1, TempColor1, OutColor, true);
                c2.SetConversionMethod();
            }
        }

        private void DuaICCProfile_Data_Data(ILGenerator CMIL, ICCProfile profile1, ICCProfile profile2)
        {
            if (profile1.PCS == profile2.PCS)
            {
                TempColor1 = profile1.GetPCSColor(true);
                var c1 = new ConversionCreator_ICC(CMIL, Data, InColor, TempColor1, false);
                c1.SetConversionMethod();
                var c2 = new ConversionCreator_ICC(c1, TempColor1, OutColor, true);
                c1.SetConversionMethod();
            }
            else
            {
                TempColor1 = profile1.GetPCSColor(true);
                TempColor2 = profile2.GetPCSColor(true);

                var c1 = new ConversionCreator_ICC(CMIL, Data, InColor, TempColor1, false);
                c1.SetConversionMethod();

                var c2 = new ConversionCreator_Color(c1, TempColor1, TempColor2, false);
                c2.SetConversionMethod();

                var c3 = new ConversionCreator_ICC(c2, TempColor2, OutColor, true);
                c3.SetConversionMethod();
            }
        }
        
        private void SingleICCProfile(ILGenerator CMIL, ICCProfile profile)
        {
            var inType = InColor.GetType();
            var outType = OutColor.GetType();

            if (inType == profile.PCS)
            {
                if (outType == profile.DataColorspace ||
                    (outType == profile.PCS && profile.Class == ProfileClassName.Abstract))
                {
                    var c = new ConversionCreator_ICC(CMIL, Data, InColor, OutColor);
                    c.SetConversionMethod();
                }
                else
                {
                    var c = new ConversionCreator_Color(CMIL, Data, InColor, OutColor);
                    c.SetConversionMethod();
                }
            }
            else if (inType == profile.DataColorspace)
            {
                if (outType == profile.PCS ||
                    (outType == profile.DataColorspace && profile.Class == ProfileClassName.DeviceLink))
                {
                    var c = new ConversionCreator_ICC(CMIL, Data, InColor, OutColor);
                    c.SetConversionMethod();
                }
                else
                {
                    TempColor1 = profile.GetPCSColor(true);
                    var c1 = new ConversionCreator_ICC(CMIL, Data, InColor, TempColor1, false);
                    c1.SetConversionMethod();
                    var c2 = new ConversionCreator_Color(c1, TempColor1, OutColor, true);
                    c2.SetConversionMethod();
                }
            }
            else
            {
                //HTODO: using two ConversionCreator classes doesn't seem to work. (output is 0;0;0) 
                if (outType == profile.DataColorspace)
                {
                    TempColor1 = profile.GetPCSColor(true);
                    var c1 = new ConversionCreator_Color(CMIL, Data, InColor, TempColor1, false);
                    c1.SetConversionMethod();
                    var c2 = new ConversionCreator_ICC(c1, TempColor1, OutColor, true);
                    c2.SetConversionMethod();
                }
                else
                {
                    var c = new ConversionCreator_Color(CMIL, Data, InColor, OutColor);
                    c.SetConversionMethod();
                }
            }
        }

        #endregion

        #region Dispose

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

        #endregion

        /// <summary>
        /// Converts the colors given in the constructor
        /// </summary>
        public virtual void Convert()
        {
            ConversionMethod(InValues, OutValues, Data);
        }
    }
}
