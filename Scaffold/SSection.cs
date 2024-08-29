using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Scaffold;

public class SSection(
    SStyle? style = null,
    string? useStyle = null,
    SPageFormat? pageFormat = null,
    SMargin? margin = null,
    ICollection<SSectionElement>? elements = null
) : IPdfScaffoldElement {
    public SStyle? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public SPageFormat? PageFormat { get; } = pageFormat;
    public SMargin? Margin { get; } = margin;
    public ICollection<SSectionElement>? Elements { get; } = elements;

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForSection(this);
    }
}