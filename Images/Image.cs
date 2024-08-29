using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Images;

public class Image(
    string path,
    SStyle? style = null,
    string? useStyle = null,
    SCrop? cropImage = null
) : SSectionElement(style, useStyle), IPdfScaffoldElement {

    public string Path { get; } = path;
    public SCrop? CropImage { get; } = cropImage;
    

    void IPdfScaffoldElement.Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForImage(this);
    }
}