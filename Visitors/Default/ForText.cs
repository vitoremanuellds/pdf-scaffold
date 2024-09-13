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
        }

        if (style.Borders != null) {
            SetBorders(paragraph, p, style);
        }

        // if (style.Padding != null) {
        //     p.Format.
        // }

        visitor.VisitedObjects.Push(p);

        foreach (STextElement item in paragraph.Content ?? [])
        {
            item.FatherStyle = style;
            item.Accept(visitor);
        }

        visitor.VisitedObjects.Pop();
    }

    private static void DoForText(this SVisitor visitor, SText text) {
        
    }


    private static void SetBorders(SParagraph paragraph, Paragraph p, SStyle style) {
        if (style.Borders!.Left != null) {
            SetBorder(paragraph, style.Borders.Left, p.Format.Borders.Left, true);
        }

        if (style.Borders!.Right != null) {
            SetBorder(paragraph, style.Borders.Right, p.Format.Borders.Right, true);
        }

        if (style.Borders!.Bottom != null) {
            SetBorder(paragraph, style.Borders.Bottom, p.Format.Borders.Bottom, false);
        }

        if (style.Borders!.Top != null) {
            SetBorder(paragraph, style.Borders.Top, p.Format.Borders.Top, false);
        }
    }

    private static void SetBorder(
        SParagraph paragraph, 
        SBorder border, 
        Border b,
        bool horizontal
    ) {
        b.Color = border.Color ?? Colors.Black;
        switch (border.BorderType) {
            case SBorderType.None:
                b.Style = BorderStyle.None;
                break;
            case SBorderType.Single:
                b.Style = BorderStyle.Single;
                break;
            case SBorderType.Dot:
                b.Style = BorderStyle.Dot;
                break;
            case SBorderType.DashDot:
                b.Style = BorderStyle.DashDot;
                break;
            case SBorderType.DashDotDot:
                b.Style = BorderStyle.DashDotDot;
                break;
            case SBorderType.DashLargeGap:
                b.Style = BorderStyle.DashLargeGap;
                break;
            case SBorderType.DashSmallGap:
                b.Style = BorderStyle.DashSmallGap;
                break;
        }
        b.Visible = border.Visible ?? false;
        SDimensions d = paragraph.FathersStyle!.Dimensions!;
        b.Width = SMetricsUtil.GetUnitValue(border.Width, horizontal ? d.X : d.Y);
    }
}