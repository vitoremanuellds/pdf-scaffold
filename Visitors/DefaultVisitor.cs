
using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Images;
using pdf_scaffold.Layout;
using pdf_scaffold.Metrics;
using pdf_scaffold.Styling;
using pdf_scaffold.Tables;
using pdf_scaffold.Texts;

namespace pdf_scaffold.Visitors;

public class DefaultVisitor : IPdfScaffoldVisitor
{
    public required MigraDoc.DocumentObjectModel.Document Document { get; set; }
    private Dictionary<string, Styling.Style> Styles = [];

    public void ForDocument(Document document)
    {
        if (document.Author != null) { Document.Info.Author = document.Author; }
        if (document.Title != null) { Document.Info.Title = document.Title; }
        if (document.Subject != null) { Document.Info.Subject = document.Subject; }
        if (document.Keywords != null) { Document.Info.Keywords = string.Join(" ", document.Keywords); }

        if (document.Styles != null) {
            foreach(Styling.Style style in document.Styles) {
                if (style.Name != null) {
                    Styles.Add(style.Name, style);
                }
            }
        }

        if (document.Sections == null) {
            throw new Exception("There must be at least one section in the document!");
        }

        foreach(IPdfScaffoldElement section in document.Sections) {
            section.Accept(this);
        }
    }

    private Styling.Style? GetStyle(Styling.Style? style, string? name) {
        if (style == null && name != null) {
            var exists = Styles.TryGetValue(name, out style);
            if (!exists) {
                throw new Exception($"The style name '{name}' provided in the UseStyle field does not exist!");
            }
        }
        return style;
    }

    public void ForSection(Section section) {
        var sec = Document.AddSection();

        PageFormat pageFormat = section.PageFormat ?? PageFormat.A4();

        if (pageFormat.Height.IsPercentage || pageFormat.Width.IsPercentage) {
            throw new Exception("The percentage measure can not be used to define the size of the section!");
        }

        sec.PageSetup.PageWidth = Unit.FromPoint(pageFormat.Width.Value);
        sec.PageSetup.PageHeight = Unit.FromPoint(pageFormat.Height.Value);

        Margin margin = section.Margin
            ?? Margin.All(new Measure(inches: 1));

        var width = pageFormat!.Width.Value;                        
        var height = pageFormat!.Height.Value;

        var left = margin.Left ?? new Measure(inches: 1);
        var right = margin.Right ?? new Measure(inches: 1);
        var top = margin.Top ?? new Measure(inches: 1);
        var bottom = margin.Bottom ?? new Measure(inches: 1);

        // Setting to null, I don't know if this is going to work.
        sec.PageSetup.LeftMargin = MetricsUtil.GetUnitValue(left, width);
        sec.PageSetup.RightMargin = MetricsUtil.GetUnitValue(right, width);
        sec.PageSetup.TopMargin = MetricsUtil.GetUnitValue(top, height);
        sec.PageSetup.BottomMargin = MetricsUtil.GetUnitValue(bottom, height);

        var elements = section.Elements ?? [];
        
        Styling.Style? style = 
            GetStyle(section.Style, section.UseStyle);

        var x = width - left.Value - right.Value;
        var y = height - top.Value - bottom.Value;

        var dimensions = new Dimensions(x: x, y: y);

        foreach (IPdfScaffoldElement element in elements) {
            element.MergeStyles(style, dimensions);
            element.Accept(this);
        }
    }

    public void ForImage(Image image)
    {
        Styling.Style? style = GetStyle(image.Style, image.UseStyle);

        var mdSection = Document.LastSection;
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
        mdImage.RelativeHorizontal = 
            style?.PositionType == PositionType.Fixed ?
            MigraDoc.DocumentObjectModel.Shapes.RelativeHorizontal.Page :
            MigraDoc.DocumentObjectModel.Shapes.RelativeHorizontal.Column;
        mdImage.RelativeVertical = 
            style?.PositionType == PositionType.Fixed ?
            MigraDoc.DocumentObjectModel.Shapes.RelativeVertical.Page :
            MigraDoc.DocumentObjectModel.Shapes.RelativeVertical.Paragraph;

        mdImage.Left = MetricsUtil.GetUnitValue(style?.LeftPosition, x); 
        mdImage.Top = MetricsUtil.GetUnitValue(style?.TopPosition, y);
        // DO RESOLUTION
        // DO BORDERS
        // DO PADDING MAYBE
    }

    public void ForColumn(Column column)
    {
        throw new NotImplementedException();
    }

    public void ForContainer(Container container)
    {
        throw new NotImplementedException();
    }

    public void ForRow(Row row)
    {
        throw new NotImplementedException();
    }

    public void ForTable(Table table)
    {
        throw new NotImplementedException();
    }

    public void ForTableCell(TableCell tableCell)
    {
        throw new NotImplementedException();
    }

    public void ForTableRow(TableRow tableRow)
    {
        throw new NotImplementedException();
    }

    public void ForBookmark(Bookmark bookmark)
    {
        throw new NotImplementedException();
    }

    public void ForHeading(Heading heading)
    {
        throw new NotImplementedException();
    }

    public void ForLink(Link link)
    {
        throw new NotImplementedException();
    }

    public void ForParagraph(Texts.Paragraph paragraph)
    {
        throw new NotImplementedException();
    }

    public void ForText(Texts.Text text)
    {
        throw new NotImplementedException();
    }
}