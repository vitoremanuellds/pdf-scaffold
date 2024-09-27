using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Layout;

/// <summary>
/// Represents a Row of content inside the Document
/// </summary>
/// <param name="elements">The elements inside the SRow.</param>
/// <param name="style">The style of the SRow.</param>
/// <param name="useStyle">The name of the status to be used inside the SRow.</param>
/// <param name="name">The name of the SRow for it to be referenced using SLink.</param>
public class SRow(
    ICollection<SSectionElement> elements,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
) : SSectionElement(style, useStyle, name) {

    // public bool? SinglePage { get; } = singlePage;
    /// <summary>
    /// The elements inside the SRow.
    /// </summary>
    public ICollection<SSectionElement> Elements { get; } = elements;
    // internal (int, int) ColSpan {get; set;} = (0, 0);
    // internal (int, int) RowSpan {get; set;} = (0, 0);


    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForRow(this);
    }
}