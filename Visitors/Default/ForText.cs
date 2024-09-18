using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Texts;

namespace PDFScaffold.Visitors.Default;

public static class ForText {

    public static void DoForText(this SVisitor visitor, SText text) {
        SStyle style = visitor.GetOrCreateStyle(text.Style, text.FathersStyle!, text.UseStyle);
        SDimensions parentsDimensions = text.FathersStyle!.Dimensions!;
        FormattedText ft = SVisitorUtils.GetMigradocObjectForText(visitor, text);
        style.Dimensions = parentsDimensions;

        SVisitorUtils.SetBookmark(ft, text.Name);
        SVisitorUtils.SetFormat(ft, style, style.Dimensions!);
    }
}