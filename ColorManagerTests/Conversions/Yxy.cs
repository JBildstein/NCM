using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Yxy : PathTestClass<ColorYxy, ColorXYZ>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.716131269823, -230.05115649479, 125.137847868557  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { -1.31652277573, 0.716131269823, 0.606114245127  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.0, 1.0, -1.996078431373  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.716131269823, 0.048919300991, 0.745368329154  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.048919300991, 0.474127587942, 0.032387903112  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.31271, 0.32902  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0, 0.333333333333, 0.333333333333  }; }
        }

        public Yxy() : base(new ColorYxy(Whitepoint.D65),
                            new ColorXYZ(Whitepoint.D65))
        { }

        [TestMethod]
        public void Yxy_XYZ_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Yxy_XYZ_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Yxy_XYZ_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void XYZ_Yxy_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void XYZ_Yxy_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void XYZ_Yxy_Max()
        {
            U_T_Max();
        }
    }
}
