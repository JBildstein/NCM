using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class LCH99c : PathTestClass<ColorLCH99c, ColorXYZ>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 71.613126982289, 3.424351069343, 268.332598495452  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 68.346296318006, -0.070777259137, -2.58657874132  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.000062571082, 302.698238337743, 0.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.716131269823, 0.048919300991, 0.745368329154  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.840562190095, 1.042197014179, 86.006075389496  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.173139727391, 1.994348507092, 43.228530259966  }; }
        }

        public LCH99c() : base(new ColorLCH99c(),
                            new ColorXYZ(Whitepoint.D65))
        { }

        [TestMethod]
        public void LCH99c_XYZ_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void LCH99c_XYZ_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void LCH99c_XYZ_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void XYZ_LCH99c_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void XYZ_LCH99c_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void XYZ_LCH99c_Max()
        {
            U_T_Max();
        }
    }
}
