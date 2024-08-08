using pdf_scaffold.Metrics;

namespace pdf_scaffold.Styling;

public class Padding
{
    public Measure? Left { get; }
    public Measure? Right { get; }
    public Measure? Top { get; }
    public Measure? Bototm { get; }


    private Padding(
        Measure? left = null,
        Measure? right = null,
        Measure? top = null,
        Measure? bottom = null
    ) {
        Left = left;
        Right = right;
        Top = top;
        Bototm = bottom;
    }

    public static Padding All(Measure? value = null) {
        return new Padding(value, value, value, value);
    }

    public static Padding Symmetrical(Measure? leftAndRight = null, Measure? topAndBottom = null) {
        return new Padding(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static Padding Each(
        Measure? left = null,
        Measure? right = null,
        Measure? top = null,
        Measure? bottom = null
    ) {
        return new Padding(left, right, top, bottom);
    }
}