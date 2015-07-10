using System;

namespace ColorManager.ColorDifference
{
    public unsafe sealed class ColorDifference_CIE76 : ColorLabDifferenceCalculator
    {
        public ColorDifference_CIE76(ColorLab Color1, ColorLab Color2)
            : base(Color1, Color2)
        { }
        
        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public override double DeltaE()
        {
            Vars[0] = Col2Values[0] - Col1Values[0];
            Vars[1] = Col2Values[1] - Col1Values[1];
            Vars[2] = Col2Values[2] - Col1Values[2];

            return Math.Sqrt(Vars[0] * Vars[0] + Vars[1] * Vars[1] + Vars[2] * Vars[2]);
        }
    }
}
