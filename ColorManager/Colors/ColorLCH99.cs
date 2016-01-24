
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCH99 color model
    /// </summary>
    public sealed class ColorLCH99 : ColorLCH99Base
    {
        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return ModelName; }
        }
        /// <summary>
        /// The name of this model
        /// </summary>
        public static string ModelName
        {
            get { return "LCH99"; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99"/> class
        /// </summary>
        public ColorLCH99()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCH99(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
