namespace pdf_scaffold;

public interface ISectionElement {

    object Build(Styling.Style? style, IDictionary<string, Styling.Style> styles);

};