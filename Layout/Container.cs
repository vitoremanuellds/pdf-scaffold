using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Layout;

public class Container(
    SStyle? style = null,
    string? useStyle = null,
    bool? singlePage = false,
    SSectionElement? content = null
) : SSectionElement(style, useStyle) {

    public bool? SinglePage { get; } = singlePage;
    public SSectionElement? Content { get; } = content;
}