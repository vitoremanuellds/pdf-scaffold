using MigraDoc.DocumentObjectModel;

namespace pdf_scaffold.Texts;

public class Text(
    string? value = null,
    Style? style = null,
    string? useStyle = null,
    bool breakLine = false
) : TextElement {

    public string? Value { get; } = value;
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public bool BreakLine { get; } = breakLine;

}