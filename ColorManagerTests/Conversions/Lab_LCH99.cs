using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_LCH99 : PathTestClass<ColorLab, ColorLCH99>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 31.554900243205, -57.274505536665, 178.495390027992  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 42.67980799993, 42.948333661026, 92.556389010936  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 61.922621178807, -158.792933361646  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.001259481476, 61.922621178807, 21.207066638354  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 31.554900243205, 27.138793357713, 305.996745902112  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 22.063565579494, 46.953957203426, -50.432714395585  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 99.998050791985, 477.128962659218, 136.814528454417  }; }
        }

        public Lab_LCH99() : base(new ColorLab(Whitepoint.D65),
                            new ColorLCH99())
        { }

        [TestMethod]
        public void Lab_LCH99_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Lab_LCH99_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Lab_LCH99_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCH99_Lab_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCH99_Lab_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCH99_Lab_Max()
        {
            U_T_Max();
        }
    }
}
