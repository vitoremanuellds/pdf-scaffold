using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;

namespace PDFScaffold.Scaffold;

public class SMargin {
    public SMeasure? Left { get; }
    public SMeasure? Right { get; }
    public SMeasure? Top { get; }
    public SMeasure? Bottom { get; }

    private SMargin(SMeasure? left = null, SMeasure? right = null, SMeasure? top = null, SMeasure? bottom = null) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    public static SMargin All(SMeasure? value = null) {
        return new SMargin(value, value, value, value);
    }

    public static SMargin Symmetrical(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) {
        return new SMargin(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static SMargin TopAndBottom() {
        return new SMargin();
    }
}