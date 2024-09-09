using PDFScaffold.Styling;

namespace PDFScaffold.Texts;

public class SHeading(
    int? level = null,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    ICollection<STextElement>? content = null
    ) : SParagraph(style, useStyle, content) {

    public int? Level { get; } = level;
}