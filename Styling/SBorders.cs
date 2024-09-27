namespace PDFScaffold.Styling;

/// <summary>
/// Represents the borders of a component.
/// </summary>
/// <remarks>
/// Creates an SBorders with the specified sides.
/// </remarks>
/// <param name="left">The left border.</param>
/// <param name="right">The right border.</param>
/// <param name="top">The top border.</param>
/// <param name="bottom">The bottom border.</param>
public class SBorders(SBorder? left = null, SBorder? right = null, SBorder? top = null, SBorder? bottom = null)
{

    /// <summary>
    /// The left border of the component.
    /// </summary>
    public SBorder? Left { get; } = left;

    /// <summary>
    /// The right border of the component.
    /// </summary>
    public SBorder? Right { get; } = right;

    /// <summary>
    /// The top border of the component.
    /// </summary>
    public SBorder? Top { get; } = top;

    /// <summary>
    /// The bottom border of the component.
    /// </summary>
    public SBorder? Bottom { get; } = bottom;

    /// <summary>
    /// Creates an SBorders with all borders identical.
    /// </summary>
    /// <param name="border">The border used to set all sides.</param>
 
    public SBorders(SBorder? border = null) : this(border, border, border, border) { }

    /// <summary>
    /// Creates an SBorders with the symetrical sides identical.
    /// </summary>
    /// <param name="leftAndRight">The border that represents the left and right borders.</param>
    /// <param name="topAndBottom">The border that represents the top and bottom borders.</param>
    public SBorders(SBorder? leftAndRight = null, SBorder? topAndBottom = null) : this(leftAndRight, leftAndRight, topAndBottom, topAndBottom) { }
}