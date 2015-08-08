
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCH99b color model
    /// </summary>
    public sealed class ColorLCH99b : ColorLCH99Base
    {
        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "LCH99b"; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99b"/> class
        /// </summary>
        public ColorLCH99b()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCH99b"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCH99b(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
