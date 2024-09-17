using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Tables;
using PDFScaffold.Visitors.Default;

public static class ForTable {

    public static void DoForTable(this SVisitor visitor, STable table) {
        SStyle style = visitor.GetStyle(table.Style, table.UseStyle) ?? new SStyle();
        style.Merge(table.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        TextFrame tf;
        Table t;

        if (father is Section section) {
            tf = section.AddTextFrame();
            t = tf.AddTable();
        } else if (father is TextFrame textFrame) {
            var tableOne = textFrame.AddTable();
            tableOne.AddColumn();
            tf = tableOne.AddRow().Cells[0].AddTextFrame();
            t = tf.AddTable();
        } else if (father is Cell c) {
            tf = c.AddTextFrame();
            t = tf.AddTable();
        } else {
            throw new Exception("An STable can only be placed inside an SSection, SColumn, SRow or SContainer");
        }

        var (columns, rows) = TableDimensions(table.Rows);

        if (table.ColumnSizes != null && table.ColumnSizes.Count != columns) {
            throw new Exception("The count of column sizes provided does not match with the number of columns inside the table.");
        }

        if (style.Width != null) {
            tf.Width = SMetricsUtil.GetUnitValue(style.Width, table.FathersStyle!.Dimensions!.X);
        } else {
            tf.Width = Unit.FromPoint(table.FathersStyle!.Dimensions!.X);
        }

        style.Dimensions = new(
            table.FathersStyle!.Dimensions!.Y,
            tf.Width.Point
        );

        if (style.Height != null) {
            tf.Height = SMetricsUtil.GetUnitValue(style.Height, table.FathersStyle!.Dimensions!.Y);
        }

        // Borders
        if (style.Borders != null) {
            var (x, y) = SetBorders(t.Borders, style, table.FathersStyle!.Dimensions!);
            style.Dimensions = new(
                style.Dimensions.Y - y,
                style.Dimensions.X - x
            );

            // Maybe we need to make the width of the tf smaller after this
        }

        // Format
        SetFormat(t, style, style.Dimensions!);

        // Shading
        SetShading(t, style);

        // Unit defaultWidth = Unit.FromPoint(style.Dimensions!.X / columns);

        for (int i = 0; i < columns; i++) {
            Column column = t.AddColumn();
            if (table.ColumnSizes != null) {
                column.Width = SMetricsUtil.GetUnitValue(table.ColumnSizes!.ElementAt(i), table.FathersStyle!.Dimensions!.X);
            }
        }

        t.GenerateRows(rows);

        visitor.VisitedObjects.Push(t);

        var cells = new Dictionary<(int, int), bool>();

        for (int i = 0; i < table.Rows.Count; i++)
        {
            STableRow row = table.Rows.ElementAt(i);
            row.RowIndex = i;
            row.Positions = cells;
            row.FathersStyle = style;
            row.Accept(visitor);
        }
    }

    public static (int, int) TableDimensions(ICollection<STableRow> rows)
    {
        int cs = 0;
        int rs = rows.Count;

        var cells = new Dictionary<(int, int), bool>();
        int rowIndex = 0;

        foreach (STableRow row in rows)
        {
            int columnIndex = 0;

            foreach (var cell in row.Cells)
            {
                while (true)
                {
                    int neededColumns = 0;

                    bool isLastColumn = cs == columnIndex + 1;
                    bool isNotLastColumn = cs > columnIndex + 1;
                    bool notEnoughColumns = cs < columnIndex + cell.ColumnSpan;
                    bool willNeedMoreColumns = isNotLastColumn && notEnoughColumns;
                    bool noColumnsLeft = cs < columnIndex + 1;

                    if (isLastColumn)
                    {
                        neededColumns = cell.ColumnSpan - 1;
                    }
                    else if (willNeedMoreColumns)
                    {
                        neededColumns = columnIndex + cell.ColumnSpan - cs;
                    }
                    else if (noColumnsLeft)
                    {
                        neededColumns = cell.ColumnSpan;
                    }

                    cs += neededColumns;

                    bool cellIsEmpty = !cells.ContainsKey((rowIndex, columnIndex));
                    if (cellIsEmpty)
                    {
                        cells.Add((rowIndex, columnIndex), true );
                        break;
                    }

                    columnIndex += 1;
                }

                if (cell.ColumnSpan > 1)
                {
                    for (int i = 1; i < cell.ColumnSpan; i++)
                    {
                        cells.TryAdd((rowIndex, columnIndex + i), true);
                    }
                    columnIndex += cell.ColumnSpan - 1;
                }

                if (cell.RowSpan > 1)
                {
                    if ((rowIndex + cell.RowSpan - 1) > rs)
                    {
                        throw new Exception($"The RowSpan \"{cell.RowSpan}\" of the cell can not be applied, because it traspasses the bounderies of the table.");
                    }
                    for (int i = 1; i < cell.RowSpan; i++)
                    {
                        for (int j = 0; j < cell.ColumnSpan; j++)
                        {
                            cells.Add((rowIndex + i, columnIndex + j), true);
                        }
                    }
                }

                columnIndex += 1;
            }

            rowIndex += 1;
        }

        return (cs, rs);
    }

    public static void DoForTableRow(this SVisitor visitor, STableRow row) {
        SStyle style = visitor.GetStyle(row.Style, row.UseStyle) ?? new SStyle();
        style.Merge(row.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Row r;

        if (father is Table table) {
            r = table.AddRow();
        } else {
            throw new Exception("The STableRow should only be placed inside an STable.");
        }

        // Style Row
        r.VerticalAlignment = style.VerticalAlignment switch {
            SAlignment.Start => VerticalAlignment.Top,
            SAlignment.Center => VerticalAlignment.Center,
            SAlignment.End => VerticalAlignment.Bottom,
            _ => VerticalAlignment.Top
        };

        r.Shading.Color = style.Shading ?? Color.Empty;

        style.Dimensions = new(
            row.FathersStyle!.Dimensions!.Y,
            row.FathersStyle!.Dimensions!.X
        );

        if (style.Height != null) {
            r.Height = SMetricsUtil.GetUnitValue(style.Height, row.FathersStyle!.Dimensions!.Y);
            style.Dimensions.Y -= r.Height.Point;
        }

        if (style.Borders != null) {
            var (x,y) = SetBorders(r.Borders, style, style.Dimensions);
            style.Dimensions.X -= x;
            style.Dimensions.Y -= y;
        }

        // Set Dimensions

        int columnIndex = 0;
        var cells = row.Positions;

        foreach (var cell in row.Cells)
        {
            Cell c;
            while (true) {
                bool cellIsEmpty = !cells!.ContainsKey((row.RowIndex, columnIndex));

                if (cellIsEmpty)
                {
                    c = r.Cells[columnIndex];
                    cells.Add((row.RowIndex, columnIndex), true );
                    break;
                }

                columnIndex += 1;
            }

            if (cell.ColumnSpan > 1) {
                c.MergeRight = cell.ColumnSpan - 1;
                for (int i = 1; i < cell.ColumnSpan; i++)
                {
                    cells.TryAdd((row.RowIndex, columnIndex + i), true);
                }
            }

            if (cell.RowSpan > 1) {
                //if ((row.RowIndex + cell.RowSpan - 1) > row.MaxRowIndex) {
                //    throw new Exception("The RowSpan of the cell can not be applied, because it traspasses the bounderies of the table.");
                //}
                c.MergeDown = cell.RowSpan - 1;
                for (int i = 1; i < cell.RowSpan; i++)
                {
                    for (int j = 0; j < cell.ColumnSpan; j++)
                    {
                        cells.Add((row.RowIndex + i, columnIndex + j), true);
                    }
                }
            }

            visitor.VisitedObjects.Push(c);
            if (row.RowIndex == 0 && row.FathersStyle!.Name != null) { SetBookmark(row.FathersStyle, c.AddTextFrame()); }
            if (style.Name != null) { SetBookmark(style, c.AddTextFrame()); }
            cell.FathersStyle = style;
            cell.Accept(visitor);
            visitor.VisitedObjects.Pop();
            
            columnIndex += 1;
        }
    }

    internal static void GenerateColumns(this Table table, int columns) {
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


    public static void DoForTableCell(this SVisitor visitor, STableCell cell) {
        SStyle style = visitor.GetStyle(cell.Style, cell.UseStyle) ?? new SStyle();
        style.Merge(cell.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        TextFrame tf;

        if (father is Cell c) {
            if (style.Name != null) { SetBookmark(style, c.AddTextFrame()); }
            tf = c.AddTextFrame();
        } else {
            throw new Exception("An STableCell can only be placed inside an STableRow.");
        }

        style.Dimensions = new(
            cell.FathersStyle!.Dimensions!.Y,
            cell.FathersStyle!.Dimensions!.X
        );

        // Set Dimensions
        if (style.Width != null) {
            tf.Width = SMetricsUtil.GetUnitValue(style.Width, cell.FathersStyle!.Dimensions!.X);
            style.Dimensions.X = tf.Width.Point;
        }

        if (style.Height != null) {
            tf.Height = SMetricsUtil.GetUnitValue(style.Height, cell.FathersStyle!.Dimensions!.Y);
            style.Dimensions.Y = tf.Height.Point;
        }

        if (style.Borders != null) {
            var (x,y) = SetBorders(c.Borders, style, style.Dimensions);
            style.Dimensions.X -= x;
            style.Dimensions.Y -= y;
        }

        // Style the Cell
        c.Shading.Color = style.Shading ?? Color.Empty;

        visitor.VisitedObjects.Push(tf);
        cell.Content.FathersStyle = style;
        cell.Content.Accept(visitor);

        visitor.VisitedObjects.Pop();
    }

    internal static void SetBookmark(SStyle style, TextFrame tf) {
        tf.Width = Unit.FromPoint(1);
        tf.Height = Unit.FromPoint(1);
        tf.AddParagraph().AddBookmark("#" + style.Name!);
    }

    internal static void SetFormat(Table table, SStyle style, SDimensions dimensions) {
        table.Format.Font.Bold = style.Bold ?? false;
        table.Format.Font.Italic = style.Italic ?? false;
        table.Format.Font.Color = style.FontColor ?? Colors.Black;
        if (style.FontSize != null) table.Format.Font.Size = SMetricsUtil.GetUnitValue(style.FontSize, dimensions.Y);
    }

    // internal static void SetWidthAndHeight(TextFrame tf, SStyle style, SDimensions dimensions) {
    //     if (style.Width != null) {
    //         tf.Width = SMetricsUtil.GetUnitValue(style.Width, dimensions.X);
    //     } else {
    //         tf.Width = Unit.FromPoint(dimensions.X);
    //     }

    //     if (style.Height != null) {
    //         tf.Height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
    //     } else {
    //         tf.Height = Unit.FromPoint(dimensions.Y);
    //     }

    //     style.Dimensions = new SDimensions(tf.Height.Point, tf.Width.Point);
    // }

    internal static (double, double) SetBorders(Borders borders, SStyle style, SDimensions dimensions) {
        double bordersWidth = 0;
        double bordersHeight = 0;

        if (style.Borders!.Left != null) {
            SetBorder(dimensions, style.Borders.Left, borders.Left, true);
            borders.DistanceFromLeft = SMetricsUtil.GetUnitValue(style.Borders.Left.DistanceFromContent ?? new SMeasure(0), dimensions.X);
            bordersWidth += (style.Borders.Left.Width ?? new SMeasure(0)).Value;
            bordersWidth += borders.DistanceFromLeft.Point;
        }

        if (style.Borders!.Right != null) {
            SetBorder(dimensions, style.Borders.Right, borders.Right, true);
            borders.DistanceFromRight = SMetricsUtil.GetUnitValue(style.Borders.Right.DistanceFromContent ?? new SMeasure(0), dimensions.X);
            bordersWidth += (style.Borders.Right.Width ?? new SMeasure(0)).Value;
            bordersWidth += borders.DistanceFromRight.Point;
        }

        if (style.Borders!.Bottom != null) {
            SetBorder(dimensions, style.Borders.Bottom, borders.Bottom, false);
            borders.DistanceFromBottom = SMetricsUtil.GetUnitValue(style.Borders.Bottom.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
            bordersHeight += (style.Borders.Bottom.Width ?? new SMeasure(0)).Value;
            bordersHeight += borders.DistanceFromBottom.Point;
        }

        if (style.Borders!.Top != null) {
            SetBorder(dimensions, style.Borders.Top, borders.Top, false);
            borders.DistanceFromTop = SMetricsUtil.GetUnitValue(style.Borders.Top.DistanceFromContent ?? new SMeasure(0), dimensions.Y);
            bordersHeight += (style.Borders.Top.Width ?? new SMeasure(0)).Value;
            bordersHeight += borders.DistanceFromTop.Point;
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

}