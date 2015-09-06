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
            get { return new double[] { -158.328767514046, 138.423391080227  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 69.583408679071, 24.142054288822  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 125.514805232063, 35.332263400781  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLab(37.650991190528, -74.524654810328, 52.244949284589, Whitepoint.D65);
            var col2 = new ColorLab(70.212813059433, -241.715684064997, -61.197215238678, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE94(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 21.960267991275;
                col1[1] = 165.516566241168;
                col1[2] = -129.029079964631;

                col2[0] = 3.667641849568;
                col2[1] = 66.541841321877;
                col2[2] = -26.007684796819;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLab(37.650991190528, -74.524654810328, 52.244949284589, Whitepoint.D65);
            var col2 = new ColorLab(70.212813059433, -241.715684064997, -61.197215238678, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE94(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 21.960267991275;
                col1[1] = 165.516566241168;
                col1[2] = -129.029079964631;

                col2[0] = 3.667641849568;
                col2[1] = 66.541841321877;
                col2[2] = -26.007684796819;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLab(37.650991190528, -74.524654810328, 52.244949284589, Whitepoint.D65);
            var col2 = new ColorLab(70.212813059433, -241.715684064997, -61.197215238678, Whitepoint.D65);

            using (var cdc = new ColorDifference_CIE94(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 21.960267991275;
                col1[1] = 165.516566241168;
                col1[2] = -129.029079964631;

                col2[0] = 3.667641849568;
                col2[1] = 66.541841321877;
                col2[2] = -26.007684796819;

                DeltaH(cdc, 1);
            }
        }
    }
}
