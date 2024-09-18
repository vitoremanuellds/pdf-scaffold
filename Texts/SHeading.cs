using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Texts;

/// <summary>
/// Represents a heading inside the SDocument.
/// </summary>
/// <param name="content">The text elements inside the SHeading.</param>
/// <param name="level">The level of the SHeading. It goes from 1 to 6.</param>
/// <param name="style">The style used in the SHeading.</param>
/// <param name="useStyle">The name of the style referenced to be used in the SHeading.</param>
/// <param name="name">The name of the SHeading. It is used to reference the SHeading using an SLink.</param>
public class SHeading(
    ICollection<STextElement> content,
    int level = 1,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
) : SParagraph(content, style, useStyle, name) {

    /// <summary>
    /// The level of the SHeading.
    /// </summary>
    public int Level { get; } = level;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForHeading(this);
    }

    internal static Unit GetFontSize(int level) {
        return level switch
        {
            1 => Unit.FromPoint(25.5),
            2 => Unit.FromPoint(22.5),
            3 => Unit.FromPoint(18),
            4 => Unit.FromPoint(15),
            5 => Unit.FromPoint(13.5),
            6 => Unit.FromPoint(12),
            _ => Unit.FromPoint(25.5),
        };
    }
}