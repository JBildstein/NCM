
namespace ColorManager.ColorDifference
{
    /// <summary>
    /// Method to calculate the CIE 94 color difference
    /// </summary>
    public enum CIE94DifferenceMethod
    {
        /// <summary>
        /// SL=1; K1=0.045; K2=0.015;
        /// </summary>
        GraphicArts,
        /// <summary>
        /// SL=2; K1=0.048; K2=0.014;
        /// </summary>
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
