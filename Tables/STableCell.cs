using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

/// <summary>
/// Represents a cell inside an STableRow and STable.
/// </summary>
/// <param name="content">The content of the STableCell.</param>
/// <param name="style">The style to be used inside the STableCell.</param>
/// <param name="useStyle">The name reference to be used inside the STableCell.</param>
/// <param name="name">The name of the STableCell. It is used to reference the STableCell using an SLink.</param>
/// <param name="columnSpan">The span of columns the STableCell will occupy. If the span is longer than the count of columns, new columns will be generated.</param>
/// <param name="rowSpan">The span of rows the STableCell will occupy. If the span is longer than the count of rows, then an exception is raised.</param>
public class STableCell(
    SSectionElement content,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    // string? name = null,
    int columnSpan = 1,
    int rowSpan = 1
) : STableElement(style, useStyle, name) {

    /// <summary>
    /// The span of columns the STableCell will occupy. If the span is longer than the count of columns, new columns will be generated.
    /// </summary>
    public int ColumnSpan { get; } = columnSpan;

    /// <summary>
    /// The span of columns the STableCell will occupy. If the span is longer than the count of columns, new columns will be generated.
    /// </summary>
    public int RowSpan { get; } = rowSpan;

    /// <summary>
    /// The content of the STableCell.
    /// </summary>
    public SSectionElement Content { get; } = content;
    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTableCell(this);
    }
}