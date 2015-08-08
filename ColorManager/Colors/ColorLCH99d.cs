
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCH99d color model
    /// </summary>
    public sealed class ColorLCH99d : ColorLCH99Base
    {
        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "LCH99d"; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99d"/> class
        /// </summary>
        public ColorLCH99d()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99d"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCH99d(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
