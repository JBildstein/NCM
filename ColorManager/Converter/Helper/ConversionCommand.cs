using System;

namespace ColorManager.Conversion
{
    /// <summary>
    /// Interface for a conversion command
    /// </summary>
    public interface IConversionCommand
    { }

    /// <summary>
    /// A conversion command for executing a method
    /// </summary>
    public sealed class CC_ExecuteMethod : IConversionCommand
    {
        internal ConversionDelegate methodC;
        internal TransformToDelegate methodTTo;
        internal TransformDelegate methodT;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        public CC_ExecuteMethod(ConversionDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            this.methodC = method;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        public CC_ExecuteMethod(TransformToDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            this.methodTTo = method;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        public CC_ExecuteMethod(TransformDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            this.methodT = method;
        }
    }

    /// <summary>
    /// A conversion command for converting between two colors
    /// </summary>
    public sealed class CC_Convert : IConversionCommand
    {
        internal Type inColor;
        internal Type outColor;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Convert"/> class
        /// </summary>
        /// <param name="inColor">The input color type</param>
        /// <param name="outColor">The output color type</param>
        public CC_Convert(Type inColor, Type outColor)
        {
            if (inColor == null || outColor == null) throw new ArgumentNullException();
            if (!inColor.IsSubclassOf(typeof(Color)) || !outColor.IsSubclassOf(typeof(Color))) throw new ArgumentException("Type must derive from Color");

            this.inColor = inColor;
            this.outColor = outColor;
        }
    }

    /// <summary>
    /// A conversion command for checking a condition
    /// </summary>
    public sealed class CC_Condition : IConversionCommand
    {
        internal ConditionDelegate condition;
        internal IConversionCommand[] IfCommands;
        internal IConversionCommand[] ElseCommands;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Condition"/> class
        /// </summary>
        /// <param name="condition">The if condition</param>
        /// <param name="ifCommands">The commands for when the if condition is true</param>
        public CC_Condition(ConditionDelegate condition, params IConversionCommand[] ifCommands)
        {
            if (condition == null || ifCommands == null) throw new ArgumentNullException();

            this.condition = condition;
            this.IfCommands = ifCommands;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Condition"/> class
        /// </summary>
        /// <param name="condition">The if condition</param>
        /// <param name="ifCommands">The commands for when the if condition is true</param>
        /// <param name="elseCommands">The commands for when the if condition is false</param>
        public CC_Condition(ConditionDelegate condition, IConversionCommand[] ifCommands, IConversionCommand[] elseCommands)
        {
            if (condition == null || ifCommands == null || elseCommands == null) throw new ArgumentNullException();

            this.condition = condition;
            this.IfCommands = ifCommands;
            this.ElseCommands = elseCommands;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Condition"/> class
        /// </summary>
        /// <param name="condition">The if condition</param>
        /// <param name="ifCommand">The command for when the if condition is true</param>
        /// <param name="elseCommand">The command for when the if condition is false</param>
        public CC_Condition(ConditionDelegate condition, IConversionCommand ifCommand, IConversionCommand elseCommand)
        {
            if (condition == null || ifCommand == null || elseCommand == null) throw new ArgumentNullException();

            this.condition = condition;
            this.IfCommands = new IConversionCommand[] { ifCommand };
            this.ElseCommands = new IConversionCommand[] { elseCommand };
        }
    }

    /// <summary>
    /// A conversion command for assigning input to output values
    /// </summary>
    public sealed class CC_Assign : IConversionCommand
    {
        internal int channels;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Assign"/> class
        /// </summary>
        /// <param name="channels">The number of channels to assign</param>
        public CC_Assign(int channels)
        {
            if (channels < 1 || channels > Color.MaxChannels) throw new ArgumentOutOfRangeException($"Number of channels must be at least one and less than {nameof(Color.MaxChannels)}({Color.MaxChannels})");
            this.channels = channels;
        }
    }
}
