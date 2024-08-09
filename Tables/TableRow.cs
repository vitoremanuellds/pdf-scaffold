using pdf_scaffold.Styling;

namespace pdf_scaffold.Tables;

public class TableRow(
    Style? style = null,
    string? useStyle = null,
    ICollection<TableCell>? cells = null
) {
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public ICollection<TableCell>? Cells { get; } = cells;
}