using System;
using ColorManager.ICC;

namespace ColorManager
{
    /// <summary>
    /// Stores information and values of a color model
    /// </summary>
    public abstract unsafe class Color
    {
        #region Variables

        #region Colormodel Information

        /// <summary>
        /// The name of this model
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Number of channels this model has
        /// </summary>
        public abstract int ChannelCount { get; }
        /// <summary>
        /// Minimum value for each channel
        /// </summary>
        public abstract double[] MinValues { get; }
        /// <summary>
        /// Maximum value for each channel
        /// </summary>
        public abstract double[] MaxValues { get; }
        /// <summary>
        /// Names of channels short
        /// </summary>
        public abstract string[] ChannelShortNames { get; }
        /// <summary>
        /// Names of channels full
        /// </summary>
        public abstract string[] ChannelFullNames { get; }
        /// <summary>
        /// The index of the channel that is cylindrical
        /// <para>If the value is bigger than 360° or smaller than 0, it will start over</para>
        /// <para>If not used, it's simply -1</para>
        /// </summary>
        public virtual int CylinderChannel { get { return -1; } }

        #endregion

        #region Values

        /// <summary>
        /// The value of a channel at given index
        /// </summary>
        /// <param name="idx">The index of the channel</param>
        /// <returns>The value of the channel</returns>
        public double this[int idx]
        {
            get
            {
                if (idx < 0 || idx >= ChannelCount) throw new IndexOutOfRangeException();
                return Values[idx];
            }
            set
            {
                if (idx < 0 || idx >= ChannelCount) throw new IndexOutOfRangeException();

                if (idx == CylinderChannel) Values[idx] = ((value % MaxValues[idx]) + MaxValues[idx]) % MaxValues[idx];
                else
                {
                    if (value < MinValues[idx]) value = MinValues[idx];
                    else if (value > MaxValues[idx]) value = MaxValues[idx];
                    Values[idx] = value;
                }
            }
        }
        /// <summary>
        /// The colorspace of this color
        /// </summary>
        public Colorspace Space
        {
            get;
            protected set;
        }
        /// <summary>
        /// A copy of the color values array
        /// </summary>
        public double[] ValueArray
        {
            get
            {
                double[] result = new double[ChannelCount];
                Buffer.BlockCopy(Values, 0, result, 0, ChannelCount * sizeof(double));
                return result;
            }
        }
        /// <summary>
        /// The values of the color
        /// </summary>
        internal protected readonly double[] Values;

        /// <summary>
        /// The number of decimals to which the values will be compared with ==, !=, Equals and GetHasCode
        /// <para>Min: 0 - Max: 15</para>
        /// </summary>
        public static int ComparisonAccuracy
        {
            get { return _ComparisonAccuracy; }
            set
            {
                if (_ComparisonAccuracy > 15) _ComparisonAccuracy = 15;
                else if (_ComparisonAccuracy < 0) _ComparisonAccuracy = 0;
                else _ComparisonAccuracy = value;
            }
        }
        private static int _ComparisonAccuracy = 6;
        /// <summary>
        /// Maximum number of channels
        /// </summary>
        public const int MaxChannels = 32;

        #endregion

        #region ICC
        
        /// <summary>
        /// States if this color is an ICC color
        /// </summary>
        public bool IsColorICC
        {
            get { return _IsColorICC; }
        }
        private bool _IsColorICC;
        /// <summary>
        /// The ICC profile associated with this color
        /// <para>Might be null if this is not an ICC color</para>
        /// </summary>
        public ICCProfile ICCProfile
        {
            get { return _ICCProfile; }
        }
        private ICCProfile _ICCProfile;
        /// <summary>
        /// States if this is the PCS color of the associated ICC profile
        /// </summary>
        public bool IsICCPCS
        {
            get { return _IsICCPCS; }
        }
        private bool _IsICCPCS;
        /// <summary>
        /// States if this is the Data color of the associated ICC profile
        /// </summary>
        public bool IsICCDataSpace
        {
            get { return _IsICCDataSpace; }
        }
        private bool _IsICCDataSpace;

        #endregion

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="Color"/> class
        /// </summary>
        /// <param name="space">The colorspace of this color</param>
        /// <param name="values">The values of this color</param>
        protected Color(Colorspace space, params double[] values)
        {
            if (ChannelCount < 1) Values = new double[values.Length];
            else Values = new double[ChannelCount];
            Verify(space, values);
        }

        private void Verify(Colorspace space, params double[] values)
        {
            if (space == null) throw new ArgumentNullException(nameof(space));
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length > MaxChannels) throw new ArgumentOutOfRangeException(nameof(values));
            if (values.Length != ChannelCount) throw new ArgumentOutOfRangeException(nameof(values));

            for (int i = 0; i < ValueArray.Length; i++) { this[i] = values[i]; }
            this.Space = space;

            var iccSpace = space as ColorspaceICC;
            if (iccSpace != null) CheckColorTypeICC(iccSpace.Profile);
        }

        private void CheckColorTypeICC(ICCProfile profile)
        {
            _ICCProfile = profile;
            _IsICCPCS = profile.PCS == this.GetType();
            _IsICCDataSpace = profile.DataColorspace == this.GetType();

            if (profile.Class == ProfileClassName.DeviceLink && !IsICCDataSpace) throw new ColorTypeException("Color with Device Link profile has to be the ICC profiles DataColorspace type.");
            else if (profile.Class == ProfileClassName.Abstract && !IsICCPCS) throw new ColorTypeException("Color with Abstract profile has to be the ICC profiles PCS type.");
            else if (!IsICCPCS && !IsICCDataSpace) throw new ColorTypeException("Color is neither of PCS or DataColorspace type of ICC profile");
            _IsColorICC = true;
        }
        
        /// <summary>
        /// Compares two colors for their equality of values and colorspace
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public static bool operator ==(Color a, Color b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;

            if (a.Space != b.Space) return false;
            else if (a.ChannelCount != b.ChannelCount) return false;
            {
                for (int i = 0; i < a.ChannelCount; i++)
                {
                    if (Math.Round(a[i], _ComparisonAccuracy) != Math.Round(b[i], _ComparisonAccuracy)) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Compares two colors for their inequality of values and colorspace
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>False if they are the same, true otherwise</returns>
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Compares this color with another for their equality of values and colorspace
        /// </summary>
        /// <param name="obj">The color to compare to</param>
        /// <returns>True if they are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            Color c = obj as Color;
            if ((object)c == null) return false;
            return c == this;
        }
        /// <summary>
        /// Calculates a hash code of this color
        /// </summary>
        /// <returns>The hash code of this color</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash *= 16777619 ^ Space.GetHashCode();
                for (int i = 0; i < ChannelCount; i++) hash *= 16777619 ^ Math.Round(Values[i], _ComparisonAccuracy).GetHashCode();
                return hash;
            }
        }
    }
}
