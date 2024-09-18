namespace PDFScaffold.Metrics;

/// <summary>
/// Defines the size of the components.
/// </summary>
public class SMeasure {

    /// <summary>
    /// The value of the SMeasure in Points.
    /// </summary>
    public double Value { get; }
    /// <summary>
    /// If the value used to create the SMeasure was a percentage, this will be true.
    /// </summary>
    public bool IsPercentage { get; } = false;


    /// <summary>
    /// Creates an SMeasure with the given value. The precedence order will be: 
    /// Poinst, Inches, Picas, Centimeters, Millimeters and Percentage.
    /// </summary>
    /// <param name="points">The size in Points. By default, an inch has 72 points.</param>
    /// <param name="inches">The size in Inches</param>
    /// <param name="picas">The size in Picas</param>
    /// <param name="centimeters">The size in Centimeters</param>
    /// <param name="millimeters">The size in Millimeters</param>
    /// <param name="percentage">The size in percentage relative to the Parent, only for Widths and Heights, or to the component itself.</param>
    /// <exception cref="Exception">If no value is given, an exception is thrown.</exception>
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