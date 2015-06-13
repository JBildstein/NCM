
namespace ColorManager
{
    public unsafe sealed class ColorLCHuv : ColorLCHBase
    {
        public override string Name
        {
            get { return "LCHab"; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 100.0, 255.0, 360.0 }; }
        }

        public ColorLCHuv()
            : base()
        { }

        public ColorLCHuv(double L, double C, double H)
            : base(L, C, H)
        { }

        public ColorLCHuv(Whitepoint wp)
            : base(wp)
        { }

        public ColorLCHuv(double L, double C, double H, Whitepoint wp)
            : base(L, C, H, wp)
        { }
    }
}
