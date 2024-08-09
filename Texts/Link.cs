using pdf_scaffold.Styling;

namespace pdf_scaffold.Texts;

public class Link(
    string? text = null,
    string? value = null,
    Style? style = null,
    string? useStyle = null,
    bool breakLine = false
) {
    
    public string? Text { get; } = text;
    public string? Value { get; } = value;
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public bool BreakLine { get; } = breakLine;

}