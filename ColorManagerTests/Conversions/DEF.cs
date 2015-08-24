using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class DEF : PathTestClass<ColorDEF, ColorBCH>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.005208513911, 0.557831926717, 0.333271450565  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.649825704973, 1.562780993387, 30.855809893887  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.732050807569, 0.955316618125, 45.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.007812770867, 0.836747890076, 119.977722203349  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.005233626396, -0.002898414731, 0.005024712471  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 0.106105802502, 1.496242479906, 0.0  }; }
        }

        public DEF() : base(new ColorDEF(Whitepoint.D65),
                            new ColorBCH(Whitepoint.D65))
        { }

        [TestMethod]
        public void DEF_BCH_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void DEF_BCH_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void DEF_BCH_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void BCH_DEF_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void BCH_DEF_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void BCH_DEF_Max()
        {
            U_T_Max();
        }
    }
}
