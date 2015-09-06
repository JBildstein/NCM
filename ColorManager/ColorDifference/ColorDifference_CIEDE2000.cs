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
            Vars[0] = (Col1Values[0] + Col2Values[0]) * 0.5;                //L_
            Vars[22] = Col1Values[2] * Col1Values[2];                       //Col1Values[2]^2
            Vars[23] = Col2Values[2] * Col2Values[2];                       //Col2Values[2]^2
            Vars[1] = Math.Sqrt(Col1Values[1] * Col1Values[1] + Vars[22]);  //C1
            Vars[2] = Math.Sqrt(Col2Values[1] * Col2Values[1] + Vars[23]);  //C2
            Vars[3] = (Vars[1] + Vars[2]) * 0.5;                            //C_
            Vars[4] = (1 - Math.Sqrt((Math.Pow(Vars[3], 7)) / (Math.Pow(Vars[3], 7) + 6103515625))) * 0.5;   //G
            Vars[5] = Col1Values[1] * (1 + Vars[4]);                        //a1'
            Vars[6] = Col2Values[1] * (1 + Vars[4]);                        //a2'
            Vars[7] = Math.Sqrt(Vars[5] * Vars[5] + Vars[22]);              //C1'
            Vars[8] = Math.Sqrt(Vars[6] * Vars[6] + Vars[23]);              //C2'
            Vars[9] = (Vars[7] + Vars[8]) * 0.5;                            //C'_
            Vars[10] = Math.Atan2(Col1Values[2], Vars[5]);                  //h1'

            if (Vars[10] < 0) Vars[10] = Vars[10] + Const.Pi2;
            else if (Vars[10] >= Const.Pi2) Vars[10] = Vars[10] - Const.Pi2;

            Vars[11] = Math.Atan2(Col2Values[2], Vars[6]);                  //h2'

            if (Vars[11] < 0) Vars[11] = Vars[11] + Const.Pi2;
            else if (Vars[11] >= Const.Pi2) Vars[11] = Vars[11] - Const.Pi2;

            if (Math.Abs(Vars[10] - Vars[11]) > Math.PI) Vars[12] = (Vars[10] + Vars[11] + Const.Pi2) * 0.5;  //H'_
            else Vars[12] = (Vars[10] + Vars[11]) * 0.5;

            Vars[13] = 1 - 0.17 * Math.Cos(Vars[12] - 0.5236) + 0.24 * Math.Cos(2 * Vars[12]) + 0.32 * Math.Cos(3 * Vars[12] + 0.10472) - 0.2 * Math.Cos(4 * Vars[12] - 1.0995574);  //T
            Vars[14] = Vars[11] - Vars[10];             //Delta h'

            if (Math.Abs(Vars[14]) > Math.PI && Vars[11] <= Vars[10]) Vars[14] = Vars[14] + Const.Pi2;
            else if (Math.Abs(Vars[14]) > Math.PI && Vars[11] > Vars[10]) Vars[14] = Vars[14] - Const.Pi2;

            Vars[15] = 2 * Math.Sqrt(Vars[7] * Vars[8]) * Math.Sin(Vars[14] * 0.5);                                     //Delta H'
            Vars[16] = 1 + ((0.015 * Math.Pow(Vars[0] - 50, 2)) / (Math.Sqrt(20 + ((Vars[0] - 50) * (Vars[0] - 50))))); //SL
            Vars[17] = 1 + 0.045 * Vars[9];             //SC
            Vars[18] = 1 + 0.015 * Vars[9] * Vars[13];  //SH
            Vars[24] = (Vars[12] - 4.799655) * (1 / 0.436332313);
            Vars[19] = 1.0471976 * Math.Exp(-(Vars[24] * Vars[24]));                                                    //Delta O
            Vars[20] = 2 * Math.Sqrt(Math.Pow(Vars[9], 7) / (Math.Pow(Vars[9], 7) + 6103515625));                       //RC            
            Vars[21] = -Vars[20] * Math.Sin(2 * Vars[19]);                                                              //RT

            Vars[25] = (Col2Values[0] - Col1Values[0]) / Vars[16];
            Vars[26] = (Vars[8] - Vars[7]) / Vars[17];
            Vars[27] = Vars[15] / Vars[18];

            return Math.Sqrt(Vars[25] * Vars[25] + Vars[26] * Vars[26] + Vars[27] * Vars[27] + Vars[21] * ((Vars[8] - Vars[7]) / Vars[17]) * ((Vars[15]) / Vars[18]));
        }
    }
}
