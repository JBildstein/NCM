using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_Yxy : PathTestClass<ColorXYZ, ColorYxy>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.315549002432, 0.387697047967, 0.849990960839  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.387697047967, 0.203155732286, 0.24960585227  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.31271, 0.32902  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.0, 0.333333333333, 0.333333333333  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.315549002432, -57.274505536665, 178.495390027992  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { -0.101251427749, 0.315549002432, -0.212529747501  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0, 1.0, -1.996078431373  }; }
        }

        public XYZ_Yxy() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorYxy(Whitepoint.D65))
        { }

        [TestMethod]
        public void XYZ_Yxy_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_Yxy_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_Yxy_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Yxy_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Yxy_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Yxy_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
