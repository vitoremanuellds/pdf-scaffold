using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public class STable(
    SStyle? style = null,
    string? useStyle = null,
    ICollection<SMeasure>? columnSizes = null,
    ICollection<STableRow>? rows = null
) : SSectionElement(style, useStyle) {
    
    public ICollection<SMeasure>? ColumnSizes { get; } = columnSizes;
    public ICollection<STableRow>? Rows { get; } = rows;
}