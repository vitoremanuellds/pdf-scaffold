using PDFScaffold.Styling;

namespace PDFScaffold.Texts;

public abstract class STextElement(SStyle? style, string? useStyle)
{
    public SStyle? style = style;
    public string? useStyle = useStyle;
}