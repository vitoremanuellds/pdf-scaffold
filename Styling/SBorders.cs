namespace PDFScaffold.Styling;

/// <summary>
/// Represents the borders of a component.
/// </summary>
public class SBorders {

    /// <summary>
    /// The left border of the component.
    /// </summary>
    public SBorder? Left { get; }

    /// <summary>
    /// The right border of the component.
    /// </summary>
    public SBorder? Right { get; }

    /// <summary>
    /// The top border of the component.
    /// </summary>
    public SBorder? Top { get; }

    /// <summary>
    /// The bottom border of the component.
    /// </summary>
    public SBorder? Bottom { get; }

    private SBorders(SBorder? left = null, SBorder? right = null, SBorder? top = null, SBorder? bottom = null) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    /// <summary>
    /// Creates an SBorders with all borders identical.
    /// </summary>
    /// <param name="border">The border used to set all sides.</param>
    /// <returns>An SBorders with all borders identical.</returns>
    public static SBorders All(SBorder? border = null) {
        return new SBorders(border, border, border, border);
    }

    /// <summary>
    /// Creates an SBorders with the symetrical sides identical.
    /// </summary>
    /// <param name="leftAndRight">The border that represents the left and right borders.</param>
    /// <param name="topAndBottom">The border that represents the top and bottom borders.</param>
    /// <returns></returns>
    public static SBorders Symmatrical(SBorder? leftAndRight = null, SBorder? topAndBottom = null) {
        return new SBorders(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    /// <summary>
    /// Creates an SBorders with the specified sides.
    /// </summary>
    /// <param name="left">The left border.</param>
    /// <param name="right">The right border.</param>
    /// <param name="top">The top border.</param>
    /// <param name="bottom">The bottom border.</param>
    /// <returns></returns>
    public static SBorders Each(SBorder? left = null, SBorder? right = null, SBorder? top = null, SBorder? bottom = null) {
        return new SBorders(left, right, top, bottom);
    }
}