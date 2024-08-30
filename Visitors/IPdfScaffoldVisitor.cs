using PDFScaffold.Images;
using PDFScaffold.Layout;
using PDFScaffold.Scaffold;
using PDFScaffold.Tables;
using PDFScaffold.Texts;

namespace PDFScaffold.Visitors;

public interface IPdfScaffoldVisitor {

    void ForDocument(SDocument document); //
    void ForSection(SSection section); //
    void ForImage(SImage image);
    void ForColumn(SColumn column);
    void ForContainer(SContainer container);
    void ForRow(SRow row);
    void ForTable(STable table);
    void ForTableCell(STableCell tableCell);
    void ForTableRow(STableRow tableRow);
    void ForBookmark(SBookmark bookmark);
    void ForHeading(SHeading heading);
    void ForLink(SLink link);
    void ForParagraph(SParagraph paragraph);
    void ForText(SText text);
}