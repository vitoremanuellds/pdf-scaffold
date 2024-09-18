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

        row.SetColumnsWidth(table, style);
        for (int i = 0; i < row.Elements.Count; i++)
        {
            var cell = tRow.Cells[i];
            if (i == 0)
            {
                SVisitorUtils.SetBookmark(cell, row.Name);
            }

            visitor.VisitedObjects.Push(cell);

            var el = row.Elements.ElementAt(i);
            style.Dimensions.X = table.Columns[i].Width.Point;
            el.FathersStyle = style;
            el.Accept(visitor);

            visitor.VisitedObjects.Pop();
        }

        row.Dimensions = style.Dimensions;
    }

    internal static void SetColumnsWidth(this SRow row, Table table, SStyle style)
    {
        IList<int> widths = [];
        var availableSpace = style.Dimensions!.X;
        for (int i = 0; i < row.Elements.Count; i++)
        {
            var el = row.Elements.ElementAt(i);
            var elDimensions = style.Dimensions!.Copy();
            var elWidth = el.Style?.Width;

            if (elWidth != null)
            {
                var realWidth = SMetricsUtil.GetUnitValue(elWidth!, elDimensions.X);
                table.Columns[i].Width = realWidth;
                availableSpace -= realWidth.Point;
            } else {
                widths.Add(i);
            }
        }
        
        var count = widths.Count;
        if (count > 0) {
            if (availableSpace <= 0) availableSpace = 1;

            var width = availableSpace / count;
            foreach (var i in widths)
            {
                table.Columns[i].Width = width;
            }
        }        
    }
}
