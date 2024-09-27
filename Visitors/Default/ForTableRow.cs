using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Tables;

namespace PDFScaffold.Visitors.Default;

internal static class ForTableRow
{
    internal static void DoForTableRow(this SVisitor visitor, STableRow row)
    {
        SStyle style = visitor.GetOrCreateStyle(row.Style, row.FathersStyle!, row.UseStyle);
        SDimensions parentsDimensions = row.FathersStyle!.Dimensions!;
        var r = SVisitorUtils.GetMigradocObjectForRow(visitor, row.RowIndex);
        style.Dimensions = parentsDimensions.Copy();

        // Style Row
        r.VerticalAlignment = (style.VerticalAlignment ?? SAlignment.Start).GetVerticalAlignment();

        SVisitorUtils.SetShading(r.Shading, style);
        SVisitorUtils.SetWidthAndHeight(r, style, parentsDimensions);
        SVisitorUtils.SetBorders(r.Borders, style, style.Dimensions);

        int columnIndex = 0;
        var cells = row.Positions!;

        foreach (var cell in row.Cells)
        {
            Cell c = FindCell(cells, row, r, ref columnIndex);
            FillColumnSpan(cells, row, cell, c, ref columnIndex);
            FillRowSpan(cells, row, cell, c, ref columnIndex);

            visitor.VisitedObjects.Push(c);
            if (row.RowIndex == 0 && columnIndex == 0)
            {
                SVisitorUtils.SetBookmark(c, row.Name);
            }
            cell.FathersStyle = style;
            cell.Accept(visitor);
            visitor.VisitedObjects.Pop();

            columnIndex += 1;
        }
    }

    internal static Cell FindCell(IDictionary<(int, int), bool> cells, STableRow row, Row r, ref int columnIndex)
    {
        while (true)
        {
            bool cellIsEmpty = !cells!.ContainsKey((row.RowIndex, columnIndex));

            if (cellIsEmpty)
            {
                cells.Add((row.RowIndex, columnIndex), true);
                return r.Cells[columnIndex];
            }

            columnIndex += 1;
        }
    }

    internal static void FillColumnSpan(IDictionary<(int, int), bool> cells, STableRow row, STableCell cell, Cell c, ref int columnIndex)
    {
        if (cell.ColumnSpan > 1)
        {
            c.MergeRight = cell.ColumnSpan - 1;
            for (int i = 1; i < cell.ColumnSpan; i++)
            {
                cells.TryAdd((row.RowIndex, columnIndex + i), true);
            }
        }
    }

    internal static void FillRowSpan(IDictionary<(int, int), bool> cells, STableRow row, STableCell cell, Cell c, ref int columnIndex)
    {
        if (cell.RowSpan > 1)
        {
            // if ((row.RowIndex + cell.RowSpan - 1) > row.MaxRowIndex) {
            //    throw new Exception("The RowSpan of the cell can not be applied, because it traspasses the bounderies of the table.");
            // }
            c.MergeDown = cell.RowSpan - 1;
            for (int i = 1; i < cell.RowSpan; i++)
            {
                for (int j = 0; j < cell.ColumnSpan; j++)
                {
                    cells.Add((row.RowIndex + i, columnIndex + j), true);
                }
            }
        }
    }
}
