using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class SVisitorUtils
{
    
    internal static SStyle GetOrCreateStyle
    (
        this SVisitor visitor, 
        SStyle? style,
        SStyle parentsStyle,
        string? useStyle
    ) {
        SStyle s = visitor.GetStyle(style, useStyle) ?? new SStyle();
        return s.Merge(parentsStyle);
    }


    internal static SDimensions Copy(this SDimensions dimensions) {
        return new SDimensions(
            dimensions.Y,
            dimensions.X
        );
    }

    internal static void SetWidthAndHeight(
        TextFrame tf,
        SStyle style,
        SDimensions dimensions
    ) {
        if (style.Width != null) {
            var width = SMetricsUtil.GetUnitValue(style.Width, dimensions.X);
            tf.Width = width;
            style.Dimensions!.X = width.Point;
        }

        if (style.Height != null) {
            var height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
            tf.Height = height;
            style.Dimensions!.Y = height.Point;
        }
    }


    internal static void SetTableBorder(
        SDimensions dimensions, 
        SBorder border, 
        Border b,
        bool horizontal
    ) {
        b.Color = border.Color ?? Colors.Black;
        switch (border.BorderType) {
            case SBorderType.None:
                b.Style = BorderStyle.None;
                break;
            case SBorderType.Single:
                b.Style = BorderStyle.Single;
                break;
            case SBorderType.Dot:
                b.Style = BorderStyle.Dot;
                break;
            case SBorderType.DashDot:
                b.Style = BorderStyle.DashDot;
                break;
            case SBorderType.DashDotDot:
                b.Style = BorderStyle.DashDotDot;
                break;
            case SBorderType.DashLargeGap:
                b.Style = BorderStyle.DashLargeGap;
                break;
            case SBorderType.DashSmallGap:
                b.Style = BorderStyle.DashSmallGap;
                break;
        }
        b.Visible = border.Visible ?? false;
        b.Width = border.Width != null ? SMetricsUtil.GetUnitValue(border.Width, horizontal ? dimensions.X : dimensions.Y) : Unit.FromPoint(1);
    }


    internal static void SetShading(Shading shading, SStyle style) {
        shading.Color = style.Shading ?? Color.Empty;
    }

    internal static void SetBookmark(TextFrame tf, SStyle style) {
        if (style.Name != null) {
            tf.AddParagraph().AddBookmark("#" + style.Name);
        }
    }
}