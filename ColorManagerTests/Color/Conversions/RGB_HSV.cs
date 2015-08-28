using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class RGB_HSV : Conversion<ColorRGB, ColorHSV>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.454626246521, 0.046609673694, 0.43789408225  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 302.460512447522, 0.897476940562, 0.454626246521  }; }
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
            get { return new double[] { 0.417483981964, 0.43789408225, 0.432337585097  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0, 0.0, 0.0  }; }
        }

        public RGB_HSV() : base(new ColorRGB(ColorspaceRGB.AdobeRGB),
                            new ColorHSV(ColorspaceRGB.AdobeRGB))
        { }

        [TestMethod]
        public void RGB_HSV_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void RGB_HSV_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void RGB_HSV_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void HSV_RGB_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void HSV_RGB_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void HSV_RGB_Max()
        {
            U_T_Max();
        }
    }
}
