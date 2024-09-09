using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Texts;

public class SParagraph(
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    ICollection<STextElement>? content = null
) : SSectionElement(style, useStyle, name) {
    public ICollection<STextElement>? Content { get; } = content;
}