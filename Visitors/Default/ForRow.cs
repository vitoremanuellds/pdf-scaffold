using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForRow
{
    internal static void DoForRow(this SVisitor visitor, SRow row)
    {
        SStyle style = visitor.GetOrCreateStyle(row.Style, row.FathersStyle!, row.UseStyle);
        SDimensions parentsDimensions = row.FathersStyle!.Dimensions!;
        var (tf, table) = SVisitorUtils.GetMigradocObjectsForTables(visitor);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
        SVisitorUtils.SetBorders(table.Borders, style, parentsDimensions);
        SVisitorUtils.SetFormat(table.Format, style, row.FathersStyle!.Dimensions!);
        SVisitorUtils.SetShading(table.Shading, style);

        table.GenerateColumns(row.Elements.Count);
        var tRow = table.AddRow();

        var customColumnsWidths = 0.0;
        var customColumns = 0;

        for (int i = 0; i < row.Elements.Count; i++)
        {
            var cell = tRow.Cells[i];
            if (i == 0)
            {
                SVisitorUtils.SetBookmark(cell, row.Name);
            }

            visitor.VisitedObjects.Push(cell);

            var el = row.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);

            if (el.Style?.Width != null)
            {
                table.Columns[i].Width = el.Dimensions!.X;
                customColumnsWidths += el.Dimensions!.X;
                customColumns += 1;
            }

            visitor.VisitedObjects.Pop();
        }

        row.SetColumnsWidth(table, style, customColumnsWidths, customColumns);
        row.Dimensions = style.Dimensions;
    }

    internal static void SetColumnsWidth(this SRow row, Table table, SStyle style, double customColumnsWidths, double customColumns)
    {
        for (int i = 0; i < row.Elements.Count; i++)
        {
            var el = row.Elements.ElementAt(i);
            if (el.Style?.Width == null)
            {
                var remainingSpace = style.Dimensions!.X - customColumnsWidths;
                var remainingColumns = row.Elements.Count - customColumns;
                table.Columns[i].Width = remainingSpace / remainingColumns;
            }
        }
    }
}
