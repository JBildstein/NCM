
namespace ColorManager
{
    public abstract class ColorLCH99Base : ColorLCHBase
    {
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 100.0, 70.0, 360.0 }; }
        }

        private static readonly Whitepoint wp = new WhitepointD65();

        public ColorLCH99Base()
            : base(wp)
        { }

        public ColorLCH99Base(double L, double C, double H)
            : base(L, C, H, wp)
        { }
    }
}
