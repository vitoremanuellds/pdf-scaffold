using MigraDoc.DocumentObjectModel;

namespace pdf_scaffold.Metrics;

internal class MetricsUtil {

    public static Unit GetUnitValue(Measure? measure, double? baseValue) {
        if (measure == null) { return null; }
        if (measure.IsPercentage && baseValue != null) { 
            return Unit.FromPoint(measure.Value * (double) baseValue); 
        }
        return Unit.FromPoint(measure.Value);
    }

}