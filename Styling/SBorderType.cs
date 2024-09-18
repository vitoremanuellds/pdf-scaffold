namespace PDFScaffold.Styling;

public enum SBorderType {
    /// <summary>
    /// No borders. Works on SColumn, SRow, STable, STableRow and STableCell.
    /// </summary>
    None,
    /// <summary>
    /// Single solid Line. Works on SColumn, SRow, STable, STableRow and STableCell.
    /// </summary>
    Single,
    /// <summary>
    /// A dotted line. Works on SColumn, SRow, STable, STableRow and STableCell.
    /// </summary>
    Dot,
    /// <summary>
    /// A dash line with small gaps. Works on SColumn, SRow, STable, STableRow and STableCell.
    /// </summary>
    DashSmallGap,
    /// <summary>
    /// A dash line with large gaps. Works on SColumn, SRow, STable, STableRow and STableCell.
    /// </summary>
    DashLargeGap,
    /// <summary>
    /// A line with dashes and dots. Works on SColumn, SRow, STable, STableRow, STableCell, SImage and SContainer.
    /// </summary>
    DashDot,
    /// <summary>
    /// A line with dashs and two dots intercalating. Works on SColumn, SRow, STable, STableRow, STableCell, SImage and SContainer.
    /// </summary>
    DashDotDot,
    /// <summary>
    /// A dash line. Works on SImage and SContainer.
    /// </summary>
    Dash,
    /// <summary>
    /// A solid line. Works on SImage and SContainer.
    /// </summary>
    Solid,
    /// <summary>
    /// A line with square dots. Works on SImage and SContainer.
    /// </summary>
    SquareDot
}