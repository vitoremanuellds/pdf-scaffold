using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

/// <summary>
/// Represents a table in the SDocument.
/// </summary>
/// <param name="rows">The rows of the STable.</param>
/// <param name="style">The style used in the STable</param>
/// <param name="useStyle">The name of the style referenced to be used in the STable</param>
/// <param name="name">The name of the STable. It is used to reference the STable using an SLink.</param>
/// <param name="columnSizes">The sizes of the columns inside the STable. If the the count of the sizes doesn't match the count of columns, an Exception is raised.</param>
public class STable(
    ICollection<STableRow> rows,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    ICollection<SMeasure>? columnSizes = null
) : SSectionElement(style, useStyle, name) {
    
    /// <summary>
    /// The size of the columns.
    /// </summary>
    public ICollection<SMeasure>? ColumnSizes { get; } = columnSizes;

    /// <summary>
    /// The rows of the table.
    /// </summary>
    public ICollection<STableRow> Rows { get; } = rows;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTable(this);
    }
}