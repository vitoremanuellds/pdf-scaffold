using PDFScaffold.Images;
using PDFScaffold.Layout;
using PDFScaffold.Scaffold;
using PDFScaffold.Tables;
using PDFScaffold.Texts;

namespace PDFScaffold.Visitors;

public interface IPdfScaffoldVisitor {

    void ForDocument(SDocument document); //
    void ForSection(SSection section); //
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