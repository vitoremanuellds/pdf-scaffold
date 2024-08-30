using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

public class SBorder(
    SMeasure? width = null,
    Color? color = null,
    bool visible = false,
    SMeasure? distanceFromContent = null,
    SBorderType? borderType = null
)
{

    public SMeasure? Width { get; } = width;
    public Color? Color { get; } = color;
    public bool Visible { get; } = visible;
    public SMeasure? DistanceFromContent { get; } = distanceFromContent;
    public SBorderType? BorderType { get; } = borderType;

}