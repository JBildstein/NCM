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
        public readonly ConversionDelegate MethodC;
        public readonly ConversionExDelegate MethodCE;
        public readonly TransformToDelegate MethodTTo;
        public readonly TransformDelegate MethodT;

        public readonly double[][] MethodCEData;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        public CC_ExecuteMethod(ConversionDelegate method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            MethodC = method;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        /// <param name="data">The data that is used for the method</param>
        public CC_ExecuteMethod(ConversionExDelegate method, double[][] data)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (data == null) throw new ArgumentNullException(nameof(data));
            MethodCE = method;
            MethodCEData = data;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        public CC_ExecuteMethod(TransformToDelegate method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            MethodTTo = method;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ExecuteMethod"/> class
        /// </summary>
        /// <param name="method">The method to execute</param>
        public CC_ExecuteMethod(TransformDelegate method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            MethodT = method;
        }
    }

    /// <summary>
    /// A conversion command for converting between two colors
    /// </summary>
    public sealed class CC_Convert : IConversionCommand
    {
        /// <summary>
        /// The input color type
        /// </summary>
        public readonly Type InColor;
        /// <summary>
        /// The output color type
        /// </summary>
        public readonly Type OutColor;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Convert"/> class
        /// </summary>
        /// <param name="inColor">The input color type</param>
        /// <param name="outColor">The output color type</param>
        public CC_Convert(Type inColor, Type outColor)
        {
            if (inColor == null || outColor == null) throw new ArgumentNullException();
            if (!inColor.IsSubclassOf(typeof(Color)) || !outColor.IsSubclassOf(typeof(Color))) throw new ArgumentException("Type must derive from Color");

            InColor = inColor;
            OutColor = outColor;
        }
    }

    /// <summary>
    /// A conversion command for checking a condition
    /// </summary>
    public sealed class CC_Condition : IConversionCommand
    {
        /// <summary>
        /// The condition to check
        /// </summary>
        public readonly ConditionDelegate Condition;
        /// <summary>
        /// The commands to execute if the condition is true
        /// </summary>
        public readonly IConversionCommand[] IfCommands;
        /// <summary>
        /// The commands to execute if the condition is false
        /// </summary>
        public readonly IConversionCommand[] ElseCommands;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Condition"/> class
        /// </summary>
        /// <param name="condition">The if condition</param>
        /// <param name="ifCommands">The commands for when the if condition is true</param>
        public CC_Condition(ConditionDelegate condition, params IConversionCommand[] ifCommands)
        {
            if (condition == null || ifCommands == null) throw new ArgumentNullException();

            Condition = condition;
            IfCommands = ifCommands;
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

            Condition = condition;
            IfCommands = ifCommands;
            ElseCommands = elseCommands;
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

            Condition = condition;
            IfCommands = new IConversionCommand[] { ifCommand };
            ElseCommands = new IConversionCommand[] { elseCommand };
        }
    }

    /// <summary>
    /// A conversion command for assigning input to output values
    /// </summary>
    public sealed class CC_Assign : IConversionCommand
    {
        /// <summary>
        /// The number of channels to assign
        /// </summary>
        public readonly int Channels;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_Assign"/> class
        /// </summary>
        /// <param name="channels">The number of channels to assign</param>
        public CC_Assign(int channels)
        {
            if (channels < 1 || channels > Color.MaxChannels) throw new ArgumentOutOfRangeException($"Number of channels must be at least one and less than {nameof(Color.MaxChannels)}({Color.MaxChannels})");
            Channels = channels;
        }
    }

    /// <summary>
    /// A conversion command for writing IL code
    /// </summary>
    public sealed class CC_ILWriter : IConversionCommand
    {
        /// <summary>
        /// The method that writes the IL code
        /// </summary>
        public readonly ILWriterDelegate WriterMethod;

        /// <summary>
        /// Creates a new instance of the <see cref="CC_ILWriter"/> class
        /// </summary>
        /// <param name="method">The method that writes the IL code</param>
        public CC_ILWriter(ILWriterDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            WriterMethod = method;
        }
    }
}
