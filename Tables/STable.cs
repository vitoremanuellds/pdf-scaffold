using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

public class STable(
    ICollection<STableRow> rows,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    ICollection<SMeasure>? columnSizes = null
) : SSectionElement(style, useStyle, name) {
    
    public ICollection<SMeasure>? ColumnSizes { get; } = columnSizes;
    public ICollection<STableRow> Rows { get; } = rows;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTable(this);
    }
}