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
            get { return new double[] { 21.180873393616, -81.555319555915  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 149.148280694403, 374.920801614176  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 145.705677420229, 365.33333909304  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(39.138992612315, -24.486125151155, 80.643811097901, Whitepoint.D65);
            var col2 = new ColorLab(62.938904458628, 23.582993459926, -58.525631187961, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE76(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 42.426603751921;
                col1[1] = 107.447638543764;
                col1[2] = 144.29999014819;

                col2[0] = 21.310527357883;
                col2[1] = 125.071628750568;
                col2[2] = -229.610579065797;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(39.138992612315, -24.486125151155, 80.643811097901, Whitepoint.D65);
            var col2 = new ColorLab(62.938904458628, 23.582993459926, -58.525631187961, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE76(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 42.426603751921;
                col1[1] = 107.447638543764;
                col1[2] = 144.29999014819;

                col2[0] = 21.310527357883;
                col2[1] = 125.071628750568;
                col2[2] = -229.610579065797;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(39.138992612315, -24.486125151155, 80.643811097901, Whitepoint.D65);
            var col2 = new ColorLab(62.938904458628, 23.582993459926, -58.525631187961, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE76(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 42.426603751921;
                col1[1] = 107.447638543764;
                col1[2] = 144.29999014819;

                col2[0] = 21.310527357883;
                col2[1] = 125.071628750568;
                col2[2] = -229.610579065797;

                DeltaH(cdc, 1);
            }
        }
    }
}
