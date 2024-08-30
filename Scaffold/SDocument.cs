using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;
using PDFScaffold.Visitors.Default;

namespace PDFScaffold.Scaffold;

/// <summary>
/// The <c>SDocument</c> class represents the PDF Document being built.
/// </summary>
/// <param name="title">The Title of the Document. It can be null.</param>
/// <param name="author">The Author of the Document. It can be null.</param>
/// <param name="subject">The Subject of the Docuemnt. It can be null.</param>
/// <param name="keywords">The Keywords related to this Document. It can be null.</param>
/// <param name="styles">
///     The list of styles that can be referenced inside
///     the document. It can be null as well.
/// </param>
/// <param name="sections">
///     The list of the sections inside the Document. It can not be null and there must
///     be at least one SSection inside of the list.
/// </param>
public class SDocument(
    string? title = null,
    string? author = null,
    string? subject = null,
    ICollection<string>? keywords = null,
    ICollection<SStyle>? styles = null,
    ICollection<SSection>? sections = null
) : IPdfScaffoldElement {
    public string? Title { get; } = title;
    public string? Author { get; } = author;
    public string? Subject { get; } = subject;
    public ICollection<string>? Keywords { get; } = keywords;
    public ICollection<SSection>? Sections { get; } = sections;
    private IDictionary<string, SStyle> _styles = new Dictionary<string, SStyle>();
    public ICollection<SStyle>? Styles { get; } = styles;

    /// <summary>
    /// The <c>Build</> method build the components inside the SDocument and add them to
    /// a Migradoc Document Object.
    /// </summary>
    /// <param name="document">A Migradoc Document object in which the new components will
    /// be created and inserted. If it is null, a new Migradoc Document object will be created.</param>
    /// <returns>Migradoc Document object populated with
    /// the corresponding components of the objects inside the SDocument.</returns>
    public Document Build(Document? document) {
        document ??= new Document();

        var visitor = new SVisitor {
            Document = document
        };

        Accept(visitor);
        return visitor.Document;
    }

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForDocument(this);
    }
}
