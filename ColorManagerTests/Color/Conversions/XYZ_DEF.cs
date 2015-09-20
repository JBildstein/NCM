using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_DEF : Conversion<ColorXYZ, ColorDEF>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.454626246521, 0.046609673694, 0.43789408225  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.331040697328, 0.58915098472, -0.38628745113  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.3848, 0.1311, 0.0361  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.454626246521, 0.046609673694, 0.43789408225  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.395675456137, 0.550904390149, -0.040953488956  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.320689, 1.2532302, -0.351315  }; }
        }

        protected override double[][] AdditionalDataToT
        {
            get { return new double[][] { DEF_XYZ_Matrix }; }
        }
        protected override double[][] AdditionalDataToU
        {
            get { return new double[][] { XYZ_DEF_Matrix }; }
        }

        private static double[] XYZ_DEF_Matrix = { 0.2053, 0.7125, 0.4670, 1.8537, -1.2797, -0.4429, -0.3655, 1.0120, -0.6104 };
        private static double[] DEF_XYZ_Matrix = { 0.671203, 0.495489, 0.153997, 0.706165, 0.0247732, 0.522292, 0.768864, -0.255621, -0.864558 };

        public XYZ_DEF() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorDEF(Whitepoint.D65))
        { }

        [TestMethod]
        public void XYZ_DEF_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_DEF_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_DEF_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void DEF_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void DEF_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void DEF_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
