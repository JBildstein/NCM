using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_ChromaticAdaption : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var ca1 = new TestChromaticAdaption(typeof(ColorXYZ));
            var ca2 = new TestChromaticAdaption(typeof(ColorXYZ));

            bool cmp1 = ca1 == ca2;
            bool cmp2 = ca1.Equals(ca2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var ca1 = new TestChromaticAdaption(typeof(ColorXYZ));
            var ca2 = new TestChromaticAdaption(typeof(ColorXYZ));

            bool cmp1 = !(ca1 == ca2);
            bool cmp2 = ca1 != ca2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            var ca1 = new TestChromaticAdaption(typeof(ColorXYZ));
            var ca2 = new TestChromaticAdaption(typeof(ColorXYZ));
            var ca3 = new TestChromaticAdaption(typeof(ColorLab));

            Assert.IsTrue(ca1 == ca2);
            Assert.IsFalse(ca1 == ca3);
        }

        [TestMethod]
        public override void Unequal()
        {
            var ca1 = new TestChromaticAdaption(typeof(ColorXYZ));
            var ca2 = new TestChromaticAdaption(typeof(ColorXYZ));
            var ca3 = new TestChromaticAdaption(typeof(ColorLab));

            Assert.IsFalse(ca1 != ca2);
            Assert.IsTrue(ca1 != ca3);
        }
    }
}
