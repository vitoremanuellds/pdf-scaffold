using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

public class STableCell(
    SSectionElement content,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    // string? name = null,
    int columnSpan = 1,
    int rowSpan = 1
) : STableElement(style, useStyle, name) {

    internal Guid Id { get; } = Guid.NewGuid();
    public int ColumnSpan { get; } = columnSpan;
    public int RowSpan { get; } = rowSpan;
    public SSectionElement Content { get; } = content;
    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTableCell(this);
    }
}