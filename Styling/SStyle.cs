using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;

namespace PDFScaffold.Styling;

/// <summary>
/// Represents the style of a component inside the SDocument. The style values set in a parent is passed down to its children, with the exception of Width, Height, TopPosition, LeftPosition, Borders, PositionType, Padding, Margin and Centered.
/// </summary>
/// <param name="name">The name of the style used to be referenced by the components in the UseStyle field when the SStyle is inside the Styles field at the SDocument.</param>
/// <param name="fontColor">The color of the font used in the component. It is applied to SParagraph, SText and SLink.</param>
/// <param name="bold">If true, the font will be bold inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="italic">If true, the font will be italic inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="underline">An SUnderline used to define the underline of the text inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="fontSize">The size of the font inside the component. It is applied to SParagraph, SText and Slink.</param>
/// <param name="superscript">If true, the text inside the component will be superscript. Only works if <c>subscript</c> is false. It is applied to SParagraph, SText and Slink.</param>
/// <param name="subscript">If true, the text inside the component will be subscript. Only works if <c>superscript</c> is false. It is applied to SParagraph, SText and Slink.</param>
/// <param name="lineSpacing">The size of the space between the lines of a paragraph. Only works on SParagraph.</param>
/// <param name="spaceBefore">The size of the space before the paragraph. Only works on SParagraph.</param>
/// <param name="spaceAfter">The size of the space after the paragraph. Only works on SParagraph.</param>
/// <param name="shading">The color of the background inside the component. It is applied to SContainer, SColumn, SRow, STable, STableRow, STableCell and SParagraph.</param>
/// <param name="width">
///     <para>The width of the whole component. If not set, the component will use all the size remaining or the size that it needs.</para>
///     <para>It is applied to all components, except SSection, STableRow and STableCell.</para>
///     <para>On SParagraph and SHeading, the Width is only applied if the Height is set as well. On SImage, if the Width is set and the Height is not, then the aspect ratio of the image will be locked. This value is only applied on the current component.</para>
/// </param>
/// <param name="height">
///     <para>The height of the whole component. If not set, the component will use the size it needs.</para> 
///     <para>It is applied to all components, except SSection, and STableCell. </para>
///     <para>On SParagraph and SHeading, the Height is only applied if the Width is set as well. On SImage, if the Width is set and the Height is not, then the aspect ratio of the image will be locked. This value is only applied on the current component.</para>
/// </param>
/// <param name="topPosition">The position of the component relative to the top of the document. It is applied to SImage.</param>
/// <param name="leftPosition">The position of the component relative to the left of the document. It is applied to SImage.</param>
/// <param name="positionType">The type of the position of the component. It is applied to SImage.</param>
/// <param name="resolution">The resolution of the component. It is applied to SImage.</param>
/// <param name="verticalAlignment">The vertical aligment of the content inside the component. It determines the alignment relative to the height of the document. It is applied to STableRow and STableCell.</param>
/// <param name="horizontalAlignment">The alignment of the component(s). It is applied to SParagraph.</param>
/// <param name="borders">The style of the component's borders. It is applied to SContainer, SImage, SParagraph, SColumn, SRow, STable, STableRow and STableCell. In SContainer and SImage, only the Left Border is used to configure the whole border of the component.</param>
/// <param name="padding">The style of the component's padding. It is applied to.</param>
/// <param name="centered">If true, the content inside the component will be centered and the <c>margin</c> will be disconsidered.It is applied to SContainer and only works correctly if both the SContainer and its content have their Widths and Heights set. This value is only applied to the current component.</param>
public class SStyle(
    string? name = null,
    Color? fontColor = null,
    bool? bold = null,
    bool? italic = null,
    SUnderline? underline = null,
    SMeasure? fontSize = null,
    bool? superscript = null,
    bool? subscript = null,
    SMeasure? lineSpacing = null,
    SMeasure? spaceBefore = null,
    SMeasure? spaceAfter = null,
    Color? shading = null,
    SMeasure? width = null,
    SMeasure? height = null,
    SPositionType? positionType = null,
    SMeasure? topPosition = null,
    SMeasure? leftPosition = null,
    double? resolution = null,
    SAlignment? verticalAlignment = null,
    SAlignment? horizontalAlignment = null,
    SBorders? borders = null,
    SPadding? padding = null,
    // ICollection<STabStop>? tabstops = null,
    //SMargin? margin = null,
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
    /// The size of the space between the lines of a paragraph. Only works on SParagraphs.
    /// </summary>
    public SMeasure? LineSpacing { get; } = lineSpacing;
    /// <summary>
    /// The size of the space before the paragraph. Only works on SParagraphs.
    /// </summary>
    public SMeasure? SpaceBefore { get; } = spaceBefore;
    /// <summary>
    /// The size of the space after the paragraph. Only works on SParagraphs.
    /// </summary>
    public SMeasure? SpaceAfter { get; } = spaceAfter;

    /// <summary>
    /// The color of the background inside the component. It is applied to SContainer, SColumn, SRow, STable, STableRow, STableCell and SParagraph.
    /// </summary>
    public Color? Shading { get; } = shading;
    /// <summary>
    /// The width of the whole component. If not set, the component will use all the size remaining or the size that it needs. It is applied to all components, except SSection, STableRow and STableCell. This value is only applied on the current component.
    /// </summary>
    public SMeasure? Width { get; } = width;
    /// <summary>
    /// The height of the whole component. If not set, the component will use the size it needs. It is applied to all components, except SSection, and STableCell. This value is only applied on the current component.
    /// </summary>
    public SMeasure? Height { get; } = height;
    /// <summary>
    /// The position of the component relative to the top of the document. It is applied to SImage.
    /// </summary>
    public SMeasure? TopPosition { get; } = topPosition;
    /// <summary>
    /// The position of the component relative to the left of the document. It is applied to SImage.
    /// </summary>
    public SMeasure? LeftPosition { get; } = leftPosition;
    /// <summary>
    /// The resolution of the component. It is applied to SImage.
    /// </summary>
    public double? Resolution { get; } = resolution;
    /// <summary>
    /// The vertical aligment of the content inside the component. It determines the alignment relative to the height of the document. It is applied to STableRow and STableCell.
    /// </summary>
    public SAlignment? VerticalAlignment { get; } = verticalAlignment;
    /// <summary>
    /// The alignment of the component(s). It is applied to SParagraph.
    /// </summary>
    public SAlignment? HorizontalAlignment { get; } = horizontalAlignment;
    /// <summary>
    /// The style of the component's borders. It is applied to SContainer, SImage, SParagraph, SColumn, SRow, STable, STableRow and STableCell. In SContainer and SImage, only the Left Border is used to configure the whole border of the component.
    /// </summary>
    public SBorders? Borders { get; } = borders;
    /// <summary>
    /// The type of the position of the component. It is applied to SImage.
    /// </summary>
    public SPositionType? PositionType { get; } = positionType;
    /// <summary>
    /// The style of the component's padding. It is applied to.
    /// </summary>
    public SPadding? Padding { get; } = padding;
    // public ICollection<STabStop>? Tabstops { get; } = tabstops;

    ///// <summary>
    ///// The style of the component's margin. It is applied to SContainer. This value is only applied to the current component.
    ///// </summary>
    //public SMargin? Margin { get; } = margin;

    /// <summary>
    /// If true, the content inside the component will be centered and the <c>margin</c> will be disconsidered.It is applied to SContainer and only works correctly if both the SContainer and its content have their Widths and Heights set. This value is only applied to the current component.
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
            LineSpacing ?? style?.LineSpacing,
            SpaceBefore ?? style?.SpaceBefore,
            SpaceAfter ?? style?.SpaceAfter,
            Shading ?? style?.Shading,
            Width,
            Height,
            PositionType,
            TopPosition,
            LeftPosition,
            Resolution ?? style?.Resolution,
            VerticalAlignment ?? style?.VerticalAlignment,
            HorizontalAlignment ?? style?.HorizontalAlignment,
            Borders,
            Padding,
            // Tabstops ?? style?.Tabstops,
            //Margin,
            Centered
        );
    }
}