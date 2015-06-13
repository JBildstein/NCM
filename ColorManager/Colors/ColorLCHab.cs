
namespace ColorManager
{
    public unsafe sealed class ColorLCHab : ColorLCHBase
    {
        public override string Name
        {
            get { return "LCHuv"; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 100.0, 255.0, 360.0 }; }
        }

        public ColorLCHab()
            : base()
        { }

        public ColorLCHab(double L, double C, double H)
            : base(L, C, H)
        { }

        public ColorLCHab(Whitepoint wp)
            : base(wp)
        { }

        public ColorLCHab(double L, double C, double H, Whitepoint wp)
            : base(L, C, H, wp)
        { }
    }
}
