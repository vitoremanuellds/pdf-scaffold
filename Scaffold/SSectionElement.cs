using PDFScaffold.Styling;

namespace PDFScaffold.Scaffold;

public abstract class SSectionElement(SStyle? style, string? useStyle) {

    internal SStyle? FathersStyle { get; set; }
    public SStyle? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;

}