using System;

namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Provides methods to calculate the difference between two colors by the CIE DE2000 formula
    /// </summary>
    public unsafe sealed class ColorDifference_CIEDE2000 : ColorLabDifferenceCalculator
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifference_CIEDE2000"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        public ColorDifference_CIEDE2000(ColorLab Color1, ColorLab Color2)
            : base(Color1, Color2)
        { }
        
        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public override double DeltaE()
        {
            Vars[0] = (Col1Values[0] + Col2Values[0]) / 2d;   //L_
            Vars[1] = Math.Sqrt(Math.Pow(Col1Values[1], 2) + Math.Pow(Col1Values[2], 2));   //C1
            Vars[2] = Math.Sqrt(Math.Pow(Col2Values[1], 2) + Math.Pow(Col2Values[2], 2));   //C2
            Vars[3] = (Vars[1] + Vars[2]) / 2d;   //C_
            Vars[4] = (1 - Math.Sqrt((Math.Pow(Vars[3], 7)) / (Math.Pow(Vars[3], 7) + 6103515625))) / 2d;   //G
            Vars[5] = Col1Values[1] * (1 + Vars[4]);   //a1'
            Vars[6] = Col2Values[1] * (1 + Vars[4]);   //a2'
            Vars[7] = Math.Sqrt(Math.Pow(Vars[5], 2) + Math.Pow(Col1Values[2], 2));   //C1'
            Vars[8] = Math.Sqrt(Math.Pow(Vars[6], 2) + Math.Pow(Col2Values[2], 2));   //C2'
            Vars[9] = (Vars[7] + Vars[8]) / 2d;  //C'_
            Vars[10] = Math.Atan2(Col1Values[2], Vars[5]);  //h1'
            if (Vars[10] < 0) Vars[10] = Vars[10] + Const.Pi2;
            else if (Vars[10] >= Const.Pi2) Vars[10] = Vars[10] - Const.Pi2;
            Vars[11] = Math.Atan2(Col2Values[2], Vars[6]);  //h2'
            if (Vars[11] < 0) Vars[11] = Vars[11] + Const.Pi2;
            else if (Vars[11] >= Const.Pi2) Vars[11] = Vars[11] - Const.Pi2;
            if (Math.Abs(Vars[10] - Vars[11]) > Math.PI) Vars[12] = (Vars[10] + Vars[11] + Const.Pi2) / 2d;  //H'_
            else Vars[12] = (Vars[10] + Vars[11]) / 2d;
            Vars[13] = 1 - 0.17 * Math.Cos(Vars[12] - 0.5236) + 0.24 * Math.Cos(2 * Vars[12]) + 0.32 * Math.Cos(3 * Vars[12] + 0.10472) - 0.2 * Math.Cos(4 * Vars[12] - 1.0995574);  //T
            Vars[14] = Vars[11] - Vars[10];  //Delta h'
            if (Math.Abs(Vars[14]) > Math.PI && Vars[11] <= Vars[10]) Vars[14] = Vars[14] + Const.Pi2;
            else if (Math.Abs(Vars[14]) > Math.PI && Vars[11] > Vars[10]) Vars[14] = Vars[14] - Const.Pi2;
            Vars[15] = 2 * Math.Sqrt(Vars[7] * Vars[8]) * Math.Sin(Vars[14] / 2d);  //Delta H'
            Vars[16] = 1 + ((0.015 * Math.Pow(Vars[0] - 50, 2)) / (Math.Sqrt(20 + Math.Pow(Vars[0] - 50, 2))));  //SL
            Vars[17] = 1 + 0.045 * Vars[9];  //SC
            Vars[18] = 1 + 0.015 * Vars[9] * Vars[13];  //SH            
            Vars[19] = 1.0471976 * Math.Exp(-Math.Pow((Vars[12] - 4.799655) / 0.436332313, 2));  //Delta O
            Vars[20] = 2 * Math.Sqrt(Math.Pow(Vars[9], 7) / (Math.Pow(Vars[9], 7) + 6103515625));  //RC            
            Vars[21] = -Vars[20] * Math.Sin(2 * Vars[19]);  //RT

            return Math.Sqrt(Math.Pow((Col2Values[0] - Col1Values[0]) / Vars[16], 2) + Math.Pow((Vars[8] - Vars[7]) / Vars[17], 2) + Math.Pow(Vars[15] / Vars[18], 2) + Vars[21] * ((Vars[8] - Vars[7]) / Vars[17]) * ((Vars[15]) / Vars[18]));
        }
    }
}
