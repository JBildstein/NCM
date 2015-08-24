using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ : PathTestClass<ColorXYZ, ColorDEF>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.716131269823, 0.048919300991, 0.745368329154  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.529963761365, 0.934766872411, -0.667212474633  }; }
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
            get { return new double[] { 0.716131269823, 0.048919300991, 0.745368329154  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.619692918812, 0.896218641152, -0.106311399914  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.320689, 1.2532302, -0.351315  }; }
        }

        public XYZ() : base(new ColorXYZ(Whitepoint.D65),
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
