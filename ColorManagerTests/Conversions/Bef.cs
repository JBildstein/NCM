using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Bef : PathTestClass<ColorBef, ColorDEF>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.010417027823, 0.115663853435, -0.33345709887  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.009746621461, 0.001204873579, -0.003473631877  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 0.0, 2.0, 2.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.005208513911, 0.557831926717, 0.333271450565  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.649825704973, 0.858433149763, 0.512862830778  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.732050807569, 0.57735026919, 0.57735026919  }; }
        }

        public Bef() : base(new ColorBef(Whitepoint.D65),
                            new ColorDEF(Whitepoint.D65))
        { }

        [TestMethod]
        public void Bef_DEF_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Bef_DEF_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Bef_DEF_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void DEF_Bef_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void DEF_Bef_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void DEF_Bef_Max()
        {
            U_T_Max();
        }
    }
}
