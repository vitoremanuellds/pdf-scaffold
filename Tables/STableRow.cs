using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public class STableRow(
    ICollection<STableCell> cells,
    SStyle? style = null,
    string? useStyle = null
) : STableElement(style, useStyle) {
    public ICollection<STableCell> Cells { get; } = cells;
}