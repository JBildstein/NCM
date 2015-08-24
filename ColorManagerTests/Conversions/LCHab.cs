using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class LCHab : PathTestClass<ColorLCHab, ColorLab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 55.022424093924, 208.421375510595, 63.732520825105  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 55.022424093924, 92.239438696638, 186.89931973835  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 255.0, 0.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 55.022424093924, 161.842751021191, -164.712262164435  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 55.022424093924, 230.918178941011, -45.503457731945  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 360.624458405139, -135.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 100.0, 360.624458405139, 45.0  }; }
        }

        public LCHab() : base(new ColorLCHab(Whitepoint.D65),
                            new ColorLab(Whitepoint.D65))
        { }

        [TestMethod]
        public void LCHab_Lab_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void LCHab_Lab_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void LCHab_Lab_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Lab_LCHab_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Lab_LCHab_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Lab_LCHab_Max()
        {
            U_T_Max();
        }
    }
}
