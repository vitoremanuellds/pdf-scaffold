using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForColumn
{

    internal static void DoForColumn(this SVisitor visitor, SColumn column)
    {
        SStyle style = visitor.GetOrCreateStyle(column.Style, column.FathersStyle!, column.UseStyle);
        SDimensions parentsDimensions = column.FathersStyle!.Dimensions!;
        bool dimensionsSet = style.Width != null && style.Height != null;
        var (tf, table) = SVisitorUtils.GetMigradocObjectsForTables(visitor, dimensionsSet);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
        SVisitorUtils.SetBorders(table.Borders, style, style.Dimensions);
        SVisitorUtils.SetFormat(table.Format, style, style.Dimensions);
        SVisitorUtils.SetShading(table.Shading, style);

        table.AddColumn(style.Dimensions!.X);
        for (int i = 0; i < column.Elements.Count; i++)
        {
            var cell = table.AddRow().Cells[0];
            if (i == 0)
            {
                SVisitorUtils.SetBookmark(cell, column.Name);
            }

            visitor.VisitedObjects.Push(cell);

            var el = column.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);

            visitor.VisitedObjects.Pop();
        }

        column.Dimensions = style.Dimensions;
    }
}
