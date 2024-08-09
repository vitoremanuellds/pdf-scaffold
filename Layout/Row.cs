using pdf_scaffold.Styling;

namespace pdf_scaffold.Layout;

public class Row(
    Style? style = null,
    string? useStyle = null,
    bool? singlePage = false,
    ICollection<ISectionElement>? elements = null
) : ISectionElement {
    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public bool? SinglePage { get; } = singlePage;
    public ICollection<ISectionElement>? Elements { get; } = elements;
}