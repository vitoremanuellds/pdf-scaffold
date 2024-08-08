using MigraDoc.DocumentObjectModel;

namespace pdf_scaffold.Metrics;

public class Measure {

    public double? Value { get; }

    public Measure(
        double? points = null, 
        double? inches = null,
        double? picas = null,
        double? centimeters = null,
        double? millimeters = null,
        double? percentage = null
    ) {
        if (points != null) {
            Value = points;
        } else if (inches != null) {
            Value = inches;
        } else if (picas != null) {
            Value = picas;
        } else if (centimeters != null) {
            Value = centimeters;
        } else if (millimeters != null) {
            Value = millimeters;
        } else {
            Value = percentage;
        }
    }

}