using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_LCHab : PathTestClass<ColorLab, ColorLCHab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 14.96419735484, -98.880598020684, -111.354721216184  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 14.96419735484, 148.920269278773, -131.604387616535  }; }
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
