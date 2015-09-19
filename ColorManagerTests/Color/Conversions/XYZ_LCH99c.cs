using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_LCH99c : Conversion<ColorXYZ, ColorLCH99c>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.780120709413, 0.071643347909, 0.228429964082  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.915561931257, 0.341074281923, 71.548572202843  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.173139727391, 1.994348507092, 43.228530259966  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 78.012070941279, 5.0150343536, 82.23478706937  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 75.236752313075, 0.498781569253, 3.891179933373  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 100.000062571082, 302.698238337743, 0.0  }; }
        }

        public XYZ_LCH99c() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorLCH99c())
        { }

        [TestMethod]
        public void XYZ_LCH99c_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_LCH99c_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_LCH99c_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCH99c_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCH99c_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCH99c_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
