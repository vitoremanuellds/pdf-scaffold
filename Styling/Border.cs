using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;

namespace pdf_scaffold.Styling;

public class Border(
    Measure? width = null,
    Color? color = null,
    bool visible = false,
    Measure? distanceFromContent = null,
    BorderType? borderType = null
)
{

    public Measure? Width { get; } = width;
    public Color? Color { get; } = color;
    public bool Visible { get; } = visible;
    public Measure? DistanceFromContent { get; } = distanceFromContent;
    public BorderType? BorderType { get; } = borderType;

}