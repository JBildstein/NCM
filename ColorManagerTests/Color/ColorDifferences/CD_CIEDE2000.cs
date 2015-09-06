using ColorManager;
using ColorManager.ColorDifference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.ColorDifferences
{
    [TestClass]
    public unsafe class CD_CIEDE2000 : ColorDifference
    {
        public override double[] DeltaCOutput
        {
            get { return new double[] { 8.274960679083, 25.456771370287  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 66.816264804557, 41.967376949756  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 352.762619402627, 236.148217051562  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(17.242191625872, -204.18527980367, 0.700562993391, Whitepoint.D65);
            var col2 = new ColorLab(22.543401286259, 108.253916935182, -163.286293075088, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIEDE2000(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 29.448983617802;
                col1[1] = 124.686224836012;
                col1[2] = 134.46548863848;

                col2[0] = 71.266975531479;
                col2[1] = -111.727658807173;
                col2[2] = 111.607158734979;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(17.242191625872, -204.18527980367, 0.700562993391, Whitepoint.D65);
            var col2 = new ColorLab(22.543401286259, 108.253916935182, -163.286293075088, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIEDE2000(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 29.448983617802;
                col1[1] = 124.686224836012;
                col1[2] = 134.46548863848;

                col2[0] = 71.266975531479;
                col2[1] = -111.727658807173;
                col2[2] = 111.607158734979;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(17.242191625872, -204.18527980367, 0.700562993391, Whitepoint.D65);
            var col2 = new ColorLab(22.543401286259, 108.253916935182, -163.286293075088, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIEDE2000(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 29.448983617802;
                col1[1] = 124.686224836012;
                col1[2] = 134.46548863848;

                col2[0] = 71.266975531479;
                col2[1] = -111.727658807173;
                col2[2] = 111.607158734979;

                DeltaH(cdc, 1);
            }
        }
    }
}
