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
            get { return new double[] { 29.611104267226, -90.365805972086  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 14.775798101291, 32.674856271668  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 6.800448372164, 72.150267072391  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(18.551095437049, 39.859850965957, 20.274218251707, Whitepoint.D65);
            var col2 = new ColorLab(25.476424833982, 11.229184491085, 10.108170288432, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIEDE2000(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 36.059942741906;
                col1[1] = 2.972650137135;
                col1[2] = -14.453191817134;

                col2[0] = 50.756107743716;
                col2[1] = 61.327442426853;
                col2[2] = 85.37845719472;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(18.551095437049, 39.859850965957, 20.274218251707, Whitepoint.D65);
            var col2 = new ColorLab(25.476424833982, 11.229184491085, 10.108170288432, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIEDE2000(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 36.059942741906;
                col1[1] = 2.972650137135;
                col1[2] = -14.453191817134;

                col2[0] = 50.756107743716;
                col2[1] = 61.327442426853;
                col2[2] = 85.37845719472;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(18.551095437049, 39.859850965957, 20.274218251707, Whitepoint.D65);
            var col2 = new ColorLab(25.476424833982, 11.229184491085, 10.108170288432, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIEDE2000(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 36.059942741906;
                col1[1] = 2.972650137135;
                col1[2] = -14.453191817134;

                col2[0] = 50.756107743716;
                col2[1] = 61.327442426853;
                col2[2] = 85.37845719472;

                DeltaH(cdc, 1);
            }
        }
    }
}
