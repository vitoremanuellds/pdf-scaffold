using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Images;

/// <summary>
/// Represents an Image in the Document.
/// </summary>
/// <param name="path">The path to the image file.</param>
/// <param name="style">The style of the image.</param>
/// <param name="useStyle">The reference to the style of the image inside the SDocument.</param>
/// <param name="name">The name of the image. It is used to reference it through a link inside the document.</param>
/// <param name="cropImage">Represents the size of the image to be cropped from it. The Width and Height of the image
/// remains the same, only the image will be cropped.</param>
public class SImage(
    string path,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    SCrop? cropImage = null
) : SSectionElement(style, useStyle, name), IPdfScaffoldElement {

    /// <summary>
    /// The path to the image file.
    /// </summary>
    public string Path { get; } = path;

    /// <summary>
    /// Represents the size of the image to be cropped from it. The Width and Height of the image
    /// remains the same, only the image will be cropped.
    /// </summary>
    public SCrop? CropImage { get; } = cropImage;

    public override void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForImage(this);
    }
}