using ColorManager;
using ColorManager.ColorDifference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.ColorDifferences
{
    [TestClass]
    public unsafe class CD_CMC : ColorDifference
    {
        public override double[] DeltaCOutput
        {
            get { return new double[] { -86.409632216911, -24.195336889968  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 92.848580605973, 175.33806567674  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 221.007556059494, 383.75611425126  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(68.299230753118, -6.984772069373, -183.810101017035, Whitepoint.D65);
            var col2 = new ColorLab(9.915364650039, -237.783012568151, -128.645469613208, Whitepoint.D65);

            using (var cdc = new ColorDifference_CMC(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 66.897016259328;
                col1[1] = -87.729733205275;
                col1[2] = 160.534756600174;

                col2[0] = 65.226921206446;
                col2[1] = 154.097315747103;
                col2[2] = -138.419835181637;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(68.299230753118, -6.984772069373, -183.810101017035, Whitepoint.D65);
            var col2 = new ColorLab(9.915364650039, -237.783012568151, -128.645469613208, Whitepoint.D65);

            using (var cdc = new ColorDifference_CMC(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 66.897016259328;
                col1[1] = -87.729733205275;
                col1[2] = 160.534756600174;

                col2[0] = 65.226921206446;
                col2[1] = 154.097315747103;
                col2[2] = -138.419835181637;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(68.299230753118, -6.984772069373, -183.810101017035, Whitepoint.D65);
            var col2 = new ColorLab(9.915364650039, -237.783012568151, -128.645469613208, Whitepoint.D65);

            using (var cdc = new ColorDifference_CMC(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 66.897016259328;
                col1[1] = -87.729733205275;
                col1[2] = 160.534756600174;

                col2[0] = 65.226921206446;
                col2[1] = 154.097315747103;
                col2[2] = -138.419835181637;

                DeltaH(cdc, 1);
            }
        }
    }
}
