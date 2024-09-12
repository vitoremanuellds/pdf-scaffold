using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Metrics;
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



        // p.Format;
        p.Format.Font.Color = style.FontColor ?? Colors.Black;
        p.Format.Font.Bold = style.Bold ?? false;
        p.Format.Font.Italic = style.Italic ?? false;
        p.Format.Font.Underline = SUnderlineUtils.GetUnderline(style.Underline ?? SUnderline.None);
        p.Format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, paragraph.FathersStyle!.Dimensions!.Y);
        p.Format.Font.Subscript = (style.Subscript ?? false) && !(style.Superscript ?? false);
        p.Format.Font.Superscript = (style.Superscript ?? false) && !(style.Subscript ?? false);
        p.Format.Shading.Color = style.Shading ?? Color.Empty;
        switch (style.HorizontalAlignment)
        {
            case SAlignment.Start:
                p.Format.Alignment = ParagraphAlignment.Left;
                break;
            case SAlignment.Center:
                p.Format.Alignment = ParagraphAlignment.Center;
                break;
            case SAlignment.End:
                p.Format.Alignment = ParagraphAlignment.Right;
                break;
            case SAlignment.Justified:
                p.Format.Alignment = ParagraphAlignment.Justify;
                break;
            case null:
                p.Format.Alignment = ParagraphAlignment.Left;

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