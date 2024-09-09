using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

public static class ForColumnRow {

    public static void DoForColumn(this SColumn column, SVisitor visitor) {
        SStyle style = visitor.GetStyle(column.Style, column.UseStyle) ?? new SStyle();
        style.Merge(column.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Table table;
        var d = column.Dimensions();

        if (father is Section section) {
            table = section.AddTable();
            for (int i = 0; i < d.Item1; i++)
            {
                table.AddColumn();
            }
            for (int i = 0; i < d.Item2; i++)
            {
                table.AddRow();
            }
        } else if (father is Table t) {
            table = t;
        } // Add verification for SContainer

        

        visitor.VisitedObjects.Push(father);

        foreach (var item in column.Elements)
        {
            item.FathersStyle = style;
            item.Accept(visitor);
        }

        visitor.VisitedObjects.Pop();
    }
}