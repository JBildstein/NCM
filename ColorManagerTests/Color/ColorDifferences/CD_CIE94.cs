using ColorManager;
using ColorManager.ColorDifference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.ColorDifferences
{
    [TestClass]
    public unsafe class CD_CIE94 : ColorDifference
    {
        public override double[] DeltaCOutput
        {
            get { return new double[] { -17.801131119653, -43.315605043277  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 62.264246596784, 55.144345225101  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 61.172362727893, 70.459648338603  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(35.679418652169, 3.282836838408, -40.730857987297, Whitepoint.D65);
            var col2 = new ColorLab(84.660455880063, -56.01410114533, -17.43254524571, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE94(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 83.955578740665;
                col1[1] = -31.540876770993;
                col1[2] = 41.770289617833;

                col2[0] = 47.674533462932;
                col2[1] = -94.988220274094;
                col2[2] = -11.288529504574;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(35.679418652169, 3.282836838408, -40.730857987297, Whitepoint.D65);
            var col2 = new ColorLab(84.660455880063, -56.01410114533, -17.43254524571, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE94(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 83.955578740665;
                col1[1] = -31.540876770993;
                col1[2] = 41.770289617833;

                col2[0] = 47.674533462932;
                col2[1] = -94.988220274094;
                col2[2] = -11.288529504574;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(35.679418652169, 3.282836838408, -40.730857987297, Whitepoint.D65);
            var col2 = new ColorLab(84.660455880063, -56.01410114533, -17.43254524571, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE94(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 83.955578740665;
                col1[1] = -31.540876770993;
                col1[2] = 41.770289617833;

                col2[0] = 47.674533462932;
                col2[1] = -94.988220274094;
                col2[2] = -11.288529504574;

                DeltaH(cdc, 1);
            }
        }
    }
}
