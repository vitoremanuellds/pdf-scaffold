using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Images;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Texts;

namespace PDFScaffold.Visitors.Default;

internal static class SVisitorUtils
{

    internal static SStyle GetOrCreateStyle(this SVisitor visitor, SStyle? style, SStyle parentsStyle, string? useStyle)
    {
        SStyle s = visitor.GetStyle(style, useStyle) ?? new SStyle();
        return s.Merge(parentsStyle);
    }

    internal static SDimensions Copy(this SDimensions dimensions)
    {
        return new SDimensions(
            dimensions.Y,
            dimensions.X
        );
    }

    internal static void SetWidthAndHeight(TextFrame? tf, SStyle style, SDimensions dimensions)
    {
        if (style.Width != null && tf != null)
        {
            var width = SMetricsUtil.GetUnitValue(style.Width, dimensions.X);
            tf.Width = width;
            style.Dimensions!.X = width.Point;
        }

        if (style.Height != null && tf != null)
        {
            var height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
            tf.Height = height;
            style.Dimensions!.Y = height.Point;
        }
    }

    internal static void SetWidthAndHeight(Image image, SStyle style, SDimensions dimensions)
    {
        if (style.Width != null)
        {
            var width = SMetricsUtil.GetUnitValue(style.Width, dimensions.X);
            image.Width = width;
            if (style.Height == null)
            {
                image.Height = width;
                style.Dimensions!.Y = image.Height.Point;
            }
            style.Dimensions!.X = width.Point;
        }

        if (style.Height != null)
        {
            var height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
            image.Height = height;
            if (style.Width == null)
            {
                image.Width = height;
                style.Dimensions!.X = image.Width.Point;
            }
            style.Dimensions!.Y = height.Point;
        }
    }

    internal static void SetWidthAndHeight(Row row, SStyle style, SDimensions dimensions)
    {
        if (style.Height != null)
        {
            row.Height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
            style.Dimensions!.Y -= row.Height.Point;
        }
    }

    //internal static void SetFormat(ParagraphFormat format, SStyle style, SDimensions dimensions)
    //{
    //    format.Font.Bold = style.Bold ?? false;
    //    format.Font.Italic = style.Italic ?? false;
    //    format.Font.Color = style.FontColor ?? Colors.Black;
    //    if (style.FontSize != null)
    //    {
    //        format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.Y);
    //    }
    //}

