using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_Lab : Conversion<ColorXYZ, ColorLab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.780120709413, 0.071643347909, 0.228429964082  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 32.178130833248, 260.477854161322, -35.773805753782  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 8.538533672582, 5.593863452017  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 78.012070941279, -109.730946283313, -69.750359159196  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.196187885134, 0.532325034854, 1.696039503892  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { -0.031246842521, 0.0, 0.512605628496  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.87426512028, 1.0, 0.052946672514  }; }
        }

        public XYZ_Lab() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorLab(Whitepoint.D65))
        { }

        [TestMethod]
        public void XYZ_Lab_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_Lab_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_Lab_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Lab_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Lab_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Lab_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
