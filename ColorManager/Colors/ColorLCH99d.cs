
namespace ColorManager
{
    public sealed class ColorLCH99d : ColorLCH99Base
    {
        public override string Name
        {
            get { return "LCH99d"; }
        }

        public ColorLCH99d()
            : base()
        { }

        public ColorLCH99d(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
