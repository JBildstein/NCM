using ColorManager.ColorDifference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.ColorDifferences
{
    public abstract class ColorDifference
    {
        public abstract double[] DeltaEOutput { get; }
        public abstract double[] DeltaHOutput { get; }
        public abstract double[] DeltaCOutput { get; }

        protected const double Margin = 0.00005;

        protected void DeltaE(ColorDifferenceCalculator cdc, int idx)
        {
            var result = cdc.DeltaE();
            Assert.AreEqual(DeltaEOutput[idx], result, Margin);
        }

        protected void DeltaH(ColorDifferenceCalculator cdc, int idx)
        {
            var result = cdc.DeltaH();
            Assert.AreEqual(DeltaHOutput[idx], result, Margin);
        }
        
        protected void DeltaC(ColorDifferenceCalculator cdc, int idx)
        {
            var result = cdc.DeltaC();
            Assert.AreEqual(DeltaCOutput[idx], result, Margin);
        }

        public abstract void DeltaE();
        public abstract void DeltaH();
        public abstract void DeltaC();
    }
}
