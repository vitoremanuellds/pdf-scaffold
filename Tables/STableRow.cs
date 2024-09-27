using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Tables;

/// <summary>
/// Represents a row inside an STable.
/// </summary>
/// <param name="cells">The cells of the STableRow.</param>
/// <param name="style">The style used in the STableRow</param>
/// <param name="useStyle">The name of the style referenced to be used in the STableRow</param>
/// <param name="name">The name of the STableRow. It is used to reference the STableRow using an SLink.</param>
public class STableRow(
    ICollection<STableCell> cells,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
) : STableElement(style, useStyle, name) {

    /// <summary>
    /// The cells of the STableRow.
    /// </summary>
    public ICollection<STableCell> Cells { get; } = cells;
    internal int RowIndex { get; set; } = -1;
    internal IDictionary<(int, int), bool>? Positions { get; set; }

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForTableRow(this);
    }
}