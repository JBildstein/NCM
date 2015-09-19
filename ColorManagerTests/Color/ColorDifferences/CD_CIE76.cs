using ColorManager;
using ColorManager.ColorDifference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.ColorDifferences
{
    [TestClass]
    public unsafe class CD_CIE76 : ColorDifference
    {
        public override double[] DeltaCOutput
        {
            get { return new double[] { 48.022926228728, -7.527620345142  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 76.360760302964, 118.208035285292  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 59.166053650281, 113.189187031759  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(78.012070941279, -109.730946283313, -69.750359159196, Whitepoint.D65);
            var col2 = new ColorLab(82.925559104386, -33.693161514678, -74.75821654499, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE76(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 39.920151608493;
                col1[1] = -41.740163183534;
                col1[2] = -109.078609910248;

                col2[0] = 6.683387670984;
                col2[1] = -121.116973366992;
                col2[2] = -28.036774207506;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(78.012070941279, -109.730946283313, -69.750359159196, Whitepoint.D65);
            var col2 = new ColorLab(82.925559104386, -33.693161514678, -74.75821654499, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE76(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 39.920151608493;
                col1[1] = -41.740163183534;
                col1[2] = -109.078609910248;

                col2[0] = 6.683387670984;
                col2[1] = -121.116973366992;
                col2[2] = -28.036774207506;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(78.012070941279, -109.730946283313, -69.750359159196, Whitepoint.D65);
            var col2 = new ColorLab(82.925559104386, -33.693161514678, -74.75821654499, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE76(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 39.920151608493;
                col1[1] = -41.740163183534;
                col1[2] = -109.078609910248;

                col2[0] = 6.683387670984;
                col2[1] = -121.116973366992;
                col2[2] = -28.036774207506;

                DeltaH(cdc, 1);
            }
        }
    }
}
