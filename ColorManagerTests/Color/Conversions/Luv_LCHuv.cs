using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Luv_LCHuv : PathTestClass<ColorLuv, ColorLCHuv>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 28.871921763696, 160.664841099952, 111.975870703336  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 28.871921763696, 195.836122269215, 34.874691102286  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 360.624458405139, -135.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 360.624458405139, 45.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 14.96419735484, 78.059700989658, 101.396667376811  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 14.96419735484, -15.424622076496, 76.520572086153  }; }
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
