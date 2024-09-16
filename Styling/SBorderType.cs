namespace PDFScaffold.Styling;

public enum SBorderType {
    /// <summary>
    /// No borders.
    /// </summary>
    None,
    /// <summary>
    /// Single solid Line.
    /// </summary>
    Single,
    /// <summary>
    /// A dotted line.
    /// </summary>
    Dot,
    /// <summary>
    /// A dash line with small gaps.
    /// </summary>
    DashSmallGap,
    /// <summary>
    /// A dash line with large gaps.
    /// </summary>
    DashLargeGap,
    /// <summary>
    /// A line with dashes and dots. Works on SImage.
    /// </summary>
    DashDot,
    /// <summary>
    /// A line with dashs and two dots intercalating. Works on SImage.
    /// </summary>
    DashDotDot,
    /// <summary>
    /// A dash line. Works on SImage.
    /// </summary>
    Dash,
    /// <summary>
    /// A solid line. Works on SImage. Works on SImage.
    /// </summary>
    Solid,
    /// <summary>
    /// A line with square dots. Works on SImage.
    /// </summary>
    SquareDot
}