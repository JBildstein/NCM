
namespace ColorManager
{
    public sealed class ColorLCH99b : ColorLCH99Base
    {
        public override string Name
        {
            get { return "LCH99b"; }
        }

        public ColorLCH99b()
            : base()
        { }

        public ColorLCH99b(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
