using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Images;

public class SImage(
    string path,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    SCrop? cropImage = null
) : SSectionElement(style, useStyle, name), IPdfScaffoldElement {

    public string Path { get; } = path;
    public SCrop? CropImage { get; } = cropImage;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForImage(this);
    }
}