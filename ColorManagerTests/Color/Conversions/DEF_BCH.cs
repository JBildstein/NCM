using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class DEF_BCH : Conversion<ColorDEF, ColorBCH>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.149641973548, 0.306116474469, 0.28165740938  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.442075686334, 1.225475546729, 42.617122556746  }; }
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
            get { return new double[] { 0.224462960323, 0.459174711704, 101.396667376811  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.201212767596, -0.019658047086, 0.097522325126  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 0.106105802502, 1.496242479906, 0.0  }; }
        }

        public DEF_BCH() : base(new ColorDEF(Whitepoint.D65),
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
