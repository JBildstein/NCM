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
            get { return new double[] { -2.350194091289, 7.221956402131  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 95.953655648734, 36.27469483365  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 187.867511692714, 48.961146434267  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(56.54364036468, -116.587189881288, 0.552391820495, Whitepoint.D65);
            var col2 = new ColorLab(15.649015053943, 31.881040123352, -114.586263900197, Whitepoint.D65);

            using (var cdc = new ColorDifference_CMC(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 31.310532664093;
                col1[1] = 81.583387114612;
                col1[2] = -41.510887980932;

                col2[0] = 36.092706008857;
                col2[1] = 83.941777394475;
                col2[2] = 7.923801354796;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(56.54364036468, -116.587189881288, 0.552391820495, Whitepoint.D65);
            var col2 = new ColorLab(15.649015053943, 31.881040123352, -114.586263900197, Whitepoint.D65);

            using (var cdc = new ColorDifference_CMC(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 31.310532664093;
                col1[1] = 81.583387114612;
                col1[2] = -41.510887980932;

                col2[0] = 36.092706008857;
                col2[1] = 83.941777394475;
                col2[2] = 7.923801354796;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(56.54364036468, -116.587189881288, 0.552391820495, Whitepoint.D65);
            var col2 = new ColorLab(15.649015053943, 31.881040123352, -114.586263900197, Whitepoint.D65);

            using (var cdc = new ColorDifference_CMC(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 31.310532664093;
                col1[1] = 81.583387114612;
                col1[2] = -41.510887980932;

                col2[0] = 36.092706008857;
                col2[1] = 83.941777394475;
                col2[2] = 7.923801354796;

                DeltaH(cdc, 1);
            }
        }
    }
}
