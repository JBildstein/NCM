using System;

namespace ColorManager.ColorDifference
{
    public unsafe sealed class ColorDifference_CIE94 : ColorLabDifferenceCalculator
    {
        public ColorDifference_CIE94(ColorLab Color1, ColorLab Color2)
            : base(Color1, Color2)
        { }
        
        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public override double DeltaE()
        {
            return DeltaE(CIE94DifferenceMethod.GraphicArts);
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="DiffMethod">The specific way to calculate the difference</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(CIE94DifferenceMethod DiffMethod)
        {
            if (DiffMethod == CIE94DifferenceMethod.Textiles) Vars[0] = 2;         //SL
            else Vars[0] = 1;

            Vars[1] = Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2]); //C1
            Vars[2] = Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]); //C2

            if (DiffMethod == CIE94DifferenceMethod.GraphicArts) Vars[3] = 0.045;  //K1
            else Vars[3] = 0.048;
            if (DiffMethod == CIE94DifferenceMethod.GraphicArts) Vars[4] = 0.015;  //K2
            else Vars[4] = 0.014;

            Vars[6] = Col1Values[1] - Col2Values[1];
            Vars[7] = Col1Values[2] - Col2Values[2];
            Vars[8] = Vars[1] - Vars[2];

            Vars[5] = Vars[6] * Vars[6] + Vars[7] * Vars[7] - Vars[8] * Vars[8];

            return Math.Sqrt(Math.Pow((Col1Values[0] - Col2Values[0]) / Vars[0], 2) + Math.Pow((Vars[1] - Vars[2]) / (1 + Vars[3] * Vars[1]), 2) + Vars[5] / Math.Pow((1 + Vars[4] * Vars[1]), 2));
        }
    }
}
