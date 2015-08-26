using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_ColorspaceGray : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var cs1 = new ColorspaceGray();
            var cs2 = new ColorspaceGray();

            bool cmp1 = cs1 == cs2;
            bool cmp2 = cs1.Equals(cs2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var cs1 = new ColorspaceGray();
            var cs2 = new ColorspaceGray();

            bool cmp1 = !(cs1 == cs2);
            bool cmp2 = cs1 != cs2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            var cs1 = new ColorspaceGray(Whitepoint.A, 2.2);
            var cs2 = new ColorspaceGray(Whitepoint.A, 2.2);

            var cs3 = new ColorspaceGray(Whitepoint.A, 2.0);
            var cs4 = new ColorspaceGray(Whitepoint.B, 2.0);

            Assert.IsTrue(cs1 == cs2);

            Assert.IsFalse(cs1 == cs3);
            Assert.IsFalse(cs1 == cs4);
        }

        [TestMethod]
        public override void Unequal()
        {
            var cs1 = new ColorspaceGray(Whitepoint.A, 2.2);
            var cs2 = new ColorspaceGray(Whitepoint.A, 2.2);

            var cs3 = new ColorspaceGray(Whitepoint.A, 2.0);
            var cs4 = new ColorspaceGray(Whitepoint.B, 2.0);

            Assert.IsFalse(cs1 != cs2);

            Assert.IsTrue(cs1 != cs3);
            Assert.IsTrue(cs1 != cs4);
        }
    }
}
