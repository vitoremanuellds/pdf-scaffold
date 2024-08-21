using pdf_scaffold.Metrics;
using pdf_scaffold.Styling;
using pdf_scaffold.Visitors;

namespace pdf_scaffold.Images;

public class Image(
    string path,
    Style? style = null,
    string? useStyle = null,
    Crop? cropImage = null
) : ISectionElement, IPdfScaffoldElement {

    public Style? Style { get; set; } = style;
    public string? UseStyle { get; } = useStyle;
    public string Path { get; } = path;
    public Crop? CropImage { get; } = cropImage;

    void IPdfScaffoldElement.Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForImage(this);
    }

    void IPdfScaffoldElement.MergeStyles(Style? style, Dimensions dimensions)
    {
        if (Style != null && style != null) {
            Style = Style.Merge(style);
            Style.FathersDimensions = dimensions;
        }
    }
}