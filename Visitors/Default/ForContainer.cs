using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForContainer
{

    internal static void DoForContainer(this SVisitor visitor, SContainer container)
    {
        SStyle style = visitor.GetOrCreateStyle(container.Style, container.FathersStyle!, container.UseStyle);
        SDimensions parentsDimensions = container.FathersStyle!.Dimensions!;
        var (bookmarkTf, textFrame) = SVisitorUtils.GetMigradocObjectsForContainer(visitor);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetBookmark(bookmarkTf, container.Name);
        SVisitorUtils.SetWidthAndHeight(textFrame, style, parentsDimensions);
        SVisitorUtils.SetBorder(textFrame.LineFormat, style);
        SVisitorUtils.SetShading(textFrame.FillFormat, style);
        SetPadding(textFrame, style);

        visitor.VisitedObjects.Push(textFrame);

        container.Content.FathersStyle = style;
        container.Content.Accept(visitor);

        CenterContent(textFrame, container.Content.Dimensions!, style);

        visitor.VisitedObjects.Pop();
        container.Dimensions = style.Dimensions;
    }


    internal static void SetBookmark(SStyle style, TextFrame tf)
    {
        tf.Width = Unit.FromPoint(1);
        tf.Height = Unit.FromPoint(1);
        tf.AddParagraph().AddBookmark("#" + style.Name!);
    }


    internal static void SetPadding(TextFrame tf, SStyle style)
    {
        if (style.Padding != null)
        {
            SPadding padding = style.Padding;

            tf.MarginLeft = SMetricsUtil.GetUnitValue(padding.Left ?? new SMeasure(0), style.Dimensions!.X);
            tf.MarginRight = SMetricsUtil.GetUnitValue(padding.Right ?? new SMeasure(0), style.Dimensions!.X);
            tf.MarginTop = SMetricsUtil.GetUnitValue(padding.Top ?? new SMeasure(0), style.Dimensions!.Y);
            tf.MarginBottom = SMetricsUtil.GetUnitValue(padding.Bottom ?? new SMeasure(0), style.Dimensions!.Y);
        }
    }

    internal static void CenterContent(TextFrame tf, SDimensions contentDimensions, SStyle style)
    {
        double sonX = contentDimensions.X;
        double sonY = contentDimensions.Y;

        if (style.HorizontalAlignment != null)
        {
            tf.MarginLeft = style.HorizontalAlignment switch
            {
                SAlignment.Center => Unit.FromPoint((style.Dimensions!.X - sonX) / 2),
                SAlignment.End => Unit.FromPoint(style.Dimensions!.X - sonX),
                _ => Unit.FromPoint(0),
            };
        }

        if (style.VerticalAlignment != null)
        {
            tf.MarginTop = style.VerticalAlignment switch
            {
                SAlignment.Center => Unit.FromPoint((style.Dimensions!.Y - sonY) / 2),
                SAlignment.End => Unit.FromPoint((style.Dimensions!.Y - sonY)),
                _ => Unit.FromPoint(0),
            };
        }

        if (style.Centered ?? false)
        {
            
            double horizontalMargins = (style.Dimensions!.X - sonX) / 2;
            double verticalMargins = (style.Dimensions!.Y - sonY) / 2;
            tf.MarginLeft = Unit.FromPoint(horizontalMargins);
            tf.MarginRight = Unit.FromPoint(horizontalMargins);
            tf.MarginTop = Unit.FromPoint(verticalMargins);
            tf.MarginBottom = Unit.FromPoint(verticalMargins);
        }
    }
}
