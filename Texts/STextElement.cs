using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

/// <summary>
/// Represents a text element inside an SParagraph.
/// </summary>
/// <param name="style">The style used in the STextElement.</param>
/// <param name="useStyle">The name of the style referenced to be used in the STextElement.</param>
/// <param name="name">The name of the STextElement. It is used to reference the STextElement using an SLink.</param>
public abstract class STextElement(SStyle? style, string? useStyle, string? name) : IPdfScaffoldElement
{
    /// <summary>
    /// The style used in the STextElement.
    /// </summary>
    public SStyle? Style { get; } = style;

    /// <summary>
    /// The name of the style referenced to be used in the STextElement.
    /// </summary>
    public string? UseStyle { get; } = useStyle;

    /// <summary>
    /// The name of the STextElement. It is used to reference the STextElement using an SLink.
    /// </summary>
    public string? Name { get; } = name;
    internal SStyle? FathersStyle { get; set; }

    public abstract void Accept(IPdfScaffoldVisitor visitor);
}