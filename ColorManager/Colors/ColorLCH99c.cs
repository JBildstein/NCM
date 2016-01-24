
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCH99c color model
    /// </summary>
    public sealed class ColorLCH99c : ColorLCH99Base
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
            get { return "LCH99c"; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99c"/> class
        /// </summary>
        public ColorLCH99c()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99c"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCH99c(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
