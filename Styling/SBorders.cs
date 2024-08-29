namespace PDFScaffold.Styling;

public class SBorders {

    public SBorder? Left { get; }
    public SBorder? Right { get; }
    public SBorder? Top { get; }
    public SBorder? Bottom { get; }

    private SBorders(SBorder? left = null, SBorder? right = null, SBorder? top = null, SBorder? bottom = null) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    public static SBorders All(SBorder? border = null) {
        return new SBorders(border, border, border, border);
    }

    public static SBorders Symmatrical(SBorder? leftAndRight = null, SBorder? topAndBottom = null) {
        return new SBorders(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static SBorders Each(SBorder? left = null, SBorder? right = null, SBorder? top = null, SBorder? bottom = null) {
        return new SBorders(left, right, top, bottom);
    }
}