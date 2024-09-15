using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

public class STableRow(
    ICollection<STableCell> cells,
    SStyle? style = null,
    string? useStyle = null
) : STableElement(style, useStyle) {
    public ICollection<STableCell> Cells { get; } = cells;
    internal int RowIndex { get; set; } = -1;
    internal int MaxRowIndex { get; set; } = -1;
    internal int MaxColumnIndex { get; set; } = -1;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTableRow(this);
    }
}