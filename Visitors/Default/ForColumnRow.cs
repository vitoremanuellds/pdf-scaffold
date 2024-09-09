using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

public static class ForColumnRow {

    public static void DoForColumn(this SVisitor visitor, SColumn column) {
        SStyle style = visitor.GetStyle(column.Style, column.UseStyle) ?? new SStyle();
        style.Merge(column.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Table table;

        if (father is Section section) {
            var d = column.Dimensions();
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

        // Need to style the Column

        visitor.VisitedObjects.Pop();
    }

    public static void DoForRow(this SVisitor visitor, SRow row) {
        SStyle style = visitor.GetStyle(row.Style, row.UseStyle) ?? new SStyle();
        style.Merge(row.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Table table;

        if (father is Section section) {
            var d = row.Dimensions();
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

        foreach (var item in row.Elements)
        {
            item.FathersStyle = style;
            // item.GetType().GetMethod("Accept")!.Invoke(item, [visitor]);
            item.Accept(visitor);
        }

        // Need to style the row

        visitor.VisitedObjects.Pop();
    }
}