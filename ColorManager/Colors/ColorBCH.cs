
namespace ColorManager
{
    /// <summary>
    /// Stores information and values of the BCH color model
    /// </summary>
    public sealed unsafe class ColorBCH : Color
    {
        #region Variables

        /// <summary>
        /// B-Channel
        /// </summary>
        public double B
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
            get { return "BCH"; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get { return 3; }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get { return new double[] { Min_B, Min_C, Min_H }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get { return new double[] { Max_B, Max_C, Max_H }; }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get { return new string[] { "B", "C", "H" }; }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Brightness", "Chromaticity", "Hue"}; }
        }
        
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public static double[] Min
        {
            get { return new double[] { Min_B, Min_C, Min_H }; }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public static double[] Max
        {
            get { return new double[] { Max_B, Max_C, Max_H }; }
        }

        /// <summary>
        /// Minimum value for the <see cref="B"/> channel
        /// </summary>
        public static readonly double Min_B = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="C"/> channel
        /// </summary>
        public static readonly double Min_C = 0.0;
        /// <summary>
        /// Minimum value for the <see cref="H"/> channel
        /// </summary>
        public static readonly double Min_H = 0.0;

        /// <summary>
        /// Maximum value for the <see cref="B"/> channel
        /// </summary>
        public static readonly double Max_B = 1.5;
        /// <summary>
        /// Maximum value for the <see cref="C"/> channel
        /// </summary>
        public static readonly double Max_C = 1.5;
        /// <summary>
        /// Maximum value for the <see cref="H"/> channel
        /// </summary>
        public static readonly double Max_H = 360.0;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBCH"/> class
        /// </summary>
        public ColorBCH()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBCH"/> class
        /// </summary>
        /// <param name="B">Value for the Brightness channel</param>
        /// <param name="C">Value for the Chromaticity channel</param>
        /// <param name="H">Value for the Hue channel</param>
        public ColorBCH(double B, double C, double H)
            : base(Colorspace.Default, B, C, H)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBCH"/> class
        /// </summary>
        /// <param name="wp">The reference white for this color</param>
        public ColorBCH(Whitepoint wp)
            : base(new Colorspace(wp), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorBCH"/> class
        /// </summary>
        /// <param name="B">Value for the Brightness channel</param>
        /// <param name="C">Value for the Chromaticity channel</param>
        /// <param name="H">Value for the Hue channel</param>
        /// <param name="wp">The reference white for this color</param>
        public ColorBCH(double B, double C, double H, Whitepoint wp)
            : base(new Colorspace(wp), B, C, H)
        { }
    }
}
