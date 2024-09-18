using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

public class STableRow(
    ICollection<STableCell> cells,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
) : STableElement(style, useStyle, name) {
    public ICollection<STableCell> Cells { get; } = cells;
    internal int RowIndex { get; set; } = -1;
    internal IDictionary<(int, int), bool>? Positions { get; set; }

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTableRow(this);
    }
}