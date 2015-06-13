
namespace ColorManager
{
    public sealed class ColorLCH99c : ColorLCH99Base
    {
        public override string Name
        {
            get { return "LCH99c"; }
        }

        public ColorLCH99c()
            : base()
        { }

        public ColorLCH99c(double L, double C, double H)
            : base(L, C, H)
        { }
    }
}
