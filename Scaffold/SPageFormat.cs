using PDFScaffold.Metrics;

namespace PDFScaffold.Scaffold;

/// <summary>
/// Defines the Format of the page inside the section. 
/// There are predefined values for it you can use: A0..A6, B5, Letter, Ledger and Legal. For example, you can use <c>SPageFormat.A4()</c> 
/// </summary>
public class SPageFormat {
    /// <summary>
    /// Defines the width of the section page.
    /// </summary>
    public SMeasure Width { get; }

    /// <summary>
    /// Defines the height of the section page.
    /// </summary>
    public SMeasure Height { get; }

    private SPageFormat(SMeasure width, SMeasure height) {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Defines a custom SPageFormat.
    /// </summary>
    /// <param name="width">The custom width of the section's page.</param>
    /// <param name="height">The custom height of the section's page.</param>
    /// <returns></returns>
    public static SPageFormat Custom(SMeasure width, SMeasure height) {
        return new SPageFormat(width, height);
    }

    public static SPageFormat A0()
    {
        return new SPageFormat(new SMeasure(centimeters: 84.1), new SMeasure(centimeters: 118.9));
    }

    public static SPageFormat A1() {
        return new SPageFormat(new SMeasure(centimeters: 59.4), new SMeasure(centimeters: 84.1));
    }

    public static SPageFormat A2() {
        return new SPageFormat(new SMeasure(centimeters: 42), new SMeasure(centimeters: 59.4));
    }

    public static SPageFormat A3() {
        return new SPageFormat(new SMeasure(centimeters: 29.7), new SMeasure(centimeters: 42));
    }

    public static SPageFormat A4() {
        return new SPageFormat(new SMeasure(centimeters: 21), new SMeasure(centimeters: 29.7));
    }

    public static SPageFormat A5() {
        return new SPageFormat(new SMeasure(centimeters: 14.8), new SMeasure(centimeters: 21));
    }

    public static SPageFormat A6() {
        return new SPageFormat(new SMeasure(centimeters: 10.5), new SMeasure(centimeters: 14.8));
    }

    public static SPageFormat B5() {
        return new SPageFormat(new SMeasure(centimeters: 18.2), new SMeasure(centimeters: 25.7));
    }

    public static SPageFormat Letter() {
        return new SPageFormat(new SMeasure(centimeters: 21.6), new SMeasure(centimeters: 27.9));
    }

    public static SPageFormat Legal() {
        return new SPageFormat(new SMeasure(centimeters: 21.6), new SMeasure(centimeters: 35.6));
    }

    public static SPageFormat Ledger() {
        return new SPageFormat(new SMeasure(centimeters: 27.9), new SMeasure(centimeters: 43.2));
    }
}