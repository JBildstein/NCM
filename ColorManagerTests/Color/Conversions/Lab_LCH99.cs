using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class Lab_LCH99 : Conversion<ColorLab, ColorLCH99>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 78.012070941279, -109.730946283313, -69.750359159196  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 84.741693247528, 42.369190027464, -168.328058316328  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 47.92536893765, -158.792933361646  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 100.001259481476, 47.771314144926, 21.207066638354  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 78.012070941279, 5.0150343536, 82.23478706937  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 69.280896658431, -1.464319302896, 7.86448656332  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 99.998050791985, 477.128962659218, 136.814528454417  }; }
        }

        public Lab_LCH99() : base(new ColorLab(Whitepoint.D65),
                            new ColorLCH99())
        { }

        [TestMethod]
        public void Lab_LCH99_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void Lab_LCH99_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void Lab_LCH99_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void LCH99_Lab_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void LCH99_Lab_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void LCH99_Lab_Max()
        {
            U_T_Max();
        }
    }
}
