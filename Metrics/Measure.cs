namespace PDFScaffold.Metrics;

public class SMeasure {

    public double Value { get; }
    public bool IsPercentage { get; } = false;

    public SMeasure(
        double? points = null, 
        double? inches = null,
        double? picas = null,
        double? centimeters = null,
        double? millimeters = null,
        double? percentage = null
    ) {
        if (points != null) {
            Value = (double) points;
        } else if (inches != null) {
            Value = 72 * (double) inches;
        } else if (picas != null) {
            Value = 12 * (double) picas;
        } else if (centimeters != null) {
            Value = 72 / 2.54 * (double) centimeters;
        } else if (millimeters != null) {
            Value = 72 / 25.4 * (double) millimeters;
        } else if (percentage != null) {
            if (percentage <= 0 || percentage > 1) {
                throw new Exception(
                    "The value for a percentage measure must be greater than 0 and lesser than or equals to 1!"
                );
            }
            IsPercentage = true;
            Value = (double) percentage;
        } else {
            throw new Exception("No value for the measure was provided!");
        }
    }

}