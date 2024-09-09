using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

public abstract class STextElement(SStyle? style, string? useStyle) : IPdfScaffoldElement
{
    public SStyle? Style = style;
    public string? UseStyle = useStyle;
    internal SStyle? FatherStyle { get; set; }

    public abstract void Accept(IPdfScaffoldVisitor visitor);
}