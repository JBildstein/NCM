using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_DEF : PathTestClass<ColorXYZ, ColorDEF>
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
