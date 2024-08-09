using pdf_scaffold.Styling;

namespace pdf_scaffold.Texts;

public class Heading(
    int? level = null,
    Style? style = null,
    string? useStyle = null,
    ICollection<TextElement>? content = null
    ) : Paragraph(style, useStyle, content) {

    public int? Level { get; } = level;
}