using System;
using ColorManager.ICC;

namespace ColorManager
{
    public sealed class ColorX : Color
    {
        //TODO: check min/max values of ColorX
        public override string Name
        {
            get { return "Color " + ChannelCount; }
        }
        public override int ChannelCount
        {
            get
            {
                if (this.Values != null) return this.Values.Length;
                else return -1;
            }
        }
        public override double[] MinValues
        {
            get
            {
                var arr = new double[min.Length];
                Buffer.BlockCopy(min, 0, arr, 0, min.Length);
                return arr;
            }
        }
        public override double[] MaxValues
        {
            get
            {
                var arr = new double[max.Length];
                Buffer.BlockCopy(max, 0, arr, 0, max.Length);
                return arr;
            }
        }
        public override string[] ChannelShortNames
        {
            get
            {
                var arr = new string[sNames.Length];
                Buffer.BlockCopy(sNames, 0, arr, 0, sNames.Length);
                return arr;
            }
        }
        public override string[] ChannelFullNames
        {
            get
            {
                var arr = new string[fNames.Length];
                Buffer.BlockCopy(fNames, 0, arr, 0, fNames.Length);
                return arr;
            }
        }
        
        public ColorX(ICCProfile profile, int channelCount)
            : base(new ColorspaceICC(profile), new double[channelCount])
        {
            InitArrays();
        }

        public ColorX(ICCProfile profile, params double[] values)
            : base(new ColorspaceICC(profile), values)
        {
            InitArrays();
        }

        public ColorX(ColorspaceICC space, int channelCount)
            : base(space, new double[channelCount])
        {
            InitArrays();
        }

        public ColorX(ColorspaceICC space, params double[] values)
            : base(space, values)
        {
            InitArrays();
        }


        private double[] min;
        private double[] max;
        private string[] sNames;
        private string[] fNames;

        private void InitArrays()
        {
            int c = this.Values.Length;

            min = new double[this.Values.Length];
            for (int i = 0; i < c; i++) min[i] = 0;

            max = new double[this.Values.Length];
            for (int i = 0; i < c; i++) max[i] = 1;

            sNames = new string[this.Values.Length];
            for (int i = 0; i < c; i++) sNames[i] = "C" + (i + 1);

            fNames = new string[this.Values.Length];
            for (int i = 0; i < c; i++) fNames[i] = "Channel " + (i + 1);
        }
    }
}
