using System;
using System.Runtime.InteropServices;
using ColorManager.Conversion;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of an RGB colorspace
    /// </summary>
    public abstract unsafe class ColorspaceRGB : Colorspace
    {
        /// <summary>
        /// Name of the colorspace
        /// </summary>
        public abstract override string Name { get; }
        /// <summary>
        /// The gamma value
        /// </summary>
        public abstract double Gamma { get; }
        /// <summary>
        /// Red primary
        /// </summary>
        public abstract double[] Cr { get; }
        /// <summary>
        /// Green primary
        /// </summary>
        public abstract double[] Cg { get; }
        /// <summary>
        /// Blue primary
        /// </summary>
        public abstract double[] Cb { get; }

        /// <summary>
        /// The conversion matrix (3x3)
        /// </summary>
        public abstract double[] CM { get; }
        /// <summary>
        /// The inverse conversion matrix (3x3)
        /// </summary>
        public abstract double[] ICM { get; }


        /// <summary>
        /// The default colorspace
        /// </summary>
        public static new ColorspaceRGB Default
        {
            get { return _Default; }
            set { if (value != null) _Default = value; }
        }
        private static ColorspaceRGB _Default = new Colorspace_sRGB();

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceRGB"/> class
        /// </summary>
        /// <param name="wp">The whitepoint of this colorspace</param>
        protected ColorspaceRGB(Whitepoint wp)
            : base(wp)
        { }

        /// <summary>
        /// Returns a custom delegate to transform a color
        /// </summary>
        /// <param name="IsInput">True if used for the input color, false otherwise</param>
        /// <returns>A custom delegate to transform a color</returns>
        public override TransformToDelegate GetTransformation(bool IsInput)
        {
            if (IsInput) return ToLinear;
            else return ToNonLinear;
        }

        /// <summary>
        /// Returns some custom data to transform a color
        /// </summary>
        /// <param name="IsInput">True if used for the input color, false otherwise</param>
        /// <returns>Custom data to transform a color</returns>
        public override CustomData GetData(bool IsInput)
        {
            if (IsInput) return new ColorspaceRGB_Data(CM);
            else return new ColorspaceRGB_Data(ICM);
        }

        /// <summary>
        /// Linearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to non-Linear input values</param>
        /// <param name="outVal">Pointer to linear output values</param>
        public unsafe abstract void ToLinear(double* inVal, double* outVal);
        /// <summary>
        /// Delinearizes a color
        /// </summary>
        /// <param name="inVal">Pointer to linear input values</param>
        /// <param name="outVal">Pointer to non-Linear output values</param>
        public unsafe abstract void ToNonLinear(double* inVal, double* outVal);

        /// <summary>
        /// Compares two colorspaces for their equality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(ColorspaceRGB a, ColorspaceRGB b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.RefWhite == b.RefWhite && a.Gamma == b.Gamma
                && a.Cr[0] == b.Cr[0] && a.Cr[1] == b.Cr[1]
                && a.Cg[0] == b.Cg[0] && a.Cg[1] == b.Cg[1]
                && a.Cb[0] == b.Cb[0] && a.Cb[1] == b.Cb[1];
        }
        /// <summary>
        /// Compares two colorspaces for their inequality of values
        /// </summary>
        /// <param name="a">First colorspace</param>
        /// <param name="b">Second colorspace</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(ColorspaceRGB a, ColorspaceRGB b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Compares this colorspace with another for their equality of values
        /// </summary>
        /// <param name="obj">The colorspace to compare to</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            ColorspaceRGB c = obj as ColorspaceRGB;
            if ((object)c == null) return base.Equals(obj);
            return c.RefWhite == RefWhite && c.Gamma == Gamma
                && c.Cr[0] == Cr[0] && c.Cr[1] == Cr[1]
                && c.Cg[0] == Cg[0] && c.Cg[1] == Cg[1]
                && c.Cb[0] == Cb[0] && c.Cb[1] == Cb[1];
        }
        /// <summary>
        /// Calculates a hash code of this colorspace
        /// </summary>
        /// <returns>The hash code of this colorspace</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ RefWhite.GetHashCode();
                hash *= 16777619 ^ Gamma.GetHashCode();
                hash *= 16777619 ^ Cr[0].GetHashCode();
                hash *= 16777619 ^ Cr[1].GetHashCode();
                hash *= 16777619 ^ Cg[0].GetHashCode();
                hash *= 16777619 ^ Cg[1].GetHashCode();
                hash *= 16777619 ^ Cb[0].GetHashCode();
                hash *= 16777619 ^ Cb[1].GetHashCode();
                return hash;
            }
        }

        #region Static List of RGB Colorspaces

        /// <summary>
        /// Readonly field that represents the AdobeRGB colorspace
        /// </summary>
        public static readonly Colorspace_AdobeRGB AdobeRGB = new Colorspace_AdobeRGB();
        /// <summary>
        /// Readonly field that represents the AppleRGB colorspace
        /// </summary>
        public static readonly Colorspace_AppleRGB AppleRGB = new Colorspace_AppleRGB();
        /// <summary>
        /// Readonly field that represents the BestRGB colorspace
        /// </summary>
        public static readonly Colorspace_BestRGB BestRGB = new Colorspace_BestRGB();
        /// <summary>
        /// Readonly field that represents the BetaRGB colorspace
        /// </summary>
        public static readonly Colorspace_BetaRGB BetaRGB = new Colorspace_BetaRGB();
        /// <summary>
        /// Readonly field that represents the BruceRGB colorspace
        /// </summary>
        public static readonly Colorspace_BruceRGB BruceRGB = new Colorspace_BruceRGB();
        /// <summary>
        /// Readonly field that represents the CIERGB colorspace
        /// </summary>
        public static readonly Colorspace_CIERGB CIERGB = new Colorspace_CIERGB();
        /// <summary>
        /// Readonly field that represents the ColorMatchRGB colorspace
        /// </summary>
        public static readonly Colorspace_ColorMatchRGB ColorMatchRGB = new Colorspace_ColorMatchRGB();
        /// <summary>
        /// Readonly field that represents the DonRGB4 colorspace
        /// </summary>
        public static readonly Colorspace_DonRGB4 DonRGB4 = new Colorspace_DonRGB4();
        /// <summary>
        /// Readonly field that represents the EktaSpacePS5 colorspace
        /// </summary>
        public static readonly Colorspace_EktaSpacePS5 EktaSpacePS5 = new Colorspace_EktaSpacePS5();
        /// <summary>
        /// Readonly field that represents the NTSCRGB colorspace
        /// </summary>
        public static readonly Colorspace_NTSCRGB NTSCRGB = new Colorspace_NTSCRGB();
        /// <summary>
        /// Readonly field that represents the PAL SECAMRGB colorspace
        /// </summary>
        public static readonly Colorspace_PAL_SECAMRGB PAL_SECAMRGB = new Colorspace_PAL_SECAMRGB();
        /// <summary>
        /// Readonly field that represents the ProPhotoRGB colorspace
        /// </summary>
        public static readonly Colorspace_ProPhotoRGB ProPhotoRGB = new Colorspace_ProPhotoRGB();
        /// <summary>
        /// Readonly field that represents the SMPTE C RGB colorspace
        /// </summary>
        public static readonly Colorspace_SMPTE_C_RGB SMPTE_C_RGB = new Colorspace_SMPTE_C_RGB();
        /// <summary>
        /// Readonly field that represents the sRGB colorspace
        /// </summary>
        public static readonly Colorspace_sRGB sRGB = new Colorspace_sRGB();
        /// <summary>
        /// Readonly field that represents the WideGamutRGB colorspace
        /// </summary>
        public static readonly Colorspace_WideGamutRGB WideGamutRGB = new Colorspace_WideGamutRGB();

        #endregion
    }
}

namespace ColorManager.Conversion
{
    /// <summary>
    /// Container for RGB colorspace conversion data
    /// </summary>
    public sealed unsafe class ColorspaceRGB_Data : CustomData
    {
        /// <summary>
        /// Pointer to the data
        /// </summary>
        public override void* DataPointer
        {
            get { return dataHandle.AddrOfPinnedObject().ToPointer(); }
        }
        private GCHandle dataHandle;

        /// <summary>
        /// Creates a new instance of the <see cref="ColorspaceRGB_Data"/> class
        /// </summary>
        /// <param name="matrix">The matrix for the colorspace</param>
        public ColorspaceRGB_Data(double[] matrix)
        {
            if (matrix == null) throw new ArgumentNullException();
            if (matrix.Length != 9) throw new ArgumentException("Matrix has to have a length of nine");

            dataHandle = GCHandle.Alloc(matrix, GCHandleType.Pinned);
        }

        /// <summary>
        /// Releases all allocated resources
        /// </summary>
        /// <param name="managed">True if called by user, false if called by finalizer</param>
        protected override void Dispose(bool managed)
        {
            if (dataHandle.IsAllocated) dataHandle.Free();
        }
    }
}
