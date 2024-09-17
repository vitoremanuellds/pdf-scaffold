using PDFScaffold.Metrics;
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
public class SContainer(
    SSectionElement content,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
    // bool? singlePage = false
) : SSectionElement(style, useStyle, name) {

    // public bool? SinglePage { get; } = singlePage;
    /// <summary>
    /// The SSectionElement contained inside the SContainer.
    /// </summary>
    public SSectionElement Content { get; } = content;


    public override void Accept(IPdfScaffoldVisitor visitor) {
        visitor.ForContainer(this);
    }
}