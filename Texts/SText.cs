using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

public class SText (
    string value,
    SStyle? style = null,
    string? name = null,
    string? useStyle = null,
    bool breakLine = false
) : STextElement(style, useStyle, name) {

    public string Value { get; } = value;
    public bool BreakLine { get; } = breakLine;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForText(this);
    }
}