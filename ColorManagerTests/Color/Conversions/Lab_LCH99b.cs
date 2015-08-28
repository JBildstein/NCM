using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_LCH99b : Conversion<ColorLab, ColorLCH99b>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 31.554900243205, -57.274505536665, 178.495390027992  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 35.244287999882, 58.498363847349, 106.138806601764  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 76.307398180223, -138.050491824795  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 99.999998198541, 76.307398180223, 41.949508175205  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 31.554900243205, 27.138793357713, 305.996745902112  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 28.077494154044, 20.322786028666, -29.765879005715  }; }
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
