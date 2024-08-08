using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;

namespace pdf_scaffold;

public class Margin {
    public Measure? Left { get; }
    public Measure? Right { get; }
    public Measure? Top { get; }
    public Measure? Bottom { get; }

    private Margin(Measure? left = null, Measure? right = null, Measure? top = null, Measure? bottom = null) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    public static Margin All(Measure? value = null) {
        return new Margin(value, value, value, value);
    }

    public static Margin Symmetrical(Measure? leftAndRight = null, Measure? topAndBottom = null) {
        return new Margin(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static Margin TopAndBottom() {
        return new Margin();
    }
}