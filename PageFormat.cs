using MigraDoc.DocumentObjectModel;

namespace pdf_scaffold;

public class PageFormat {

    public Unit Width { get; set; }
    public Unit Height { get; set; }

    public static PageFormat A0() {
        return new PageFormat();
    }

    public static PageFormat A1() {
        return new PageFormat();
    }

    public static PageFormat A2() {
        return new PageFormat();
    }

    public static PageFormat A3() {
        return new PageFormat();
    }

    public static PageFormat A4() {
        return new PageFormat();
    }

    public static PageFormat A5() {
        return new PageFormat();
    }

    public static PageFormat A6() {
        return new PageFormat();
    }

    public static PageFormat B5() {
        return new PageFormat();
    }

    public static PageFormat Letter() {
        return new PageFormat();
    }

    public static PageFormat Legal() {
        return new PageFormat();
    }

    public static PageFormat Ledger() {
        return new PageFormat();
    }
}