using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;
using PDFScaffold.Visitors;
using PDFScaffold.Visitors.Default;

namespace PDFScaffold.Scaffold;

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

    public Document Build(Document? document) {
        document ??= new Document();

        var visitor = new Visitor {
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
