using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

/// <summary>
/// Represents a paragraph inside the SDocument.
/// </summary>
/// <param name="content">The text elements of the SParagraph.</param>
/// <param name="style">The style used in the SParagraph.</param>
/// <param name="useStyle">The name of the style referenced to be used in the SParagraph.</param>
/// <param name="name">The name of the SParagraph. It is used to reference the SParagraph using an SLink.</param>
public class SParagraph(
    ICollection<STextElement> content,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
) : SSectionElement(style, useStyle, name) {

    /// <summary>
    /// The text elements of the SParagraph.
    /// </summary>
    public ICollection<STextElement> Content { get; } = content;

    /// <summary>
    /// Represents a paragraph inside the SDocument.
    /// </summary>
    /// <param name="content">The text of the SParagraph.</param>
    /// <param name="style">The style used in the SParagraph.</param>
    /// <param name="useStyle">The name of the style referenced to be used in the SParagraph.</param>
    /// <param name="name">The name of the SParagraph. It is used to reference the SParagraph using an SLink.</param>
    public SParagraph(
        string content,
        SStyle? style = null,
        string? useStyle = null,
        string? name = null
    ) : this([new SText(content)], style, useStyle, name) { }

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForParagraph(this);
    }
}