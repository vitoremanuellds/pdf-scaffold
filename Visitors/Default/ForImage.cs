using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForImage {

    public static void DoForImage(this Visitor visitor, Images.Image image) {
        Styling.Style? style = visitor.GetStyle(image.Style, image.UseStyle);

        var mdSection = visitor.Document.LastSection;
        var mdImage = mdSection.AddImage(image.Path);

        var crop = image.CropImage;
        var height = style?.Height?.Value;
        var width = style?.Width?.Value;

        if (crop != null && height != null && width != null) {
            mdImage.PictureFormat.CropTop = MetricsUtil.GetUnitValue(crop.FromTop, height);
            mdImage.PictureFormat.CropBottom = MetricsUtil.GetUnitValue(crop.FromBottom, height);
            mdImage.PictureFormat.CropLeft = MetricsUtil.GetUnitValue(crop.FromLeft, width);
            mdImage.PictureFormat.CropRight = MetricsUtil.GetUnitValue(crop.FromRight, width);
        }

        var x = style?.FathersDimensions?.X;
        var y = style?.FathersDimensions?.Y;

        mdImage.Width = MetricsUtil.GetUnitValue(style?.Width, x);
        mdImage.Height = MetricsUtil.GetUnitValue(style?.Height, y);
        mdImage.WrapFormat.Style = 
            style?.PositionType == PositionType.Fixed ?
            WrapStyle.None :
            WrapStyle.TopBottom;
        mdImage.RelativeHorizontal = 
            style?.PositionType == PositionType.Fixed ?
            RelativeHorizontal.Page :
            RelativeHorizontal.Column;
        mdImage.RelativeVertical = 
            style?.PositionType == PositionType.Fixed ?
            RelativeVertical.Page :
            RelativeVertical.Paragraph;

        mdImage.Left = MetricsUtil.GetUnitValue(style?.LeftPosition, x); 
        mdImage.Top = MetricsUtil.GetUnitValue(style?.TopPosition, y);        
        mdImage.Resolution = style?.Resolution ?? 72;

        var dashStyles = new Dictionary<Styling.BorderType, DashStyle>{
            { Styling.BorderType.Dash, DashStyle.Dash },
            { Styling.BorderType.DashDot, DashStyle.DashDot },
            { Styling.BorderType.DashDotDot, DashStyle.DashDotDot },
            { Styling.BorderType.Solid, DashStyle.Solid },
            { Styling.BorderType.SquareDot, DashStyle.SquareDot },
        };

        Styling.Border? border = style?.Borders?.Left;

        if (border != null) {
            mdImage.LineFormat.Color = border?.Color ?? Colors.Black;
            Styling.BorderType borderType = 
                border?.BorderType ?? Styling.BorderType.Solid;

            dashStyles.TryGetValue(borderType, out DashStyle dashStyle);
            mdImage.LineFormat.DashStyle = dashStyle;
            mdImage.LineFormat.Visible = border?.Visible ?? true;
            mdImage.LineFormat.Width = MetricsUtil.GetUnitValue(border?.Width, width);
        }
        
        Padding? padding = style?.Padding;

        if (padding != null) {
            mdImage.WrapFormat.DistanceBottom = MetricsUtil.GetUnitValue(padding.Bottom, height);
            mdImage.WrapFormat.DistanceTop = MetricsUtil.GetUnitValue(padding.Top, height);
            mdImage.WrapFormat.DistanceLeft = MetricsUtil.GetUnitValue(padding.Left, width);
            mdImage.WrapFormat.DistanceRight = MetricsUtil.GetUnitValue(padding.Right, width);
        }
    }
}