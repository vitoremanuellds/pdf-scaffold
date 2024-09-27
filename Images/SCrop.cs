using PDFScaffold.Metrics;

namespace PDFScaffold.Images;

/// <summary>
/// Represents the size of the image to be cropped from it. The Width and Height of the image
/// remains the same, only the image will be cropped.
/// </summary>
/// <remarks>
/// Creates an SCrop with the sides specified.
/// </remarks>
/// <param name="fromLeft">Size to crop from the left side.</param>
/// <param name="fromRight">Size to crop from the right side.</param>
/// <param name="fromTop">Size to crop from the top side.</param>
/// <param name="fromBottom">Size to crop from the bottom side.</param>
/// <returns>An SCrop with the sides specified.</returns>
public class SCrop(
    SMeasure? fromLeft = null,
    SMeasure? fromRight = null,
    SMeasure? fromTop = null,
    SMeasure? fromBottom = null
    )
{
    /// <summary>
    /// The size to crop from the left side.
    /// </summary>
    public SMeasure? FromLeft { get; } = fromLeft;

    /// <summary>
    /// The size to crop from the right side.
    /// </summary>
    public SMeasure? FromRight { get; } = fromRight;

    /// <summary>
    /// The size to crop from the top side.
    /// </summary>
    public SMeasure? FromTop { get; } = fromTop;

    /// <summary>
    /// The size to crop from the bottom side.
    /// </summary>
    public SMeasure? FromBottom { get; } = fromBottom;

    /// <summary>
    /// Creates an SCrop with all the same size to all its sides.
    /// </summary>
    /// <param name="value">The SMeasure used to define the size to crop from all the sides.</param>
    /// <returns>An SCrop will all sides the same value.</returns>
    public SCrop(SMeasure? value = null) : this(value, value, value, value)
    { }

    /// <summary>
    /// Creates an SCrop with the symetrical sides identic.
    /// </summary>
    /// <param name="leftAndRight">The size to crop from the left and right side.</param>
    /// <param name="topAndBottom">The size to crop from the top and bottom side.</param>
    /// <returns>An SCrop with symetrical sides identic.</returns>
    public SCrop(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) : this(leftAndRight, leftAndRight, topAndBottom, topAndBottom) { }
}