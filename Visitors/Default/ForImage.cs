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

        var x = image.FathersStyle!.Dimensions!.X;
        var y = image.FathersStyle!.Dimensions!.Y;

        var crop = image.CropImage;
        var height = style.Height ?? new SMeasure(points: y);
        var width = style.Width ?? new SMeasure(points: x);
        var h = height.Value;
        var w = width.Value;

        if (crop != null) {
            mdImage.PictureFormat.CropTop = SMetricsUtil.GetUnitValue(crop.FromTop ?? new SMeasure(0), h);
            mdImage.PictureFormat.CropBottom = SMetricsUtil.GetUnitValue(crop.FromBottom ?? new SMeasure(0), h);
            
            mdImage.PictureFormat.CropLeft = SMetricsUtil.GetUnitValue(crop.FromLeft ?? new SMeasure(0), w);
            mdImage.PictureFormat.CropRight = SMetricsUtil.GetUnitValue(crop.FromRight ?? new SMeasure(0), w);
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
            mdImage.Left = SMetricsUtil.GetUnitValue(style.LeftPosition, x);
        }

        if (style.TopPosition != null)
        {
            mdImage.Top = SMetricsUtil.GetUnitValue(style.TopPosition, y);
        }
        mdImage.Resolution = style.Resolution ?? 72;

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
            var borderWidth = SMetricsUtil.GetUnitValue(border.Width ?? new SMeasure(1), w);
            mdImage.LineFormat.Width = borderWidth;
            width = new SMeasure(width.Value - (2 * borderWidth.Point));
            height = new SMeasure(height.Value - (2 * borderWidth.Point));
        }
        
        SMargin? margin = style.Margin;

        if (margin != null) {
            mdImage.WrapFormat.DistanceBottom = SMetricsUtil.GetUnitValue(margin.Bottom ?? new SMeasure(0), h);
            mdImage.WrapFormat.DistanceTop = SMetricsUtil.GetUnitValue(margin.Top ?? new SMeasure(0), h);
            mdImage.WrapFormat.DistanceLeft = SMetricsUtil.GetUnitValue(margin.Left ?? new SMeasure(0), w);
            mdImage.WrapFormat.DistanceRight = SMetricsUtil.GetUnitValue(margin.Right ?? new SMeasure(0), w);
        }

        if (style.Width != null)
        {
            mdImage.Width = SMetricsUtil.GetUnitValue(style.Width, x);
        }
        if (style.Height != null)
        {
            mdImage.Height = SMetricsUtil.GetUnitValue(style.Height, y);
        }

        style.Dimensions = new SDimensions(height.Value, width.Value);
    }
}