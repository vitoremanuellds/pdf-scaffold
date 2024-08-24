using pdf_scaffold.Images;
using pdf_scaffold.Layout;
using pdf_scaffold.Tables;
using pdf_scaffold.Texts;

namespace pdf_scaffold.Visitors.Default;

public class Visitor : IPdfScaffoldVisitor
{
    public required MigraDoc.DocumentObjectModel.Document Document { get; set; }
    internal Dictionary<string, Styling.Style> Styles { get; set; } = [];

    public void ForDocument(Document document)
    {
        this.DoForDocument(document);
    }

    internal Styling.Style? GetStyle(Styling.Style? style, string? name) {
        if (style == null && name != null) {
            var exists = Styles.TryGetValue(name, out style);
            if (!exists) {
                throw new Exception($"The style name '{name}' provided in the UseStyle field does not exist!");
            }
        }
        return style;
    }

    public void ForSection(Section section) {
        this.DoForSection(section);
    }

    public void ForImage(Image image)
    {
        this.DoForImage(image);
    }

    public void ForColumn(Column column)
    {
        throw new NotImplementedException();
    }

    public void ForContainer(Container container)
    {
        throw new NotImplementedException();
    }

    public void ForRow(Row row)
    {
        throw new NotImplementedException();
    }

    public void ForTable(Table table)
    {
        throw new NotImplementedException();
    }

    public void ForTableCell(TableCell tableCell)
    {
        throw new NotImplementedException();
    }

    public void ForTableRow(TableRow tableRow)
    {
        throw new NotImplementedException();
    }

    public void ForBookmark(Bookmark bookmark)
    {
        throw new NotImplementedException();
    }

    public void ForHeading(Heading heading)
    {
        throw new NotImplementedException();
    }

    public void ForLink(Link link)
    {
        throw new NotImplementedException();
    }

    public void ForParagraph(Texts.Paragraph paragraph)
    {
        throw new NotImplementedException();
    }

    public void ForText(Texts.Text text)
    {
        throw new NotImplementedException();
    }
}