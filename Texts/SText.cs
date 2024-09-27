using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

/// <summary>
/// Represents a text inside an SParagraph.
/// </summary>
/// <param name="value">The text value.</param>
/// <param name="style">The style used in the SText.</param>
/// <param name="useStyle">The name of the style referenced to be used in the SText.</param>
/// <param name="name">The name of the SText. It is used to reference the SText using an SLink.</param>
/// <param name="breakLine">If true, the line will be broken after the text.</param>
public class SText (
    string value,
    SStyle? style = null,
    string? name = null,
    string? useStyle = null,
    bool breakLine = false
) : STextElement(style, useStyle, name) {

    /// <summary>
    /// The value of the text.
    /// </summary>
    public string Value { get; } = value;

    /// <summary>
    /// Tells if the line should be broken after the text.
    /// </summary>
    public bool BreakLine { get; } = breakLine;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForText(this);
    }
}