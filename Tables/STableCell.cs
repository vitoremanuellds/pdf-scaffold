using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public class STableCell(
    SStyle? style = null,
    string? useStyle = null,
    // string? name = null,
    int columnSpan = 1,
    int rowSpan = 1,
    SSectionElement? content = null
) : STableElement(style, useStyle) {

    internal Guid Id { get; } = Guid.NewGuid();
    public int ColumnSpan { get; } = columnSpan;
    public int RowSpan { get; } = rowSpan;
    public SSectionElement? Content { get; } = content;
}