    internal static void SetFormat(FormattedText formattedText, SStyle style, SDimensions dimensions, bool isLink = false)
    {
        formattedText.Bold = style.Bold ?? false;
        formattedText.Italic = style.Italic ?? false;
        formattedText.Color = style.FontColor ?? (isLink ? Colors.BlueViolet : Colors.Black);

        switch (style.Underline)
        {
            case SUnderline.None:
                formattedText.Underline = Underline.None;
                break;
            case SUnderline.Dash:
                formattedText.Underline = Underline.Dash;
                break;
            case SUnderline.DotDash:
                formattedText.Underline = Underline.DotDash;
                break;
            case SUnderline.DotDotDash:
                formattedText.Underline = Underline.DotDotDash;
                break;
            case SUnderline.Dotted:
                formattedText.Underline = Underline.Dotted;
                break;
            case SUnderline.Single:
                formattedText.Underline = Underline.Single;
                break;
            case SUnderline.OnWords:
                formattedText.Underline = Underline.Words;
                break;
        }

        if (style.FontSize != null)
        {
            formattedText.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.X);
        }
        formattedText.Subscript = (style.Subscript ?? false) && !(style.Superscript ?? false);
        formattedText.Superscript = (style.Superscript ?? false) && !(style.Subscript ?? false);
    }

    internal static void SetFormat(ParagraphFormat format, SStyle style, SDimensions dimensions, bool isHeading = false, int level = 1)
    {
        format.Font.Color = style.FontColor ?? Colors.Black;
        format.Font.Bold = style.Bold ?? isHeading;
        format.Font.Italic = style.Italic ?? false;
        format.Font.Underline = SUnderlineUtils.GetUnderline(style.Underline ?? SUnderline.None);
        if (style.FontSize != null)
        {
            format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.Y);
        }
        else if (isHeading)
        {
            format.Font.Size = SHeading.GetFontSize(level);
        }

        if (isHeading)
        {
            format.OutlineLevel = level switch
            {
                1 => OutlineLevel.Level1,
                2 => OutlineLevel.Level2,
                3 => OutlineLevel.Level3,
                4 => OutlineLevel.Level4,
                5 => OutlineLevel.Level5,
                6 => OutlineLevel.Level6,
                _ => OutlineLevel.Level1,
            };
        }

        format.Font.Subscript = (style.Subscript ?? false) && !(style.Superscript ?? false);
        format.Font.Superscript = (style.Superscript ?? false) && !(style.Subscript ?? false);
        switch (style.Alignment)
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
        if (style.LineSpacing != null)
        {
            format.LineSpacingRule = LineSpacingRule.Single;
            format.LineSpacing = SMetricsUtil.GetUnitValue(style.LineSpacing, dimensions.Y);
        }
        format.SpaceBefore = SMetricsUtil.GetUnitValue(style.SpaceBefore ?? new(10), dimensions.Y);
        if (style.SpaceAfter != null)
        {
            format.SpaceAfter = SMetricsUtil.GetUnitValue(style.SpaceAfter, dimensions.Y);
        }
    }

    internal static void SetBorders(Borders borders, SStyle style, SDimensions dimensions)
    {
        if (style.Borders != null)
        {
            double bordersWidth = 0;
            double bordersHeight = 0;

            if (style.Borders!.Left != null)
            {
                SetBorder(dimensions, style.Borders.Left, borders.Left, true);
                borders.DistanceFromLeft = SMetricsUtil.GetUnitValue(style.Borders.Left.DistanceFromContent ?? new SMeasure(0), dimensions.X);
                bordersWidth += (style.Borders.Left.Width ?? new SMeasure(0)).Value;
                bordersWidth += borders.DistanceFromLeft.Point;
            }

            if (style.Borders!.Right != null)
            {
                SetBorder(dimensions, style.Borders.Right, borders.Right, true);
                borders.DistanceFromRight = SMetricsUtil.GetUnitValue(style.Borders.Right.DistanceFromContent ?? new SMeasure(0), dimensions.X);
                bordersWidth += (style.Borders.Right.Width ?? new SMeasure(0)).Value;
                bordersWidth += borders.DistanceFromRight.Point;
            }

            if (style.Borders!.Bottom != null)
            {
                SetBorder(dimensions, style.Borders.Bottom, borders.Bottom, false);
                borders.DistanceFromBottom = SMetricsUtil.GetUnitValue(style.Borders.Bottom.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
                bordersHeight += (style.Borders.Bottom.Width ?? new SMeasure(0)).Value;
                bordersHeight += borders.DistanceFromBottom.Point;
            }

            if (style.Borders!.Top != null)
            {
                SetBorder(dimensions, style.Borders.Top, borders.Top, false);
                borders.DistanceFromTop = SMetricsUtil.GetUnitValue(style.Borders.Top.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
                bordersHeight += (style.Borders.Top.Width ?? new SMeasure(0)).Value;
                bordersHeight += borders.DistanceFromTop.Point;
            }


            style.Dimensions!.X -= bordersWidth;
            style.Dimensions!.Y -= bordersHeight;

            // Maybe we will need to subtract from the height and width
        }
    }

    internal static void SetBorder(SDimensions dimensions, SBorder border, Border b, bool horizontal)
    {
        b.Color = border.Color ?? Colors.Black;
        switch (border.BorderType)
        {
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
        b.Width = border.Width != null ? SMetricsUtil.GetUnitValue(border.Width, horizontal ? dimensions.X : dimensions.Y) : Unit.FromPoint(1);
    }

    internal static void SetBorder(LineFormat lf, SStyle style)
    {
        if (style.Borders != null)
        {
            SBorder border = style.Borders.Left ?? style.Borders.Right ?? style.Borders.Top ?? style.Borders.Bottom!;
            lf.Color = border?.Color ?? Colors.Black;
            lf.DashStyle = (border?.BorderType ?? SBorderType.Solid) switch
            {
                SBorderType.Dash => DashStyle.Dash,
                SBorderType.DashDot => DashStyle.DashDot,
                SBorderType.DashDotDot => DashStyle.DashDotDot,
                SBorderType.Solid => DashStyle.Solid,
                SBorderType.SquareDot => DashStyle.SquareDot,
                _ => DashStyle.Solid
            };
            lf.Visible = border?.Visible ?? true;
            lf.Width = SMetricsUtil.GetUnitValue(border?.Width ?? new SMeasure(1), style.Dimensions!.X);
            style.Dimensions.X -= 2 * lf.Width.Point;
            style.Dimensions.Y -= 2 * lf.Width.Point;
        }
    }

    internal static void SetShading(Shading shading, SStyle style)
    {
        shading.Color = style.Shading ?? Color.Empty;
    }

    internal static void SetShading(FillFormat ff, SStyle style)
    {
        ff.Color = style.Shading ?? Color.Empty;
    }

    internal static void SetBookmark(Cell cell, string? name)
    {
        if (name != null)
        {
            var tf = cell.AddTextFrame();
            tf.Width = 1;
            tf.Height = 1;
            tf.AddParagraph().AddBookmark("#" + name);
        }
    }

    internal static void SetBookmark(TextFrame tf, string? name)
    {
        tf.Width = 1;
        tf.Height = 1;
        if (name != null)
        {
            tf.AddParagraph().AddBookmark("#" + name);
        }
    }

    internal static void SetBookmark(Hyperlink link, string? name)
    {
        if (name != null)
        {
            link.AddBookmark('#' + name, false);
        }
    }

    internal static void SetBookmark(FormattedText ft, string? name)
    {
        if (name != null)
        {
            ft.AddBookmark('#' + name, false);
        }
    }

    internal static VerticalAlignment GetVerticalAlignment(this SAlignment alignment)
    {
        return alignment switch
        {
            SAlignment.Start => MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Top,
            SAlignment.Center => MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center,
            SAlignment.End => MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Bottom,
            _ => MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Top
        };
    }

    internal static void GenerateColumns(this Table table, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            table.AddColumn();
        }
    }

    internal static void GenerateRows(this Table table, int rows)
    {
        for (int i = 0; i < rows; i++)
        {
            table.AddRow();
        }
    }

    internal static (TextFrame?, Table) GetMigradocObjectsForTables(SVisitor visitor, bool needTextFrame = false)
    {
        var migraDocParent = visitor.VisitedObjects.Peek();

        if (migraDocParent is Section section)
        {
            if (!needTextFrame)
            {
                return (null, section.AddTable());
            }
            var tf = section.AddTextFrame();
            return (tf, tf.AddTable());
        }
        else if (migraDocParent is Cell cell)
        {
            var tf = cell.AddTextFrame();
            return (tf, tf.AddTable());
        }
        else if (migraDocParent is TextFrame t)
        {
            if (!needTextFrame)
            {
                return (null, t.AddTable());
            }
            var table = t.AddTable();
            table.AddColumn();
            var tf = table.AddRow().Cells[0].AddTextFrame();
            return (tf, tf.AddTable());
        }
        else
        {
            throw new Exception("An SColumn or SRow can only be placed inside SSection, SColumn, SRow, SContainer and STableCells.");
        }
    }

    internal static (TextFrame, TextFrame) GetMigradocObjectsForContainer(SVisitor visitor)
    {
        var migraDocParent = visitor.VisitedObjects.Peek();

        if (migraDocParent is Section section)
        {
            return (section.AddTextFrame(), section.AddTextFrame());
        }
        else if (migraDocParent is Cell cell)
        {
            return (cell.AddTextFrame(), cell.AddTextFrame());
        }
        else if (migraDocParent is TextFrame textFrame)
        {
            var table = textFrame.AddTable();
            table.AddColumn();
            var c = table.AddRow().Cells[0];
            return (c.AddTextFrame(), c.AddTextFrame());
        }
        else
        {
            throw new Exception("An SContainer can only be placed inside SSection, SColumn, SRow, SContainer and STableCells.");
        }
    }

    internal static (TextFrame, Image) GetMigradocObjectsForImage(SVisitor visitor, SImage image)
    {
        var migraDocParent = visitor.VisitedObjects.Peek();

        if (migraDocParent is Section section)
        {
            var tf = section.AddTextFrame();
            return (tf, section.AddImage(image.Path));
        }
        else if (migraDocParent is Cell cell)
        {
            var tf = cell.AddTextFrame();
            return (tf, cell.AddImage(image.Path));
        }
        else if (migraDocParent is TextFrame t)
        {
            var table = t.AddTable();
            table.AddColumn();
            var tf = table.AddRow().Cells[0].AddTextFrame();
            return (tf, t.AddImage(image.Path));
        }
        else
        {
            throw new Exception("An SImage can only be placed inside SSection, SColumn, SRow, SContainer and STableCells.");
        }
    }

    internal static (TextFrame, TextFrame?, Cell) GetMigradocObjectsForCell(SVisitor visitor, bool needTextFrame = false)
    {
        var migradocParent = visitor.VisitedObjects.Peek();

        if (migradocParent is Cell c)
        {
            if (!needTextFrame) {
                return (c.AddTextFrame(), null, c);
            }
            return (c.AddTextFrame(), c.AddTextFrame(), c);
        }
        else
        {
            throw new Exception("An STableCell can only be placed inside an STableRow.");
        }
    }

    internal static Row GetMigradocObjectForRow(SVisitor visitor, int rowIndex)
    {
        var migradocParent = visitor.VisitedObjects.Peek();
        if (migradocParent is Table table)
        {
            return table.Rows[rowIndex];
        }
        else
        {
            throw new Exception("The STableRow should only be placed inside an STable.");
        }
    }

    internal static (TextFrame?, Paragraph) GetMigradocObjectsForParagraph(SVisitor visitor, bool needTextFrame = false)
    {
        var migradocParent = visitor.VisitedObjects.Peek();
        if (migradocParent is Section section)
        {
            if (!needTextFrame)
            {
                return (null, section.AddParagraph());
            }
            var tf = section.AddTextFrame();
            return (tf, tf.AddParagraph());
        }
        else if (migradocParent is Cell cell)
        {
            if (!needTextFrame)
            {
                return (null, cell.AddParagraph());
            }
            var tf = cell.AddTextFrame();
            return (tf, tf.AddParagraph());
        }
        else if (migradocParent is TextFrame frame)
        {
            if (!needTextFrame)
            {
                return (null, frame.AddParagraph());
            }
            var table = frame.AddTable();
            table.AddColumn();
            var tf = table.AddRow().Cells[0].AddTextFrame();
            return (tf, tf.AddParagraph());
        }
        else
        {
            throw new Exception("An SParagraph can not be placed outside a SSection, SColumn, SRow, SContainer or STableCell.");
        }
    }

    internal static Hyperlink GetMigradocObjectForLinks(SVisitor visitor, SLink link)
    {
        var migradocParent = visitor.VisitedObjects.Peek();

        Hyperlink l;

        if (migradocParent is Paragraph paragraph)
        {
            bool isBookmarkLink = link.Link.StartsWith("#");

            if (isBookmarkLink)
            {
                return paragraph.AddHyperlink(link.Link);
            }
            else
            {
                return paragraph.AddWebLink(link.Link);
            }
        }
        else
        {
            throw new Exception("An SLink can only be inside of an SParagraph!");
        }
    }

    internal static FormattedText GetMigradocObjectForText(SVisitor visitor, SText text)
    {
        var migradocParent = visitor.VisitedObjects.Peek();

        if (migradocParent is Paragraph paragraph)
        {
            return paragraph.AddFormattedText(text.Value);
        }
        else
        {
            throw new Exception("An SText can be inside only an SParagraph!");
        }
    }
}
