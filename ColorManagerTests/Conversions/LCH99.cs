using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    [TestClass]
    public unsafe class LCH99 : PathTestClass<ColorLCH99, ColorLab>
    {
        protected override double[] Rand_In_T
        {
            get { return new double[] { 71.613126982289, 3.424351069343, 268.332598495452  }; }
        }
        protected override double[] Rand_Out_U
        {
            get { return new double[] { 61.479656020036, 1.353667075439, -5.111627796683  }; }
        }
        protected override double[] Min_Out_U
        {
            get { return new double[] { 0.0, 0.0, 0.0  }; }
        }
        protected override double[] Max_Out_U
        {
            get { return new double[] { 99.998050791985, 477.128962659218, 136.814528454417  }; }
        }
        protected override double[] Rand_In_U
        {
            get { return new double[] { 71.613126982289, -230.05115649479, 125.137847868557  }; }
        }
        protected override double[] Rand_Out_T
        {
            get { return new double[] { 79.852083353188, 53.685941737155, 145.435018634003  }; }
        }
        protected override double[] Min_Out_T
        {
            get { return new double[] { 0.0, 61.922621178807, -158.792933361646  }; }
        }
        protected override double[] Max_Out_T
        {
            get { return new double[] { 100.001259481476, 61.922621178807, 21.207066638354  }; }
        }

        public LCH99() : base(new ColorLCH99(),
                            new ColorLab(Whitepoint.D65))
        { }

        [TestMethod]
        public void LCH99_Lab_Random()
        {
            T_U_Random();
        }

        [TestMethod]
        public void LCH99_Lab_Min()
        {
            T_U_Min();
        }

        [TestMethod]
        public void LCH99_Lab_Max()
        {
            T_U_Max();
        }

        [TestMethod]
        public void Lab_LCH99_Random()
        {
            U_T_Random();
        }

        [TestMethod]
        public void Lab_LCH99_Min()
        {
            U_T_Min();
        }

        [TestMethod]
        public void Lab_LCH99_Max()
        {
            U_T_Max();
        }
    }
}
