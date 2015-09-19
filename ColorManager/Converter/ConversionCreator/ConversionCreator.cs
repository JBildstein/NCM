using System;
using System.Reflection.Emit;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Factory to create a conversion method (abstract class)
    /// </summary>
    public abstract class ConversionCreator : ILWriter
    {
        #region Variables
        
        /// <summary>
        /// States if it's the first conversion globally
        /// </summary>
        protected override bool IsFirstG
        {
            get { return base.IsFirstG; }
        }
        /// <summary>
        /// States if it's the last conversion globally
        /// </summary>
        protected override bool IsLastG
        {
            get { return base.IsLastG; }
        }
        
        /// <summary>
        /// The conversion data
        /// </summary>
        protected readonly ConversionData Data;
        /// <summary>
        /// The input color
        /// </summary>
        protected readonly Color InColor;
        /// <summary>
        /// The output color
        /// </summary>
        protected readonly Color OutColor;

        #endregion

        #region Init

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator"/> class
        /// </summary>
        /// <param name="CMIL">The ILGenerator for the conversion method</param>
        /// <param name="data">The conversion data</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        protected ConversionCreator(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor)
            : base(CMIL)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (inColor == null) throw new ArgumentNullException(nameof(inColor));
            if (outColor == null) throw new ArgumentNullException(nameof(outColor));
            
            Data = data;
            InColor = inColor;
            OutColor = outColor;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator"/> class
        /// </summary>
        /// <param name="CMIL">The ILGenerator for the conversion method</param>
        /// <param name="data">The conversion data</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        /// <param name="isLast">States if the output color is the last color</param>
        protected ConversionCreator(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor, bool isLast)
            : this(CMIL, data, inColor, outColor)
        {
            base.IsLastG = isLast;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConversionCreator"/> class
        /// </summary>
        /// <param name="parent">The parent <see cref="ConversionCreator"/></param>
        /// /// <param name="isLast">States if the output color is the last color</param>
        /// <param name="inColor">The input color</param>
        /// <param name="outColor">The output color</param>
        protected ConversionCreator(ConversionCreator parent, Color inColor, Color outColor, bool isLast)
            : base(parent.ILG)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (inColor == null) throw new ArgumentNullException(nameof(inColor));
            if (outColor == null) throw new ArgumentNullException(nameof(outColor));
            
            Data = parent.Data;
            InColor = inColor;
            OutColor = outColor;
            base.IsFirstG = false;
            base.IsLastG = isLast;
            IsTempVar1 = !parent.IsTempVar1;
        }

        /// <summary>
        /// Sets the conversion method
        /// </summary>
        public abstract void SetConversionMethod();

        #endregion
    }
}