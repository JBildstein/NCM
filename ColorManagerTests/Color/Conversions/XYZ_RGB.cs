using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class XYZ_RGB : Conversion<ColorXYZ, ColorRGB>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.315549002432, 0.387697047967, 0.849990960839  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.398403724731, 0.70028206455, 0.914440290514  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.057881256182, 0.97615153079, 0.958246449718  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.315549002432, 0.387697047967, 0.849990960839  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.200358302997, 0.154258363041, 0.704183153813  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 0.9504701, 1.0000001, 1.08883  }; }
        }

        public XYZ_RGB() : base(new ColorXYZ(Whitepoint.D65),
                            new ColorRGB(ColorspaceRGB.AdobeRGB))
        { }

        [TestMethod]
        public void XYZ_RGB_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void XYZ_RGB_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void XYZ_RGB_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void RGB_XYZ_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void RGB_XYZ_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void RGB_XYZ_Max()
        {
            U_T_Max();
        }
    }
}
