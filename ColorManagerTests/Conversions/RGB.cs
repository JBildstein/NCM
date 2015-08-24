using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class RGB : PathTestClass<ColorRGB, ColorHSL>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 0.005208513911, 0.557831926717, 0.333271450565  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 155.618788026483, 0.981498615248, 0.281520220314  }; }
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
            get { return new double[] { 1.87506500812, 0.557831926717, 0.333271450565  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 0.519180905953, 0.158981738992, 0.147361995176  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 1.0, 1.0, 1.0  }; }
        }

        public RGB() : base(new ColorRGB(ColorspaceRGB.AdobeRGB),
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
