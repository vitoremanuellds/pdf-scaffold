using pdf_scaffold.Styling;

namespace pdf_scaffold.Layout;

public class Container(
    Style? style = null,
    string? useStyle = null,
    bool? singlePage = false,
    ISectionElement? content = null
) : ISectionElement {
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public bool? SinglePage { get; } = singlePage;
    public ISectionElement? Content { get; } = content;
}