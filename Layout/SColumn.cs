using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;
using PDFScaffold.Visitors.Default;

namespace PDFScaffold.Layout;

/// <summary>
/// Represents a Column inside the SDocument.
/// </summary>
/// <param name="elements">The Elements inside the column.</param>
/// <param name="style">The style to use inside the column.</param>
/// <param name="useStyle">The name used to reference an style inside the SDocument.</param>
/// <param name="name">The name used to reference the component using an SLink.</param>
public class SColumn(
    ICollection<SSectionElement> elements,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
    // bool? singlePage = false
) : SSectionElement(style, useStyle, name) {

    // public bool? SinglePage { get; } = singlePage;

    /// <summary>
    /// The Elements inside the column.
    /// </summary>
    public ICollection<SSectionElement> Elements { get; } = elements;
    // internal (int, int) ColSpan {get; set;} = (0, 0);
    // internal (int, int) RowSpan {get; set;} = (0, 0);

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForColumn(this);
    }
}