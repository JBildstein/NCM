using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_LCH99d : Conversion<ColorXYZ, ColorLCH99d>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.069197953501, 0.580555973635, 0.666333085492  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.081006570027, 1.162587975856, 14.786332383143  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 16.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.16869321214, 1.834498566302, 10.304322509353  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 6.919795350134, 40.638918154472, 239.879910777267  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 5.973662036485, 0.208319931305, -79.951775874195  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 99.999440645771, 287.05675082776, 207.651107668896  }; }
        }

        public XYZ_LCH99d() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorLCH99d())
        { }

        [TestMethod]
        public void XYZ_LCH99d_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_LCH99d_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_LCH99d_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCH99d_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCH99d_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCH99d_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
