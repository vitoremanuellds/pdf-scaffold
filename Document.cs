using pdf_scaffold.Styling;
using pdf_scaffold.Visitors;

namespace pdf_scaffold;

public class Document(
    string? title = null,
    string? author = null,
    string? subject = null,
    ICollection<string>? keywords = null,
    ICollection<Style>? styles = null,
    ICollection<Section>? sections = null
) : IPdfScaffoldElement {
    public string? Title { get; } = title;
    public string? Author { get; } = author;
    public string? Subject { get; } = subject;
    public ICollection<string>? Keywords { get; } = keywords;
    public ICollection<Section>? Sections { get; } = sections;
    private IDictionary<string, Style> _styles;
    public ICollection<Style>? Styles { get; } = styles;

    public MigraDoc.DocumentObjectModel.Document Build(MigraDoc.DocumentObjectModel.Document? document) {
        document ??= new MigraDoc.DocumentObjectModel.Document();

        var visitor = new DefaultVisitor {
            Document = document
        };

        this.Accept(visitor);
        return visitor.Document;
    }

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        visitor.ForDocument(this);
    }
}
