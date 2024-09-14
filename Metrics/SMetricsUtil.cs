using MigraDoc.DocumentObjectModel;

namespace PDFScaffold.Metrics;

internal class SMetricsUtil {

    public static Unit GetUnitValue(SMeasure measure, double? baseValue) {
        if (measure.IsPercentage && baseValue != null) { 
            return Unit.FromPoint(measure.Value * (double) baseValue); 
        }
        return Unit.FromPoint(measure.Value);
    }

}