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

        int columns = 0;
        int rows = table.Rows.Count;

        for (int i = 0; i < rows; i++)
        {
            // t.AddRow();
            STableRow row = table.Rows.ElementAt(i);
            row.RowIndex = i;
            row.MaxRowIndex = rows;
            columns = Math.Max(columns, row.Cells.Count);
        }

        if (table.ColumnSizes != null && table.ColumnSizes.Count != columns) {
            throw new Exception("The count of column sizes provided does not match with the number of columns inside the table.");
        }

        for (int i = 0; i < columns; i++) {
            Column column = t.AddColumn();
            if (table.ColumnSizes != null) {
                column.Width = SMetricsUtil.GetUnitValue(table.ColumnSizes!.ElementAt(i), table.FathersStyle!.Dimensions!.X);
            }
        }

        visitor.VisitedObjects.Push(table);

        // Style the Table

        // Set Dimensions

        foreach (STableRow row in table.Rows)
        {
            row.FathersStyle = style;
            row.Accept(visitor);
        }
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

        foreach (var cell in row.Cells)
        {
            Cell c;
            while (true) {
                int neededColumns = 0;

                bool isLastColumn = table.Columns.Count == columnIndex + 1;
                bool isNotLastColumn = table.Columns.Count > columnIndex + 1;
                bool notEnoughColumns = table.Columns.Count < columnIndex + cell.ColumnSpan;
                bool willNeedMoreColumns = isNotLastColumn && notEnoughColumns;
                bool noColumnsLeft = table.Columns.Count < columnIndex + 1;

                if (isLastColumn) {
                    neededColumns = cell.ColumnSpan - 1;
                } else if (willNeedMoreColumns) {
                    neededColumns = columnIndex + cell.ColumnSpan - table.Columns.Count;
                } else if (noColumnsLeft) {
                    neededColumns = cell.ColumnSpan;
                }

                table.GenerateColumns(neededColumns);

                c = r.Cells[columnIndex];
                bool cellIsEmpty = c.Elements.Count == 0;
                if (cellIsEmpty) {
                    break;
                }

                columnIndex += 1;
            }

            if (cell.ColumnSpan > 1) {
                c.MergeRight = cell.ColumnSpan - 1;
            }

            if (cell.RowSpan > 1) {
                if ((row.RowIndex + cell.RowSpan - 1) > row.MaxRowIndex) {
                    throw new Exception("The RowSpan of the cell can not be applied, because it traspasses the bounderies of the table.");
                }
                c.MergeDown = cell.RowSpan - 1;
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