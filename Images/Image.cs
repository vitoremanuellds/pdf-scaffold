using pdf_scaffold.Styling;

namespace pdf_scaffold.Images;

public class Image(
    string? path = null,
    Style? style = null,
    string? useStyle = null,
    Crop? cropImage = null
) : ISectionElement {

    public Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public string? Path { get; } = path;
    public Crop? CropImage { get; } = cropImage;

}