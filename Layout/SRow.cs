using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

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

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForRow(this);
    }
}