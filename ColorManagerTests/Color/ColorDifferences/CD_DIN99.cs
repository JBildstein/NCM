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
            get { return new double[] { -1.450737747574, -23.825848332292  }; }
        }
        public override double[] DeltaEOutput
        {
            get { return new double[] { 46.101985083368, 54.81611834892  }; }
        }
        public override double[] DeltaHOutput
        {
            get { return new double[] { 16.846699537289, -41.006448934528  }; }
        }

        [TestMethod]
        public override void DeltaC()
        {
            var col1 = new ColorLCH99(62.487821480021, 7.729473035191, 277.020495107873);
            var col2 = new ColorLCH99(19.598695779964, 9.180210782765, 96.023577519703);

            using (var cdc = new ColorDifference_DIN99(col1, col2))
            {
                DeltaC(cdc, 0);

                col1[0] = 34.58548699952;
                col1[1] = 21.236276234843;
                col1[2] = 285.18673975169;

                col2[0] = 62.073792292305;
                col2[1] = 45.062124567135;
                col2[2] = 202.160452968516;

                DeltaC(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaE()
        {
            var col1 = new ColorLCH99(62.487821480021, 7.729473035191, 277.020495107873);
            var col2 = new ColorLCH99(19.598695779964, 9.180210782765, 96.023577519703);

            using (var cdc = new ColorDifference_DIN99(col1, col2))
            {
                DeltaE(cdc, 0);

                col1[0] = 34.58548699952;
                col1[1] = 21.236276234843;
                col1[2] = 285.18673975169;

                col2[0] = 62.073792292305;
                col2[1] = 45.062124567135;
                col2[2] = 202.160452968516;

                DeltaE(cdc, 1);
            }
        }

        [TestMethod]
        public override void DeltaH()
        {
            var col1 = new ColorLCH99(62.487821480021, 7.729473035191, 277.020495107873);
            var col2 = new ColorLCH99(19.598695779964, 9.180210782765, 96.023577519703);

            using (var cdc = new ColorDifference_DIN99(col1, col2))
            {
                DeltaH(cdc, 0);

                col1[0] = 34.58548699952;
                col1[1] = 21.236276234843;
                col1[2] = 285.18673975169;

                col2[0] = 62.073792292305;
                col2[1] = 45.062124567135;
                col2[2] = 202.160452968516;

                DeltaH(cdc, 1);
            }
        }
    }
}
