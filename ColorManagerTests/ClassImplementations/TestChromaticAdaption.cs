using System;
using ColorManager;
using ColorManager.Conversion;

namespace ColorManagerTests
{
    public sealed class TestChromaticAdaption : ChromaticAdaption
    {
        public override Type ColorType
        {
            get { return tp; }
        }
        public override ConversionDelegate Method
        {
            get { throw new NotImplementedException(); }
        }

        private Type tp;

        public TestChromaticAdaption(Type tp)
        {
            this.tp = tp;
        }

        public override CustomData GetCAData(ConversionData data)
        {
            throw new NotImplementedException();
        }
    }
}
