using pdf_scaffold.Metrics;
using pdf_scaffold.Styling;

namespace pdf_scaffold.Tables;

public class Table(
    Style? style = null,
    string? useStyle = null,
    ICollection<Measure>? columnSizes = null,
    ICollection<TableRow>? rows = null
) : ISectionElement {
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public ICollection<Measure>? ColumnSizes { get; } = columnSizes;
    public ICollection<TableRow>? Rows { get; } = rows;
}