using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_Lab : Conversion<ColorXYZ, ColorLab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.315549002432, 0.387697047967, 0.849990960839  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 68.584117134126, -18.371506969358, -38.319420763539  }; }
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
            get { return new double[] { 31.554900243205, -57.274505536665, 178.495390027992  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.024501962605, 0.068898831624, -0.086755294905  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { -0.062249569084, 0.0, 3.07130517342  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 3.27242163697, 1.0, -0.057738482062  }; }
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
