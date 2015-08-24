using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class LCH99b : PathTestClass<ColorLCH99b, ColorLab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 71.613126982289, 3.424351069343, 268.332598495452  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 68.192920015264, 0.107931461887, -2.488611325703  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.000002114323, 276.85893992631, -21.496390511705  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 71.613126982289, -230.05115649479, 125.137847868557  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 74.796041201348, 67.105071611755, 156.629111198813  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 76.307398180223, -138.050491824795  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 99.999998198541, 76.307398180223, 41.949508175205  }; }
        }

        public LCH99b() : base(new ColorLCH99b(),
                            new ColorLab(Whitepoint.D65))
        { }

        [TestMethod]
        public void LCH99b_Lab_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void LCH99b_Lab_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void LCH99b_Lab_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Lab_LCH99b_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Lab_LCH99b_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Lab_LCH99b_Max()
        {
            U_T_Max();
        }
    }
}
