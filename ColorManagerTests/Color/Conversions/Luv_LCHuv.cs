using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Luv_LCHuv : Conversion<ColorLuv, ColorLCHuv>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 42.929035354373, -106.832587218649, -16.522765140293  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 42.929035354373, 108.102744922187, -171.208272031884  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 181.019335983756, -135.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 179.605122421383, 45.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 61.421368052914, 214.216007474678, 183.634709399023  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 61.421368052914, -213.785113523192, -13.580246472989  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 100.0, 255.0, 0.0  }; }
        }

        public Luv_LCHuv() : base(new ColorLuv(Whitepoint.D65),
                            new ColorLCHuv(Whitepoint.D65))
        { }

        [TestMethod]
        public void Luv_LCHuv_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Luv_LCHuv_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Luv_LCHuv_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCHuv_Luv_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCHuv_Luv_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCHuv_Luv_Max()
        {
            U_T_Max();
        }
    }
}
