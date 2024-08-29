using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public class STableRow(
    SStyle? style = null,
    string? useStyle = null,
    ICollection<STableCell>? cells = null
) : STableElement(style, useStyle) {
    public ICollection<STableCell>? Cells { get; } = cells;
}