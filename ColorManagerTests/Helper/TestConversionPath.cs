using System;
using ColorManager.Conversion;

namespace ColorManagerTests
{
    public sealed class TestConversionPath : ConversionPath
    {
        public override IConversionCommand[] Commands
        {
            get { throw new NotImplementedException(); }
        }
        public override Type From
        {
            get { return ftp; }
        }
        public override Type To
        {
            get { return ttp; }
        }

        private Type ftp, ttp;

        public TestConversionPath(Type From, Type To)
        {
            ftp = From;
            ttp = To;
        }
    }
}
