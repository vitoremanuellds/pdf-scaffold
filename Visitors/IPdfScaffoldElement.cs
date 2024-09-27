using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors;

public interface IPdfScaffoldElement {

    void Accept(IPdfScaffoldVisitor visitor);
}