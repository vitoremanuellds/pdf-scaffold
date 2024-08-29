using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Layout;

public class Column(
    SStyle? style = null,
    string? useStyle = null,
    bool? singlePage = false,
    ICollection<SSectionElement>? elements = null
) : SSectionElement(style, useStyle) {

    public bool? SinglePage { get; } = singlePage;
    public ICollection<SSectionElement>? Elements { get; } = elements;

}