using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class DEF_Bef : PathTestClass<ColorDEF, ColorBef>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.149641973548, 0.306116474469, 0.28165740938  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.442075686334, 0.692452636352, 0.637124859129  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.732050807569, 0.57735026919, 0.57735026919  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.299283947097, -0.387767051062, -0.43668518124  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.24294461123, -0.116052453596, -0.13069286468  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 0.0, 2.0, 2.0  }; }
        }

        public DEF_Bef() : base(new ColorDEF(Whitepoint.D65),
                            new ColorBef(Whitepoint.D65))
        { }

        [TestMethod]
        public void DEF_Bef_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void DEF_Bef_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void DEF_Bef_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Bef_DEF_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Bef_DEF_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Bef_DEF_Max()
        {
            U_T_Max();
        }
    }
}
