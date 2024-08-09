namespace pdf_scaffold.Texts;

public class Bookmark (
    string name
) : ISectionElement, TextElement {
    public string Name { get; } = name;
}