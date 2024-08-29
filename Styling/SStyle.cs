using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

public class SStyle(
    string? name = null,
    Color? fontColor = null,
    bool? bold = null,
    bool? italic = null,
    SUnderline? underline = null,
    SMeasure? fontSize = null,
    bool? superscript = null,
    bool? subscript = null,
    Color? shading = null,
    SMeasure? width = null,
    SMeasure? height = null,
    SMeasure? topPosition = null,
    SMeasure? leftPosition = null,
    double? resolution = null,
    SAlignment? verticalAlignment = null,
    SAlignment? horizontalAlignment = null,
    SBorders? borders = null,
    SPositionType? positionType = null,
    SPadding? padding = null,
    ICollection<STabStop>? tabstops = null
    )
{
    internal SDimensions? FathersDimensions { get; set; }
    public string? Name { get; } = name;
    public Color? FontColor { get; } = fontColor;
    public bool? Bold { get; } = bold;
    public bool? Italic { get; } = italic;
    // To do
    public SUnderline? Underline { get; } = underline;
    public SMeasure? FontSize { get; } = fontSize;
    public bool? Superscript { get; } = superscript;
    public bool? Subscript { get; } = subscript;
    public Color? Shading { get; } = shading;
    public SMeasure? Width { get; } = width;
    public SMeasure? Height { get; } = height;
    public SMeasure? TopPosition { get; } = topPosition;
    public SMeasure? LeftPosition { get; } = leftPosition;
    public double? Resolution { get; } = resolution;
    public SAlignment? VerticalAlignment { get; } = verticalAlignment;
    public SAlignment? HorizontalAlignment { get; } = horizontalAlignment;
    public SBorders? Borders { get; } = borders;
    public SPositionType? PositionType { get; } = positionType;
    public SPadding? Padding { get; } = padding;
    public ICollection<STabStop>? Tabstops { get; } = tabstops;

    public SStyle Merge(SStyle style) {
        return new SStyle(
            Name ?? style.Name,
            FontColor ?? style.FontColor,
            Bold ?? style.Bold,
            Italic ?? style.Italic,
            Underline ?? style.Underline,
            FontSize ?? style.FontSize,
            Superscript ?? style.Superscript,
            Subscript ?? style.Subscript,
            Shading ?? style.Shading,
            Width ?? style.Width,
            Height ?? style.Height,
            TopPosition ?? style.TopPosition,
            LeftPosition ?? style.LeftPosition,
            Resolution ?? style.Resolution,
            VerticalAlignment ?? style.VerticalAlignment,
            HorizontalAlignment ?? style.HorizontalAlignment,
            Borders ?? style.Borders,
            PositionType ?? style.PositionType,
            Padding ?? style.Padding,
            Tabstops ?? style.Tabstops
        );
    }
}