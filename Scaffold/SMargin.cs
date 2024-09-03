using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Scaffold;

/// <summary>
/// Defines an object that describes the margin of an element.
/// </summary>
public class SMargin {
    /// <summary>
    /// The left margin.
    /// </summary>
    public SMeasure? Left { get; }
    /// <summary>
    /// The right margin.
    /// </summary>
    public SMeasure? Right { get; }
    /// <summary>
    /// The top margin.
    /// </summary>
    public SMeasure? Top { get; }
    /// <summary>
    /// The bottom margin.
    /// </summary>
    public SMeasure? Bottom { get; }

    private SMargin(SMeasure? left = null, SMeasure? right = null, SMeasure? top = null, SMeasure? bottom = null) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    /// <summary>
    /// Creates an SMargin object with all the margins the same as <paramref name="value"></paramref>
    /// </summary>
    /// <param name="value">The value of the margins.</param>
    /// <returns>An SMargin with all the same margins.</returns>
    public static SMargin All(SMeasure? value = null) {
        return new SMargin(value, value, value, value);
    }

    /// <summary>
    /// Creates an SMargin with symetrical margins.
    /// </summary>
    /// <param name="leftAndRight">The value of the left and right margins.</param>
    /// <param name="topAndBottom">The value of the top and bottom margins.</param>
    /// <returns>An SMargin with symetrical margins.</returns>
    public static SMargin Symmetrical(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) {
        return new SMargin(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    /// <summary>
    /// Creates an SMargin with values for each side.
    /// </summary>
    /// <param name="left">The size of the left margin.</param>
    /// <param name="right">The size of the right margin.</param>
    /// <param name="top">The size of the top margin.</param>
    /// <param name="bottom">The size of the bottom margin.</param>
    /// <returns>An SMargin with values for each side.</returns>
    public static SMargin Each(SMeasure? left = null, SMeasure? right = null, SMeasure? top = null, SMeasure? bottom = null) {
        return new SMargin(left, right, top, bottom);
    }
}