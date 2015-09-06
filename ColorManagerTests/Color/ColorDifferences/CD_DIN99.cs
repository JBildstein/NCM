using ColorManager;
using ColorManager.ColorDifference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.ColorDifferences
{
    [TestClass]
    public unsafe class CD_DIN99 : ColorDifference
    {
        public override double[] DeltaCOutput
        {
            get { return new double[] { 21.794690245201, -7.157753449007  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 63.161471267004, 72.620165774572  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { -31.393927725084, -36.593918482278  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLCH99(7.370276338128, 45.385185013239, 178.566310942437);
            var col2 = new ColorLCH99(57.657294859019, 23.590494768038, 121.231753095627);

            using (var cdc = new ColorDifference_DIN99(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 83.22850988164;
                col1[1] = 24.829945533923;
                col1[2] = 241.744964842566;

                col2[0] = 20.912058018573;
                col2[1] = 31.98769898293;
                col2[2] = 160.777604656656;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLCH99(7.370276338128, 45.385185013239, 178.566310942437);
            var col2 = new ColorLCH99(57.657294859019, 23.590494768038, 121.231753095627);

            using (var cdc = new ColorDifference_DIN99(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 83.22850988164;
                col1[1] = 24.829945533923;
                col1[2] = 241.744964842566;

                col2[0] = 20.912058018573;
                col2[1] = 31.98769898293;
                col2[2] = 160.777604656656;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLCH99(7.370276338128, 45.385185013239, 178.566310942437);
            var col2 = new ColorLCH99(57.657294859019, 23.590494768038, 121.231753095627);

            using (var cdc = new ColorDifference_DIN99(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 83.22850988164;
                col1[1] = 24.829945533923;
                col1[2] = 241.744964842566;

                col2[0] = 20.912058018573;
                col2[1] = 31.98769898293;
                col2[2] = 160.777604656656;

                DeltaH(cdc, 1);
            }
        }
    }
}
