
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the LCHab color model
    /// </summary>
    public unsafe sealed class ColorLCHab : ColorLCHBase
    {
        #region Variables

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "LCHuv"; }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min_L, Min_C, Min_H }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_L, Max_C, Max_H }; }
        }

        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_L, Min_C, Min_H }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_L, Max_C, Max_H }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="ColorLCHBase.L"/> channel
        /// </summary>
        public static readonly double Min_L = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="ColorLCHBase.C"/> channel
        /// </summary>
        public static readonly double Min_C = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="ColorLCHBase.H"/> channel
        /// </summary>
        public static readonly double Min_H = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="ColorLCHBase.L"/> channel
        /// </summary>
        public static readonly double Max_L = 100.0;
        /// <summary>
        /// Maximum value for the <see cref="ColorLCHBase.C"/> channel
        /// </summary>
        public static readonly double Max_C = 255.0;
        /// <summary>
        /// Maximum value for the <see cref="ColorLCHBase.H"/> channel
        /// </summary>
        public static readonly double Max_H = 360.0;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        public ColorLCHab()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorLCHab(double L, double C, double H)
            : base(L, C, H)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorLCHab(Whitepoint wp)
            : base(wp)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorLCHab"/> class
        /// </summary>
        /// <param name="L">Value for the Lightness channel</param>
        /// <param name="C">Value for the Chroma channel</param>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorLCHab(double L, double C, double H, Whitepoint wp)
            : base(L, C, H, wp)
        { }
    }
}
