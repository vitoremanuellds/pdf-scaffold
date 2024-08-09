using pdf_scaffold.Metrics;

namespace pdf_scaffold.Images;

public class Crop {
    public Measure? FromLeft { get; }
    public Measure? FromRight { get; }
    public Measure? FromTop { get; }
    public Measure? FromBottom { get; }

    private Crop(
        Measure? fromLeft = null, 
        Measure? fromRight = null, 
        Measure? fromTop = null, 
        Measure? fromBottom = null
    ) {
        FromLeft = fromLeft;
        FromRight = fromRight;
        FromTop = fromTop;
        FromBottom = fromBottom;
    }

    public static Crop All(Measure? value = null) {
        return new Crop(value, value, value, value);
    }

    public static Crop Symmetrical(Measure? leftAndRight = null, Measure? topAndBottom = null) {
        return new Crop(leftAndRight, leftAndRight, topAndBottom, topAndBottom);
    }

    public static Crop Each(
        Measure? fromLeft = null, 
        Measure? fromRight = null, 
        Measure? fromTop = null, 
        Measure? fromBottom = null
    ) {
        return new Crop(fromLeft, fromRight, fromTop, fromBottom);
    }
}