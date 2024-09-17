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

        Paragraph p;
        Image mdImage;

        object father = visitor.VisitedObjects.Peek();

        if (father != null && father is Section section) {
            p = section.AddParagraph();
            mdImage = p.AddImage(image.Path);
        } else if (father is Cell cell) {
            p = cell.AddParagraph();
            mdImage = p.AddImage(image.Path);
        } else if (father is TextFrame textFrame) {
            p = textFrame.AddParagraph();
            mdImage = p.AddImage(image.Path);
        } else {
            throw new Exception("An SImage can not be used outside an SSection, SColumn, SRow, SContainer and STableCell");
        }

        if (image.Name != null) {
            p.AddBookmark("#" + image.Name, false);
        }

        SetWidthAndHeight(mdImage, style, image.FathersStyle!.Dimensions!);

        SBorder? border = style.Borders?.Left;

        if (border != null) {
            mdImage.LineFormat.Color = border.Color ?? Colors.Black;
            SBorderType borderType = 
                border.BorderType ?? SBorderType.Solid;

            mdImage.LineFormat.DashStyle = borderType switch {
                SBorderType.Dash => DashStyle.Dash,
                SBorderType.DashDot => DashStyle.DashDot,
                SBorderType.DashDotDot => DashStyle.DashDotDot,
                SBorderType.Solid => DashStyle.Solid,
                SBorderType.SquareDot => DashStyle.SquareDot,
                _ => DashStyle.Solid
            };
            mdImage.LineFormat.Visible = border.Visible ?? true;
            var borderWidth = SMetricsUtil.GetUnitValue(border.Width ?? new SMeasure(1), style.Dimensions!.X);
            mdImage.LineFormat.Width = borderWidth;
            style.Dimensions!.X -= 2 * borderWidth.Point;
            style.Dimensions!.Y -= 2 * borderWidth.Point;
        }

        var crop = image.CropImage;
        if (crop != null) {
            mdImage.PictureFormat.CropTop = SMetricsUtil.GetUnitValue(crop.FromTop ?? new SMeasure(0), style.Dimensions!.Y);
            mdImage.PictureFormat.CropBottom = SMetricsUtil.GetUnitValue(crop.FromBottom ?? new SMeasure(0), style.Dimensions!.Y);
            
            mdImage.PictureFormat.CropLeft = SMetricsUtil.GetUnitValue(crop.FromLeft ?? new SMeasure(0), style.Dimensions!.X);
            mdImage.PictureFormat.CropRight = SMetricsUtil.GetUnitValue(crop.FromRight ?? new SMeasure(0), style.Dimensions!.X);
        }

        mdImage.WrapFormat.Style = 
            style.PositionType == SPositionType.Fixed ?
            WrapStyle.None :
            WrapStyle.TopBottom;
        mdImage.RelativeHorizontal = 
            style.PositionType == SPositionType.Fixed ?
            RelativeHorizontal.Page :
            RelativeHorizontal.Column;
        mdImage.RelativeVertical = 
            style.PositionType == SPositionType.Fixed ?
            RelativeVertical.Page :
            RelativeVertical.Paragraph;

        if (style.LeftPosition != null)
        {
            mdImage.Left = SMetricsUtil.GetUnitValue(style.LeftPosition, style.Dimensions!.X);
        }

        if (style.TopPosition != null)
        {
            mdImage.Top = SMetricsUtil.GetUnitValue(style.TopPosition, style.Dimensions!.Y);
        }

        mdImage.Resolution = style.Resolution ?? 72;
        
        SMargin? margin = style.Margin;

        if (margin != null) {
            mdImage.WrapFormat.DistanceBottom = SMetricsUtil.GetUnitValue(margin.Bottom ?? new SMeasure(0), style.Dimensions!.Y);
            mdImage.WrapFormat.DistanceTop = SMetricsUtil.GetUnitValue(margin.Top ?? new SMeasure(0), style.Dimensions!.Y);
            mdImage.WrapFormat.DistanceLeft = SMetricsUtil.GetUnitValue(margin.Left ?? new SMeasure(0), style.Dimensions!.X);
            mdImage.WrapFormat.DistanceRight = SMetricsUtil.GetUnitValue(margin.Right ?? new SMeasure(0), style.Dimensions!.X);
        }
    }

    internal static void SetWidthAndHeight(Image image, SStyle style, SDimensions dimensions) {
        if (style.Width != null) {
            image.Width = SMetricsUtil.GetUnitValue(style.Width, dimensions.X);
        } else {
            image.Width = Unit.FromPoint(dimensions.X);
        }

        if (style.Height != null) {
            image.Height = SMetricsUtil.GetUnitValue(style.Height, dimensions.Y);
        } else {
            image.Height = Unit.FromPoint(dimensions.Y);
        }

        style.Dimensions = new SDimensions(image.Height.Point, image.Width.Point);
    }
}