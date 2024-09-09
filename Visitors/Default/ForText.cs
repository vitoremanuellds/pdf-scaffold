using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Styling;
using PDFScaffold.Texts;

namespace PDFScaffold.Visitors.Default;

public static class ForText {

    public static void DoForParagraph(this SVisitor visitor, SParagraph paragraph) {
        SStyle style = visitor.GetStyle(paragraph.Style, paragraph.UseStyle) ?? new SStyle();
        style.Merge(paragraph.FathersStyle);

        var father = visitor.VisitedObjects.Peek();

        Paragraph p;

        if (father is Section section) {
            p = section.AddParagraph();
        } else if (father is Table table) {
            p = table.Rows[paragraph.TablePos.Item2].Cells[paragraph.TablePos.Item1].AddParagraph();
        } else if (father is TextFrame frame) {
            p = frame.AddParagraph();
        } else {
            throw new Exception("A paragraph can not be placed outside a SSection, SColumn, SRow or SContainer");
        }

        if (paragraph.Name != null) {
            p.AddBookmark(paragraph.Name);
        }

        

        visitor.VisitedObjects.Push(p);

        foreach (STextElement item in paragraph.Content ?? [])
        {
            item.FatherStyle = style;
            item.Accept(visitor);
        }

        visitor.VisitedObjects.Pop();
    }
}