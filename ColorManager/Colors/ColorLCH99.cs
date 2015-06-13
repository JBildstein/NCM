
namespace ColorManager
{
    public sealed class ColorLCH99 : ColorLCH99Base
    {
        public override string Name
        {
            get { return "LCH99"; }
        }

        public ColorLCH99()
            : base()
        { }

        public ColorLCH99(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
