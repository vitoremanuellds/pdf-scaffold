using pdf_scaffold.Visitors;

namespace pdf_scaffold;

public interface ISectionElement : IPdfScaffoldElement {
    void MergeStyles(Styling.Style? style);

};