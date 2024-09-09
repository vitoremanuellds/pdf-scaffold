using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
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
        } else if (father is Table table) {
            mdImage = table.Rows[image.TablePos.Item2].Cells[image.TablePos.Item1].AddImage(image.Path);
        } else {
            throw new Exception("An SImage can not be used outside a SSection or an SColumn/SRow");
        }

        var x = image.FathersStyle?.Dimensions?.X;
        var y = image.FathersStyle?.Dimensions?.Y;

        var crop = image.CropImage;
        var height = style.Height ?? new SMeasure(points: y);
        var width = style.Width ?? new SMeasure(points: x);
        var h = height.Value;
        var w = width.Value;

        if (crop != null) {
            mdImage.PictureFormat.CropTop = SMetricsUtil.GetUnitValue(crop.FromTop, h);
            mdImage.PictureFormat.CropBottom = SMetricsUtil.GetUnitValue(crop.FromBottom, h);
            mdImage.PictureFormat.CropLeft = SMetricsUtil.GetUnitValue(crop.FromLeft, w);
            mdImage.PictureFormat.CropRight = SMetricsUtil.GetUnitValue(crop.FromRight, w);
        }

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
            mdImage.LineFormat.Width = SMetricsUtil.GetUnitValue(border?.Width, w);
        }
        
        SMargin? margin = style?.Margin;

        if (margin != null) {
            mdImage.WrapFormat.DistanceBottom = SMetricsUtil.GetUnitValue(margin.Bottom, h);
            mdImage.WrapFormat.DistanceTop = SMetricsUtil.GetUnitValue(margin.Top, h);
            mdImage.WrapFormat.DistanceLeft = SMetricsUtil.GetUnitValue(margin.Left, w);
            mdImage.WrapFormat.DistanceRight = SMetricsUtil.GetUnitValue(margin.Right, w);
        }
    }
}