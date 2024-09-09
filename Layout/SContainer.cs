using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;

namespace PDFScaffold.Layout;

/// <summary>
/// A container that wraps an SSectionElement and can be styled.
/// </summary>
/// <param name="content">The SSectionElement contained inside the SContainer.</param>
/// <param name="style">The SStyle used inside the SContainer.</param>
/// <param name="useStyle">The name of the Style to be used on the SContainer.</param>
/// <param name="singlePage">If true, the container will ocupay the rest of whole page. If false, the container will grow to fit the content. If true, then the width and height will be overwritten.</param>
public class SContainer(
    SSectionElement content,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null,
    bool? singlePage = false
) : SSectionElement(style, useStyle, name) {

    public bool? SinglePage { get; } = singlePage;
    public SSectionElement Content { get; } = content;

    public new void Accept(IPdfScaffoldVisitor visitor) {
        visitor.ForContainer(this);
    }
}