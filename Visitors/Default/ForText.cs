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
            p.AddBookmark('#' + paragraph.Name);
        }

        // p.Format;
        p.Format.Font.Color = style.FontColor ?? Colors.Black;
        p.Format.Font.Bold = style.Bold ?? false;
        p.Format.Font.Italic = style.Italic ?? false;
        p.Format.Font.Underline = SUnderlineUtils.GetUnderline(style.Underline ?? SUnderline.None);
        if (style.FontSize != null)
        {
            p.Format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, paragraph.FathersStyle!.Dimensions!.Y);
        }
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

        visitor.VisitedObjects.Push(p);
        style.Dimensions = paragraph.FathersStyle!.Dimensions;

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


    private static void SetBorders(SParagraph paragraph, Paragraph p, SStyle style) {
        SDimensions d = paragraph.FathersStyle!.Dimensions!;
        if (style.Borders!.Left != null) {
            SetBorder(paragraph, style.Borders.Left, p.Format.Borders.Left, true);
            p.Format.Borders.DistanceFromLeft = SMetricsUtil.GetUnitValue(style.Borders.Left.DistanceFromContent ?? new SMeasure(0), d.X);
        }

        if (style.Borders!.Right != null) {
            SetBorder(paragraph, style.Borders.Right, p.Format.Borders.Right, true);
            p.Format.Borders.DistanceFromRight = SMetricsUtil.GetUnitValue(style.Borders.Right.DistanceFromContent ?? new SMeasure(0), d.X);
        }

        if (style.Borders!.Bottom != null) {
            SetBorder(paragraph, style.Borders.Bottom, p.Format.Borders.Bottom, false);
            p.Format.Borders.DistanceFromBottom = SMetricsUtil.GetUnitValue(style.Borders.Bottom.DistanceFromContent ?? new SMeasure(0), d.Y);
        }

        if (style.Borders!.Top != null) {
            SetBorder(paragraph, style.Borders.Top, p.Format.Borders.Top, false);
            p.Format.Borders.DistanceFromTop = SMetricsUtil.GetUnitValue(style.Borders.Top.DistanceFromContent ?? new SMeasure(0), d.Y);
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
            p.AddBookmark('#' + heading.Name);
        }

        // p.Format;
        p.Format.Font.Color = style.FontColor ?? Colors.Black;
        p.Format.Font.Bold = style.Bold ?? true;
        p.Format.Font.Italic = style.Italic ?? false;
        p.Format.Font.Underline = SUnderlineUtils.GetUnderline(style.Underline ?? SUnderline.None);
        if (style.FontSize != null)
        {
            p.Format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, heading.FathersStyle!.Dimensions!.Y);
        } else {
            p.Format.Font.Size = SHeading.GetFontSize(heading.Level);
        }

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
            SetBorders(heading, p, style);
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
        style.Dimensions = heading.FathersStyle!.Dimensions;

        foreach (STextElement item in heading.Content ?? [])
        {
            item.FathersStyle = style;
            item.Accept(visitor);
        }

        visitor.VisitedObjects.Pop();
    }
}