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
        style.Merge(column.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Table table;

        if (father is Section section)
        {
            table = section.AddTable();
        } else if (father is Cell c) {
            table = c.AddTextFrame().AddTable();
        } else if (father is TextFrame tf) {
            table = tf.AddTable();
        } else {
            throw new Exception("An SColumn can only be inside an SSection, SContainer or SRow");
        }

        // Format
        SetFormat(table, style, column.FathersStyle!.Dimensions!);

        // Borders
        if (style.Borders != null) { 
            SetBorders(table, style, column.FathersStyle!.Dimensions!);
        }

        // Padding

        // Shading
        SetShading(table, style);

        table.AddColumn();

        for (int i = 0; i < column.Elements.Count; i++)
        {
            var cell = table.AddRow().Cells[0];
            visitor.VisitedObjects.Push(cell);

            var el = column.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);

            visitor.VisitedObjects.Pop();
        }
    }

    public static void DoForRow(this SVisitor visitor, SRow row) {
        SStyle style = visitor.GetStyle(row.Style, row.UseStyle) ?? new SStyle();
        style.Merge(row.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Table table;

        if (father is Section section)
        {
            table = section.AddTable();
        }
        else if (father is Cell c) {
            table = c.AddTextFrame().AddTable();
        } else if (father is TextFrame tf) {
            table = tf.AddTable();
        } else {
            throw new Exception("An SColumn can only be inside an SSection, SContainer or SRow");
        }

        // Format
        SetFormat(table, style, row.FathersStyle!.Dimensions!);

        // Borders
        if (style.Borders != null) { 
            SetBorders(table, style, row.FathersStyle!.Dimensions!);
        }

        // Padding

        // Shading
        SetShading(table, style);

        table.GenerateColumns(row.Elements.Count);
        var tRow = table.AddRow();

        for (int i = 0; i < row.Elements.Count; i++)
        {
            var cell = tRow.Cells[i];
            visitor.VisitedObjects.Push(cell);

            var el = row.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);

            visitor.VisitedObjects.Pop();
        }
    }

    internal static void SetFormat(Table table, SStyle style, SDimensions dimensions) {
        table.Format.Font.Bold = style.Bold ?? false;
        table.Format.Font.Italic = style.Italic ?? false;
        table.Format.Font.Color = style.FontColor ?? Colors.Black;
        if (style.FontSize != null) table.Format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.Y);
    }

    

    internal static void SetBorders(Table table, SStyle style, SDimensions dimensions) {
        if (style.Borders!.Left != null) {
            SetBorder(dimensions, style.Borders.Left, table.Format.Borders.Left, true);
            table.Format.Borders.DistanceFromLeft = SMetricsUtil.GetUnitValue(style.Borders.Left.DistanceFromContent ?? new SMeasure(0), dimensions.X);
        }

        if (style.Borders!.Right != null) {
            SetBorder(dimensions, style.Borders.Right, table.Format.Borders.Right, true);
            table.Format.Borders.DistanceFromRight = SMetricsUtil.GetUnitValue(style.Borders.Right.DistanceFromContent ?? new SMeasure(0), dimensions.X);
        }

        if (style.Borders!.Bottom != null) {
            SetBorder(dimensions, style.Borders.Bottom, table.Format.Borders.Bottom, false);
            table.Format.Borders.DistanceFromBottom = SMetricsUtil.GetUnitValue(style.Borders.Bottom.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
        }

        if (style.Borders!.Top != null) {
            SetBorder(dimensions, style.Borders.Top, table.Format.Borders.Top, false);
            table.Format.Borders.DistanceFromTop = SMetricsUtil.GetUnitValue(style.Borders.Top.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
        }
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
}