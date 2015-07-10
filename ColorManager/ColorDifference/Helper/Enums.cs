
namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Method to calculate the CIE 94 color difference
    /// </summary>
    public enum CIE94DifferenceMethod
    {
        GraphicArts,
        Textiles,
    }

    /// <summary>
    /// Method to calculate the CMC color difference
    /// </summary>
    public enum CMCDifferenceMethod
    {
        /// <summary>
        /// luma:chromaticity = 2:1
        /// </summary>
        Acceptability = 2,
        /// <summary>
        /// luma:chromaticity = 1:1
        /// </summary>
        Perceptibility = 1,
    }
}
