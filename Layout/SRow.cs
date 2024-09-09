using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Layout;

public class SRow(
    ICollection<SSectionElement> elements,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    bool? singlePage = false
) : SSectionElement(style, useStyle, name) {

    public bool? SinglePage { get; } = singlePage;
    public ICollection<SSectionElement> Elements { get; } = elements;
    internal (int, int) ColSpan {get; set;} = (0, 0);
    internal (int, int) RowSpan {get; set;} = (0, 0);

    internal (int, int) Dimensions() {
        int columns = 0;
        int lines = 1;
        int c = 0;

        foreach (var e in Elements) {
            (int, int) d = (1, 1);

            if (e is SColumn column) {
                column.ColSpan = (column.ColSpan.Item1 + c, 0);
                column.RowSpan = (column.RowSpan.Item1, 0);
                d = column.Dimensions();
                column.ColSpan = (column.ColSpan.Item1, column.ColSpan.Item2 + d.Item1);
                column.RowSpan = (column.RowSpan.Item1, column.RowSpan.Item2 + d.Item2);
            } else if (e is SRow row) {
                row.ColSpan = (row.ColSpan.Item1 + c, 0);
                row.RowSpan = (row.RowSpan.Item1, 0);
                d = row.Dimensions();
                row.ColSpan = (row.ColSpan.Item1, row.ColSpan.Item2 + d.Item1);
                row.RowSpan = (row.RowSpan.Item1, row.RowSpan.Item2 + d.Item2);
            }

            columns += d.Item1;
            lines = Math.Max(lines, d.Item2);
            c += d.Item1;
        }

        return (columns, lines);
    }
}