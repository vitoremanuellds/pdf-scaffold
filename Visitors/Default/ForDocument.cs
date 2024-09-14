using PDFScaffold.Scaffold;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForDocument {

    public static void DoForDocument(this SVisitor visitor, SDocument document) {
        if (document.Author != null) { visitor.Document.Info.Author = document.Author; }
        if (document.Title != null) { visitor.Document.Info.Title = document.Title; }
        if (document.Subject != null) { visitor.Document.Info.Subject = document.Subject; }
        if (document.Keywords != null) { 
            visitor.Document.Info.Keywords = string.Join(" ", document.Keywords); 
        }

        if (document.Styles != null) {
            foreach(SStyle style in document.Styles) {
                if (style.Name != null) {
                    visitor.Styles.Add(style.Name, style);
                }
            }
        }

        if (document.Sections == null || document.Sections.Count == 0) {
            throw new Exception("There must be at least one section in the document!");
        }

        foreach(SSection section in document.Sections) {
            section.Accept(visitor);
        }
    }

}