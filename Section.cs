using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;
using pdf_scaffold.Visitors;

namespace pdf_scaffold;

public class Section(
    Styling.Style? style = null,
    string? useStyle = null,
    PageFormat? pageFormat = null,
    Margin? margin = null,
    ICollection<ISectionElement>? elements = null
) : IPdfScaffoldElement {
    public Styling.Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public PageFormat? PageFormat { get; } = pageFormat;
    public Margin? Margin { get; } = margin;
    public ICollection<ISectionElement>? Elements { get; } = elements;

    void IPdfScaffoldElement.Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForSection(this);
    }

    void IPdfScaffoldElement.MergeStyles(Styling.Style? style, Dimensions dimensions)
    {
        throw new NotImplementedException();
    }
}