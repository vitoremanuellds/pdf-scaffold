using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

public abstract class STableElement(SStyle? style, string? useStyle, string? name) {

    internal SStyle? FathersStyle { get; set; }
    public SStyle? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public string? Name { get; } = name;
    public abstract void Accept(IPdfScaffoldVisitor visitor);

}