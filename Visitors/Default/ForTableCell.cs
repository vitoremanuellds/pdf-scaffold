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
        bool dimensionsSet = style.Width != null && style.Height != null;
        var (bookmarkTf, tf, c) = SVisitorUtils.GetMigradocObjectsForCell(visitor, dimensionsSet);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetBookmark(bookmarkTf, cell.Name);
        SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
        SVisitorUtils.SetBorders(c.Borders, style, style.Dimensions);
        SVisitorUtils.SetShading(c.Shading, style);

        if (tf == null) {
            visitor.VisitedObjects.Push(c);    
        } else {
            visitor.VisitedObjects.Push(tf);
        }
        cell.Content.FathersStyle = style;
        cell.Content.Accept(visitor);

        visitor.VisitedObjects.Pop();
    }
}
