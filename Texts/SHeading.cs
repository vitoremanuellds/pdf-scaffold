using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;

namespace PDFScaffold.Texts;

public class SHeading(
    ICollection<STextElement> content,
    int level = 1,
    SStyle? style = null,
    string? useStyle = null,
    string? name = null
) : SParagraph(style, useStyle, name, content) {

    public int Level { get; } = level;

    public static Unit GetFontSize(int level) {
        return level switch
        {
            1 => Unit.FromPoint(25.5),
            2 => Unit.FromPoint(22.5),
            3 => Unit.FromPoint(18),
            4 => Unit.FromPoint(15),
            5 => Unit.FromPoint(13.5),
            6 => Unit.FromPoint(12),
            _ => Unit.FromPoint(25.5),
        };
    }
}