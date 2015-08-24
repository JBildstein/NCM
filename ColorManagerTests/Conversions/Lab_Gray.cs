using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_Gray : PathTestClass<ColorLab, ColorGray>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 14.96419735484, -98.880598020684, -111.354721216184  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.421720084738  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.149641973548  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 1.531501900001, 0.0, 0.0  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 100.0, 0.0, 0.0  }; }
        }

        public Lab_Gray() : base(new ColorLab(Whitepoint.D65),
                            new ColorGray(new ColorspaceGray(2.2)))
        { }

        [TestMethod]
        public void Lab_Gray_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Lab_Gray_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Lab_Gray_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Gray_Lab_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Gray_Lab_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Gray_Lab_Max()
        {
            U_T_Max();
        }
    }
}
