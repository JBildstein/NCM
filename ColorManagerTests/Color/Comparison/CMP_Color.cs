using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_Color : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var col1 = new TestColor();
            var col2 = new TestColor();

            bool cmp1 = col1 == col2;
            bool cmp2 = col1.Equals(col2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var col1 = new TestColor();
            var col2 = new TestColor();

            bool cmp1 = !(col1 == col2);
            bool cmp2 = col1 != col2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            var col1 = new TestColor(0.5, 0.5, 0.5, new Colorspace(Whitepoint.A));
            var col2 = new TestColor(0.5, 0.5, 0.5, new Colorspace(Whitepoint.A));

            var col3 = new TestColor(0.7, 0.7, 0.7, new Colorspace(Whitepoint.A));
            var col4 = new TestColor(0.7, 0.7, 0.7, new Colorspace(Whitepoint.B));

            Assert.IsTrue(col1 == col2);

            Assert.IsFalse(col1 == col3);
            Assert.IsFalse(col1 == col4);
        }

        [TestMethod]
        public override void Unequal()
        {
            var col1 = new TestColor(0.5, 0.5, 0.5, new Colorspace(Whitepoint.A));
            var col2 = new TestColor(0.5, 0.5, 0.5, new Colorspace(Whitepoint.A));

            var col3 = new TestColor(0.7, 0.7, 0.7, new Colorspace(Whitepoint.A));
            var col4 = new TestColor(0.7, 0.7, 0.7, new Colorspace(Whitepoint.B));

            Assert.IsFalse(col1 != col2);

            Assert.IsTrue(col1 != col3);
            Assert.IsTrue(col1 != col4);
        }
    }
}
