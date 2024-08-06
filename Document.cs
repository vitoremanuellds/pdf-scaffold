namespace pdf_scaffold;

public class Document
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Subject { get; set; }
    public List<string>? Keywords { get; set; }
    public required List<Section> Sections { get; set; }
    public List<Style>? Styles { get; set; }
}
