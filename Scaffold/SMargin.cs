using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Scaffold;

/// <summary>
/// Defines an object that describes the margin of an element.
/// </summary>
/// <remarks>
/// Creates an SMargin with values for each side.
/// </remarks>
/// <param name="left">The size of the left margin.</param>
/// <param name="right">The size of the right margin.</param>
/// <param name="top">The size of the top margin.</param>
/// <param name="bottom">The size of the bottom margin.</param>
/// <returns>An SMargin with values for each side.</returns>
public class SMargin(SMeasure? left = null, SMeasure? right = null, SMeasure? top = null, SMeasure? bottom = null)
{
    /// <summary>
    /// The left margin.
    /// </summary>
    public SMeasure? Left { get; } = left;
    /// <summary>
    /// The right margin.
    /// </summary>
    public SMeasure? Right { get; } = right;
    /// <summary>
    /// The top margin.
    /// </summary>
    public SMeasure? Top { get; } = top;
    /// <summary>
    /// The bottom margin.
    /// </summary>
    public SMeasure? Bottom { get; } = bottom;

    /// <summary>
    /// Creates an SMargin object with all the margins the same as <paramref name="value"></paramref>
    /// </summary>
    /// <param name="value">The value of the margins.</param>
    /// <returns>An SMargin with all the same margins.</returns>
    public SMargin(SMeasure? value = null) : this(value, value, value, value) { }

    /// <summary>
    /// Creates an SMargin with symetrical margins.
    /// </summary>
    /// <param name="leftAndRight">The value of the left and right margins.</param>
    /// <param name="topAndBottom">The value of the top and bottom margins.</param>
    /// <returns>An SMargin with symetrical margins.</returns>
    public SMargin(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) : this(leftAndRight, leftAndRight, topAndBottom, topAndBottom) { }
}