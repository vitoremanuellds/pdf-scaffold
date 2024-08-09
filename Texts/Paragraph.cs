using pdf_scaffold.Styling;

namespace pdf_scaffold.Texts;

public class Paragraph(
    Style? style = null,
    string? useStyle = null,
    ICollection<TextElement>? content = null
) : ISectionElement {
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public ICollection<TextElement>? Content { get; } = content;
}