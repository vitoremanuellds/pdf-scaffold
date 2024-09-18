using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Tables;
using PDFScaffold.Visitors.Default;

internal static class ForTable
{

    internal static void DoForTable(this SVisitor visitor, STable table)
    {
        SStyle style = visitor.GetOrCreateStyle(table.Style, table.FathersStyle!, table.UseStyle);
        SDimensions parentsDimensions = table.FathersStyle!.Dimensions!;
        var (tf, t) = SVisitorUtils.GetMigradocObjectsForTables(visitor);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
        SVisitorUtils.SetBorders(t.Borders, style, style.Dimensions);
        SVisitorUtils.SetFormat(t.Format, style, style.Dimensions);
        SVisitorUtils.SetShading(t.Shading, style);

        if (table.Rows.Count < 1)
        {
            throw new Exception("There has to be at least one STableRow inside an STable.");
        }

        var (columns, rows) = TableDimensions(table.Rows);
        if (table.ColumnSizes != null && table.ColumnSizes.Count != columns)
        {
            throw new Exception("The count of column sizes provided does not match with the number of columns inside the table.");
        }

        GenerateColumns(table, t, style.Dimensions, columns);
        t.GenerateRows(rows);
        SVisitorUtils.SetBookmark(t.Rows[0].Cells[0], table.Name);

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

        table.Dimensions = style.Dimensions;
    }

    internal static (int, int) TableDimensions(ICollection<STableRow> rows)
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
                FindNextEmptyCell(cells, cell, rowIndex, ref cs, ref columnIndex);
                FillColumnSpan(cells, rowIndex, cell, ref columnIndex);
                FillRowSpan(cells, rowIndex, cell, rs, columnIndex);
                columnIndex += 1;
            }
            rowIndex += 1;
        }
        return (cs, rs);
    }

    internal static void GenerateColumns(STable table, Table t, SDimensions dimensions, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            Column column = t.AddColumn();
            if (table.ColumnSizes != null)
            {
                column.Width = SMetricsUtil.GetUnitValue(table.ColumnSizes!.ElementAt(i), dimensions.X);
            }
        }
    }

    internal static void FindNextEmptyCell(IDictionary<(int, int), bool> cells, STableCell cell, int rowIndex, ref int cs, ref int columnIndex)
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
                cells.Add((rowIndex, columnIndex), true);
                break;
            }

            columnIndex += 1;
        }
    }

    internal static void FillColumnSpan(IDictionary<(int, int), bool> cells, int rowIndex, STableCell cell, ref int columnIndex)
    {
        if (cell.ColumnSpan > 1)
        {
            for (int i = 1; i < cell.ColumnSpan; i++)
            {
                cells.TryAdd((rowIndex, columnIndex + i), true);
            }
            columnIndex += cell.ColumnSpan - 1;
        }
    }

    internal static void FillRowSpan(IDictionary<(int, int), bool> cells, int rowIndex, STableCell cell, int rs, int columnIndex)
    {
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
    }

}
