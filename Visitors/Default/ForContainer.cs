using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

public static class ForContainer {

    public static void DoForContainer(this SContainer container, SVisitor visitor) {
        SStyle style = visitor.GetStyle(container.Style, container.UseStyle) ?? new SStyle();
        style.Merge(container.FathersStyle);

        TextFrame textFrame;
        var father = visitor.VisitedObjects.Peek();

        if (father != null && father is Section section) {
            textFrame = section.AddTextFrame();
        } else {
            throw new Exception("A Container can not be used inside other elements than a SSection");
        }

        SDimensions availableSpace = container.FathersStyle!.Dimensions!;

        if (container.SinglePage != null && (bool) container.SinglePage) {
            textFrame.Height = Unit.FromPoint(availableSpace.Y);
            textFrame.Width = Unit.FromPoint(availableSpace.X);
        } else {
            if (style.Width != null) {
                textFrame.Width = SMetricsUtil.GetUnitValue(style.Width, availableSpace.X);
            }

            if (style.Height != null) {
                textFrame.Height = SMetricsUtil.GetUnitValue(style.Height, availableSpace.Y);
            }
        }

        if (style.Shading != null) {
            textFrame.FillFormat.Color = style.Shading ?? Colors.White;
        }

        var dashStyles = new Dictionary<SBorderType, DashStyle>{
            { SBorderType.Dash, DashStyle.Dash },
            { SBorderType.DashDot, DashStyle.DashDot },
            { SBorderType.DashDotDot, DashStyle.DashDotDot },
            { SBorderType.Solid, DashStyle.Solid },
            { SBorderType.SquareDot, DashStyle.SquareDot },
        };

        SBorder? border = style.Borders?.Left;

        if (border != null) {
            textFrame.LineFormat.Color = border?.Color ?? Colors.Black;
            SBorderType borderType = 
                border?.BorderType ?? SBorderType.Solid;

            dashStyles.TryGetValue(borderType, out DashStyle dashStyle);
            textFrame.LineFormat.DashStyle = dashStyle;
            textFrame.LineFormat.Visible = border?.Visible ?? true;
            textFrame.LineFormat.Width = SMetricsUtil.GetUnitValue(border?.Width, availableSpace.X);
        }

        SMargin? margin = style.Margin;

        if (margin != null) {
            textFrame.MarginLeft = SMetricsUtil.GetUnitValue(margin.Left, availableSpace.X);
            textFrame.MarginRight = SMetricsUtil.GetUnitValue(margin.Right, availableSpace.X);
            textFrame.MarginTop = SMetricsUtil.GetUnitValue(margin.Top, availableSpace.Y);
            textFrame.MarginBottom = SMetricsUtil.GetUnitValue(margin.Bottom, availableSpace.Y);
        }

        visitor.VisitedObjects.Push(textFrame);

        container.Content.FathersStyle = style;
        container.Content.Accept(visitor);

        if (style.Centered ?? false) {
            double sonX = container.Content.Style!.Dimensions!.X;
            double sonY = container.Content.Style!.Dimensions!.Y;
            double horizontalMargins = (availableSpace.X - sonX) / 2;
            double verticalMargins = (availableSpace.Y - sonY) / 2;
            textFrame.MarginLeft = Unit.FromPoint(horizontalMargins);
            textFrame.MarginRight = Unit.FromPoint(horizontalMargins);
            textFrame.MarginTop = Unit.FromPoint(verticalMargins);
            textFrame.MarginBottom = Unit.FromPoint(verticalMargins);
        }

        visitor.VisitedObjects.Pop();
    }
}