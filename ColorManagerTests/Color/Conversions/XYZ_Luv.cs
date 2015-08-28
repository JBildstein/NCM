using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_Luv : Conversion<ColorXYZ, ColorLuv>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.315549002432, 0.387697047967, 0.849990960839  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 68.584117134126, -46.756870621616, -59.194272527898  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 16.492438258486, 6.952279872078  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 31.554900243205, -57.274505536665, 178.495390027992  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.009989548224, 0.068898831624, -0.119041821209  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.334084125872, 1.0, -0.929955093409  }; }
        }

        public XYZ_Luv() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorLuv(Whitepoint.D65))
        { }

        [TestMethod]
        public void XYZ_Luv_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_Luv_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_Luv_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Luv_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Luv_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Luv_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
