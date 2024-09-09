using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public class STable(
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    ICollection<SMeasure>? columnSizes = null,
    ICollection<STableRow>? rows = null
) : SSectionElement(style, useStyle, name) {
    
    public ICollection<SMeasure>? ColumnSizes { get; } = columnSizes;
    public ICollection<STableRow>? Rows { get; } = rows;
}