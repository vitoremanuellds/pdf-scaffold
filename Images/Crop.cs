using PDFScaffold.Metrics;

namespace PDFScaffold.Images;

public class SCrop {
    public SMeasure? FromLeft { get; }
    public SMeasure? FromRight { get; }
    public SMeasure? FromTop { get; }
    public SMeasure? FromBottom { get; }

    private SCrop(
        SMeasure? fromLeft = null, 
        SMeasure? fromRight = null, 
        SMeasure? fromTop = null, 
        SMeasure? fromBottom = null
    ) {
        FromLeft = fromLeft;
        FromRight = fromRight;
        FromTop = fromTop;
        FromBottom = fromBottom;
    }

    public static SCrop All(SMeasure? value = null) {
        return new SCrop(value, value, value, value);
    }

    public static SCrop Symmetrical(SMeasure? leftAndRight = null, SMeasure? topAndBottom = null) {
        return new SCrop(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static SCrop Each(
        SMeasure? fromLeft = null, 
        SMeasure? fromRight = null, 
        SMeasure? fromTop = null, 
        SMeasure? fromBottom = null
    ) {
        return new SCrop(fromLeft, fromRight, fromTop, fromBottom);
    }
}