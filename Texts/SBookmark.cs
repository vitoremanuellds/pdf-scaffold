using PDFScaffold.Scaffold;

namespace PDFScaffold.Texts;

public class SBookmark (
    string name
) : STextElement(null, null) {
    public string Name { get; } = name;
}