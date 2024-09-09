using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

public class SLink(
    string? text = null,
    string? value = null,
    SStyle? style = null,
    string? useStyle = null,
    bool breakLine = false
) : STextElement(style, useStyle) {
    
    public string? Text { get; } = text;
    public string? Value { get; } = value;
    public bool BreakLine { get; } = breakLine;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForLink(this);
    }
}