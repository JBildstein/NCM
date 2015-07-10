using System;
using System.Runtime.InteropServices;

namespace ColorManager.ColorDifference
{
    public unsafe sealed class ColorDifference_CMC : ColorLabDifferenceCalculator
    {
        #region Variables

        private readonly bool AdjustColor1 = false;
        private readonly bool AdjustColor2 = false;

        private ColorConverter Converter1;
        private ColorConverter Converter2;

        private ColorLab _Color1;
        private ColorLab _Color2;

        private new double* Col1Values;
        private new double* Col2Values;

        private double* _Col1Values;
        private double* _Col2Values;

        private GCHandle _Col1ValuesHandle;
        private GCHandle _Col2ValuesHandle;

        /// <summary>
        /// 35 degree in radian
        /// </summary>
        private const double Rad35 = 0.61086523819801535192329176897101;
        /// <summary>
        /// 164 degree in radian
        /// </summary>
        private const double Rad164 = 2.8623399732707005061548528603213;
        /// <summary>
        /// 168 degree in radian
        /// </summary>
        private const double Rad168 = 2.9321531433504736892318004910609;
        /// <summary>
        /// 345 degree in radian
        /// </summary>
        private const double Rad345 = 6.0213859193804370403867331512857;

        #endregion

        #region Init/Dispose

        public ColorDifference_CMC(ColorLab Color1, ColorLab Color2)
            : base(Color1, Color2)
        {
            if (Color1.Space.ReferenceWhite != Whitepoint.D65)
            {
                AdjustColor1 = true;
                _Color1 = new ColorLab(Whitepoint.D65);
                Converter1 = new ColorConverter(Color1, _Color1);

                _Col1ValuesHandle = GCHandle.Alloc(_Color1.Values, GCHandleType.Pinned);
                _Col1Values = (double*)_Col1ValuesHandle.AddrOfPinnedObject();
            }

            if (Color2.Space.ReferenceWhite != Whitepoint.D65)
            {
                AdjustColor2 = true;
                _Color2 = new ColorLab(Whitepoint.D65);
                Converter2 = new ColorConverter(Color2, _Color2);

                _Col2ValuesHandle = GCHandle.Alloc(_Color2.Values, GCHandleType.Pinned);
                _Col2Values = (double*)_Col2ValuesHandle.AddrOfPinnedObject();
            }
        }

        protected override void Dispose(bool managed)
        {
            base.Dispose(managed);

            if (_Col1ValuesHandle.IsAllocated) _Col1ValuesHandle.Free();
            if (_Col2ValuesHandle.IsAllocated) _Col2ValuesHandle.Free();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <returns>The difference between Color1 and Color2</returns>
        public override double DeltaE()
        {
            return DeltaE(CMCDifferenceMethod.Perceptibility);
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="DiffMethod">The specific way to calculate the difference</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(CMCDifferenceMethod DiffMethod)
        {
            return DeltaE((int)DiffMethod, 1);
        }

        /// <summary>
        /// Calculate the difference between two colors
        /// </summary>
        /// <param name="luma">Luma</param>
        /// <param name="chroma">Chromaticity</param>
        /// <returns>The difference between Color1 and Color2</returns>
        public double DeltaE(double luma, double chroma)
        {
            CheckConversion();

            Vars[1] = Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2]); //C1
            Vars[2] = Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]); //C2
            Vars[14] = Col1Values[1] - Col2Values[1];
            Vars[15] = Col1Values[2] - Col2Values[2];
            Vars[16] = Vars[1] - Vars[2];
            Vars[3] = Vars[14] * Vars[14] + Vars[15] * Vars[15] - Vars[16] * Vars[16];

            if (Vars[3] < 0) Vars[4] = 0;           //Delta H
            else Vars[4] = Math.Sqrt(Vars[3]);

            if (Col1Values[0] < 16) Vars[5] = 0.511;   //SL
            else Vars[5] = (Col1Values[0] * 0.040975) / (1 + Col1Values[0] * 0.01765);

            Vars[6] = ((0.0638 * Vars[1]) / (1 + 0.0131 * Vars[1])) + 0.638;   //SC
            Vars[7] = Math.Atan2(Col1Values[2], Col1Values[1]);   //H1

            if (Vars[7] < 0) Vars[7] = Vars[7] + Const.Pi2;
            else if (Vars[7] >= Const.Pi2) Vars[7] -= Const.Pi2;

            //T
            if (Vars[7] <= Rad345 && Vars[7] >= Rad164) Vars[8] = 0.56 + Math.Abs(0.2 * Math.Cos(Vars[7] + Rad168));
            else Vars[8] = 0.36 + Math.Abs(0.4 * Math.Cos(Vars[7] + Rad35));

            Vars[0] = Vars[1] * Vars[1] * Vars[1] * Vars[1];
            Vars[9] = Math.Sqrt(Vars[0] / (Vars[0] + 1900));  //F
            Vars[10] = Vars[6] * (Vars[9] * Vars[8] + 1 - Vars[9]);  //SH

            Vars[11] = (Col1Values[0] - Col2Values[0]) / (luma * Vars[5]);
            Vars[12] = (Vars[1] - Vars[2]) / (chroma * Vars[6]);
            Vars[13] = Vars[4] / Vars[10];

            return Math.Sqrt(Vars[11] * Vars[11] + Vars[12] * Vars[12] + Vars[13] * Vars[13]);
        }

        //TODO: do something about the conversion and still using base.DeltaH/C (problem with the new Col12Values)

        /// <summary>
        /// Calculate the hue difference between two colors
        /// </summary>
        /// <returns>The hue difference between Color1 and Color2</returns>
        public override double DeltaH()
        {
            CheckConversion();

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
            CheckConversion();

            return Math.Sqrt(Col1Values[1] * Col1Values[1] + Col1Values[2] * Col1Values[2])     //C1
                 - Math.Sqrt(Col2Values[1] * Col2Values[1] + Col2Values[2] * Col2Values[2]);    //C1
        }

        #endregion

        #region Subroutines

        private void CheckConversion()
        {
            if (AdjustColor1)
            {
                Converter1.Convert();
                Col1Values = _Col1Values;
            }
            else Col1Values = base.Col1Values;

            if (AdjustColor2)
            {
                Converter2.Convert();
                Col1Values = _Col2Values;
            }
            else Col2Values = base.Col2Values;
        }

        #endregion
    }
}
