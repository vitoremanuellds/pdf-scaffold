using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Layout;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

public static class ForColumn {

    public static void DoForColumn(this SVisitor visitor, SColumn column) {
        SStyle style = visitor.GetOrCreateStyle(column.Style, column.FathersStyle!, column.UseStyle);

        var father = visitor.VisitedObjects.Peek();
        TextFrame tf;
        Table table;

        if (father is Section section)
        {
            tf = section.AddTextFrame();
            table = tf.AddTable();
        } else if (father is Cell c) {
            tf = c.AddTextFrame();
            table = tf.AddTable();
        } else if (father is TextFrame t) {
            tf = t;
            table = tf.AddTable();
        } else {
            throw new Exception("An SColumn can only be inside an SSection, SContainer or SRow");
        }

        style.Dimensions = column.FathersStyle!.Dimensions!.Copy();

        SetWidthAndHeight(tf, style, column.FathersStyle!.Dimensions!);

        if (style.Borders != null) { 
            var (x, y) = SetBorders(table, style, column.FathersStyle!.Dimensions!);
            style.Dimensions!.X -= x;
            style.Dimensions!.Y -= y;

            // Maybe we will need to subtract from the height and width
        }
        

        // Format
        SetFormat(table, style, column.FathersStyle!.Dimensions!);

        // Shading
        SetShading(table, style);

        table.AddColumn(style.Dimensions!.X);

        for (int i = 0; i < column.Elements.Count; i++)
        {
            var cell = table.AddRow().Cells[0];
            if (i == 0 && style.Name != null) { SetBookmark(style, cell.AddTextFrame()); }
            visitor.VisitedObjects.Push(cell);

            var el = column.Elements.ElementAt(i);
            el.FathersStyle = style;
            el.Accept(visitor);

            visitor.VisitedObjects.Pop();
        }

        column.Dimensions = style.Dimensions;
    }


    internal static (TextFrame, Table) GetMigradocObjects(this SColumn column, SVisitor visitor) {
        var migraDocParent = visitor.VisitedObjects.Peek();

        if (migraDocParent is Section section) 
        {
            var tf = section.AddTextFrame();
            return (tf, tf.AddTable());
        } 
        else 
        {
            throw new Exception("An SColumn can not be placed inside another SColumn.");
        }
    }

}