
namespace ColorManager.Conversion
{
    public unsafe delegate void ConversionDelegate(double* inColor, double* outColor, ConversionData data);
    public unsafe delegate void TransformToDelegate(double* inValues, double* outValues);
    public unsafe delegate void TransformDelegate(double* values);
    public delegate bool ConditionDelegate(ConversionData data);
}
