using System;

namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Provides methods to calculate the difference between two colors by the DIN99 formula
    /// </summary>
    public unsafe sealed class ColorDifference_DIN99 : ColorDifferenceCalculator
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifference_DIN99"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        public ColorDifference_DIN99(ColorLCH99 Color1, ColorLCH99 Color2)
            : base(Color1, Color2)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifference_DIN99"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        public ColorDifference_DIN99(ColorLCH99b Color1, ColorLCH99b Color2)
            : base(Color1, Color2)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifference_DIN99"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        public ColorDifference_DIN99(ColorLCH99c Color1, ColorLCH99c Color2)
            : base(Color1, Color2)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifference_DIN99"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        public ColorDifference_DIN99(ColorLCH99d Color1, ColorLCH99d Color2)
            : base(Color1, Color2)
        { }


        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public override double DeltaE()
        {
            Vars[0] = Col1Values[1] * Math.Cos(Col1Values[2] * Const.Pi180);   //a1
            Vars[1] = Col1Values[1] * Math.Sin(Col1Values[2] * Const.Pi180);   //b1
            Vars[2] = Col2Values[1] * Math.Cos(Col2Values[2] * Const.Pi180);   //a2
            Vars[3] = Col2Values[1] * Math.Sin(Col2Values[2] * Const.Pi180);   //b2

            Vars[4] = Col1Values[0] - Col2Values[0];
            Vars[5] = Vars[0] - Vars[2];
            Vars[6] = Vars[1] - Vars[3];
            return Math.Sqrt(Vars[4] * Vars[4] + Vars[5] * Vars[5] + Vars[6] * Vars[6]);
        }

        /// <summary>
        /// Calculate the hue difference between two colors
        /// </summary>
        /// <returns>The hue difference between Col1Values and Col2Values</returns>
        public override double DeltaH()
        {
            Vars[0] = Col1Values[1] * Math.Cos(Col1Values[2] * Const.Pi180);
            Vars[1] = Col1Values[1] * Math.Sin(Col1Values[2] * Const.Pi180);
            Vars[2] = Col2Values[1] * Math.Cos(Col2Values[2] * Const.Pi180);
            Vars[3] = Col2Values[1] * Math.Sin(Col2Values[2] * Const.Pi180);
            Vars[4] = Math.Sqrt(0.5 * (Col2Values[1] * Col1Values[1] + Vars[2] * Vars[0] + Vars[3] * Vars[1]));

            if (Math.Abs(Vars[4]) < Const.Delta) return 0;
            else return (Vars[0] * Vars[3] - Vars[2] * Vars[1]) / Vars[4];
        }

        /// <summary>
        /// Calculate the chroma difference between two colors
        /// </summary>
        /// <returns>The chroma difference between Color1 and Color2</returns>
        public override double DeltaC()
        {
            return Col1Values[1] - Col2Values[1];
        }
    }
}
