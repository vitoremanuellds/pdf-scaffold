using pdf_scaffold.Metrics;
using pdf_scaffold.Styling;

namespace pdf_scaffold.Visitors;

public interface IPdfScaffoldElement {

    void Accept(IPdfScaffoldVisitor visitor);
    internal void MergeStyles(Style? style, Dimensions dimensions);

}