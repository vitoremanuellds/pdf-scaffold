using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PDFScaffold.Layout;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

public static class ForContainer {

    public static void DoForContainer(this SContainer container, SVisitor visitor) {
        SStyle? style = visitor.GetStyle(container.Style, container.UseStyle);
        style?.Merge(container.FathersStyle);

        TextFrame textFrame;
        var father = visitor.VisitedObjects.Peek();

        if (father != null && father is Section section) {
            textFrame = section.AddTextFrame();
        } else {
            throw new Exception("A Container can not be used inside other elements than a SSection");
        }

        

    }
}