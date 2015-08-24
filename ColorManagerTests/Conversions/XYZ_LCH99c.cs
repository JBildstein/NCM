using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_LCH99c : PathTestClass<ColorXYZ, ColorLCH99c>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.315549002432, 0.387697047967, 0.849990960839  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.370651156499, 1.310084325554, 64.115807856018  }; }
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
            get { return new double[] { 31.554900243205, 27.138793357713, 305.996745902112  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 28.22698197065, 20.073999692279, -29.39658556854  }; }
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
