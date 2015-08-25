using System;

namespace ColorManager
{
    internal static class Const
    {
        /// <summary>
        /// Smallest value that is not considered zero
        /// </summary>
        public const double Delta = 0.000000000000000001;
        /// <summary>
        /// Epsilon constant (216 / 24389)
        /// </summary>
        public const double Epsilon = 216d / 24389d;
        /// <summary>
        /// Kappa constant (24389 / 27)
        /// </summary>
        public const double Kappa = 24389d / 27d;
        /// <summary>
        /// Kappa * Epsilon
        /// </summary>
        public const double KapEps = Kappa * Epsilon;
        /// <summary>
        /// Pi / 180
        /// </summary>
        public const double Pi180 = Math.PI / 180d;
        /// <summary>
        /// 180 / Pi
        /// </summary>
        public const double Pi180_1 = 180d / Math.PI;
        /// <summary>
        /// Pi / 2
        /// </summary>
        public const double Pi2 = Math.PI / 2d;
        /// <summary>
        /// Cos of 16°
        /// </summary>
        public const double cos16 = 0.96126169593831886191649704855706;
        /// <summary>
        /// Sin of 16°
        /// </summary>
        public const double sin16 = 0.2756373558169991856499715746113;
        /// <summary>
        /// Cos of 26°
        /// </summary>
        public const double cos26 = 0.89879404629916699278229567669579;
        /// <summary>
        /// Sin of 26°
        /// </summary>
        public const double sin26 = 0.43837114678907741745273454065827;
        /// <summary>
        /// Cos of 50°
        /// </summary>
        public const double cos50 = 0.64278760968653932632264340990726;
        /// <summary>
        /// Sin of 50°
        /// </summary>
        public const double sin50 = 0.76604444311897803520239265055542;
        /// <summary>
        /// 1 / 3
        /// </summary>
        public const double div1_3 = 1 / 3d;

        #region LCH99 Constants

        public const double LCH99_L1 = 105.51;
        public const double LCH99_L2 = 0.0158;
        public const double LCH99_f = 0.7;
        public const double LCH99_CG = 0.045;
        public const double LCH99_Cd = 1 / 0.045;

        public const double LCH99b_L1 = 303.671;
        public const double LCH99b_L2 = 0.0039;
        public const double LCH99b_f = 0.83;
        public const double LCH99b_CG = 0.075;
        public const double LCH99b_Cd = 23;
        public const double LCH99b_angle = 26;

        public const double LCH99c_L1 = 317.651;
        public const double LCH99c_L2 = 0.0037;
        public const double LCH99c_f = 0.94;
        public const double LCH99c_CG = 0.066;
        public const double LCH99c_Cd = 23;

        public const double LCH99d_L1 = 325.221;
        public const double LCH99d_L2 = 0.0036;
        public const double LCH99d_f = 1.14;
        public const double LCH99d_CG = 0.06;
        public const double LCH99d_Cd = 22.5;
        public const double LCH99d_angle = 16;

        #endregion

        #region XYZ/DEF Matrices

        /// <summary>
        /// XYZ to DEF 3x3 transformation matrix
        /// </summary>
        public static double[,] XYZ_DEF_Matrix
        {
            get { return new double[,] { { XYZ_DEF_11, XYZ_DEF_12, XYZ_DEF_13 }, { XYZ_DEF_21, XYZ_DEF_22, XYZ_DEF_23 }, { XYZ_DEF_31, XYZ_DEF_32, XYZ_DEF_33 } }; }
        }
        /// <summary>
        /// DEF to XYZ 3x3 transformation matrix
        /// </summary>
        public static double[,] DEF_XYZ_Matrix
        {
            get { return new double[,] { { DEF_XYZ_11, DEF_XYZ_12, DEF_XYZ_13 }, { DEF_XYZ_21, DEF_XYZ_22, DEF_XYZ_23 }, { DEF_XYZ_31, DEF_XYZ_32, DEF_XYZ_33 } }; }
        }

        public const double XYZ_DEF_11 = 0.2053;
        public const double XYZ_DEF_12 = 0.7125;
        public const double XYZ_DEF_13 = 0.4670;
        public const double XYZ_DEF_21 = 1.8537;
        public const double XYZ_DEF_22 = -1.2797;
        public const double XYZ_DEF_23 = -0.4429;
        public const double XYZ_DEF_31 = -0.3655;
        public const double XYZ_DEF_32 = 1.0120;
        public const double XYZ_DEF_33 = -0.6104;

        public const double DEF_XYZ_11 = 0.671203;
        public const double DEF_XYZ_12 = 0.495489;
        public const double DEF_XYZ_13 = 0.153997;
        public const double DEF_XYZ_21 = 0.706165;
        public const double DEF_XYZ_22 = 0.0247732;
        public const double DEF_XYZ_23 = 0.522292;
        public const double DEF_XYZ_31 = 0.768864;
        public const double DEF_XYZ_32 = -0.255621;
        public const double DEF_XYZ_33 = -0.864558;

        #endregion
    }
}
