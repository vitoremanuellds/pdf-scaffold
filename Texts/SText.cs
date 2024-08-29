using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;

namespace PDFScaffold.Texts;

public class SText (
    string? value = null,
    SStyle? style = null,
    string? useStyle = null,
    bool breakLine = false
) : STextElement(style, useStyle) {

    public string? Value { get; } = value;
    public bool BreakLine { get; } = breakLine;

}