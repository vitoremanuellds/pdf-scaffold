using MigraDoc.DocumentObjectModel;
using PDFScaffold.Images;
using PDFScaffold.Layout;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Tables;
using PDFScaffold.Texts;

namespace PDFScaffold.Visitors.Default;

public class SVisitor : IPdfScaffoldVisitor
{
    public required Document Document { get; set; }
    internal Dictionary<string, SStyle> Styles { get; set; } = [];
    internal Stack<object> VisitedObjects { get; } = new Stack<object>();

    public void ForDocument(SDocument document)
    {
        this.DoForDocument(document);
    }

    internal SStyle? GetStyle(SStyle? style, string? name) {
        if (style == null && name != null) {
            var exists = Styles.TryGetValue(name, out style);
            if (!exists) {
                throw new Exception($"The style name '{name}' provided in the UseStyle field does not exist!");
            }
        }
        return style;
    }

    public void ForSection(SSection section) {
        this.DoForSection(section);
    }

    public void ForImage(SImage image)
    {
        this.DoForImage(image);
    }

    public void ForColumn(SColumn column)
    {
        this.DoForColumn(column);
    }

    public void ForContainer(SContainer container)
    {
        this.DoForContainer(container);
    }

    public void ForRow(SRow row)
    {
        this.DoForRow(row);
    }

    public void ForTable(STable table)
    {
        this.DoForTable(table);
    }

    public void ForTableCell(STableCell tableCell)
    {
        this.DoForTableCell(tableCell);
    }

    public void ForTableRow(STableRow tableRow)
    {
        this.DoForTableRow(tableRow);
    }

    // public void ForBookmark(SBookmark bookmark)
    // {
    //     throw new NotImplementedException();
    // }

    public void ForHeading(SHeading heading)
    {
        this.DoForHeading(heading);
    }

    public void ForLink(SLink link)
    {
        this.DoForLink(link);
    }

    public void ForParagraph(SParagraph paragraph)
    {
        this.DoForParagraph(paragraph);
    }

    public void ForText(SText text)
    {
        this.DoForText(text);
    }
}