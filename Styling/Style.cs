using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;

namespace pdf_scaffold.Styling;

public class Style(
    string? name = null,
    Color? fontColor = null,
    bool? bold = null,
    bool? italic = null,
    Underline? underline = null,
    Measure? fontSize = null,
    bool? superscript = null,
    bool? subscript = null,
    Color? shading = null,
    Measure? width = null,
    Measure? height = null,
    Measure? topPosition = null,
    Measure? leftPosition = null,
    double? resolution = null,
    Alignment? verticalAlignment = null,
    Alignment? horizontalAlignment = null,
    Borders? borders = null,
    PositionType? positionType = null,
    Padding? padding = null,
    ICollection<TabStop>? tabstops = null
    )
{
    public string? Name { get; } = name;
    public Color? FontColor { get; } = fontColor;
    public bool? Bold { get; } = bold;
    public bool? Italic { get; } = italic;
    // To do
    public Underline? Underline { get; } = underline;
    public Measure? FontSize { get; } = fontSize;
    public bool? Superscript { get; } = superscript;
    public bool? Subscript { get; } = subscript;
    public Color? Shading { get; } = shading;
    public Measure? Width { get; } = width;
    public Measure? Height { get; } = height;
    public Measure? TopPosition { get; } = topPosition;
    public Measure? LeftPosition { get; } = leftPosition;
    public double? Resolution { get; } = resolution;
    public Alignment? VerticalAlignment { get; } = verticalAlignment;
    public Alignment? HorizontalAlignment { get; } = horizontalAlignment;
    public Borders? Borders { get; } = borders;
    public PositionType? PositionType { get; } = positionType;
    public Padding? Padding { get; } = padding;
    public ICollection<TabStop>? Tabstops { get; } = tabstops;

    public Style Merge(Style style) {
        return new Style(
            Name ?? style.Name,
            FontColor ?? style.FontColor,
            Bold ?? style.Bold,
            Italic ?? style.Italic,
            Underline ?? style.Underline,
            FontSize ?? style.FontSize,
            Superscript ?? style.Superscript,
            Subscript ?? style.Subscript,
            Shading ?? style.Shading,
            Width,
            Height,
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