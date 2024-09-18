using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

/// <summary>
/// Represents a link inside the SDocument.
/// </summary>
/// <param name="link">The url of the link. To reference a component inside the SDocument, the link should start with the character '#'.</param>
/// <param name="text">The text of the link. If not set, the text of the link will be the URL passed.</param>
/// <param name="style">The style used in the SLink.</param>
/// <param name="useStyle">The name of the style referenced to be used in the SLink.</param>
/// <param name="name">The name of the SLink. It is used to reference the SLink using another SLink.</param>
/// <param name="breakLine">If true, the link will break the line after it.</param>
public class SLink(
    string link,
    string? text = null,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    bool breakLine = false
) : STextElement(style, useStyle, name) {

    /// <summary>
    /// The text of the link. If not set, the text of the link will be the URL passed.
    /// </summary>
    public string? Text { get; } = text;

    /// <summary>
    /// The url of the link. To reference a component inside the SDocument, the link should start with the character '#'.
    /// </summary>
    public string Link { get; } = link;

    /// <summary>
    /// Tells if the line should be broken after the link.
    /// </summary>
    public bool BreakLine { get; } = breakLine;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForLink(this);
    }
}