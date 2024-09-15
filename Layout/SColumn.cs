using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;
using PDFScaffold.Visitors.Default;

namespace PDFScaffold.Layout;

public class SColumn(
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

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForColumn(this);
    }
}