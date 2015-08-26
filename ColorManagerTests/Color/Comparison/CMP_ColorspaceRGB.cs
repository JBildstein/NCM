using ColorManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Comparison
{
    [TestClass]
    public class CMP_ColorspaceRGB : Compare
    {
        [TestMethod]
        public override void IsEqualSame()
        {
            var cs1 = new TestColorspaceRGB();
            var cs2 = new TestColorspaceRGB();

            bool cmp1 = cs1 == cs2;
            bool cmp2 = cs1.Equals(cs2);

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void IsUnEqualSame()
        {
            var cs1 = new TestColorspaceRGB();
            var cs2 = new TestColorspaceRGB();

            bool cmp1 = !(cs1 == cs2);
            bool cmp2 = cs1 != cs2;

            Assert.IsTrue(cmp1 == cmp2);
        }

        [TestMethod]
        public override void Equal()
        {
            double[] Cr1 = { 0.5, 0.5 };
            double[] Cg1 = { 0.5, 0.5 };
            double[] Cb1 = { 0.5, 0.5 };

            double[] Cr2 = { 0.7, 0.7 };
            double[] Cg2 = { 0.7, 0.7 };
            double[] Cb2 = { 0.7, 0.7 };

            var cs1 = new TestColorspaceRGB(Whitepoint.A, Cr1, Cg1, Cb1, 2.2);
            var cs2 = new TestColorspaceRGB(Whitepoint.A, Cr1, Cg1, Cb1, 2.2);

            var cs3 = new TestColorspaceRGB(Whitepoint.A, Cr1, Cg1, Cb1, 2.0);  //G
            var cs4 = new TestColorspaceRGB(Whitepoint.A, Cr2, Cg2, Cb2, 2.2);  //Chroma
            var cs5 = new TestColorspaceRGB(Whitepoint.B, Cr1, Cg1, Cb1, 2.2);  //WP

            var cs6 = new TestColorspaceRGB(Whitepoint.B, Cr1, Cg1, Cb1, 2.0);  //G, WP
            var cs7 = new TestColorspaceRGB(Whitepoint.A, Cr2, Cg2, Cb2, 2.0);  //Chroma, G
            var cs8 = new TestColorspaceRGB(Whitepoint.B, Cr1, Cg1, Cb1, 2.2);  //WP, Chroma
            var cs9 = new TestColorspaceRGB(Whitepoint.B, Cr2, Cg2, Cb2, 2.0);  //WP, Chroma, G

            Assert.IsTrue(cs1 == cs2);

            Assert.IsFalse(cs1 == cs3);
            Assert.IsFalse(cs1 == cs4);
            Assert.IsFalse(cs1 == cs5);
            Assert.IsFalse(cs1 == cs6);
            Assert.IsFalse(cs1 == cs7);
            Assert.IsFalse(cs1 == cs8);
            Assert.IsFalse(cs1 == cs9);
        }

        [TestMethod]
        public override void Unequal()
        {
            double[] Cr1 = { 0.5, 0.5 };
            double[] Cg1 = { 0.5, 0.5 };
            double[] Cb1 = { 0.5, 0.5 };

            double[] Cr2 = { 0.7, 0.7 };
            double[] Cg2 = { 0.7, 0.7 };
            double[] Cb2 = { 0.7, 0.7 };

            var cs1 = new TestColorspaceRGB(Whitepoint.A, Cr1, Cg1, Cb1, 2.2);
            var cs2 = new TestColorspaceRGB(Whitepoint.A, Cr1, Cg1, Cb1, 2.2);

            var cs3 = new TestColorspaceRGB(Whitepoint.A, Cr1, Cg1, Cb1, 2.0);  //G
            var cs4 = new TestColorspaceRGB(Whitepoint.A, Cr2, Cg2, Cb2, 2.2);  //Chroma
            var cs5 = new TestColorspaceRGB(Whitepoint.B, Cr1, Cg1, Cb1, 2.2);  //WP

            var cs6 = new TestColorspaceRGB(Whitepoint.B, Cr1, Cg1, Cb1, 2.0);  //G, WP
            var cs7 = new TestColorspaceRGB(Whitepoint.A, Cr2, Cg2, Cb2, 2.0);  //Chroma, G
            var cs8 = new TestColorspaceRGB(Whitepoint.B, Cr1, Cg1, Cb1, 2.2);  //WP, Chroma
            var cs9 = new TestColorspaceRGB(Whitepoint.B, Cr2, Cg2, Cb2, 2.0);  //WP, Chroma, G
            
            Assert.IsFalse(cs1 != cs2);

            Assert.IsTrue(cs1 != cs3);
            Assert.IsTrue(cs1 != cs4);
            Assert.IsTrue(cs1 != cs5);
            Assert.IsTrue(cs1 != cs6);
            Assert.IsTrue(cs1 != cs7);
            Assert.IsTrue(cs1 != cs8);
            Assert.IsTrue(cs1 != cs9);
        }
    }
}
