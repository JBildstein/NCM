using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class RGB_HSL : Conversion<ColorRGB, ColorHSL>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.149641973548, 0.306116474469, 0.28165740938  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 170.621194528715, 0.343327702649, 0.227879224009  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 0.0, 0.0, 1.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 163.66544874742, 0.046609673694, 0.43789408225  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.417483981964, 0.458304182536, 0.44719118823  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0, 1.0, 1.0  }; }
        }

        public RGB_HSL() : base(new ColorRGB(ColorspaceRGB.AdobeRGB),
                            new ColorHSL(ColorspaceRGB.AdobeRGB))
        { }

        [TestMethod]
        public void RGB_HSL_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void RGB_HSL_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void RGB_HSL_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void HSL_RGB_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void HSL_RGB_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void HSL_RGB_Max()
        {
            U_T_Max();
        }
    }
}
