using ColorManager;

namespace ColorManagerTests
{
    public sealed class TestColor : Color
    {
        public override string Name
        {
            get { return "Test"; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "1", "2", "3" }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "1", "2", "3" }; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0, 0, 0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 1, 1, 1 }; }
        }

        public TestColor()
            : base(Colorspace.Default, 0, 0, 0)
        { }

        public TestColor(Colorspace space)
            : base(space, 0, 0, 0)
        { }

        public TestColor(double val1, double val2, double val3)
            : base(Colorspace.Default, val1, val2, val3)
        { }

        public TestColor(double val1, double val2, double val3, Colorspace space)
            : base(space, val1, val2, val3)
        { }
    }
}
