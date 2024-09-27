using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

/// <summary>
/// The representation of a border of a component.
/// </summary>
/// <param name="width">The width of the border.</param>
/// <param name="color">The color of the border.</param>
/// <param name="visible">If true, the border will be shown.</param>
/// <param name="distanceFromContent">The distance from the border to the content.</param>
/// <param name="borderType">The type of the border.</param>
public class SBorder(
    SMeasure? width = null,
    Color? color = null,
    bool? visible = true,
    SMeasure? distanceFromContent = null,
    SBorderType? borderType = SBorderType.Single
)
{

    /// <summary>
    /// The width of the border.
    /// </summary>
    public SMeasure? Width { get; } = width;

    /// <summary>
    /// The color of the border.
    /// </summary>
    public Color? Color { get; } = color;

    /// <summary>
    /// If true, the border will be shown.
    /// </summary>
    public bool? Visible { get; } = visible;

    /// <summary>
    /// The distance from the border to the content.
    /// </summary>
    public SMeasure? DistanceFromContent { get; } = distanceFromContent;

    /// <summary>
    /// The type of the border.
    /// </summary>
    public SBorderType? BorderType { get; } = borderType;
}