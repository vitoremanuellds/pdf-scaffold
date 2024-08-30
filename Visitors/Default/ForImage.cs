using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PDFScaffold.Images;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForImage {

    public static void DoForImage(this SVisitor visitor, SImage image) {
        SStyle style = visitor.GetStyle(image.Style, image.UseStyle) ?? new SStyle();
        style.Merge(image.FathersStyle);

        Image mdImage;

        object father = visitor.VisitedObjects.Peek();

        if (father != null && father is Section section) {
            mdImage = section.AddImage(image.Path);
        } else {
            throw new Exception("An SImage can not be used outside a Section");
        }

        var crop = image.CropImage;
        var height = style.Height?.Value;
        var width = style.Width?.Value;

        if (crop != null && height != null && width != null) {
            mdImage.PictureFormat.CropTop = SMetricsUtil.GetUnitValue(crop.FromTop, height);
            mdImage.PictureFormat.CropBottom = SMetricsUtil.GetUnitValue(crop.FromBottom, height);
            mdImage.PictureFormat.CropLeft = SMetricsUtil.GetUnitValue(crop.FromLeft, width);
            mdImage.PictureFormat.CropRight = SMetricsUtil.GetUnitValue(crop.FromRight, width);
        }

        var x = image.FathersStyle?.Dimensions?.X;
        var y = image.FathersStyle?.Dimensions?.Y;

        mdImage.Width = SMetricsUtil.GetUnitValue(style?.Width, x);
        mdImage.Height = SMetricsUtil.GetUnitValue(style?.Height, y);
        mdImage.WrapFormat.Style = 
            style?.PositionType == SPositionType.Fixed ?
            WrapStyle.None :
            WrapStyle.TopBottom;
        mdImage.RelativeHorizontal = 
            style?.PositionType == SPositionType.Fixed ?
            RelativeHorizontal.Page :
            RelativeHorizontal.Column;
        mdImage.RelativeVertical = 
            style?.PositionType == SPositionType.Fixed ?
            RelativeVertical.Page :
            RelativeVertical.Paragraph;

        mdImage.Left = SMetricsUtil.GetUnitValue(style?.LeftPosition, x); 
        mdImage.Top = SMetricsUtil.GetUnitValue(style?.TopPosition, y);        
        mdImage.Resolution = style?.Resolution ?? 72;

        var dashStyles = new Dictionary<SBorderType, DashStyle>{
            { SBorderType.Dash, DashStyle.Dash },
            { SBorderType.DashDot, DashStyle.DashDot },
            { SBorderType.DashDotDot, DashStyle.DashDotDot },
            { SBorderType.Solid, DashStyle.Solid },
            { SBorderType.SquareDot, DashStyle.SquareDot },
        };

        SBorder? border = style?.Borders?.Left;

        if (border != null) {
            mdImage.LineFormat.Color = border?.Color ?? Colors.Black;
            SBorderType borderType = 
                border?.BorderType ?? SBorderType.Solid;

            dashStyles.TryGetValue(borderType, out DashStyle dashStyle);
            mdImage.LineFormat.DashStyle = dashStyle;
            mdImage.LineFormat.Visible = border?.Visible ?? true;
            mdImage.LineFormat.Width = SMetricsUtil.GetUnitValue(border?.Width, width);
        }
        
        SPadding? padding = style?.Padding;

        if (padding != null) {
            mdImage.WrapFormat.DistanceBottom = SMetricsUtil.GetUnitValue(padding.Bottom, height);
            mdImage.WrapFormat.DistanceTop = SMetricsUtil.GetUnitValue(padding.Top, height);
            mdImage.WrapFormat.DistanceLeft = SMetricsUtil.GetUnitValue(padding.Left, width);
            mdImage.WrapFormat.DistanceRight = SMetricsUtil.GetUnitValue(padding.Right, width);
        }
    }
}