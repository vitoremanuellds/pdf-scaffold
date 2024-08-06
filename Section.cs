namespace pdf_scaffold;

public class Section {

    public PageFormat? PageFormat { get; set; }
    public Margin? Margin { get; set; }
    public required List<SectionElement> Elements { get; set; }

}