using System.Runtime.InteropServices;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Tables;
using PDFScaffold.Visitors.Default;

public static class ForTable {

    public static void DoForTable(this SVisitor visitor, STable table) {
        SStyle style = visitor.GetStyle(table.Style, table.UseStyle) ?? new SStyle();
        style.Merge(table.FathersStyle);

        var father = visitor.VisitedObjects.Peek();
        Table t;

        if (father is Section section) {
            t = section.AddTable();
        } else if (father is TextFrame tf) {
            t = tf.AddTable();
        } else if (father is Cell c) {
            t = c.AddTextFrame().AddTable();
        } else {
            throw new Exception("An STable can only be placed inside an SSection, SColumn, SRow or SContainer");
        }

        IList<IList<Guid>> pos = [];

        int columns = 0;
        int rows = table.Rows.Count;

        for (int i = 0; i < rows; i++)
        {
            t.AddRow();
            columns = Math.Max(columns, table.Rows.ElementAt(i).Cells.Count);
        }

        if (table.ColumnSizes != null && table.ColumnSizes.Count != columns) {
            throw new Exception("The count of column sizes provided does not match with the number of columns inside the table.");
        }

        for (int i = 0; i < columns; i++) {
            Column column = t.AddColumn();
            if (table.ColumnSizes != null) {
                column.Width = SMetricsUtil.GetUnitValue(table.ColumnSizes!.ElementAt(i), table.FathersStyle!.Dimensions!.X);
            }
        }

        
    }

}