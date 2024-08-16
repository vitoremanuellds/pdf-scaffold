using pdf_scaffold.Styling;
using pdf_scaffold.Visitors;

namespace pdf_scaffold.Images;

public class Image(
    string? path = null,
    Style? style = null,
    string? useStyle = null,
    Crop? cropImage = null
) : ISectionElement {

    public Style? Style { get; set; } = style;
    public string? UseStyle { get; } = useStyle;
    public string? Path { get; } = path;
    public Crop? CropImage { get; } = cropImage;

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public void MergeStyles(Style? style)
    {
        if (Style != null && style != null) {
            Style = Style.Merge(style);
        }
    }
}