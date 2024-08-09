using pdf_scaffold.Styling;

namespace pdf_scaffold.Tables;

public class TableCell(
    Style? style = null,
    string? useStyle = null,
    int? columnSpan = null,
    int? rowSpan = null,
    ISectionElement? content = null
) {
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public int? ColumnSpan { get; } = columnSpan;
    public int? RowSpan { get; } = rowSpan;
    public ISectionElement? Content { get; } = content;
}