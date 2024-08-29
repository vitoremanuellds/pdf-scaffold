using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

public class STabStop(SMeasure? position = null, SAlignment? alignment = null) {

    public SMeasure? Position { get; } = position;
    public SAlignment? Alignment { get; } = alignment;

}