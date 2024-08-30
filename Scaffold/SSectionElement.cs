using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Scaffold;

/// <summary>
/// An element that can be used inside a SSection.
/// </summary>
/// <param name="style">The style of the SSection Element.</param>
/// <param name="useStyle">A reference to a style inside the SDocument's style list.</param>
public abstract class SSectionElement(SStyle? style, string? useStyle) : IPdfScaffoldElement {

    internal SStyle? FathersStyle { get; set; }
    /// <summary>
    /// The style of the SSection Element.
    /// </summary>
    public SStyle? Style { get; } = style;
    /// <summary>
    /// A reference to a style inside the SDocument's style list.
    /// </summary>
    public string? UseStyle { get; } = useStyle;

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        throw new Exception("This method should not be called by the super class!");
    }
}