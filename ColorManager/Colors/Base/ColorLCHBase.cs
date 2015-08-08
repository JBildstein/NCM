
namespace ColorManager
{
    /// <summary>
    /// Base class for LCH colors (abstract class)
    /// </summary>
    public abstract class ColorLCHBase : Color
    {
        /// <summary>
        /// L-Channel
        /// </summary>
        public double L
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// C-Channel
        /// </summary>
        public double C
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// H-Channel
        /// </summary>
        public double H
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// The index of the channel that is cylindrical
        /// <para>If the value is bigger than 360° or smaller than 0, it will start over</para>
        /// <para>If not used, it's simply -1</para>
        /// </summary>
        public override int CylinderChannel
        {
            get { return 2; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get { return 3; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "L", "C", "H" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Lightness", "Chromaticity", "Hue" }; }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHBase"/> class
        /// </summary>
        protected ColorLCHBase()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHBase"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        protected ColorLCHBase(double L, double C, double H)
            : base(Colorspace.Default, L, C, H)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHBase"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        protected ColorLCHBase(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHBase"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="wp">The reference white for this color</param>
        protected ColorLCHBase(double L, double C, double H, Whitepoint wp)
            : base(new Colorspace(wp), L, C, H)
        { }
    }
}
