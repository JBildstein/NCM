using System;
using System.Runtime.InteropServices;
using ColorManager.Conversion;

namespace ColorManager
{
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

        protected ColorspaceRGB(Whitepoint wp)
            : base(wp)
        { }

        public override TransformToDelegate GetTransformation(bool IsInput)
        {
            if (IsInput) return ToLinear;
            else return ToNonLinear;
        }

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
        
        public static bool operator ==(ColorspaceRGB a, ColorspaceRGB b)
        {
            if (object.ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.RefWhite == b.RefWhite && a.Gamma == b.Gamma
                && a.Cr[0] == b.Cr[0] && a.Cr[1] == b.Cr[1]
                && a.Cg[0] == b.Cg[0] && a.Cg[1] == b.Cg[1]
                && a.Cb[0] == b.Cb[0] && a.Cb[1] == b.Cb[1];
        }
        public static bool operator !=(ColorspaceRGB a, ColorspaceRGB b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            ColorspaceRGB c = obj as ColorspaceRGB;
            if ((object)c == null) return base.Equals(obj);
            return c.RefWhite == this.RefWhite && c.Gamma == this.Gamma
                && c.Cr[0] == this.Cr[0] && c.Cr[1] == this.Cr[1]
                && c.Cg[0] == this.Cg[0] && c.Cg[1] == this.Cg[1]
                && c.Cb[0] == this.Cb[0] && c.Cb[1] == this.Cb[1];
        }
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
    }
}

namespace ColorManager.Conversion
{
    public sealed unsafe class ColorspaceRGB_Data : CustomData
    {
        public override void* DataPointer
        {
            get { return dataHandle.AddrOfPinnedObject().ToPointer(); }
        }
        private GCHandle dataHandle;

        public ColorspaceRGB_Data(double[] matrix)
        {
            if (matrix == null) throw new ArgumentNullException();
            if (matrix.Length != 9) throw new ArgumentException("Matrix has to have a length of nine");

            dataHandle = GCHandle.Alloc(matrix, GCHandleType.Pinned);
        }

        protected override void Dispose(bool managed)
        {
            if (dataHandle.IsAllocated) dataHandle.Free();
        }
    }
}
