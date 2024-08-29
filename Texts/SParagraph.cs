using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Texts;

public class SParagraph(
    SStyle? style = null,
    string? useStyle = null,
    ICollection<STextElement>? content = null
) : SSectionElement(style, useStyle) {
    public ICollection<STextElement>? Content { get; } = content;
}