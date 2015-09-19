using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_LCH99b : Conversion<ColorLab, ColorLCH99b>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 78.012070941279, -109.730946283313, -69.750359159196  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 80.662889217191, 54.585606039678, -148.64610147749  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 61.26742286972, -138.050491824795  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 99.999998198541, 61.099646056654, 41.949508175205  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 78.012070941279, 5.0150343536, 82.23478706937  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 75.105521270677, 0.196448426161, 3.715940393499  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 100.000002114323, 276.85893992631, -21.496390511705  }; }
        }

        public Lab_LCH99b() : base(new ColorLab(Whitepoint.D65),
                            new ColorLCH99b())
        { }

        [TestMethod]
        public void Lab_LCH99b_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Lab_LCH99b_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Lab_LCH99b_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCH99b_Lab_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCH99b_Lab_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCH99b_Lab_Max()
        {
            U_T_Max();
        }
    }
}
