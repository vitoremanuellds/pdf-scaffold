using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

/// <summary>
/// Represents the padding of a component in the SDocument.
/// </summary>
/// <param name="left">The size of the left padding.</param>
/// <param name="right">The size of the right padding.</param>
/// <param name="top">The size of the top padding.</param>
/// <param name="bottom">The size of the bottom padding.</param>
public class SPadding(
    SMeasure? left = null,
    SMeasure? right = null,
    SMeasure? top = null,
    SMeasure? bottom = null
    )
{
    /// <summary>
    /// The size of the left padding.
    /// </summary>
    public SMeasure? Left { get; } = left;

    /// <summary>
    /// The size of the right padding.
    /// </summary>
    public SMeasure? Right { get; } = right;

    /// <summary>
    /// The size of the top padding.
    /// </summary>
    public SMeasure? Top { get; } = top;

    /// <summary>
    /// The size of the bottom padding.
    /// </summary>
    public SMeasure? Bottom { get; } = bottom;

    /// <summary>
    /// Creates an SPadding with all the side sizes with the same value.
    /// </summary>
    /// <param name="value">The size used to set all sides.</param>
    public SPadding(SMeasure? value = null) : this(value, value, value, value) { }

    /// <summary>
    /// Creates an SPadding with the symetrical sides identical.
    /// </summary>
    /// <param name="leftAndRight">The size of the left and right sides.</param>
    /// <param name="topAndBottom">The size of the top and bottom sides.</param>
    public SPadding(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) : this(leftAndRight, leftAndRight, topAndBottom, topAndBottom) { }
}