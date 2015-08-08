using System;

namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Provides methods to calculate the difference between two colors by the CMC formula
    /// </summary>
    public unsafe sealed class ColorDifference_CMC : ColorLabDifferenceCalculator
    {
        #region Variables
        
        /// <summary>
        /// 35 degree in radians
        /// </summary>
        private const double Rad35 = 0.61086523819801535192329176897101;
        /// <summary>
        /// 164 degree in radians
        /// </summary>
        private const double Rad164 = 2.8623399732707005061548528603213;
        /// <summary>
        /// 168 degree in radians
        /// </summary>
        private const double Rad168 = 2.9321531433504736892318004910609;
        /// <summary>
        /// 345 degree in radians
        /// </summary>
        private const double Rad345 = 6.0213859193804370403867331512857;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorDifference_CMC"/> class
        /// </summary>
        /// <param name="Color1">First color to compare</param>
        /// <param name="Color2">Second color to compare</param>
        public ColorDifference_CMC(ColorLab Color1, ColorLab Color2)
            : base(Color1, Color2)
        {
            if (Color1.Space.ReferenceWhite != Whitepoint.D65
                || Color2.Space.ReferenceWhite != Whitepoint.D65)
            {
                throw new ArgumentException("Color must have a Whitepoint of D65");
            }
        }
        
        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public override double DeltaE()
        {
            return DeltaE(1, 1);    //CMCDifferenceMethod.Perceptibility
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="DiffMethod">The specific way to calculate the difference</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(CMCDifferenceMethod DiffMethod)
        {
            switch (DiffMethod)
            {
                case CMCDifferenceMethod.Acceptability:
                    return DeltaE(2, 1);

                case CMCDifferenceMethod.Perceptibility:
                    return DeltaE(1, 1);

                default:
                    throw new ArgumentException("Not a valid enum value");
            }
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="luma">Luma</param>
        /// <param name="chroma">Chromaticity</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(double luma, double chroma)
        {
            Vars[1] = Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2]); //C1
            Vars[2] = Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]); //C2
            Vars[14] = Col1Values[1] - Col2Values[1];
            Vars[15] = Col1Values[2] - Col2Values[2];
            Vars[16] = Vars[1] - Vars[2];
            Vars[3] = Vars[14] * Vars[14] + Vars[15] * Vars[15] - Vars[16] * Vars[16];

            if (Vars[3] < 0) Vars[4] = 0;       //Delta H
            else Vars[4] = Math.Sqrt(Vars[3]);

            if (Col1Values[0] < 16) Vars[5] = 0.511;    //SL
            else Vars[5] = (Col1Values[0] * 0.040975) / (1 + Col1Values[0] * 0.01765);

            Vars[6] = ((0.0638 * Vars[1]) / (1 + 0.0131 * Vars[1])) + 0.638;    //SC
            Vars[7] = Math.Atan2(Col1Values[2], Col1Values[1]);                 //H1

            if (Vars[7] < 0) Vars[7] = Vars[7] + Const.Pi2;
            else if (Vars[7] >= Const.Pi2) Vars[7] -= Const.Pi2;

            //T
            if (Vars[7] <= Rad345 && Vars[7] >= Rad164) Vars[8] = 0.56 + Math.Abs(0.2 * Math.Cos(Vars[7] + Rad168));
            else Vars[8] = 0.36 + Math.Abs(0.4 * Math.Cos(Vars[7] + Rad35));

            Vars[0] = Vars[1] * Vars[1] * Vars[1] * Vars[1];
            Vars[9] = Math.Sqrt(Vars[0] / (Vars[0] + 1900));        //F
            Vars[10] = Vars[6] * (Vars[9] * Vars[8] + 1 - Vars[9]); //SH

            Vars[11] = (Col1Values[0] - Col2Values[0]) / (luma * Vars[5]);
            Vars[12] = (Vars[1] - Vars[2]) / (chroma * Vars[6]);
            Vars[13] = Vars[4] / Vars[10];

            return Math.Sqrt(Vars[11] * Vars[11] + Vars[12] * Vars[12] + Vars[13] * Vars[13]);
        }
    }
}
