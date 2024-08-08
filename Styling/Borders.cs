namespace pdf_scaffold.Styling;

public class Borders {

    public Border? Left { get; }
    public Border? Right { get; }
    public Border? Top { get; }
    public Border? Bottom { get; }

    private Borders(Border? left = null, Border? right = null, Border? top = null, Border? bottom = null) {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    public static Borders All(Border? border = null) {
        return new Borders(border, border, border, border);
    }

    public static Borders Symmatrical(Border? leftAndRight = null, Border? topAndBottom = null) {
        return new Borders(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static Borders Each(Border? left = null, Border? right = null, Border? top = null, Border? bottom = null) {
        return new Borders(left, right, top, bottom);
    }
}