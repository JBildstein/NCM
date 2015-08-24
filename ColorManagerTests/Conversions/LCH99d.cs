using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class LCH99d : PathTestClass<ColorLCH99d, ColorXYZ>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 71.613126982289, 3.424351069343, 268.332598495452  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 68.423308919296, 1.219724990111, -2.108905718231  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 99.999440645771, 287.05675082776, 207.651107668896  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.716131269823, 0.048919300991, 0.745368329154  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.837364409818, 1.035695672524, 55.886507226588  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 16.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.16869321214, 1.834498566302, 10.304322509353  }; }
        }

        public LCH99d() : base(new ColorLCH99d(),
                            new ColorXYZ(Whitepoint.D65))
        { }

        [TestMethod]
        public void LCH99d_XYZ_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void LCH99d_XYZ_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void LCH99d_XYZ_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void XYZ_LCH99d_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void XYZ_LCH99d_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void XYZ_LCH99d_Max()
        {
            U_T_Max();
        }
    }
}
