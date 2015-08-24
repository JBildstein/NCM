using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class HSV : PathTestClass<ColorHSV, ColorRGB>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 1.87506500812, 0.557831926717, 0.333271450565  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 0.333271450565, 0.153171867084, 0.147361995176  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 1.0, 0.0, 0.0  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 0.005208513911, 0.557831926717, 0.333271450565  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 155.618788026483, 0.990662933292, 0.557831926717  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 0.0, 0.0, 1.0  }; }
        }

        public HSV() : base(new ColorHSV(ColorspaceRGB.AdobeRGB),
                            new ColorRGB(ColorspaceRGB.AdobeRGB))
        { }

        [TestMethod]
        public void HSV_RGB_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void HSV_RGB_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void HSV_RGB_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void RGB_HSV_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void RGB_HSV_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void RGB_HSV_Max()
        {
            U_T_Max();
        }
    }
}
