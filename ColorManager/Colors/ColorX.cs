using System;
using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of a color model with x number of channels
    /// </summary>
    public sealed class ColorX : Color
    {
        #region Variables

        /// <summary>
        /// The name of this model
        /// </summary>
        public override string Name
        {
            get { return "Color " + ChannelCount; }
        }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public override int ChannelCount
        {
            get
            {
                if (Values != null) return Values.Length;
                else return -1;
            }
        }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public override double[] MinValues
        {
            get
            {
                var arr = new double[min.Length];
                Buffer.BlockCopy(min, 0, arr, 0, min.Length);
                return arr;
            }
        }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public override double[] MaxValues
        {
            get
            {
                var arr = new double[max.Length];
                Buffer.BlockCopy(max, 0, arr, 0, max.Length);
                return arr;
            }
        }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public override string[] ChannelShortNames
        {
            get
            {
                var arr = new string[sNames.Length];
                Buffer.BlockCopy(sNames, 0, arr, 0, sNames.Length);
                return arr;
            }
        }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public override string[] ChannelFullNames
        {
            get
            {
                var arr = new string[fNames.Length];
                Buffer.BlockCopy(fNames, 0, arr, 0, fNames.Length);
                return arr;
            }
        }

        /// <summary>
        /// Maximum number of channels for <see cref="ColorX"/>
        /// </summary>
        public new const int MaxChannels = 15;
        /// <summary>
        /// Minimum number of channels for <see cref="ColorX"/>
        /// </summary>
        public const int MinChannels = 2;
        
        /// <summary>
        /// Minimum value for all channels
        /// </summary>
        public static readonly double Min = 0.0;
        /// <summary>
        /// Maximum value for all channels
        /// </summary>
        public static readonly double Max = 1.0;

        private double[] min;
        private double[] max;
        private string[] sNames;
        private string[] fNames;

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="ColorX"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        /// <param name="channelCount">The number of channels for this color (Max=15, Min=2)</param>
        public ColorX(ICCProfile profile, int channelCount)
            : base(new ColorspaceICC(profile), new double[channelCount])
        {
            InitArrays();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorX"/> class
        /// </summary>
        /// <param name="profile">The ICC profile for this color</param>
        /// <param name="values">The values for this color (Length Max=15, Min=2)</param>
        public ColorX(ICCProfile profile, params double[] values)
            : base(new ColorspaceICC(profile), values)
        {
            InitArrays();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorX"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        /// <param name="channelCount">The number of channels for this color (Max=15, Min=2)</param>
        public ColorX(ColorspaceICC space, int channelCount)
            : base(space, new double[channelCount])
        {
            InitArrays();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ColorX"/> class
        /// </summary>
        /// <param name="space">The ICC space for this color</param>
        /// <param name="values">The values for this color (Length Max=15, Min=2)</param>
        public ColorX(ColorspaceICC space, params double[] values)
            : base(space, values)
        {
            InitArrays();
        }

        private void InitArrays()
        {
            if (Values.Length > MaxChannels) throw new ArgumentOutOfRangeException(nameof(Values));

            int c = Values.Length;

            min = new double[Values.Length];
            for (int i = 0; i < c; i++) min[i] = Min;

            max = new double[Values.Length];
            for (int i = 0; i < c; i++) max[i] = Max;

            sNames = new string[Values.Length];
            for (int i = 0; i < c; i++) sNames[i] = "C" + (i + 1);

            fNames = new string[Values.Length];
            for (int i = 0; i < c; i++) fNames[i] = "Channel " + (i + 1);
        }
    }
}
