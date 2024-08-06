using MigraDoc.DocumentObjectModel;

namespace pdf_scaffold;

public class Margin {
    public Unit Left { get; set; }
    public Unit Right { get; set; }
    public Unit Top { get; set; }
    public Unit Bottom { get; set; }

    public static Margin All() {
        return new Margin();
    }

    public static Margin LeftAndRight() {
        return new Margin();
    }

    public static Margin TopAndBottom() {
        return new Margin();
    }
}