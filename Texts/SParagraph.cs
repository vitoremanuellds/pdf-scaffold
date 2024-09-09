using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

public class SParagraph(
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    ICollection<STextElement>? content = null
) : SSectionElement(style, useStyle, name) {
    public ICollection<STextElement>? Content { get; } = content;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForParagraph(this);
    }
}