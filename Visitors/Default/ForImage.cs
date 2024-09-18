using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PDFScaffold.Images;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForImage
{

    internal static void DoForImage(this SVisitor visitor, SImage image)
    {
        SStyle style = visitor.GetOrCreateStyle(image.Style, image.FathersStyle!, image.UseStyle);
        SDimensions parentsDimensions = image.FathersStyle!.Dimensions!;
        var (tf, mdImage) = SVisitorUtils.GetMigradocObjectsForImage(visitor, image);
        style.Dimensions = parentsDimensions.Copy();

        SVisitorUtils.SetBookmark(tf, image.Name);
        SVisitorUtils.SetWidthAndHeight(mdImage, style, parentsDimensions);
        SVisitorUtils.SetBorder(mdImage.LineFormat, style);
        image.CropImage(mdImage, style);
        SetPosition(mdImage, style);

        mdImage.Resolution = style.Resolution ?? 72;

        // SMargin? margin = style.Margin;

        // if (margin != null)
        // {
        //     mdImage.WrapFormat.DistanceBottom = SMetricsUtil.GetUnitValue(margin.Bottom ?? new SMeasure(0), style.Dimensions!.Y);
        //     mdImage.WrapFormat.DistanceTop = SMetricsUtil.GetUnitValue(margin.Top ?? new SMeasure(0), style.Dimensions!.Y);
        //     mdImage.WrapFormat.DistanceLeft = SMetricsUtil.GetUnitValue(margin.Left ?? new SMeasure(0), style.Dimensions!.X);
        //     mdImage.WrapFormat.DistanceRight = SMetricsUtil.GetUnitValue(margin.Right ?? new SMeasure(0), style.Dimensions!.X);
        // }

        image.Dimensions = style.Dimensions;
    }

    internal static void CropImage(this SImage image, Image mdImage, SStyle style)
    {
        var crop = image.CropImage;
        if (crop != null)
        {
            mdImage.PictureFormat.CropTop = SMetricsUtil.GetUnitValue(crop.FromTop ?? new SMeasure(0), style.Dimensions!.Y);
            mdImage.PictureFormat.CropBottom = SMetricsUtil.GetUnitValue(crop.FromBottom ?? new SMeasure(0), style.Dimensions!.Y);

            mdImage.PictureFormat.CropLeft = SMetricsUtil.GetUnitValue(crop.FromLeft ?? new SMeasure(0), style.Dimensions!.X);
            mdImage.PictureFormat.CropRight = SMetricsUtil.GetUnitValue(crop.FromRight ?? new SMeasure(0), style.Dimensions!.X);
        }
    }

    internal static void SetPosition(Image mdImage, SStyle style)
    {
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
    }
}
