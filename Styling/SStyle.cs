using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;

namespace PDFScaffold.Styling;

/// <summary>
/// Represents the style of a component inside the SDocument.
/// </summary>
/// <param name="name">The name of the style used to be referenced inside the SDocument.</param>
/// <param name="fontColor">The color of the font used in the component. It is applied to SParagraph, SText and SLink.</param>
/// <param name="bold">If true, the font will be bold inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="italic">If true, the font will be italic inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="underline">An SUnderline used to define the underline of the text inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="fontSize">The size of the font inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="superscript">If true, the text inside the component will be superscript. Only works if <paramref name="subscript"> is false. It is applied to SParagraph, SText and Slink.</param>
/// <param name="subscript">If true, the text inside the component will be subscript. Only works if <paramref name="superscript"> is false. It is applied to SParagraph, SText and Slink.</param>
/// <param name="shading">The color of the background inside the component. It is applied to SContainer and SParagraph.</param>
/// <param name="width">The width of the whole component. If not set, the component will use all the size remaining. It is applied to SImage, SContainer. This value is only applied on the current component.</param>
/// <param name="height">The height of the whole component. If not set, the component will use all the size remaining. It is applied to SImage, SContainer. This value is only applied on the current component.</param>
/// <param name="topPosition"></param>
/// <param name="leftPosition"></param>
/// <param name="resolution"></param>
/// <param name="verticalAlignment"></param>
/// <param name="horizontalAlignment">The alignment of the component(s). It is applied to SParagraph.</param>
/// <param name="borders">The style of the component's borders. It is applied to SContainer, SImage and SParagraph. In SContainer and SImage, only the Left Border is used to configure the whole border of the component.</param>
/// <param name="positionType"></param>
/// <param name="padding">The style of the component's padding. It is applied to.</param>
/// <param name="tabstops"></param>
/// <param name="margin">The style of the component's margin. It is applied to SImage, SContainer. This value is only applied to the current component.</param>
/// <param name="centered">If true, the content inside the component will be centered and the <paramref name="margin"> will be disconsidered.It is applied to SContainer. This value is only applied to the current component.</param>
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
    // ICollection<STabStop>? tabstops = null,
    SMargin? margin = null,
    bool? centered = null
) {
    internal SDimensions? Dimensions { get; set; }
    /// <summary>
    /// The name of the style used to be referenced inside the SDocument.
    /// </summary>
    public string? Name { get; } = name;
    /// <summary>
    /// The color of the font used in the component. It is applied to SParagraph, SText and SLink.
    /// </summary>
    public Color? FontColor { get; } = fontColor;
    /// <summary>
    /// If true, the font will be bold inside the component. It is applied to SParagraph, SText and Slink.
    /// </summary>
    public bool? Bold { get; } = bold;
    /// <summary>
    /// If true, the font will be italic inside the component. It is applied to SParagraph, SText and Slink.
    /// </summary>
    public bool? Italic { get; } = italic;
    /// <summary>
    /// An SUnderline used to define the underline of the text inside the component. It is applied to SParagraph, SText and Slink.
    /// </summary>
    public SUnderline? Underline { get; } = underline;
    /// <summary>
    /// The size of the font inside the component. It is applied to SParagraph, SText and Slink.
    /// </summary>
    public SMeasure? FontSize { get; } = fontSize;
    /// <summary>
    /// If true, the text inside the component will be superscript. Only works if <paramref name="subscript"> is false. It is applied to SParagraph, SText and Slink.
    /// </summary>
    public bool? Superscript { get; } = superscript;
    /// <summary>
    /// If true, the text inside the component will be subscript. Only works if <paramref name="superscript"> is false. It is applied to SParagraph, SText and Slink.
    /// </summary>
    public bool? Subscript { get; } = subscript;
    /// <summary>
    /// /// <param name="shading">The color of the background inside the component. It is applied to SContainer and SParagraph.</param>
    /// </summary>
    public Color? Shading { get; } = shading;
    /// <summary>
    /// The width of the whole component. If not set, the component will use all the size remaining. It is applied to SImage, SContainer. This value is only applied on the current component.
    /// </summary>
    public SMeasure? Width { get; } = width;
    /// <summary>
    /// The height of the whole component. If not set, the component will use all the size remaining. It is applied to SImage, SContainer. This value is only applied on the current component.
    /// </summary>
    public SMeasure? Height { get; } = height;
    /// <summary>
    ///
    /// </summary>
    public SMeasure? TopPosition { get; } = topPosition;
    /// <summary>
    ///
    /// </summary>
    public SMeasure? LeftPosition { get; } = leftPosition;
    /// <summary>
    ///
    /// </summary>
    public double? Resolution { get; } = resolution;
    /// <summary>
    ///
    /// </summary>
    public SAlignment? VerticalAlignment { get; } = verticalAlignment;
    /// <summary>
    /// The alignment of the component(s). It is applied to SParagraph.
    /// </summary>
    public SAlignment? HorizontalAlignment { get; } = horizontalAlignment;
    /// <summary>
    /// The style of the component's borders. It is applied to SContainer, SImage and SParagraph. In SContainer and SImage, only the Left Border is used to configure the whole border of the component.
    /// </summary>
    public SBorders? Borders { get; } = borders;
    /// <summary>
    /// 
    /// </summary>
    public SPositionType? PositionType { get; } = positionType;
    /// <summary>
    /// The style of the component's padding. It is applied to.
    /// </summary>
    public SPadding? Padding { get; } = padding;
    // public ICollection<STabStop>? Tabstops { get; } = tabstops;

    /// <summary>
    /// The style of the component's margin. It is applied to SImage, SContainer. This value is only applied to the current component.
    /// </summary>
    public SMargin? Margin { get; } = margin;

    /// <summary>
    /// If true, the content inside the component will be centered and the <paramref name="margin"> will be disconsidered.It is applied to SContainer. This value is only applied to the current component.
    /// </summary>
    public bool? Centered { get; } = centered;

    public SStyle Merge(SStyle? style) {
        return new SStyle(
            Name ?? style?.Name,
            FontColor ?? style?.FontColor,
            Bold ?? style?.Bold,
            Italic ?? style?.Italic,
            Underline ?? style?.Underline,
            FontSize ?? style?.FontSize,
            Superscript ?? style?.Superscript,
            Subscript ?? style?.Subscript,
            Shading ?? style?.Shading,
            Width,
            Height,
            TopPosition ?? style?.TopPosition,
            LeftPosition ?? style?.LeftPosition,
            Resolution ?? style?.Resolution,
            VerticalAlignment ?? style?.VerticalAlignment,
            HorizontalAlignment ?? style?.HorizontalAlignment,
            Borders ?? style?.Borders,
            PositionType ?? style?.PositionType,
            Padding,
            // Tabstops ?? style?.Tabstops,
            Margin,
            Centered
        );
    }
}