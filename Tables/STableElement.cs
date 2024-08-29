using PDFScaffold.Styling;

namespace PDFScaffold.Tables;

public abstract class STableElement(SStyle? style, string? useStyle) {

    private SStyle? FathersStyle { get; set; }
    public SStyle? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;

}