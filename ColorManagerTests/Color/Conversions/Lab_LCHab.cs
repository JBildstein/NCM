using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_LCHab : Conversion<ColorLab, ColorLCHab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 61.421368052914, 86.216007474678, 2.074585824308  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 61.421368052914, 86.240963881534, 1.378422430189  }; }
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

        public Lab_LCHab() : base(new ColorLab(Whitepoint.D65),
                            new ColorLCHab(Whitepoint.D65))
        { }

        [TestMethod]
        public void Lab_LCHab_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Lab_LCHab_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Lab_LCHab_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCHab_Lab_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCHab_Lab_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCHab_Lab_Max()
        {
            U_T_Max();
        }
    }
}
