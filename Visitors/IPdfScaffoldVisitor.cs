using pdf_scaffold.Images;
using pdf_scaffold.Layout;
using pdf_scaffold.Tables;
using pdf_scaffold.Texts;

namespace pdf_scaffold.Visitors;

public interface IPdfScaffoldVisitor {

    void ForDocument(Document document); //
    void ForSection(Section section); //
    void ForImage(Image image);
    void ForColumn(Column column);
    void ForContainer(Container container);
    void ForRow(Row row);
    void ForTable(Table table);
    void ForTableCell(TableCell tableCell);
    void ForTableRow(TableRow tableRow);
    void ForBookmark(Bookmark bookmark);
    void ForHeading(Heading heading);
    void ForLink(Link link);
    void ForParagraph(Paragraph paragraph);
    void ForText(Text text);
}