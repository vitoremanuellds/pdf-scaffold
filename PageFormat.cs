using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;

namespace pdf_scaffold;

public class PageFormat {

    public Measure Width { get; }
    public Measure Height { get; }

    private PageFormat(Measure width, Measure height) {
        Width = width;
        Height = height;
    }

    public static PageFormat Custom(Measure width, Measure height) {
        return new PageFormat(width, height);
    }

    public static PageFormat A0()
    {
        return new PageFormat(new Measure(centimeters: 84.1), new Measure(centimeters: 118.9));
    }

    public static PageFormat A1() {
        return new PageFormat(new Measure(centimeters: 59.4), new Measure(centimeters: 84.1));
    }

    public static PageFormat A2() {
        return new PageFormat(new Measure(centimeters: 42), new Measure(centimeters: 59.4));
    }

    public static PageFormat A3() {
        return new PageFormat(new Measure(centimeters: 29.7), new Measure(centimeters: 42));
    }

    public static PageFormat A4() {
        return new PageFormat(new Measure(centimeters: 21), new Measure(centimeters: 29.7));
    }

    public static PageFormat A5() {
        return new PageFormat(new Measure(centimeters: 14.8), new Measure(centimeters: 21));
    }

    public static PageFormat A6() {
        return new PageFormat(new Measure(centimeters: 10.5), new Measure(centimeters: 14.8));
    }

    public static PageFormat B5() {
        return new PageFormat(new Measure(centimeters: 18.2), new Measure(centimeters: 25.7));
    }

    public static PageFormat Letter() {
        return new PageFormat(new Measure(centimeters: 21.6), new Measure(centimeters: 27.9));
    }

    public static PageFormat Legal() {
        return new PageFormat(new Measure(centimeters: 21.6), new Measure(centimeters: 35.6));
    }

    public static PageFormat Ledger() {
        return new PageFormat(new Measure(centimeters: 27.9), new Measure(centimeters: 43.2));
    }
}