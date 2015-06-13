using System;

namespace ColorManager.Conversion
{
    public interface IConversionCommand
    { }

    public sealed class CC_ExecuteMethod : IConversionCommand
    {
        internal ConversionDelegate methodC;
        internal TransformToDelegate methodTTo;
        internal TransformDelegate methodT;

        public CC_ExecuteMethod(ConversionDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            this.methodC = method;
        }

        public CC_ExecuteMethod(TransformToDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            this.methodTTo = method;
        }

        public CC_ExecuteMethod(TransformDelegate method)
        {
            if (method == null) throw new ArgumentNullException();
            this.methodT = method;
        }
    }

    public sealed class CC_Convert : IConversionCommand
    {
        internal Type inColor;
        internal Type outColor;

        public CC_Convert(Type inColor, Type outColor)
        {
            if (inColor == null || outColor == null) throw new ArgumentNullException();
            if (!inColor.IsSubclassOf(typeof(Color)) || !outColor.IsSubclassOf(typeof(Color))) throw new ArgumentException("Type must derive from Color");

            this.inColor = inColor;
            this.outColor = outColor;
        }
    }

    public sealed class CC_Condition : IConversionCommand
    {
        internal ConditionDelegate condition;
        internal IConversionCommand[] IfCommands;
        internal IConversionCommand[] ElseCommands;

        public CC_Condition(ConditionDelegate condition, params IConversionCommand[] ifCommands)
        {
            if (condition == null || ifCommands == null) throw new ArgumentNullException();

            this.condition = condition;
            this.IfCommands = ifCommands;
        }

        public CC_Condition(ConditionDelegate condition, IConversionCommand[] ifCommands, IConversionCommand[] elseCommands)
        {
            if (condition == null || ifCommands == null || elseCommands == null) throw new ArgumentNullException();

            this.condition = condition;
            this.IfCommands = ifCommands;
            this.ElseCommands = elseCommands;
        }

        public CC_Condition(ConditionDelegate condition, IConversionCommand ifCommand, IConversionCommand elseCommand)
        {
            if (condition == null || ifCommand == null || elseCommand == null) throw new ArgumentNullException();

            this.condition = condition;
            this.IfCommands = new IConversionCommand[] { ifCommand };
            this.ElseCommands = new IConversionCommand[] { elseCommand };
        }
    }

    public sealed class CC_Assign : IConversionCommand
    {
        internal int channels;

        public CC_Assign(int channels)
        {
            if (channels < 1 || channels > Color.MaxChannels) throw new ArgumentOutOfRangeException("Number of channels must be at least one and less than Color.MaxChannels");
            this.channels = channels;
        }
    }
}
