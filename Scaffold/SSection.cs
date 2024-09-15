using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Scaffold;

/// <summary>
/// Represents a Section of the Document.
/// </summary>
/// <param name="style">An SStyle wich defines the style of all the children inside
/// the section.</param>
/// <param name="useStyle">A reference to the name of the style inside the SDocument Style list.</param>
/// <param name="pageFormat">The format of the section page. It defines the dimensions of the page.
/// If it is null, then the default <c>PageFormat</c> will be A4.</param>
/// <param name="margin">Defines the margins of the Section. If null, the default margin value will be
/// 1 inch.</param>
/// <param name="elements">The list of elements inside of this section. It can be null.
/// </param>
public class SSection(
    ICollection<SSectionElement> elements,
    SStyle? style = null,
    string? useStyle = null,
    SPageFormat? pageFormat = null,
    SMargin? margin = null
) : IPdfScaffoldElement {
    public SStyle? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public SPageFormat? PageFormat { get; } = pageFormat;
    public SMargin? Margin { get; } = margin;
    public ICollection<SSectionElement>? Elements { get; } = elements;

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForSection(this);
    }
}