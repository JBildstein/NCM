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
            return DeltaE(1, 0.045, 0.015); //CIE94DifferenceMethod.GraphicArts
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="DiffMethod">The specific way to calculate the difference</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(CIE94DifferenceMethod DiffMethod)
        {
            switch (DiffMethod)
            {
                case CIE94DifferenceMethod.GraphicArts:
                    return DeltaE(1, 0.045, 0.015);
                case CIE94DifferenceMethod.Textiles:
                    return DeltaE(2, 0.048, 0.014);

                default:
                    throw new ArgumentException("Not a valid enum value");
            }
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="SL">SL parameter</param>
        /// <param name="K1">K1 parameter</param>
        /// <param name="K2">K2 parameter</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(double SL, double K1, double K2)
        {
            Vars[1] = Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2]); //C1
            Vars[2] = Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]); //C2            

            Vars[6] = Col1Values[1] - Col2Values[1];
            Vars[7] = Col1Values[2] - Col2Values[2];
            Vars[8] = Vars[1] - Vars[2];

            Vars[5] = Vars[6] * Vars[6] + Vars[7] * Vars[7] - Vars[8] * Vars[8];

            Vars[0] = (Col1Values[0] - Col2Values[0]) / SL;
            Vars[3] = Vars[8] / (1 + K1 * Vars[1]);
            Vars[4] = 1 + K2 * Vars[1];

            return Math.Sqrt(Vars[0] * Vars[0] + Vars[3] * Vars[3] + Vars[5] / (Vars[4] * Vars[4]));
        }
    }
}
