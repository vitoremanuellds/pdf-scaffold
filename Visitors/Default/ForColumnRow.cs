using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

public static class ForColumnRow {

    public static void DoForColumn(this SVisitor visitor, SColumn column) {
        SStyle style = visitor.GetStyle(column.Style, column.UseStyle) ?? new SStyle();
        style = style.Merge(column.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        TextFrame tf;
        Table table;

        if (father is Section section)
        {
            tf = section.AddTextFrame();
            table = tf.AddTable();
        } else if (father is Cell c) {
            tf = c.AddTextFrame();
            table = tf.AddTable();
        } else if (father is TextFrame t) {
            tf = t;
            table = tf.AddTable();
        } else {
            throw new Exception("An SColumn can only be inside an SSection, SContainer or SRow");
        }

        style.Dimensions = new(column.FathersStyle!.Dimensions!.Y, column.FathersStyle!.Dimensions!.X);

        SetWidthAndHeight(tf, style, column.FathersStyle!.Dimensions!);

        if (style.Borders != null) { 
            var (x, y) = SetBorders(table, style, column.FathersStyle!.Dimensions!);
            style.Dimensions!.X -= x;
            style.Dimensions!.Y -= y;

            // Maybe we will need to subtract from the height and width
        }
        

        // Format
        SetFormat(table, style, column.FathersStyle!.Dimensions!);

        // Shading
        SetShading(table, style);

        table.AddColumn(style.Dimensions!.X);

        for (int i = 0; i < column.Elements.Count; i++)
        {
            var cell = table.AddRow().Cells[0];
            if (i == 0 && style.Name != null) { SetBookmark(style, cell.AddTextFrame()); }
            visitor.VisitedObjects.Push(cell);

            var el = column.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);

            visitor.VisitedObjects.Pop();
        }

        column.Dimensions = style.Dimensions;
    }

    public static void DoForRow(this SVisitor visitor, SRow row) {
        SStyle style = visitor.GetStyle(row.Style, row.UseStyle) ?? new SStyle();
        style = style.Merge(row.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        TextFrame tf;
        Table table;

        if (father is Section section)
        {
            tf = section.AddTextFrame();
            table = tf.AddTable();
        } else if (father is Cell c) {
            tf = c.AddTextFrame();
            table = tf.AddTable();
        } else if (father is TextFrame t) {
            tf = t;
            table = tf.AddTable();
        } else {
            throw new Exception("An SColumn can only be inside an SSection, SContainer or SRow");
        }
        
        style.Dimensions = new(row.FathersStyle!.Dimensions!.Y, row.FathersStyle!.Dimensions!.X);

        SetWidthAndHeight(tf, style, row.FathersStyle!.Dimensions!, true);

        // Borders
        if (style.Borders != null) { 
            var (x,y) = SetBorders(table, style, row.FathersStyle!.Dimensions!);
            style.Dimensions!.X -= x;
            style.Dimensions!.Y -= y;

            // Maybe we need to subtract from the height and width
        }

        // Format
        SetFormat(table, style, row.FathersStyle!.Dimensions!);

        // Shading
        SetShading(table, style);

        table.GenerateColumns(row.Elements.Count);
        var tRow = table.AddRow();

        var customWidths = 0.0;
        var customWidthsCount = 0;

        for (int i = 0; i < row.Elements.Count; i++)
        {
            var cell = tRow.Cells[i];
            if (i == 0 && style.Name != null) { SetBookmark(style, cell.AddTextFrame()); }
            visitor.VisitedObjects.Push(cell);

            var el = row.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);
            if (el.Style?.Width != null) {
                table.Columns[i].Width = el.Dimensions!.X;
                customWidths += el.Dimensions!.X;
                customWidthsCount += 1;
            }

            visitor.VisitedObjects.Pop();
        }

        for (int i = 0; i < row.Elements.Count; i++) {
            var el = row.Elements.ElementAt(i);
            if (el.Style?.Width == null) {
                table.Columns[i].Width = (style.Dimensions.X - customWidths) / (row.Elements.Count - customWidthsCount);
            }
        }

        row.Dimensions = style.Dimensions;
    }

