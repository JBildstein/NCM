using ColorManager.ICC;

namespace ColorManager
{
    public class ColorYCbCr : Color
    {
        /// <summary>
        /// Y-Channel
        /// </summary>
        public double Y
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        /// <summary>
        /// Cb-Channel
        /// </summary>
        public double Cb
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        /// <summary>
        /// Cr-Channel
        /// </summary>
        public double Cr
        {
            get { return this[2]; }
            set { this[2] = value; }
        }
        //TODO: check min/max values of ColorYCbCr
        public override string Name
        {
            get { return "YCbCr"; }
        }
        public override int ChannelCount
        {
            get { return 3; }
        }
        public override double[] MinValues
        {
            get { return new double[] { 0.0, 0.0, 0.0 }; }
        }
        public override double[] MaxValues
        {
            get { return new double[] { 1.0, 1.0, 1.0 }; }
        }
        public override string[] ChannelShortNames
        {
            get { return new string[] { "Y", "Cb", "Cr" }; }
        }
        public override string[] ChannelFullNames
        {
            get { return new string[] { "Y", "Cb", "Cr" }; }
        }
        
        #region Constructor ICC

        public ColorYCbCr(ICCProfile profile)
            : base(new ColorspaceICC(profile), 0, 0, 0)
        { }

        public ColorYCbCr(double Y, double Cb, double Cr, ICCProfile profile)
            : base(new ColorspaceICC(profile), Y, Cb, Cr)
        { }

        public ColorYCbCr(ColorspaceICC space)
            : base(space, 0, 0, 0)
        { }

        public ColorYCbCr(double Y, double Cb, double Cr, ColorspaceICC space)
            : base(space, Y, Cb, Cr)
        { }

        #endregion
    }
}
