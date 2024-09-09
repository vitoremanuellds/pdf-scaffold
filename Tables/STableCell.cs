using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public class STableCell(
    SStyle? style = null,
    string? useStyle = null,
    // string? name = null,
    int? columnSpan = null,
    int? rowSpan = null,
    SSectionElement? content = null
) : STableElement(style, useStyle) {
    public int? ColumnSpan { get; } = columnSpan;
    public int? RowSpan { get; } = rowSpan;
    public SSectionElement? Content { get; } = content;
}