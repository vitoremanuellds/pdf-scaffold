using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;

namespace pdf_scaffold.Visitors.Default;

internal static class ForSection {

    public static void DoForSection(this Visitor visitor, Section section) {
        var sec = visitor.Document.AddSection();

        PageFormat pageFormat = section.PageFormat ?? PageFormat.A4();

        if (pageFormat.Height.IsPercentage || pageFormat.Width.IsPercentage) {
            throw new Exception("The percentage measure can not be used to define the size of the section!");
        }

        sec.PageSetup.PageWidth = Unit.FromPoint(pageFormat.Width.Value);
        sec.PageSetup.PageHeight = Unit.FromPoint(pageFormat.Height.Value);

        Margin margin = section.Margin
            ?? Margin.All(new Measure(inches: 1));

        var width = pageFormat!.Width.Value;                        
        var height = pageFormat!.Height.Value;

        var left = margin.Left ?? new Measure(inches: 1);
        var right = margin.Right ?? new Measure(inches: 1);
        var top = margin.Top ?? new Measure(inches: 1);
        var bottom = margin.Bottom ?? new Measure(inches: 1);

        // Setting to null, I don't know if this is going to work.
        sec.PageSetup.LeftMargin = MetricsUtil.GetUnitValue(left, width);
        sec.PageSetup.RightMargin = MetricsUtil.GetUnitValue(right, width);
        sec.PageSetup.TopMargin = MetricsUtil.GetUnitValue(top, height);
        sec.PageSetup.BottomMargin = MetricsUtil.GetUnitValue(bottom, height);

        var elements = section.Elements ?? [];
        
        Styling.Style? style = 
            visitor.GetStyle(section.Style, section.UseStyle);

        var x = width - left.Value - right.Value;
        var y = height - top.Value - bottom.Value;

        var dimensions = new Dimensions(x: x, y: y);

        foreach (IPdfScaffoldElement element in elements) {
            element.MergeStyles(style, dimensions);
            element.Accept(visitor);
        }
    }
}