using pdf_scaffold.Styling;

namespace pdf_scaffold;

public class Document(
    string? title = null,
    string? author = null,
    string? subject = null,
    ICollection<string>? keywords = null,
    ICollection<Style>? styles = null,
    ICollection<Section>? sections = null
) {
    public string? Title { get; } = title;
    public string? Author { get; } = author;
    public string? Subject { get; } = subject;
    public ICollection<string>? Keywords { get; } = keywords;
    public ICollection<Section>? Sections { get; } = sections;
    private IDictionary<string, Style> _styles;
    public ICollection<Style>? Styles { get; } = styles;

    public MigraDoc.DocumentObjectModel.Document Build() {
        var document = new MigraDoc.DocumentObjectModel.Document();
        if (Author != null) { document.Info.Author = Author; }
        if (Title != null) { document.Info.Title = Title; }
        if (Subject != null) { document.Info.Subject = Subject; }
        if (Keywords != null) { document.Info.Keywords = String.Join(" ", Keywords); }

        if (Styles != null) {
            foreach(Style style in Styles) {
                if (style.Name != null) {
                    _styles.Add(style.Name, style);
                }
            }
        }
        
        if (Sections == null) {
            throw new Exception("There must be at least one section in the document!");
        }

        foreach(Section section in Sections) {
            document.Add(section.Build(_styles));
        }

        return document;
    }
}
