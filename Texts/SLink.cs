using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

public class SLink(
    string link,
    string? text = null,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    bool breakLine = false
) : STextElement(style, useStyle, name) {
    
    public string? Text { get; } = text;
    public string Link { get; } = link;
    public bool BreakLine { get; } = breakLine;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForLink(this);
    }
}