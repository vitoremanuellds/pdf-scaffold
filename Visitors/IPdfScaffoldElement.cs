namespace pdf_scaffold.Visitors;

public interface IPdfScaffoldElement {

    void Accept(IPdfScaffoldVisitor visitor);

}