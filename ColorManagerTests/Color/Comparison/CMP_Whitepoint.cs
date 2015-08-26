using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_Whitepoint : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var wp1 = new TestWhitepoint();
            var wp2 = new TestWhitepoint();

            bool cmp1 = wp1 == wp2;
            bool cmp2 = wp1.Equals(wp2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var wp1 = new TestWhitepoint();
            var wp2 = new TestWhitepoint();

            bool cmp1 = !(wp1 == wp2);
            bool cmp2 = wp1 != wp2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            var wp1 = new TestWhitepoint(0.5, 0.5, 0.5, 0.5, 0.5);
            var wp2 = new TestWhitepoint(0.5, 0.5, 0.5, 0.5, 0.5);

            var wp3 = new TestWhitepoint(0.7, 0.7, 0.7, 0.7, 0.7);
            var wp4 = new TestWhitepoint(0.7, 0.7, 0.7, 0.2, 0.2);

            Assert.IsTrue(wp1 == wp2);

            Assert.IsFalse(wp1 == wp3);
            Assert.IsFalse(wp1 == wp4);
        }

        [TestMethod]
        public override void Unequal()
        {
            var wp1 = new TestWhitepoint(0.5, 0.5, 0.5, 0.5, 0.5);
            var wp2 = new TestWhitepoint(0.5, 0.5, 0.5, 0.5, 0.5);

            var wp3 = new TestWhitepoint(0.7, 0.7, 0.7, 0.7, 0.7);
            var wp4 = new TestWhitepoint(0.7, 0.7, 0.7, 0.2, 0.2);

            Assert.IsFalse(wp1 != wp2);

            Assert.IsTrue(wp1 != wp3);
            Assert.IsTrue(wp1 != wp4);
        }
    }
}
