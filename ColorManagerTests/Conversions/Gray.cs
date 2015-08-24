using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Gray : PathTestClass<ColorGray, ColorLab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.005208513911  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.000947917241, 0.0, 0.0  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 0.0, 0.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.520851391144, 29.494282625846, -85.031560211923  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.09165214833  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0  }; }
        }

        public Gray() : base(new ColorGray(new ColorspaceGray(2.2)),
                            new ColorLab(Whitepoint.D65))
        { }

        [TestMethod]
        public void Gray_Lab_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Gray_Lab_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Gray_Lab_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Lab_Gray_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Lab_Gray_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Lab_Gray_Max()
        {
            U_T_Max();
        }
    }
}