    internal static void SetFormat(Table table, SStyle style, SDimensions dimensions) {
        table.Format.Font.Bold = style.Bold ?? false;
        table.Format.Font.Italic = style.Italic ?? false;
        table.Format.Font.Color = style.FontColor ?? Colors.Black;
        if (style.FontSize != null) table.Format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.Y);
    }

    internal static void SetWidthAndHeight(TextFrame tf, SStyle style, SDimensions dimensions, bool isRow = false) {
        if (style.Width != null) {
            tf.Width = SMetricsUtil.GetUnitValue(style.Width, dimensions.X);
            style.Dimensions!.X = tf.Width.Point;
        } 
        // else if (!isRow) {
        //     tf.Width = Unit.FromPoint(dimensions.X);
        // }

        if (style.Height != null) {
            tf.Height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
            style.Dimensions!.X = tf.Height.Point;
        } 
        // else if (!isRow) {
        //     tf.Height = Unit.FromPoint(dimensions.Y);
        // }
    }

    internal static (double, double) SetBorders(Table table, SStyle style, SDimensions dimensions) {
        double bordersWidth = 0;
        double bordersHeight = 0;

        if (style.Borders!.Left != null) {
            SetBorder(dimensions, style.Borders.Left, table.Format.Borders.Left, true);
            table.Format.Borders.DistanceFromLeft = SMetricsUtil.GetUnitValue(style.Borders.Left.DistanceFromContent ?? new SMeasure(0), dimensions.X);
            bordersWidth += (style.Borders.Left.Width ?? new SMeasure(0)).Value;
            bordersWidth += table.Format.Borders.DistanceFromLeft.Point;
        }

        if (style.Borders!.Right != null) {
            SetBorder(dimensions, style.Borders.Right, table.Format.Borders.Right, true);
            table.Format.Borders.DistanceFromRight = SMetricsUtil.GetUnitValue(style.Borders.Right.DistanceFromContent ?? new SMeasure(0), dimensions.X);
            bordersWidth += (style.Borders.Right.Width ?? new SMeasure(0)).Value;
            bordersWidth += table.Format.Borders.DistanceFromRight.Point;
        }

        if (style.Borders!.Bottom != null) {
            SetBorder(dimensions, style.Borders.Bottom, table.Format.Borders.Bottom, false);
            table.Format.Borders.DistanceFromBottom = SMetricsUtil.GetUnitValue(style.Borders.Bottom.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
            bordersHeight += (style.Borders.Bottom.Width ?? new SMeasure(0)).Value;
            bordersHeight += table.Format.Borders.DistanceFromBottom.Point;
        }

        if (style.Borders!.Top != null) {
            SetBorder(dimensions, style.Borders.Top, table.Format.Borders.Top, false);
            table.Format.Borders.DistanceFromTop = SMetricsUtil.GetUnitValue(style.Borders.Top.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
            bordersHeight += (style.Borders.Top.Width ?? new SMeasure(0)).Value;
            bordersHeight += table.Format.Borders.DistanceFromTop.Point;
        }

        return (bordersWidth, bordersHeight);
    }

    internal static void SetBorder(
        SDimensions dimensions, 
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
        b.Width = border.Width != null ? SMetricsUtil.GetUnitValue(border.Width, horizontal ? dimensions.X : dimensions.Y) : Unit.FromPoint(1);
    }

    public static void SetShading (Table table, SStyle style) {
        if (style.Shading != null) {
            table.Shading.Color = (Color) style.Shading;
        }
    }

    internal static void SetBookmark(SStyle style, TextFrame tf) {
        tf.Width = Unit.FromPoint(1);
        tf.Height = Unit.FromPoint(1);
        tf.AddParagraph().AddBookmark("#" + style.Name!);
    }
}