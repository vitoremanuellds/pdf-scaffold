using pdf_scaffold.Styling;

namespace pdf_scaffold;

public class Document(
    string? title = null,
    string? author = null,
    string? subject = null,
    List<string>? keywords = null,
    List<Style>? styles = null,
    List<Section>? sections = null
) {
    public string? Title { get; } = title;
    public string? Author { get; } = author;
    public string? Subject { get; } = subject;
    public List<string>? Keywords { get; } = keywords;
    public List<Section>? Sections { get; } = sections;
    public List<Style>? Styles { get; } = styles;
}
