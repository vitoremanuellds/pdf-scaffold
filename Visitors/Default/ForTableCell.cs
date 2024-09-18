using PDFScaffold.Tables;
using PDFScaffold.Styling;
using PDFScaffold.Metrics;

namespace PDFScaffold.Visitors.Default;

internal static class ForTableCell
{
    internal static void DoForTableCell(this SVisitor visitor, STableCell cell)
    {
        SStyle style = visitor.GetOrCreateStyle(cell.Style, cell.FathersStyle!, cell.UseStyle);
        SDimensions parentsDimensions = cell.FathersStyle!.Dimensions!;
        var (bookmarkTf, tf, c) = SVisitorUtils.GetMigradocObjectsForCell(visitor);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetBookmark(bookmarkTf, style);
        SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
        SVisitorUtils.SetTableBorders(c.Borders, style, style.Dimensions);
        SVisitorUtils.SetShading(c.Shading, style);

        visitor.VisitedObjects.Push(tf);
        cell.Content.FathersStyle = style;
        cell.Content.Accept(visitor);

        visitor.VisitedObjects.Pop();
    }
}
