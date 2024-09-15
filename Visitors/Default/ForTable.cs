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
        Table t;

        if (father is Section section) {
            t = section.AddTable();
        } else if (father is TextFrame tf) {
            t = tf.AddTable();
        } else if (father is Cell c) {
            t = c.AddTextFrame().AddTable();
        } else {
            throw new Exception("An STable can only be placed inside an SSection, SColumn, SRow or SContainer");
        }

        var (columns, rows) = TableDimensions(table.Rows);

        if (table.ColumnSizes != null && table.ColumnSizes.Count != columns) {
            throw new Exception("The count of column sizes provided does not match with the number of columns inside the table.");
        }

        Unit defaultWidth = Unit.FromPoint(table.FathersStyle!.Dimensions!.X / columns);

        for (int i = 0; i < columns; i++) {
            Column column = t.AddColumn();
            if (table.ColumnSizes != null) {
                column.Width = SMetricsUtil.GetUnitValue(table.ColumnSizes!.ElementAt(i), table.FathersStyle!.Dimensions!.X);
            } else
            {
                column.Width = defaultWidth;
            }
        }

        t.GenerateRows(rows);

        visitor.VisitedObjects.Push(t);

        // Style the Table

        // Set Dimensions


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
            tf = c.AddTextFrame();
        } else {
            throw new Exception("An STableCell can only be placed inside an STableRow.");
        }

        // Set Dimensions

        // Style the Cell

        visitor.VisitedObjects.Push(tf);
        cell.Content.FathersStyle = style;
        cell.Content.Accept(visitor);

        visitor.VisitedObjects.Pop();
    }

}