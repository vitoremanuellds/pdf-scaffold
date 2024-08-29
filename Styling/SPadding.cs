using PDFScaffold.Metrics;

namespace PDFScaffold.Styling;

public class SPadding
{
    public SMeasure? Left { get; }
    public SMeasure? Right { get; }
    public SMeasure? Top { get; }
    public SMeasure? Bottom { get; }


    private SPadding(
        SMeasure? left = null,
        SMeasure? right = null,
        SMeasure? top = null,
        SMeasure? bottom = null
    ) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    public static SPadding All(SMeasure? value = null) {
        return new SPadding(value, value, value, value);
    }

    public static SPadding Symmetrical(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) {
        return new SPadding(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static SPadding Each(
        SMeasure? left = null,
        SMeasure? right = null,
        SMeasure? top = null,
        SMeasure? bottom = null
    ) {
        return new SPadding(left, right, top, bottom);
    }
}