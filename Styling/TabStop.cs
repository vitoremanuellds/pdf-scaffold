using pdf_scaffold.Metrics;

namespace pdf_scaffold.Styling;

public class TabStop(Measure? position = null, Alignment? alignment = null) {

    public Measure? Position { get; } = position;
    public Alignment? Alignment { get; } = alignment;

}