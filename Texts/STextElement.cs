using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

public abstract class STextElement(SStyle? style, string? useStyle, string? name) : IPdfScaffoldElement
{
    public SStyle? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public string? Name { get; } = name;
    internal SStyle? FathersStyle { get; set; }

    public abstract void Accept(IPdfScaffoldVisitor visitor);
}