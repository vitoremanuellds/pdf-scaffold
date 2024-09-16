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
        } else if (father is Cell cell) {
            p = cell.AddParagraph();
        } else if (father is TextFrame frame) {
            p = frame.AddParagraph();
        } else {
            throw new Exception("A paragraph can not be placed outside a SSection, SColumn, SRow or SContainer");
        }

        if (paragraph.Name != null) {
            p.AddBookmark('#' + paragraph.Name, false);
        }

        if (style.Borders != null) {
            var (x, y) = SetBorders(paragraph, p, style);
            style.Dimensions = new SDimensions(
                paragraph.FathersStyle!.Dimensions!.Y - y,
                paragraph.FathersStyle!.Dimensions!.X - x
            );
        }

        // p.Format;
        SetFormat(p.Format, style, paragraph.FathersStyle!.Dimensions!);

        visitor.VisitedObjects.Push(p);

        foreach (STextElement item in paragraph.Content ?? [])
        {
            item.FathersStyle = style;
            item.Accept(visitor);
        }

        visitor.VisitedObjects.Pop();
    }

    public static void DoForText(this SVisitor visitor, SText text) {
        SStyle style = visitor.GetStyle(text.Style, text.UseStyle) ?? new SStyle();
        style.Merge(text.FathersStyle);

        var father = visitor.VisitedObjects.Peek();

        FormattedText t;

        if (father is Paragraph paragraph) {
            t = paragraph.AddFormattedText(text.Value ?? "");
            if (text.BreakLine) {
                paragraph.AddText("\n");
            }
            if (text.Name != null) {
                t.AddBookmark('#' + text.Name!, false);
            }
        } else {
            throw new Exception("An SText can be inside only an SParagraph!");
        }

        t.Bold = style.Bold ?? false;
        t.Italic = style.Italic ?? false;
        t.Color = style.FontColor ?? Colors.Black;
        switch(style.Underline) {
            case SUnderline.None:
                t.Underline = Underline.None;
                break;
            case SUnderline.Dash:
                t.Underline = Underline.Dash;
                break;
            case SUnderline.DotDash:
                t.Underline = Underline.DotDash;
                break;
            case SUnderline.DotDotDash:
                t.Underline = Underline.DotDotDash;
                break;
            case SUnderline.Dotted:
                t.Underline = Underline.Dotted;
                break;
            case SUnderline.Single:
                t.Underline = Underline.Single;
                break;
            case SUnderline.OnWords:
                t.Underline = Underline.Words;
                break;
        }

        if (style.FontSize != null)
        {
            t.Size = SMetricsUtil.GetUnitValue(style.FontSize, text.FathersStyle!.Dimensions!.X);
        }
        t.Subscript = (style.Subscript ?? false) && !(style.Superscript ?? false);
        t.Superscript = (style.Superscript ?? false) && !(style.Subscript ?? false);
    }


    public static void DoForLink(this SVisitor visitor, SLink link) {
        SStyle style = visitor.GetStyle(link.Style, link.UseStyle) ?? new SStyle();
        style.Merge(link.FathersStyle);

        var father = visitor.VisitedObjects.Peek();

        Hyperlink l;

        if (father is Paragraph paragraph) {
            bool isBookmarkLink = link.Link[0] == '#';

            if (isBookmarkLink) {
                l = paragraph.AddHyperlink(link.Link);
            }
            else {
                l = paragraph.AddWebLink(link.Link);
            }

            if (link.Name != null) {
                l.AddBookmark('#' + l.Name!, false);
            }
        } else {
            throw new Exception("An SText can be inside only an SParagraph!");
        }

        if (link.Text != null) {
            FormattedText t = l.AddFormattedText();

            if (link.BreakLine) {
                l.AddText("\n");
            }

            t.Bold = style.Bold ?? false;
            t.Italic = style.Italic ?? false;
            t.Color = style.FontColor ?? Colors.Black;
            switch(style.Underline) {
                case SUnderline.None:
                    t.Underline = Underline.None;
                    break;
                case SUnderline.Dash:
                    t.Underline = Underline.Dash;
                    break;
                case SUnderline.DotDash:
                    t.Underline = Underline.DotDash;
                    break;
                case SUnderline.DotDotDash:
                    t.Underline = Underline.DotDotDash;
                    break;
                case SUnderline.Dotted:
                    t.Underline = Underline.Dotted;
                    break;
                case SUnderline.Single:
                    t.Underline = Underline.Single;
                    break;
                case SUnderline.OnWords:
                    t.Underline = Underline.Words;
                    break;
            }

            if (style.FontSize != null)
            {
                t.Size = SMetricsUtil.GetUnitValue(style.FontSize, link.FathersStyle!.Dimensions!.X);
            }
            t.Subscript = (style.Subscript ?? false) && !(style.Superscript ?? false);
            t.Superscript = (style.Superscript ?? false) && !(style.Subscript ?? false);
        }
    }


    private static (double, double) SetBorders(SParagraph paragraph, Paragraph p, SStyle style) {
        double bordersWidth = 0;
        double bordersHeight = 0;

        SDimensions d = paragraph.FathersStyle!.Dimensions!;
        if (style.Borders!.Left != null) {
            SetBorder(paragraph, style.Borders.Left, p.Format.Borders.Left, true);
            p.Format.Borders.DistanceFromLeft = SMetricsUtil.GetUnitValue(style.Borders.Left.DistanceFromContent ?? new SMeasure(0), d.X);
            bordersWidth += (style.Borders.Left.Width ?? new SMeasure(0)).Value;
            bordersWidth += p.Format.Borders.DistanceFromLeft.Point;
        }

        if (style.Borders!.Right != null) {
            SetBorder(paragraph, style.Borders.Right, p.Format.Borders.Right, true);
            p.Format.Borders.DistanceFromRight = SMetricsUtil.GetUnitValue(style.Borders.Right.DistanceFromContent ?? new SMeasure(0), d.X);
            bordersWidth += (style.Borders.Right.Width ?? new SMeasure(0)).Value;
            bordersWidth += p.Format.Borders.DistanceFromRight.Point;
        }

        if (style.Borders!.Bottom != null) {
            SetBorder(paragraph, style.Borders.Bottom, p.Format.Borders.Bottom, false);
            p.Format.Borders.DistanceFromBottom = SMetricsUtil.GetUnitValue(style.Borders.Bottom.DistanceFromContent ?? new SMeasure(0), d.Y);
            bordersHeight += (style.Borders.Bottom.Width ?? new SMeasure(0)).Value;
            bordersHeight += p.Format.Borders.DistanceFromBottom.Point;
        }

        if (style.Borders!.Top != null) {
            SetBorder(paragraph, style.Borders.Top, p.Format.Borders.Top, false);
            p.Format.Borders.DistanceFromTop = SMetricsUtil.GetUnitValue(style.Borders.Top.DistanceFromContent ?? new SMeasure(0), d.Y);
            bordersHeight += (style.Borders.Top.Width ?? new SMeasure(0)).Value;
            bordersHeight += p.Format.Borders.DistanceFromTop.Point;
        }

        return (bordersWidth, bordersHeight);
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
        b.Width = border.Width != null ? SMetricsUtil.GetUnitValue(border.Width, horizontal ? d.X : d.Y) : Unit.FromPoint(1);
    }

    public static void DoForHeading(this SVisitor visitor, SHeading heading) {
        SStyle style = visitor.GetStyle(heading.Style, heading.UseStyle) ?? new SStyle();
        style.Merge(heading.FathersStyle);

        Paragraph p;
        var father = visitor.VisitedObjects.Peek();

        if (father is Section section) {
            p = section.AddParagraph();
        }
        else if (father is Cell cell)
        {
            p = cell.AddParagraph();
        }
        else if (father is TextFrame frame)
        {
            p = frame.AddParagraph();
        }
        else
        {
            throw new Exception("The heading can only be placed inside an SSection.");
        }

        if (heading.Name != null) {
            p.AddBookmark('#' + heading.Name, false);
        }

        // p.Format;
        SetFormat(p.Format, style, heading.FathersStyle!.Dimensions!);

        if (style.Borders != null) {
            var (x,y) = SetBorders(heading, p, style);
            style.Dimensions = new SDimensions(
                heading.FathersStyle!.Dimensions!.Y - y,
                heading.FathersStyle!.Dimensions!.X - x
            );
        }

        p.Format.OutlineLevel = heading.Level switch {
            1 => OutlineLevel.Level1,
            2 => OutlineLevel.Level2,
            3 => OutlineLevel.Level3,
            4 => OutlineLevel.Level4,
            5 => OutlineLevel.Level5,
            6 => OutlineLevel.Level6,
            _ => OutlineLevel.Level1,
        };

        visitor.VisitedObjects.Push(p);

        foreach (STextElement item in heading.Content ?? [])
        {
            item.FathersStyle = style;
            item.Accept(visitor);
        }

        visitor.VisitedObjects.Pop();
    }

    internal static void SetFormat(ParagraphFormat format, SStyle style, SDimensions dimensions) {
        format.Font.Color = style.FontColor ?? Colors.Black;
        format.Font.Bold = style.Bold ?? false;
        format.Font.Italic = style.Italic ?? false;
        format.Font.Underline = SUnderlineUtils.GetUnderline(style.Underline ?? SUnderline.None);
        if (style.FontSize != null)
        {
            format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.Y);
        }
        format.Font.Subscript = (style.Subscript ?? false) && !(style.Superscript ?? false);
        format.Font.Superscript = (style.Superscript ?? false) && !(style.Subscript ?? false);
        format.Shading.Color = style.Shading ?? Color.Empty;
        switch (style.HorizontalAlignment)
        {
            case SAlignment.Start:
                format.Alignment = ParagraphAlignment.Left;
                break;
            case SAlignment.Center:
                format.Alignment = ParagraphAlignment.Center;
                break;
            case SAlignment.End:
                format.Alignment = ParagraphAlignment.Right;
                break;
            case SAlignment.Justified:
                format.Alignment = ParagraphAlignment.Justify;
                break;
        }
    }
}