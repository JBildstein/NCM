using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_Colorspace : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var cs1 = new Colorspace();
            var cs2 = new Colorspace();

            bool cmp1 = cs1 == cs2;
            bool cmp2 = cs1.Equals(cs2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var cs1 = new Colorspace();
            var cs2 = new Colorspace();

            bool cmp1 = !(cs1 == cs2);
            bool cmp2 = cs1 != cs2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            var cs1 = new Colorspace(Whitepoint.A);
            var cs2 = new Colorspace(Whitepoint.A);
            var cs3 = new Colorspace(Whitepoint.B);

            Assert.IsTrue(cs1 == cs2);
            Assert.IsFalse(cs1 == cs3);
        }

        [TestMethod]
        public override void Unequal()
        {
            var cs1 = new Colorspace(Whitepoint.A);
            var cs2 = new Colorspace(Whitepoint.A);
            var cs3 = new Colorspace(Whitepoint.B);

            Assert.IsFalse(cs1 != cs2);
            Assert.IsTrue(cs1 != cs3);
        }
    }
}
