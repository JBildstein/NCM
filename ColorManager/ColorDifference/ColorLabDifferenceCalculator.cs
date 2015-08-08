using System;

namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Provides methods to calculate the difference between two Lab colors (abstract class)
    /// </summary>
    public unsafe abstract class ColorLabDifferenceCalculator : ColorDifferenceCalculator
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ColorLabDifferenceCalculator"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        protected ColorLabDifferenceCalculator(ColorLab Color1, ColorLab Color2)
            : base(Color1, Color2)
        { }
                        
        /// <summary>
        /// Calculate the hue difference between two colors
        /// </summary>
        /// <returns>The hue difference between Color1 and Color2</returns>
        public override double DeltaH()
        {
            Vars[1] = Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2]); //C1
            Vars[2] = Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]); //C2

            Vars[3] = Col1Values[1] - Col2Values[1];
            Vars[4] = Col1Values[2] - Col2Values[2];
            Vars[5] = Vars[1] - Vars[2];

            Vars[0] = Vars[3] * Vars[3] + Vars[4] * Vars[4] - Vars[5] * Vars[5];

            if (Vars[0] < 0) return 0;
            else return Math.Sqrt(Vars[0]);
        }

        /// <summary>
        /// Calculate the chroma difference between two colors
        /// </summary>
        /// <returns>The chroma difference between Color1 and Color2</returns>
        public override double DeltaC()
        {
            return Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2])     //C1
                 - Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]);    //C1
        }
    }
}
