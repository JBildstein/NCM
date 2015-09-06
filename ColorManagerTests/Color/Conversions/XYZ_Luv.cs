using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_Luv : Conversion<ColorXYZ, ColorLuv>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.357404035985, 0.826589720196, 0.574532881996  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 92.864927879689, -119.648021449045, 54.849204651089  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.0, 16.492438258486, 6.952279872078  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 35.740403598519, 166.560757300146, 38.011769817915  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.201904590134, 0.088739191709, 0.0  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0, 1.0, 0.0  }; }
        }

        public XYZ_Luv() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorLuv(Whitepoint.D65))
        { }

        [TestMethod]
        public void XYZ_Luv_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_Luv_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_Luv_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Luv_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Luv_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Luv_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
