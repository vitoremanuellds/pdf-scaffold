using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Layout;

public class SContainer(
    SStyle? style = null,
    string? useStyle = null,
    bool? singlePage = false,
    SSectionElement? content = null
) : SSectionElement(style, useStyle) {

    public bool? SinglePage { get; } = singlePage;
    public SSectionElement? Content { get; } = content;

    public new void Accept(IPdfScaffoldVisitor visitor) {
        visitor.ForContainer(this);
    }
}