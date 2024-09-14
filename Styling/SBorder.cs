using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

public class SBorder(
    SMeasure? width = null,
    Color? color = null,
    bool? visible = true,
    SMeasure? distanceFromContent = null,
    SBorderType? borderType = SBorderType.Single
)
{

    public SMeasure? Width { get; } = width;
    public Color? Color { get; } = color;
    public bool? Visible { get; } = visible;
    public SMeasure? DistanceFromContent { get; } = distanceFromContent;
    public SBorderType? BorderType { get; } = borderType;
}