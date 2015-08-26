using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_ConversionPath : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var cp1 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));
            var cp2 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));

            bool cmp1 = cp1 == cp2;
            bool cmp2 = cp1.Equals(cp2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var cp1 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));
            var cp2 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));

            bool cmp1 = !(cp1 == cp2);
            bool cmp2 = cp1 != cp2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            var cp1 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));
            var cp2 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));

            var cp3 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorRGB));
            var cp4 = new TestConversionPath(typeof(ColorRGB), typeof(ColorXYZ));

            Assert.IsTrue(cp1 == cp2);

            Assert.IsFalse(cp1 == cp3);
            Assert.IsFalse(cp1 == cp4);
        }

        [TestMethod]
        public override void Unequal()
        {
            var cp1 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));
            var cp2 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorLab));

            var cp3 = new TestConversionPath(typeof(ColorXYZ), typeof(ColorRGB));
            var cp4 = new TestConversionPath(typeof(ColorRGB), typeof(ColorXYZ));

            Assert.IsFalse(cp1 != cp2);

            Assert.IsTrue(cp1 != cp3);
            Assert.IsTrue(cp1 != cp4);
        }
    }
}
